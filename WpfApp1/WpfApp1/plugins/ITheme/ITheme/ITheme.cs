using System.Windows;


namespace MyTheme
{
    public interface ITheme
    {
        string Name { get; }
        ResourceDictionary Rainbow();
    }
}
