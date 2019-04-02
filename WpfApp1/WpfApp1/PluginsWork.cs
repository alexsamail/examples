using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading;
using System.Reflection;

namespace Enumerables
{
    class PluginsWork
    {
        static private Task LoadPlugins(string _themesPath, List<object> MyList)
        {
            return Task.Run(() =>
            {
                var dlls = Directory.GetFiles(_themesPath, "*.dll", SearchOption.TopDirectoryOnly);

                foreach (var dll in dlls)
                {
                    var temp = Assembly.LoadFile(dll);
                    var types = temp.GetTypes();
                    foreach (var type in types)
                    {
                        if (type.GetInterfaces().Contains(typeof(MyTheme.ITheme)))
                        {
                            object objofclass = type.Assembly.CreateInstance(type.FullName);
                            MyList.Add(objofclass);
                        }
                    }
                }
                Thread.Sleep(5000);
            });
        }
        static private void AddPlugins(MenuItem menu, List<object> MyList, MainWindow window)
        {
            var newmenuitem = new MenuItem();
            newmenuitem.Header = "Стандартный";
            newmenuitem.Click += (object sender, RoutedEventArgs e) => window.StandartStyle();
            menu.Items.Add(newmenuitem);
            foreach (object o in MyList)
            {
                var newMenuItem = new MenuItem();
                newMenuItem.Header = ((MyTheme.ITheme)o).Name;
                newMenuItem.Click += (object sender, RoutedEventArgs e) => window.SetStyle(((MyTheme.ITheme)o).Rainbow());
                menu.Items.Add(newMenuItem);
            }
        }
        static public async void ToPlugAsync(string _themesPath, List<object> MyList, MenuItem menu, MainWindow window)
        {
            await LoadPlugins(_themesPath, MyList);
            AddPlugins(menu, MyList, window);
        }
    }
}
