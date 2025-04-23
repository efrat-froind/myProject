
using BlApi;

namespace Bllmplementation;

internal class ProductImplementation : IProduct
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Product item)
    {
        try
        {
            
            DO.Product p = BO.Tools.ConvertBOProductToDOProduct(item);
            return _dal.Product.Create(p);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Product.Delete(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Product? Read(int id)
    {
        try
        {
            return BO.Tools.ConvertDOProductToBOProduct(_dal.Product.Read(id));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<BO.Product?> ReadAll(Func<BO.Product, bool>? filter = null)
    {
        try
        {
            return _dal.Product.ReadAll(product => filter(BO.Tools.ConvertDOProductToBOProduct(product)))
                   .Select(p => BO.Tools.ConvertDOProductToBOProduct(p)).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<BO.SaleInProduct> Sales(int code, bool isForEveryone)
    {
        return _dal.Sale.ReadAll(s => s._code == code &&
                              isForEveryone == true || s._isForEveryone == true &&
                              DateTime.Now >= s._dateStart && s._dateFinifh <= DateTime.Now)
                              .Select(s=> new BO.SaleInProduct()
                              {
                                     Id=s._id,
                                     QuantityForSale=s._amountForSale,
                                     Price=s._priceForSale ??0,
                                     IsForEveryone = s._isForEveryone ?? false
                              }).ToList();
   
              



    }

    public void Update(BO.Product item)
    {
        try
        {
            DO.Product p = BO.Tools.ConvertBOProductToDOProduct(item);
            _dal.Product.Update(p);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
