using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace MyTheme
{
    public class Theme : ITheme
    {
        public string Name
        {
            get
            {
                return "PinkAndWhite";
            }
        }
        public ResourceDictionary Rainbow()
        {
            ResourceDictionary pinkAndWhite = new ResourceDictionary();

            Style firstColor = new Style();
            firstColor.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.White)));
            pinkAndWhite.Add("firstColor", firstColor);

            Style secondColor = new Style();
            secondColor.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Pink)));
            secondColor.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.White));
            pinkAndWhite.Add("secondColor", secondColor);

            return pinkAndWhite;
        }
    }
}
