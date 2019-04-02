using System.Windows;
using System.Threading;
using System.Windows.Threading;
namespace Enumerables
{
    public partial class PageWindow : Window
    {
        public GameEnumerable catalog = new GameEnumerable();
        private PageContent first;
        private PageContent previos;
        private PageContent next;
        private PageContent last;
        private int index;

        public PageWindow()
        {
            InitializeComponent();
            catalog = FileWork.BinaryLoad();
            new Thread(Begin).Start();
        }
        private void Begin()
        {
            LoadFirstPage();
            LoadLastPage();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ConstellFrame.Content = first;
            });
            index = 0;
            new Thread(LoadNextPage).Start();
        }
        private void LoadFirstPage()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                first = new PageContent
                {
                    DataContext = catalog.gameCatalog[0]
                };
            });
        }
        private void LoadLastPage()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                last = new PageContent
                {
                    DataContext = catalog.gameCatalog[catalog.gameCatalog.Length - 1]
                };
            });
        }
        private void LoadPrevPage()
        {
            if (index == 0)
                return;
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                previos = new PageContent
                {
                    DataContext = catalog.gameCatalog[index - 1]
                };
            });
        }
        private void LoadNextPage()
        {
            if (index == catalog.gameCatalog.Length - 1)
                return;
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                next = new PageContent
                {
                    DataContext = catalog.gameCatalog[index + 1]
                };
            });
        }
        private void FirstShow(object sender, RoutedEventArgs e)
        {
            ConstellFrame.Content = first;
            index = 0;
            previos = null;
            new Thread(LoadNextPage).Start();
        }
        private void PrevShow(object sender, RoutedEventArgs e)
        {
            if (index == 0)
                return;
            next = ConstellFrame.Content as PageContent;
            ConstellFrame.Content = previos;
            index--;
            new Thread(LoadPrevPage).Start();
        }
        private void NextShow(object sender, RoutedEventArgs e)
        {
            if (index == catalog.gameCatalog.Length - 1)
                return;
            previos = ConstellFrame.Content as PageContent;
            ConstellFrame.Content = next;
            index++;
            new Thread(LoadNextPage).Start();
        }
        private void LastShow(object sender, RoutedEventArgs e)
        {
            ConstellFrame.Content = last;
            index = catalog.gameCatalog.Length - 1;
            next = null;
            new Thread(LoadPrevPage).Start();
        }
    }
}
