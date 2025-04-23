using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    List<BO.SaleInProduct> AddProductToOrder(BO.Order order, int code, int quantity);
    void CalcTotalPriceForProduct(BO.ProductInOrder product);
    void CalcTotalPrice(BO.Order order);
    void DoOrder(BO.Order order);

    void SearchSaleForProduct(BO.ProductInOrder p, bool isForEveryone);
}
