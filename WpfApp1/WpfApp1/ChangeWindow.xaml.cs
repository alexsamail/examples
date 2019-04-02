using System;
using System.Windows;
using System.Windows.Input;

namespace Enumerables
{
    public partial class ChangeWindow : Window
    {
        public string _name
        {
            get { return NameBox.Text; }
        }
        public string _image
        {
            get { return ImageBox.Text; }
        }
        public string _number
        {
            get { return NumberBox.Text; }
        }
        public string _genre
        {
            get { return GenreBox.Text; }
        }
        public string _descr
        {
            get { return DescriptionBox.Text; }
        }

        public ChangeWindow()
        {
            InitializeComponent();
            NumberBox.PreviewTextInput += new TextCompositionEventHandler(NumberBox_PreviewTextInput);
        }
        void NumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
        private void click_ok(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
