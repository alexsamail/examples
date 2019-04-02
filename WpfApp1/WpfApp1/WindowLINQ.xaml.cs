using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Enumerables
{
    public partial class WindowLINQ : Window
    {
        GameEnumerable catalog = new GameEnumerable();
        public WindowLINQ()
        {
            InitializeComponent();
            catalog = FileWork.BinaryLoad();
            listBox.ItemsSource = catalog; 
        }

        private void SortByEven_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<GameCatalog> evens = from item in catalog.AsParallel()  // отложенное выполнение PLINQ
                                                where item.Number % 2 == 0
                                                select item;
            listBox.ItemsSource = evens;
        }

        private void SortByNumber_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<GameCatalog> numbers = catalog.AsParallel().OrderBy(s => s.Number);
            listBox.ItemsSource = numbers;
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<GameCatalog> names = catalog.AsParallel().OrderBy(s => s.Name);
            listBox.ItemsSource = names;
        }

        private void SortByGenre_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<IGrouping<string, GameCatalog>> sIE = catalog.AsParallel().GroupBy(s => s.Genre);
            listBox.ItemsSource = sIE;
        }
    }
}
