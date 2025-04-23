
using System.Reflection;
using DalApi;
using DalTest;
using DO;
using Tools;


internal class Program
{
    private static IDal? s_dal= Factory.Get;
    private static void Main(string[] args)
    {
        Console.WriteLine("HFGD");
        try
        {
            int select1= PrintMainMenu();
            int select2;
            while(select1!=0)
            {
                switch(select1)
                {
                    case 1:
                        Console.WriteLine("Product");
                        ProductMenu();
                        break;
                    case 2:
                        Console.WriteLine("Sale");
                        SaleMenu();
                        break;
                    case 3:
                        Console.WriteLine("Client");
                        ClientMenu();
                        break;
                    case 4:
                        LogManager.Remove();
                        Console.WriteLine("clear successfully");
                        break;
                    case 6:
                        Console.WriteLine("for Initialization");
                        Initialization.Initialize();
                        break;
                    default:
                        Console.WriteLine("Wrong selection please select again.");
                        break;
                }
                select1=PrintMainMenu();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }
    }

    private static void ProductMenu()
    {
        int select;
        select = PrintSubMenu("Product");
        while(select!=0)
        {
            switch(select)
            {
                case 1:
                    AddProduct();
                    break;
                case 2:
                    Read(s_dal.Product);
                    break;
                case 3:
                    ReadAll(s_dal.Product.ReadAll());
                    break;
                case 4:
                    UpdateProduct();
                    break;
                case 5:
                    Delete(s_dal.Product);
                    break;
                default:
                    Console.WriteLine("Wrong selection please select again.");
                    break;
            }
            select = PrintSubMenu("Product");
        }
    }

    private static void AddProduct()
    {
        string _name;
        Category _category;
        double _price;
        int _QuantityInStock;
        string _image;
        Console.WriteLine("Enter the Name of the product");
        _name = Console.ReadLine();
        Console.WriteLine("Enter the category:between 0 to 4");
        int cat;
        if (!int.TryParse(Console.ReadLine(), out cat)) _category = 0;
        else
            _category = (Category)cat;
        Console.WriteLine("Enter Price");
        if (!double.TryParse(Console.ReadLine(), out _price)) _price = 0;
        Console.WriteLine("Enter count in stock");
        if (!int.TryParse(Console.ReadLine(), out _QuantityInStock)) _QuantityInStock = 0;
        Console.WriteLine("Enter the Url of img");
        _image= Console.ReadLine();
        Product p = new Product(0, _name, _category, _price, _QuantityInStock, _image);
        int code=s_dal.Product.Create(p);
        p=p with { _code=code };

        Console.WriteLine("Product created successfully");
        Console.WriteLine(p);
    }

    private static void ReadAll<T>(List<T> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }

    private static void Read<T>(ICrud<T> icrud)
    {
        try
        {
            Console.WriteLine("Enter Code");
            int code = int.Parse(Console.ReadLine());
            Console.WriteLine(icrud.Read(code));
        }
        catch(Exception e) 
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }
    }

    private static void UpdateProduct()
    {
        try
        {
            int _code;
            string _name;
            Category _category;
            double _price;
            int _QuantityInStock;
            string _image;
            Console.WriteLine("Enter the Code of the product");
            _code = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Name of the product");
            _name = Console.ReadLine();
            Console.WriteLine("Enter the category:between 0 to 4");
            int cat;
            if (!int.TryParse(Console.ReadLine(), out cat)) _category = 0;
            else
                _category = (Category)cat;
            Console.WriteLine("Enter Price");
            if (!double.TryParse(Console.ReadLine(), out _price)) _price = 0;
            Console.WriteLine("Enter count in stock");
            if (!int.TryParse(Console.ReadLine(), out _QuantityInStock)) _QuantityInStock = 0;
            Console.WriteLine("Enter the Url of img");
            _image = Console.ReadLine();
            Product p = new Product(_code, _name, _category, _price, _QuantityInStock, _image);
            s_dal.Product.Update(p);
   
            Console.WriteLine("Product updated successfully");
            Console.WriteLine(p);
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }
        
    }

    private static void Delete<T>(ICrud<T> icrud)
    {
        try
        {
            Console.WriteLine("Enter Code");
            int code = int.Parse(Console.ReadLine());
            icrud.Delete(code);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }
    }

