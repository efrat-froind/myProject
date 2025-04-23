using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using BlApi;
using static System.Net.Mime.MediaTypeNames;


namespace Bllmplementation;
 
internal class OrderImplementation : IOrder
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// מוסיף מוצר להזמנה המצוינת.
    /// </summary>
    /// <param name="order">ההזמנה אליה יתווסף המוצר.</param>
    /// <param name="code">המזהה של המוצר להוספה.</param>
    /// <param name="quantity">הכמות של המוצר להוספה. יכולה להיות שלילית.</param>
    /// <returns>רשימה של מבצעים שהוחלו על המוצר בהזמנה.</returns>
    public List<BO.SaleInProduct> AddProductToOrder(BO.Order order, int code, int quantity)
    {
        BO.Product product = (BO.Product)(from p in _dal.Product.ReadAll()
                             where p._code == code
                             select new BO.Product(
                                 p._code,
                                 p._name,
                                 (BO.Category?)p._category,
                                 p._price,
                                 p._QuantityInStock,
                                 p._image
                             ));
        BO.ProductInOrder productInOrder = (BO.ProductInOrder)(from p in order.ListProductInOrder
                                                   where p.Id == product.Code
                                                   select p);
        int newQuantity = quantity;
        if (productInOrder != null)
        {
            newQuantity += productInOrder.Quantity;
        }
        if (product.QuantityInStock < newQuantity)
        {
            throw new BO.BlNotEnoughInStock("Out of stock");
        }
        else if (productInOrder != null)
        {
            productInOrder.Quantity = newQuantity;
        }
        else
        { 
            productInOrder = new BO.ProductInOrder(product.Code, product.Name, product.Price, quantity, null, quantity * product.Price);
        }
            SearchSaleForProduct(productInOrder, true);
            CalcTotalPriceForProduct(productInOrder);
            CalcTotalPrice(order);
        
        return productInOrder.ListSaleInProduct;
    }

    /// <summary>
    /// מחשב את המחיר הסופי עבור ההזמנה המצוינת.
    /// </summary>
    /// <param name="order">ההזמנה עבור לה ייחשב המחיר הסופי.</param>
    public void CalcTotalPrice(BO.Order order)
    {
        order.TotalPrice = (from p in order.ListProductInOrder
                            select p.totalPrice).Sum();
    }

    /// <summary>
    /// מחשב את המחיר לתשלום עבור מוצר ספציפי בהזמנה, כולל מבצעים רלוונטיים.
    /// </summary>
    /// <param name="product">המוצר בהזמנה עבורו ייחשב המחיר.</param>
    public void CalcTotalPriceForProduct(BO.ProductInOrder product)
    {
        DO.Product p = _dal.Product.Read(product.Id);
        SearchSaleForProduct(product,true);
        if(product.ListSaleInProduct==null)
        {      
            product.totalPrice = p._price * product.Quantity;
        }
        else
        {
            int count = product.Quantity;
            List<BO.SaleInProduct> sales = new List<BO.SaleInProduct>();
            foreach (BO.SaleInProduct sale in product.ListSaleInProduct)
            {
                if(count>=sale.QuantityForSale)
                {
                    int amount = count / sale.QuantityForSale;
                    product.totalPrice += amount * sale.Price;
                    count -= sale.QuantityForSale * amount;
                    sales.Add(sale);
                }
                if (count == 0)
                    break;
            }
            product.ListSaleInProduct = sales;
            product.totalPrice += count * p._price;       
        }
    }

    /// <summary>
    /// מעבד את ההזמנה על ידי עדכון הכמויות במלאי בבסיס הנתונים.
    /// </summary>
    /// <param name="order">ההזמנה לעיבוד.</param>
    public void DoOrder(BO.Order order)
    {

        foreach (BO.ProductInOrder product in order.ListProductInOrder)
        {
            DO.Product p = _dal.Product.Read(product.Id);
            BO.Product p2 = BO.Tools.ConvertDOProductToBOProduct(p);
            p2.QuantityInStock -=  product.Quantity;
        }
    }


    /// <summary>
    /// מחפש מבצעים רלוונטיים עבור המוצר המצויין בהזמנה.
    /// </summary>
    /// <param name="p">המוצר בהזמנה לחיפוש מבצעים.</param>
    /// <param name="isForEveryone">מציין האם ההזמנה היא עבור לקוח קיים.</param>
    public void SearchSaleForProduct(BO.ProductInOrder p, bool isForEveryone)
    {
        p.ListSaleInProduct = _dal.Sale.ReadAll(s => s._code == p.Id
                              && DateTime.Now >= s._dateStart && s._dateFinifh <= DateTime.Now
                              && p.Quantity>=s._amountForSale
                              && isForEveryone==true||s._isForEveryone==true)
                              .Select(s=>new BO.SaleInProduct()
                              {
                                  Id = s._id,
                                  QuantityForSale = s._amountForSale,
                                  Price = s._priceForSale ?? 0,
                                  IsForEveryone = s._isForEveryone ?? false

                              }).ToList();

        p.ListSaleInProduct.OrderBy(s => s.Price / s.QuantityForSale);

    }
}
