using BO

using BlApi


namespace UI
{
    public partial class ProductMenu : Form
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public ProductMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
