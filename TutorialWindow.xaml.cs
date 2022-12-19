using FirstWpfAPPColorPicker;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ColorChanger
{
    /// <summary>
    /// Interaktionslogik für TutorialWindow.xaml
    /// </summary>
    public partial class TutorialWindow : Window
    {
        private int cnt = 0;
        private Image[] images = new Image[]
        {
            new Image(), new Image(), new Image(), new Image(), new Image(),new Image(),new Image(),new Image(),new Image(),new Image()
        };
        static Window window = Application.Current.MainWindow;
        private MainWindow wind = window as MainWindow;
        private bool FrontLastClicked = false;
        private bool BackLastClicked = false;

        private string[] texts = new string[]
        {
            "This is the Standard Page. Here you can see many features of this application",
            "Next up we have the Choose File Button. You press this button to select your  PlayerData.dat File. The path to it can be found on the Github Page!",
            "Here you can see the Type Selection. You select whatever you want to edit. For example if you wanna edit the Color of the left saber then you press on left saber and it should automatically load the colors from your File.",
            "Here you can select which Color Slot you want to edit. The first of your personal colors would be I Slot this is why its already selected!",
            "With the sliders you can see you can edit every color and the Color Preview on the left of it will update in Real Time. If you select any other Type the changes will not Save automatically. You will need to hit the SAVE button first.",
            "Here you can see the Overloader. It is the main reason why I made this program. If you press it, you can Drag the sliders to Values above 255. This will also apply ingame. This is how people like Electrostats or Person get oversaturated Colors.",
            "Now we come to the Auto Overloader. If you press it, there will be a popup. It asks you to Press the button of which Color you want to Auto Overload. For example if you press the Auto Overload Red button it will multiply the red value by 2",
            "On this image you can see the default presets I made. These were added by me to make your life just a bit easier. It also contains your Custom Presets. The Custom Presets can be edited in the presets.json file also contained in the folder!",
            "This is the Random Color button. I think by now you can imagine what it does. Thats right it picks a random value for each Slider and out of that you get a random color.",
            "So yeah this is the last Function. As mentioned before you need to press this button everytime you change a color of a Type."
        };
        public TutorialWindow()
        {
            Top = wind.Top;
            Left = wind.Left;
            AddImagesToArray();
            InitializeComponent();
        }

        private void AddImagesToArray()
        {
            images[0].Source = new BitmapImage(new Uri("/IMG/StandardPage.png", UriKind.Relative));
            images[1].Source = new BitmapImage(new Uri("/IMG/ChooseFileSelected.png", UriKind.Relative));
            images[2].Source = new BitmapImage(new Uri("/IMG/TypeSelection.png", UriKind.Relative));
            images[3].Source = new BitmapImage(new Uri("/IMG/SlotSelection.png", UriKind.Relative));
            images[4].Source = new BitmapImage(new Uri("/IMG/SlidersSelected.png", UriKind.Relative));
            images[5].Source = new BitmapImage(new Uri("/IMG/OverloadSelected.png", UriKind.Relative));
            images[6].Source = new BitmapImage(new Uri("/IMG/AutoOverloadSelected.png", UriKind.Relative));
            images[7].Source = new BitmapImage(new Uri("/IMG/CustomizableButtonsSelected.png", UriKind.Relative));
            images[8].Source = new BitmapImage(new Uri("/IMG/RandomColorSelected.png", UriKind.Relative));
            images[9].Source = new BitmapImage(new Uri("/IMG/SaveButtonSelected.png", UriKind.Relative));
        }

        private void NextImage(object sender, RoutedEventArgs e)
        {
            if (BackLastClicked)
            {
                cnt++;
            }
            if (cnt == images.Length)
            {
                this.Close();
                return;
            }

            FrontLastClicked = true;
            BackLastClicked = false;
            TutorialImage.Source = images[cnt].Source;
            TutorialText.Text = texts[cnt];
            cnt++;
        }

        private void PreviousImage(object sender, RoutedEventArgs e)
        {

            if (FrontLastClicked)
            {
                cnt--;
            }
            if (cnt - 1 < 0)
            {
                this.Close();
                return;
            }
            FrontLastClicked = false;
            BackLastClicked = true;
            cnt--;
            TutorialImage.Source = images[cnt].Source;
            TutorialText.Text = texts[cnt];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
