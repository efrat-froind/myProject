
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using DalApi;
using DO;
using Tools;

namespace Dal;

internal class ProductImplementation : IProduct
{
    private static string XMLDATAPATH = @"..\xml\products.xml";
    XmlSerializer serializer = new XmlSerializer(typeof(List <Product>));

    public int Create(Product item)
    {
        List<Product> products = new List<Product>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            products = serializer.Deserialize(fileStream) as List<Product?>;
        }
        Product p = item with { _code = Config.getProductMinCode };
        products.Add(p);
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Create))
        {
            serializer.Serialize(fileStream, products);
        };
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return p._code;
    }

    public void Delete(int id)
    {
        List<Product> products = new List<Product>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            products = serializer.Deserialize(fileStream) as List<Product?>;
        }
        Product p = Read(id);
        products.Remove(p);
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Create))
        {
            serializer.Serialize(fileStream, products);
        };
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }

    public Product? Read(int id)
    {
        List<Product> products = new List<Product>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            products = serializer.Deserialize(fileStream) as List<Product?>;
        }
        var p = products.FirstOrDefault(i => i._code == id);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return p ?? throw new dalTheEntityDoesNotExist("The product does not exist");
    }

    public Product? Read(Func<Product, bool> filter)
    {
        List<Product> products = new List<Product>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            products = serializer.Deserialize(fileStream) as List<Product?>;
        }
        Product? p = products.FirstOrDefault(i => filter(i) == true);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return p ?? throw new dalTheEntityDoesNotExist("The product not exist");
    }

    public List<Product?> ReadAll(Func<Product, bool>? filter = null)
    {
        //List<Product> products = new List<Product>();
        //LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        //if (filter != null)
        //{
        //    using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        //    {
        //        products = serializer.Deserialize(fileStream) as List<Product?>;
        //    }
        //    List<Product?> products2 = products.Where(i => filter(i) == true).ToList();
        //    LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        //    return products2;
        //}
        //LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        //return new List<Product?>(products);
        List<Product> products = new List<Product>();
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        using (FileStream fileStream = new FileStream(XMLDATAPATH, FileMode.Open))
        {
            products = serializer.Deserialize(fileStream) as List<Product?>;
        }
        if (filter != null)
        {
            products = products.Where(i => filter(i) == true).ToList();
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");

        }
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return products;
    }

    public void Update(Product item)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Delete(item._code);
        Create(item);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }
}
