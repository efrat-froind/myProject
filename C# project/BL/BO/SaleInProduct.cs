using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
    public class SaleInProduct
    {
       public int Id { get; init; }
       public int QuantityForSale { get; set; }
       public double Price { get; set; }
       public bool IsForEveryone { get; set; }
    }
