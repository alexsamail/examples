using System.Windows.Controls;

namespace Enumerables
{
    public partial class PageContent : Page
    {
        public PageContent()
        {
            InitializeComponent();
            GameEnumerable catalog = FileWork.BinaryLoad();
        }
    }
}
