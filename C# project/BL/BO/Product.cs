using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
    public class Product
    {
        public int Code { get; }
        public string Name { get; set; }
        public Category? Category { get; set; }
        public double Price { get; set; }
        public int? QuantityInStock { get; set; }
        public string? Image { get; set; }
        public List<BO.SaleInProduct> ListSaleInProduct{ get; set; }
    public DO.Category? Category1 { get; }

    public Product(int code,string name,Category ? category,double price,int ? quantityInStock,string image)
       {        
          Code = code;
          Name = name;
          Category = category;
          Price = price;
          QuantityInStock = quantityInStock;
          Image = image;
       }

}
