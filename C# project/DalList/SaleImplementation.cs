namespace Dal;
using DO;
using DalApi;
using System;
using System.Reflection;
using Tools;

internal class SaleImplementation : ISale
{
    public int Create(Sale item)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Sale s = null;
        Product p = DataSource.SProduct.FirstOrDefault(i=>i._code== item._code);
        if (p != null)
        {
            s = item with { _id = DataSource.Config.SaleCode };
            DataSource.SSale.Add(s);
        }
        else
            throw new dalTheEntityDoesNotExist("The product in the sale does not exist.");
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return s._id;
    }
    public Sale? Read(int id)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Sale s = DataSource.SSale.FirstOrDefault(i => i._id == id);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return s ?? throw new dalTheEntityDoesNotExist("The sale does not exist.");
    }
    //public List<Sale> ReadAll()
    //{
    //    LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
    //    LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    //    return new List<Sale>(DataSource.SSale);
    //}
    public void Update(Sale item)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Delete(item._id);
        DataSource.SSale.Add(item);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }
    public void Delete(int id)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Sale s = Read(id);
        DataSource.SSale.Remove(s);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
    }

    public List<Sale?> ReadAll(Func<Sale, bool>? filter)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        if (filter != null)
        {
            List<Sale?> sales = DataSource.SSale.Where(i => filter(i) == true).ToList();
            LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
            return sales;
        }
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return new List<Sale?>(DataSource.SSale);
    }

    public Sale? Read(Func<Sale, bool> filter)
    {
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "Start");
        Sale? s = DataSource.SSale.FirstOrDefault(i => filter(i) == true);
        LogManager.WriteToLog(MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, "End");
        return s ?? throw new dalTheEntityDoesNotExist("The product not exist");
    }
}
