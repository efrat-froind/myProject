using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class ProductInOrder
{
    public int Id { get; init; }
    public string NameProduct { get; set; }
    public double BasePrice { get; set; }
    public int Quantity { get; set; }
    public List<BO.SaleInProduct> ListSaleInProduct { get; set; }
    public double totalPrice { get; set; }

    public ProductInOrder(int Id, string NameProduct, double BasePrice, int Quantity, List<BO.SaleInProduct> ListSaleInProduct, double totalPrice) 
    {
        Id = Id;
        NameProduct = NameProduct;
        BasePrice = BasePrice;
        Quantity = Quantity;
        ListSaleInProduct = ListSaleInProduct;
        totalPrice = totalPrice;
    }
}
