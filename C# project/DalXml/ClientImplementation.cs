
using System.Reflection;
using DalApi;
using Tools;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class ClientImplementation : IClient
{
    XElement xmlClient = XElement.Load(XMLDATAPATH);
    private static string XMLDATAPATH = @"..\xml\clients.xml";
    private const string XMLCLIENT = "clients.xml";
    private const string ARRAYOFCLIENT = "ArrayOfClient";
    private const string CLIENT = "Client";
    private const string ID = "id";
    private const string USERNAME = "userName";
    private const string ADDRESS = "address";
    private const string PHONE = "phone";
    public int Create(Client item)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        List<Client>? client = new List<Client>();
        if (xmlClient.Descendants(ID).Any(c => int.Parse(c.Value) == item._id))
            throw new dalTheCodeAlreadyExists("The client already exists");
        XElement newClient = new XElement(CLIENT,
                    new XElement(ID, item._id),
                    new XElement(USERNAME, item._userName),
                    new XElement(ADDRESS, item._address),
                    new XElement(PHONE, item._phone)
                    );
        XElement isExist = xmlClient.Element(ARRAYOFCLIENT);
        if (isExist != null)
        {
            xmlClient.Add(newClient);
        }
        xmlClient.Save(XMLDATAPATH);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return item._id;
    }

    public void Delete(int id)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Client c = Read(id);
        xmlClient.Element(CLIENT).Element(ID).Elements()
            .Single(c => int.Parse(c.Element(ID).Value) == id)
            .Remove();
        xmlClient.Save(XMLCLIENT);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }

    public Client? Read(int id)
    {

        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        List<Client> client = xmlClient.Element(ID).Elements()
            .Single(c => int.Parse(c.Value) == id)
            .Element(CLIENT).Elements()
            .Select(c1 => new Client()
            {
                _id = int.Parse(c1.Element(ID).Value),
                _userName = c1.Element(USERNAME).Value,
                _address = c1.Element(ADDRESS).Value,
                _phone = int.Parse(c1.Element(PHONE).Value)
            }).ToList();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return client.Single() ?? throw new dalTheEntityDoesNotExist("The client not exist");
    }

    public Client? Read(Func<Client, bool> filter)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        List<Client> clients = xmlClient.Element(CLIENT).Elements()
            .Where(c => filter(new Client()
            {
                _id = int.Parse(c.Element(ID).Value),
                _userName = c.Element(USERNAME).Value,
                _address = c.Element(ADDRESS).Value,
                _phone = int.Parse(c.Element(PHONE).Value)
            }) == true)
            .Select(c => new Client()
            {
                _id = int.Parse(c.Element(ID).Value),
                _userName = c.Element(USERNAME).Value,
                _address = c.Element(ADDRESS).Value,
                _phone = int.Parse(c.Element(PHONE).Value)
            })
            .ToList();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        Client? c = clients.FirstOrDefault();
        return c ?? throw new dalTheEntityDoesNotExist("The client not exist");
    }

    public List<Client?> ReadAll(Func<Client, bool>? filter)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        if (filter != null)
        {
            List<Client> clients = xmlClient.Element(CLIENT).Elements()
            .Where(c => filter(new Client()
            {
                _id = int.Parse(c.Element(ID).Value),
                _userName = c.Element(USERNAME).Value,
                _address = c.Element(ADDRESS).Value,
                _phone = int.Parse(c.Element(PHONE).Value)
            }) == true)
            .Select(c => new Client()
            {
                _id = int.Parse(c.Element(ID).Value),
                _userName = c.Element(USERNAME).Value,
                _address = c.Element(ADDRESS).Value,
                _phone = int.Parse(c.Element(PHONE).Value)
            })
            .ToList();
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
            return clients;
        }
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        //return new List<Client?>(
        //    xmlClient.Element(Client).Elements()
        //    .Select(c => new Client()
        //    {
        //        _id = int.Parse(c.Element(ID).Value),
        //        _userName = c.Element(USERNAME).Value,
        //        _address = c.Element(ADDRESS).Value,
        //        _phone = int.Parse(c.Element(PHONE).Value)
        //    })
        //    .ToList());
        return new List<Client?>(
    xmlClient.Element("Client")?.Elements()
    .Select(c => new Client()
    {
        _id = int.TryParse(c.Element("ID")?.Value, out var id) ? id : 0, // או ערך ברירת מחדל אחר
        _userName = c.Element("USERNAME")?.Value ?? "Unknown", // ערך ברירת מחדל
        _address = c.Element("ADDRESS")?.Value ?? "Unknown", // ערך ברירת מחדל
        _phone = int.TryParse(c.Element("PHONE")?.Value, out var phone) ? phone : 0 // או ערך ברירת מחדל אחר
    })
    .ToList());
    }

    public void Update(Client item)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Delete(item._id);
        Create(item);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }
}
