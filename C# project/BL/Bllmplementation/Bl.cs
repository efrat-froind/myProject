using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Bllmplementation;

internal class Bl : IBl
{
    public IProduct Product => new ProductImplementation();

    public ISale Sale => new SaleImplementation();

    public IClient Client => new ClientImplementation();

    public IOrder Order => new OrderImplementation();
}
