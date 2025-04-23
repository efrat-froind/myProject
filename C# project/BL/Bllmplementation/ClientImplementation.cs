
using BlApi;

namespace Bllmplementation;


internal class ClientImplementation : IClient
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public int Create(BO.Client item)
    {      
        try
        {
            DO.Client c=BO.Tools.ConvertBOClientToDOclient(item);
            return _dal.Client.Create(c);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }      
    }

    public void Delete(int id)
    {
        try
        {
            _dal.Client.Delete(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool IsExist(int id)
    {
        try
        {
            _dal.Client.Read(id);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public BO.Client? Read(int id)
    {
        try
        {
             return  BO.Tools.ConvertDOClientToBOclient(_dal.Client.Read(id));    
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public List<BO.Client?> ReadAll(Func<BO.Client, bool>? filter = null)
    {
        try
        {
            return _dal.Client.ReadAll(client=>filter(BO.Tools.ConvertDOClientToBOclient(client)))
                   .Select(c=> BO.Tools.ConvertDOClientToBOclient(c)).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(BO.Client item)
    {
        try
        {
            DO.Client c = BO.Tools.ConvertBOClientToDOclient(item);
            _dal.Client.Update(c);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
