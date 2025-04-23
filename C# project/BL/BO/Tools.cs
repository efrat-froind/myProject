using System.Reflection;
using System.Text;

namespace BO;

public static class Tools
{
    public static DO.Client ConvertBOClientToDOclient(BO.Client c)
    {
         return new DO.Client(c.Id,
            c.Address,
            c.UserName,
            c.Phone);               
    }

    public static DO.Product ConvertBOProductToDOProduct(BO.Product p)
    {
        return new DO.Product(p.Code,
            p.Name,
            (DO.Category)p.Category,
            p.Price,
            p.QuantityInStock,
            p.Image);
    }


    public static DO.Sale ConvertBOSaleToDOSale(BO.Sale s)
    {
        return new DO.Sale(s.Id,
            s.Code,
            s.AmountForSale,
            s.PriceForSale,
            s.IsForEveryone,
            s.DateStart,
            s.DateFinifh);
    }



    public static BO.Client ConvertDOClientToBOclient(this DO.Client c)
    {
        return new BO.Client(c._id,c._userName,c._address,(int)c._phone);
    }

    public static BO.Product ConvertDOProductToBOProduct(DO.Product p)
    {
        return new BO.Product(p._code,
            p._name,
            (BO.Category)p._category,
            p._price,
            (int)p._QuantityInStock,
            p._image);
    }


    public static BO.Sale ConvertDOSaleToBOSale(DO.Sale s)
    {
        return new BO.Sale(s._id,
            s._code,
            s._amountForSale,
            (Double)s._priceForSale,
            (bool)s._isForEveryone,
            s._dateStart,
            s._dateFinifh);
    }


    public static String ToStringProperty<T>(this T obj)
    {
        StringBuilder sb= new StringBuilder();
        Type t = obj.GetType();
        foreach (PropertyInfo prop in t.GetProperties())
        {
            sb.AppendLine($"{prop.Name} = {prop.GetValue(obj)}");
        }
        return sb.ToString();

    } 
}
