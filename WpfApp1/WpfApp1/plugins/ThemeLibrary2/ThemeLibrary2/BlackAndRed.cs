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
                return "BlackAndRed";
            }
        }
        public ResourceDictionary Rainbow()
        {
            ResourceDictionary blackAndRed = new ResourceDictionary();

            Style firstColor = new Style();
            firstColor.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Red)));
            blackAndRed.Add("firstColor", firstColor);

            Style secondColor = new Style();
            secondColor.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Black)));
            secondColor.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.White));
            blackAndRed.Add("secondColor", secondColor);

            return blackAndRed;
        }
    }
}
