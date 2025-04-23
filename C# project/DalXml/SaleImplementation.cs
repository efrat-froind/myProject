using System.Reflection;
using System.Xml.Serialization;
using DalApi;
using DO;
using Tools;

namespace Dal;

internal class SaleImplementation : ISale
{
    private static string XMLDATAPATH = @"..\xml\sales.xml";
    //private static string XMLDATAPATH2 = @"..\xml\product.xml";
    XmlSerializer serializer = new XmlSerializer(typeof(List<Sale>));
    XmlSerializer serializer2 = new XmlSerializer(typeof(List<Product>));
    private static string XMLDATAPATHP = @"..\xml\products.xml";
    public int Create(Sale item)
    {
        List<Product> products = new List<Product>();
        List<Sale> salaes = new List<Sale>();
        Sale s = null;
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            salaes = serializer.Deserialize(fileStream) as List<Sale?>;
        }
        using (FileStream fileStream = new FileStream(XMLDATAPATHP, FileMode.Open))
        {
            products = serializer2.Deserialize(fileStream) as List<Product?>;
        }
        Product p = products.FirstOrDefault(i => i._code == item._code);
        if (p != null)
        {
            s = item with { _id = Config.getSaleMinCode};
            salaes.Add(s);
        }
        else
            throw new dalTheEntityDoesNotExist("The product in the sale does not exist.");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Create))
        {
            serializer.Serialize(fileStream, salaes);
        };
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return s._code;
    }

    public void Delete(int id)
    {
        List<Sale> salaes = new List<Sale>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            salaes = serializer.Deserialize(fileStream) as List<Sale?>;
        }
        Sale s = Read(id);
        salaes.Remove(s);
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Create))
        {
            serializer.Serialize(fileStream, salaes);
        };
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }

    public Sale? Read(int id)
    {
        List<Sale> salaes = new List<Sale>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            salaes = serializer.Deserialize(fileStream) as List<Sale?>;
        }
        var s = salaes.FirstOrDefault(i => i._id == id);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return s ?? throw new dalTheEntityDoesNotExist("The product does not exist");
    }

    public Sale? Read(Func<Sale, bool> filter)
    {
        List<Sale> salaes = new List<Sale>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            salaes = serializer.Deserialize(fileStream) as List<Sale?>;
        }
        Sale? s = salaes.FirstOrDefault(i => filter(i) == true);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return s ?? throw new dalTheEntityDoesNotExist("The product not exist");
    }

    public List<Sale?> ReadAll(Func<Sale, bool>? filter = null)
    {
        List<Sale> salaes = new List<Sale>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            salaes = serializer.Deserialize(fileStream) as List<Sale?>;
        }
        if (filter != null)
        {
            List<Sale?> salaes2 = salaes.Where(i => filter(i) == true).ToList();
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
            return salaes2;
        }
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return new List<Sale?>(salaes);
    }

    public void Update(Sale item)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Delete(item._id);
        Create(item);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }
}
