using System.Windows;
using FirstWpfAPPColorPicker;

namespace ColorChanger;

/// <summary>
///     Interaktionslogik für StartScreen.xaml
/// </summary>
public partial class StartScreen : Window
{
    private static readonly MainWindow window = Application.Current.MainWindow as MainWindow;

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