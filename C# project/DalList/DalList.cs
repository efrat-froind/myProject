
using DalApi;

namespace Dal;

internal sealed class DalList : IDal
{
    public IClient Client => new ClientImplementation();
    public ISale Sale => new SaleImplementation();
    public IProduct Product => new ProductImplementation();

    private static readonly DalList instance = new DalList();
    public static DalList Instance { get { return instance; } }

    private DalList() { }
}
