using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

    public class Sale
    {
       public int Id { get; init; }
       public int Code { get; }
       public int AmountForSale { get; init; }
       public double? PriceForSale { get; init; }
       public bool? IsForEveryone { get; init; }
       public DateTime DateStart { get; init; }
       public DateTime DateFinifh { get; init; }

       public Sale(int id,int code,int amountForSale,double priceForSale, bool isForEveryone,DateTime dateStart,DateTime dateFinifh)
       {
          Id = id;
          Code = code;
          AmountForSale = amountForSale;
          PriceForSale = priceForSale;
          IsForEveryone = isForEveryone;
          DateStart = dateStart;
          DateFinifh = dateFinifh;   
       }
}
