using System.Xml;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    private static string NAMEXML = @"..\xml\data-config.xml";
    static XElement xmlConfig = XElement.Load(NAMEXML);
    private const string PRODUCTMAXCODE = "ProductMinCode";
    private const string SALEMAXCODE = "SaleMinCode";
    private static int productMaxCode;

    static XmlDocument xmlDoc = new XmlDocument();
    public static int getProductMinCode
    {
        get
        {
            productMaxCode = int.Parse(xmlConfig.Element(PRODUCTMAXCODE).Value);
            int code = productMaxCode + 1;
            xmlConfig.Element(PRODUCTMAXCODE).SetValue(code);
            xmlConfig.Save(NAMEXML);
            return productMaxCode;
        }
    }
    private static int saleMaxCode;
    public static int getSaleMinCode
    {
        get
        {
            saleMaxCode = int.Parse(xmlConfig.Element(SALEMAXCODE).Value);
            int code = saleMaxCode + 1;
            xmlConfig.Element(SALEMAXCODE).SetValue(code);
            xmlConfig.Save(NAMEXML);
            return saleMaxCode;
        }
    }

}
