using FirstWpfAPPColorPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ColorChanger
{
    /// <summary>
    /// Interaktionslogik für StartScreen.xaml
    /// </summary>
    ///
    ///
    
    public partial class StartScreen : Window
    {
        private static MainWindow window = Application.Current.MainWindow as MainWindow;
        public StartScreen()
        {
            Top = window.Top;
            Left = window.Left;
            Topmost = true;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