    private static void SaleMenu()
    {
        int select;
        select = PrintSubMenu("Sale");
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    AddSale();
                    break;
                case 2:
                    Read(s_dal.Sale);
                    break;
                case 3:
                    ReadAll(s_dal.Sale.ReadAll());
                    break;
                case 4:
                    UpdateSale();
                    break;
                case 5:
                    Delete(s_dal.Sale);
                    break;
                default:
                    Console.WriteLine("Wrong selection please select again.");
                    break;
            }
            select = PrintSubMenu("Sale");
        }
    }

    private static void AddSale()
    {
        try 
        {
            int _code;
            int _amountForSale;
            double _priceForSale;
            Boolean _isForEveryone;
            DateTime _dateStart;
            DateTime _dateFinifh;
            Console.WriteLine("Enter the Code of the product");
            _code = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the _amountForSale");
            if (!int.TryParse(Console.ReadLine(), out _amountForSale)) _amountForSale = 0;
            Console.WriteLine("Enter the _priceForSale");
            if (!double.TryParse(Console.ReadLine(), out _priceForSale)) _priceForSale = 0;
            Console.WriteLine("Enter _isForEveryone");
            if (!Boolean.TryParse(Console.ReadLine(), out _isForEveryone)) _isForEveryone = false;
            Console.WriteLine("Enter _dateStart of Sale");
            if (!DateTime.TryParse(Console.ReadLine(), out _dateStart)) _dateStart = DateTime.Now;
            Console.WriteLine("Enter _dateFinifh of Sale");
            if (!DateTime.TryParse(Console.ReadLine(), out _dateFinifh)) _dateFinifh = DateTime.Now.AddDays(4);
            Sale s = new Sale(0, _code, _amountForSale, _priceForSale, _isForEveryone, _dateStart, _dateFinifh);
            int id = s_dal.Sale.Create(s);
            s = s with { _id = id };

            Console.WriteLine("Sale created successfully");
            Console.WriteLine(s);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }
        
    }


    private static void UpdateSale()
    {
        try
        {
            int _id;
            int _code;
            int _amountForSale;
            double _priceForSale;
            Boolean _isForEveryone;
            DateTime _dateStart;
            DateTime _dateFinifh;
            Console.WriteLine("Enter the id of the Sale");
            _id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Code of the product");
            _code = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the _amountForSale");
            if (!int.TryParse(Console.ReadLine(), out _amountForSale)) _amountForSale = 0;
            Console.WriteLine("Enter the _priceForSale");
            if (!double.TryParse(Console.ReadLine(), out _priceForSale)) _priceForSale = 0;
            Console.WriteLine("Enter _isForEveryone");
            if (!Boolean.TryParse(Console.ReadLine(), out _isForEveryone)) _isForEveryone = false;
            Console.WriteLine("Enter _dateStart of Sale");
            if (!DateTime.TryParse(Console.ReadLine(), out _dateStart)) _dateStart = DateTime.Now;
            Console.WriteLine("Enter _dateFinifh of Sale");
            if (!DateTime.TryParse(Console.ReadLine(), out _dateFinifh)) _dateFinifh = DateTime.Now.AddDays(4);
            Sale s = new Sale(_id, _code, _amountForSale, _priceForSale, _isForEveryone, _dateStart, _dateFinifh);
            s_dal.Sale.Update(s);
            

            Console.WriteLine("Sale updated successfully");
            Console.WriteLine(s);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }

    }


    private static void ClientMenu()
    {
        int select;
        select = PrintSubMenu("Client");
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    AddClient();
                    break;
                case 2:
                    Read(s_dal.Client);
                    break;
                case 3:
                    ReadAll(s_dal.Client.ReadAll());
                    break;
                case 4:
                    UpdateClient();
                    break;
                case 5:
                    Delete(s_dal.Client);
                    break;
                default:
                    Console.WriteLine("Wrong selection please select again.");
                    break;
            }
            select = PrintSubMenu("Client");
        }
    }

    private static void AddClient()
    {
        try
        {
            int _id;
            string _userName;
            string _address;
            int _phone;
            Console.WriteLine("Enter the Id of Client");
            _id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the userName of Client");
            _userName = Console.ReadLine();
            Console.WriteLine("Enter the address of Client");
            _address = Console.ReadLine();
            Console.WriteLine("Enter _phone  of Client");
            if (!int.TryParse(Console.ReadLine(), out _phone)) _phone = 0;
            Client c = new Client(_id, _userName, _address, _phone);
            s_dal.Client.Create(c);

            Console.WriteLine("Client created successfully");
            Console.WriteLine(c);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }

    }


    private static void UpdateClient()
    {
        try
        {
            int _id;
            string _userName;
            string _address;
            int _phone;
            Console.WriteLine("Enter the Id of Client");
            _id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the userName of Client");
            _userName = Console.ReadLine();
            Console.WriteLine("Enter the address of Client");
            _address = Console.ReadLine();
            Console.WriteLine("Enter _phone  of Client");
            if (!int.TryParse(Console.ReadLine(), out _phone)) _phone = 0;
            Client c = new Client(_id, _userName, _address, _phone);
            s_dal.Client.Update(c);

            Console.WriteLine("Client updated successfully");
            Console.WriteLine(c);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, e.Message);
        }

    }


    public static int PrintMainMenu()
    {
        Console.WriteLine("For Products press 1");
        Console.WriteLine("For Sales press 2");
        Console.WriteLine("For Clients press 3");
        Console.WriteLine("To clear Log Directory press 4");
        Console.WriteLine("To exit press 0");
        Console.WriteLine("for Initialization press 6");
        int select;
        if (!int.TryParse(Console.ReadLine(), out select))
            select = -1;
        return select;
    }

    public static int PrintSubMenu(string item)
    {
        Console.WriteLine($"To add {item} press 1");
        Console.WriteLine($"To read one {item} press 2");
        Console.WriteLine($"To read all {item} press 3");
        Console.WriteLine($"To update {item} press 4");
        Console.WriteLine($"To delete {item} press 5");
        Console.WriteLine("To go back press 0");
        int select;
        if (!int.TryParse(Console.ReadLine(), out select))
            select = -1;
        return select;
    }
}