
using System.Diagnostics;
using DalApi;

namespace Dal
{
    public sealed class DalXml : IDal
    {
        public IProduct Product =>new ProductImplementation();

        public ISale Sale => new SaleImplementation();

        public IClient Client => new ClientImplementation();

        private static readonly DalXml instance = new DalXml();
        public static DalXml Instance { get { return instance; } }

        private DalXml() { }
    }
}
