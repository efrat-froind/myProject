
namespace DalTest;

using DO;
using DalApi;

public static class Initialization
{
    private static  IDal? s_dal;
    private static List<int> productCodes= new List<int>();

    public static void createProduct (){
        productCodes.Add(s_dal.Product.Create(new Product(100,"נעלי פומה לבנות לנשים",Category.נעליים,450,25,"")));
        productCodes.Add(s_dal.Product.Create(new Product(102, "גרבי חצי רגל-אדידאס", Category.גרביים, 60, 42, "")));
    }
    public static void createSale()
    {
        s_dal.Sale.Create(new Sale(1, productCodes[0], 5, 10, true, DateTime.Now, DateTime.Parse("12-10-2024")));
        s_dal.Sale.Create(new Sale(2, productCodes[1], 4, 60, false, DateTime.Parse("12-10-2024"), DateTime.Parse("22-11-2024")));
    }
    public static void createClient (){
        s_dal.Client.Create(new Client(10,"כהן חני","רבי טרפון 23",0524876354));
        s_dal.Client.Create(new Client(11, "לוי רחל", "אור החיים 25", 0527181558));
    }

    public static void Initialize() {
        s_dal = Factory.Get;

        createClient();
        createProduct();
        createSale();
    }
}

