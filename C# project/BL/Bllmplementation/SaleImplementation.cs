

using BlApi;


namespace Bllmplementation;

internal class SaleImplementation : ISale
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Sale item)
    {
        try
        {
            DO.Sale s = BO.Tools.ConvertBOSaleToDOSale(item);
            return _dal.Sale.Create(s);

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
            _dal.Sale.Delete(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public BO.Sale? Read(int id)
    {
        try
        {
            return BO.Tools.ConvertDOSaleToBOSale(_dal.Sale.Read(id));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<BO.Sale?> ReadAll(Func<BO.Sale, bool>? filter = null)
    {
        try
        {
            return _dal.Sale.ReadAll(sale => filter(BO.Tools.ConvertDOSaleToBOSale(sale)))
                   .Select(s => BO.Tools.ConvertDOSaleToBOSale(s)).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(BO.Sale item)
    {
        try
        {
            DO.Sale s = BO.Tools.ConvertBOSaleToDOSale(item);
            _dal.Sale.Update(s);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
