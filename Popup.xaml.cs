using FirstWpfAPPColorPicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaktionslogik für Popup.xaml
    /// </summary>
    public partial class Popup : Window
    {
        static Window window = Application.Current.MainWindow;
        private MainWindow wind = window as MainWindow;
        public enum SliderColor
        {
            // items of the enum
           Red,Green, Blue
        }
        
        public Popup()
        {
            Top = wind.Top;
            Left = wind.Left;
            Topmost = true;
            InitializeComponent();
        }
        public void AutoOversaturateDoing(Popup.SliderColor color)
        {
            if (window as MainWindow == null)
            {
                return;
            }
            switch (color)
            {
                case Popup.SliderColor.Red:
                    if (wind.RedSlider.Value * 2 > wind.RedSlider.Maximum)
                    {
                        Overload();
                    }
                    wind.RedSlider.Value*= 2;
                    break;
                case Popup.SliderColor.Blue:
                    if (wind.BlueSlider.Value * 2 > wind.BlueSlider.Maximum)
                    {
                        Overload();
                    }
                    wind.BlueSlider.Value *= 2;
                    break;
                case Popup.SliderColor.Green:
                    if (wind.GreenSlider.Value * 2 > wind.GreenSlider.Maximum)
                    {
                        Overload();
                    }
                    wind.GreenSlider.Value *= 2;
                    break;
            }
        }

        private void RedAutoOverload(object sender, RoutedEventArgs e)
        {
            AutoOversaturateDoing(SliderColor.Red);
            Close();
        }
        private void BlueAutoOverload(object sender, RoutedEventArgs e)
        {
            AutoOversaturateDoing(SliderColor.Blue);
            Close();
        }
        private void GreenAutoOverload(object sender, RoutedEventArgs e)
        {
            AutoOversaturateDoing(SliderColor.Green);
            Close();
        }

        private void Overload()
        {
            if (window as MainWindow == null)
            {
                return;
            }
            for (var i = 0; i < (window as MainWindow).overloadCount; i++)
            {
                wind.RedSlider.Maximum += 255.0;
                wind.GreenSlider.Maximum += 255.0;
                wind.BlueSlider.Maximum += 255.0;
                wind.AlphaSlider.Maximum += 255.0;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
