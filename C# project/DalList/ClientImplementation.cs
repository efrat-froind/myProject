
namespace Dal;
using DO;
using DalApi;
using Tools;
using System.Reflection;

internal class ClientImplementation : IClient
{
    private readonly Client i;

    public int Create(Client item) {
        
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        var isExists = DataSource.SClient.FirstOrDefault(i => i._id == item._id);
         if(isExists!= null)
         {
              throw new dalTheCodeAlreadyExists("The client already exists");
         }
         DataSource.SClient.Add(item);
         LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
         return item._id;
        
    }
    public Client? Read(int id) {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Client? c = DataSource.SClient.FirstOrDefault(i => i._id == id);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return c?? throw new dalTheEntityDoesNotExist("The client not exist");

    }
    public void Update(Client item) {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Delete(item._id);
        DataSource.SClient.Add(item);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }
    public void Delete(int id) {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Client c = Read(id);
        DataSource.SClient.Remove(c);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }

    public List<Client?> ReadAll(Func<Client, bool>? filter)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        if (filter != null)
        {
            List<Client?> clients = DataSource.SClient.Where(i => filter(i) == true).ToList();
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
            return clients;
        }
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return new List<Client?>(DataSource.SClient);
    }

    public Client? Read(Func<Client, bool> filter)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Client? c = DataSource.SClient.FirstOrDefault(i => filter(i) == true);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return c ?? throw new dalTheEntityDoesNotExist("The client not exist");
    }
}
