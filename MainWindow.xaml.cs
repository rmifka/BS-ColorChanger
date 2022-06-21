using System;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using Newtonsoft.Json;
using BeatSaber;
using HSVConfig;

namespace FirstWpfAPPColorPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public OpenFileDialog dlg = new OpenFileDialog();
        public int selectedUser = 1;
        public int leftOrRight = 1;
        public int overloadCount = 1;

        const int MAXIMUM = 510;
        const int MINIMUM = 255;
        SolidColorBrush BACKGROUND_COLOR = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 88, 88, 88));

        public PlayerData playerData;


        public MainWindow()
        {
            InitializeComponent();
            dlg.Filter = "Data Files (.dat)|*.dat";
            dlg.DefaultExt = ".dat";
        }

        public void LoadFile()
        {
            if (dlg.CheckFileExists && dlg.FileName.Length != 0)
            {
                try
                {
                    StreamReader sr = new StreamReader(dlg.OpenFile());
                    string json = sr.ReadToEnd();
                    PlayerData playerDataOneLoad = JsonConvert.DeserializeObject<PlayerData>(json);
                    sr.Close();

                    playerData = playerDataOneLoad;
                    SliderValue(selectedUser, leftOrRight);
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex.Message);
                }

            }
        }
        /*
        public HSV LoadHSVFile()
        {
            if (dlg.CheckFileExists && dlg.FileName.Length != 0)
            {
                StreamReader sr = new StreamReader(dlg.OpenFile());
                string json = sr.ReadToEnd();
                HSV hsvData = JsonConvert.DeserializeObject<HSV>(json);
                sr.Close();

                return hsvData;
            }
            return null;
        }
        public Judgments ChangeHSVFileId(int user, int i)
        {
            HSV playerData = LoadHSVFile();
            if (playerData == null)
            {
                return null;
            }
            HSVConfig.Color col = new HSVConfig.Color() { r = RedSlider.Value / MINIMUM, g = GreenSlider.Value / 255, b = BlueSlider.Value / 255, a = AlphaSlider.Value / 255 };

            Judgments j = new Judgments() { color = col, threshold = i };

            return j;
        }
        */

        public PlayerData ChangeFileId(int user, int i)
        {

            if (playerData == null)
            {
                return null;
            }
            BeatSaber.Color col = new BeatSaber.Color() { r = RedSlider.Value / 255, g = GreenSlider.Value / 255, b = BlueSlider.Value / 255, a = AlphaSlider.Value / 255 };

            if (i == 1)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].saberAColor = col;
            }
            else if (i == 2)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].saberBColor = col;
            }
            else if (i == 3)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].environmentColor0 = col;
            }
            else if (i == 4)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].environmentColor1 = col;
            }
            else if (i == 5)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].obstaclesColor = col;
            }
            return playerData;
        }
        public void SaveFile(PlayerData playerData)
        {
            if (playerData != null)
            {
                FileStream theFile = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
                string result = JsonConvert.SerializeObject(playerData);
                StreamWriter sw = new StreamWriter(theFile);
                sw.WriteLine(result);
                sw.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < overloadCount; i++)
            {
                RedSlider.Maximum += MINIMUM;
                GreenSlider.Maximum += MINIMUM;
                BlueSlider.Maximum += MINIMUM;
                AlphaSlider.Maximum += MINIMUM;
            }
        }



        private void SliderValue(int user, int i)
        {

            if (playerData == null)
            {
                return;
            }
            var colorData = playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1];



            if (i == 1)
            {
                if (colorData.saberAColor.r * 255 > 255 || colorData.saberAColor.b * 255 > 255 || colorData.saberAColor.g * 255 > 255 || colorData.saberAColor.a * 255 > 255)
                {
                    SetMaximum();
                }
                else
                {
                    SetMinimum(); ;
                }
                RedSlider.Value = Math.Round(colorData.saberAColor.r * 255);
                GreenSlider.Value = Math.Round(colorData.saberAColor.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.saberAColor.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.saberAColor.a * MINIMUM);

            }
            else if (i == 2)
            {
                if (colorData.saberBColor.r * MINIMUM > MINIMUM || colorData.saberBColor.b * MINIMUM > MINIMUM || colorData.saberBColor.g * MINIMUM > MINIMUM || colorData.saberBColor.a * MINIMUM > MINIMUM)
                {
                    RedSlider.Maximum = MAXIMUM;
                    GreenSlider.Maximum = MAXIMUM;
                    BlueSlider.Maximum = MAXIMUM;
                    AlphaSlider.Maximum = MAXIMUM;
                }
                else
                {
                    SetMinimum();
                }
                RedSlider.Value = Math.Round(colorData.saberBColor.r * MINIMUM);
                GreenSlider.Value = Math.Round(colorData.saberBColor.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.saberBColor.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.saberBColor.a * MINIMUM);

            }
            else if (i == 3)
            {
                if (colorData.environmentColor0.r * MINIMUM > MINIMUM || colorData.environmentColor0.b * MINIMUM > MINIMUM || colorData.environmentColor0.g * MINIMUM > MINIMUM || colorData.environmentColor0.a * MINIMUM > MINIMUM)
                {
                    SetMaximum();
                }
                else
                {
                    SetMinimum();
                }


                RedSlider.Value = Math.Round(colorData.environmentColor0.r * MINIMUM);
                GreenSlider.Value = Math.Round(colorData.environmentColor0.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.environmentColor0.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.environmentColor0.a * MINIMUM);

            }
            else if (i == 4)
            {
                if (colorData.environmentColor1.r * MINIMUM > MINIMUM || colorData.environmentColor1.b * MINIMUM > MINIMUM || colorData.environmentColor1.g * MINIMUM > MINIMUM || colorData.environmentColor1.a * MINIMUM > MINIMUM)
                {
                    SetMaximum();
                }
                else
                {
                    SetMinimum();
                }
                RedSlider.Value = Math.Round(colorData.environmentColor1.r * MINIMUM);
                GreenSlider.Value = Math.Round(colorData.environmentColor1.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.environmentColor1.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.environmentColor1.a * MINIMUM);

            }
            else if (i == 5)
            {
                if (colorData.obstaclesColor.r * MINIMUM > MINIMUM || colorData.obstaclesColor.b * MINIMUM > MINIMUM || colorData.obstaclesColor.g * MINIMUM > MINIMUM || colorData.obstaclesColor.a * MINIMUM > MINIMUM)
                {
                    SetMaximum();
                }
                else
                {
                    SetMinimum();
                }
                RedSlider.Value = Math.Round(colorData.obstaclesColor.r * MINIMUM);
                GreenSlider.Value = Math.Round(colorData.obstaclesColor.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.obstaclesColor.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.obstaclesColor.a * MINIMUM);
            }

        }

        private void SetMinimum()
        {
            RedSlider.Maximum = MINIMUM;
            GreenSlider.Maximum = MINIMUM;
            BlueSlider.Maximum = MINIMUM;
            AlphaSlider.Maximum = MINIMUM;
        }
        private void SetMaximum()
        {
            RedSlider.Maximum = MAXIMUM;
            GreenSlider.Maximum = MAXIMUM;
            BlueSlider.Maximum = MAXIMUM;
            AlphaSlider.Maximum = MAXIMUM;
        }
        private void SelectFile(object sender, RoutedEventArgs e)
        {

            var file = dlg.ShowDialog();
            dlg.Multiselect = false;
            LoadFile();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RedSlider.Maximum = MINIMUM;
            GreenSlider.Maximum = MINIMUM;
            BlueSlider.Maximum = MINIMUM;
            AlphaSlider.Maximum = MINIMUM;
            if (AlphaSlider.Value > MINIMUM)
            {
                AlphaSlider.Value = MINIMUM;
            }
            if (RedSlider.Value > MINIMUM)
            {
                RedSlider.Value = MINIMUM;
            }
            if (GreenSlider.Value > MINIMUM)
            {
                GreenSlider.Value = MINIMUM;
            }
            if (BlueSlider.Value > MINIMUM)
            {
                BlueSlider.Value = MINIMUM;
            }
        }

        private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SaveToUserClick(object sender, RoutedEventArgs e)
        {
            PlayerData playerData = ChangeFileId(selectedUser, leftOrRight);
            if (playerData == null)
            {
                MessageBox.Show("Please select a valid file first!");
                return;
            }

            SaveFile(playerData);
            MessageBox.Show("Saved to user " + selectedUser);

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            for (int i = 0; i < 4; i++)
            {
                Button button = (Button)My_Menu.Items[i];
                button.Background = BACKGROUND_COLOR;
            }
            b.Background = Brushes.LightGreen;
            //6 stelle = die zahl die ich benutzen will - 1
            for (int i = 1; i <= 4; i++)
            {
                if (b.Name.Contains(i.ToString()))
                {
                    selectedUser = i;
                    SliderValue(selectedUser, leftOrRight);
                }

            }

        }

        private void Click_ColorLeftOrRight(object sender, RoutedEventArgs e)
        {

            Button b = (Button)sender;
            foreach (Button button in LeftOrRight.Items)
            {

                button.Background = BACKGROUND_COLOR;
            }
            b.Background = Brushes.LightGreen;
            if (b.Name.Contains("Left"))
            {
                leftOrRight = 1;
            }
            else if (b.Name.Contains("Right"))
            {
                leftOrRight = 2;
            }
            else if (b.Name.Contains("EnvironmentOuter"))
            {
                leftOrRight = 3;
            }
            else if (b.Name.Contains("EnvironmentInner"))
            {
                leftOrRight = 4;
            }
            else if (b.Name.Contains("Wall"))
            {
                leftOrRight = 5;
            }
            SliderValue(selectedUser, leftOrRight);
        }

        private void ClearAllCampaigns(object sender, RoutedEventArgs e)
        {

            if (playerData == null)
            {
                MessageBox.Show("Please select a valid file first!");
                return;
            }
            for (int i = 0; i < playerData.localPlayers[0].missionsStatsData.Count; i++)
            {
                playerData.localPlayers[0].missionsStatsData[i].cleared = true;
            }

        }

        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            ColorPreviewChange();
        }

        private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            ColorPreviewChange();
        }

        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            ColorPreviewChange();
        }

        private void AutoOversaturate(object sender, RoutedEventArgs e)
        {
            Button_Click(sender, e);
            AlphaSlider.Value *= 2;
            GreenSlider.Value *= 2;
            RedSlider.Value *= 2;
            BlueSlider.Value *= 2;

        }

        public void ColorPreviewChange()
        {
            int redSliderValue = (int)RedSlider.Value;
            int greenSliderValue = (int)GreenSlider.Value;
            int blueSliderValue = (int)BlueSlider.Value;
            if (redSliderValue > MINIMUM)
            {
                redSliderValue = 255;
            }
            if (greenSliderValue > MINIMUM)
            {
                greenSliderValue = 255;
            }
            if (blueSliderValue > MINIMUM)
            {
                blueSliderValue = 255;
            }
            ColorPreview.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)redSliderValue, (byte)greenSliderValue, (byte)blueSliderValue));
        }
        private void OverloadCountBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = OverloadCountBox.Text;
            overloadCount = int.TryParse(text, out overloadCount) ? overloadCount : 1;
        }

        private void CheckHex(object sender, TextChangedEventArgs e)
        {
            string text = HexadecimalBox.Text.ToUpper();


            if (text.Length >= 2)
            {
                RedSlider.Value = HexToInt(text, 0, 1);
            }
            if (text.Length >= 4)
            {
                GreenSlider.Value = HexToInt(text, 2, 3);
            }
            if (text.Length >= 6)
            {
                BlueSlider.Value = HexToInt(text, 4, 5);
            }
            if (text.Length == 8)
            {
                AlphaSlider.Value = HexToInt(text, 6, 7);
            }

        }

        public int HexToInt(string text, int index1, int index2)
        {
            string text2places = string.Empty;
            text2places = text[index1].ToString().ToUpper() + text[index2].ToString().ToUpper();
            for (int i = 0; i < text2places.Length; i++)
            {
                if (!char.IsDigit(text2places[i])
                    && text2places[i] != 'F'
                    && text2places[i] != 'E'
                    && text2places[i] != 'D'
                    && text2places[i] != 'C'
                    && text2places[i] != 'B'
                    && text2places[i] != 'A')
                {
                    return 0;
                }
            }
            return Convert.ToInt32(text2places, 16);
        }

        private void RandomColor(object sender, RoutedEventArgs e)
        {
            Random randoInt = new Random();
            RedSlider.Value = randoInt.Next(0, 255);
            GreenSlider.Value = randoInt.Next(0, 255);
            BlueSlider.Value = randoInt.Next(0, 255);
        }

        private void SerColorFromPreset(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            //# FFC3FBE4        
            Brush backgroundColor = b.Background;
            int redBrush = ((SolidColorBrush)backgroundColor).Color.R;
            int greenBrush = ((SolidColorBrush)backgroundColor).Color.G;
            int blueBrush = ((SolidColorBrush)backgroundColor).Color.B;

            if (b.Name.Contains("Overload"))
            {
                SetMaximum();
                redBrush *= 2;
                greenBrush *= 2;
                blueBrush *= 2;
            }
            else
            {
                SetMinimum();
            }

            RedSlider.Value = redBrush;
            GreenSlider.Value = greenBrush;
            BlueSlider.Value = blueBrush;      
            AlphaSlider.Value = MAXIMUM;
        }
    }
}
