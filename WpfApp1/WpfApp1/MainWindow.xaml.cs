using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Configuration;
using System.Windows.Controls;

namespace Enumerables
{
    public partial class MainWindow : Window
    {
        public Miniature windowMin; 
        public GameEnumerable catalog = new GameEnumerable();
        public List<object> MyList = new List<object>();
        private readonly string _themesPath = Path.Combine(Directory.GetCurrentDirectory(),
            @ConfigurationManager.AppSettings["plugins"]);
        public int i = 0;
        private MediaPlayer player;
        public MainWindow()
        {
            player = new MediaPlayer();
            player.Open(new Uri(@"..\..\Resources\sound.mp3", UriKind.Relative));
            player.Play();
            
            
            InitializeComponent();
            Loaded += MainWindow_loaded;

            var watcher = new FileSystemWatcher
            {
                Path = @"..\..\",
                Filter = "*.txt",
                IncludeSubdirectories = true
            };

            FileSystemEventHandler handler = (o, e) =>
            Console.WriteLine("File {0} was {1}", e.FullPath, e.ChangeType);
            watcher.Created += handler;
            watcher.Changed += handler;
            watcher.Deleted += handler;
            watcher.Renamed += (o, e) =>
            Console.WriteLine("Renamed: {0} -> {1}", e.OldFullPath, e.FullPath);
            watcher.Error += (o, e) =>
            Console.WriteLine("Error: {0}", e.GetException().Message);
            watcher.EnableRaisingEvents = true;

            PluginsWork.ToPlugAsync(_themesPath, MyList, PluginMenu, this);
            LoadHotkeys();
            LoadPictures();
        }
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Convert.ToBoolean(ConfigurationManager.AppSettings["isHotKeysEnabled"]);
        }
        private void LoadHotkeys()
        {
            KeyConverter keyConverter = new KeyConverter();
            SaveBinding.Key = (Key)keyConverter.ConvertFromString(ConfigurationManager.AppSettings["saveHK"]);
            LoadBinding.Key = (Key)keyConverter.ConvertFromString(ConfigurationManager.AppSettings["loadHK"]);
        }
        private void LoadPictures()
        {
            try
            {
                ImageSourceConverter test = new ImageSourceConverter(); 
                SaveBinaryIcon.Source= (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["stone"]);
                FileIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["stone"]);
                LoadBinaryIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["pen"]);
                LINQIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["pen"]);
                SaveIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["rockstar"]);
                PageIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["rockstar"]);
                LoadIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["toriel"]);
                PluginIcon.Source = (ImageSource)test.ConvertFromString(ConfigurationManager.AppSettings["toriel"]);
            }
            catch
            {
                return;
            }
        }
        private void MainWindow_loaded(object sender, RoutedEventArgs e)
        {       
            catalog = FileWork.BinaryLoad();
            this.DataContext = catalog.gameCatalog[0];

            windowMin = new Miniature { Owner = this };
            windowMin.ShowDialog();

        }
     
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (i + 1 < catalog.gameCatalog.Length) i++; 
            this.DataContext = catalog.gameCatalog[i];
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (i - 1 >= 0) i--;
            this.DataContext = catalog.gameCatalog[i];
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow window1 = new ChangeWindow();
            if (window1.ShowDialog() == true)
            {
                if (window1._name == "" || window1._image == "" || window1._number == "" 
                    || window1._genre == "" || window1._descr == "") MessageBox.Show("Пустое поле!");
                else
                {
                    if (catalog.SearchItem(window1._name) == -1)
                    {
                        int _num = Convert.ToInt32(window1._number);
                        catalog.AddItem(window1._name, window1._image, _num, window1._genre, window1._descr);
                        MessageBox.Show("Элемент был добавлен");
                        i = 0;
                        this.DataContext = catalog.gameCatalog[i];
                        FileWork.SaveToFile(catalog);
                        FileWork.BinarySave(catalog);
                    }
                    else MessageBox.Show("Элемент с таким именем уже существует!");
                }
            }
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow window1 = new ChangeWindow();
            if (window1.ShowDialog() == true)
            {
                if (window1._name == "" || window1._image == "" || window1._number == ""
                    || window1._genre == "" || window1._descr == "") MessageBox.Show("Пустое поле!");
                else
                {
                    if (catalog.SearchItem(window1._name) != -1)
                    {
                        int _num = Convert.ToInt32(window1._number);
                        catalog.ChangeItem(window1._name, window1._image, _num, window1._genre, window1._descr);
                        MessageBox.Show("Элемент был изменен!");
                        i = 0;
                        this.DataContext = catalog.gameCatalog[i];
                        FileWork.SaveToFile(catalog);
                        FileWork.BinarySave(catalog);
                    }
                    else MessageBox.Show("Элементa с таким именем не существует!");
                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow window1 = new ChangeWindow();
            if (window1.ShowDialog() == true)
            {
                if (window1._name == "") MessageBox.Show("Пустое поле!");
                else
                {
                    if (catalog.SearchItem(window1._name) != -1)
                    {
                        if (catalog.gameCatalog.Length != 1)
                        {
                            catalog.RemoveItem(window1._name);
                            MessageBox.Show("Элемент был удален!");
                            i = 0;
                            this.DataContext = catalog.gameCatalog[i];
                            FileWork.SaveToFile(catalog);
                            FileWork.BinarySave(catalog);
                        }
                    }
                    else MessageBox.Show("Элементa с таким именем не существует!");
                }
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow window1 = new ChangeWindow();
            if (window1.ShowDialog() == true)
            {
                if (window1._name == "") MessageBox.Show("Пустое поле!");
                else
                {
                    if (catalog.SearchItem(window1._name) != -1)
                    {
                        i = catalog.SearchItem(window1._name);
                        this.DataContext = catalog.gameCatalog[i];
                        FileWork.SaveToFile(catalog);
                        FileWork.BinarySave(catalog);
                    }
                    else MessageBox.Show("Элементa с таким именем не существует!");
                }
            }
        }

        public void StandartStyle()
        {
            Resources.Clear();
            ResourceDictionary standart = new ResourceDictionary();

            Style firstColor = new Style();
            firstColor.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.CadetBlue)));
            standart.Add("firstColor", firstColor);

            Style secondColor = new Style();
            secondColor.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.SkyBlue)));
            secondColor.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.Black));
            standart.Add("secondColor", secondColor);
            Resources = standart;

        }
        public void SetStyle(ResourceDictionary styleResourses)
        {
            Resources.Clear();
            Resources = styleResourses;
        }
     
        private void SaveBinary_Click(object sender, RoutedEventArgs e)
        {
            FileWork.BinarySave(catalog);
            MessageBox.Show("Сохранено");
        }
        private void LoadBinary_Click(object sender, RoutedEventArgs e)
        {
            catalog = FileWork.BinaryLoad();
            MessageBox.Show("Загружено");
            this.DataContext = catalog.gameCatalog[0];
            i = 0;
        }
        private void SaveToFile_Click(object sender, RoutedEventArgs e)
        {
            FileWork.SaveToFile(catalog);
            MessageBox.Show("Сохранено");
        }
        private void LoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            catalog = FileWork.LoadFromFile();
            MessageBox.Show("Загружено");
            this.DataContext = catalog.gameCatalog[0];
            i = 0;
        }
        private void LINQ_Click(object sender, RoutedEventArgs e)
        {
            WindowLINQ wind = new WindowLINQ { Owner = this};
            wind.ShowDialog();
            
        }
        private void PageWiew_Click(object sender, RoutedEventArgs e)
        {
            PageWindow window = new PageWindow { Owner = this };
            window.ShowDialog(); 
        }
        
    }

}
