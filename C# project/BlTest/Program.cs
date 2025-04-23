
using DalApi;
using DalTest;
using System.Reflection;
using Tools;

namespace BlTest;
internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    static BO.Order order = new BO.Order();
    private static void Main(string[] args)
    {
        try
        {
            Initialization.Initialize();
            int id;
            Console.WriteLine("Enter the Id");
            id = int.Parse(Console.ReadLine());
            if (s_bl.Client.IsExist(id))
                order.IsPreferredCustomer = true;
            int select1 = PrintMainMenu();
            int select2;
            while (select1 == 1)
            {
                select2 = PrintSubMenu();
                switch (select2)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        select1 = PrintMainMenu();
                        break;
                    default:
                        Console.WriteLine("Wrong selection please select again.");
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void AddProduct()
    {
        int code;
        int quantity;
        Console.WriteLine("Enter the Code of the product");
        code = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the Quantity of the product");
        quantity = int.Parse(Console.ReadLine());
        List<BO.SaleInProduct> sales = s_bl.Order.AddProductToOrder(order, code, quantity);
        foreach (var sale in sales)
        {
            Console.WriteLine("sales for product:");
            Console.WriteLine($"Id={sale.Id} QuantityForSale={sale.QuantityForSale} Price={sale.Price} IsForEveryone={sale.IsForEveryone}");
        }
        Console.WriteLine($"Price for order {order.TotalPrice}");
    }


    private static int PrintMainMenu()
    {
        Console.WriteLine("To place an order press 1");
        Console.WriteLine("To exit press 0");
        int select;
        if (!int.TryParse(Console.ReadLine(), out select))
            select = -1;
        return select;
    }

    private static int PrintSubMenu()
    {
        Console.WriteLine("To add product press 1");
        Console.WriteLine("To complete the order press 2");
        int select;
        if (!int.TryParse(Console.ReadLine(), out select))
            select = -1;
        return select;
    }
}
