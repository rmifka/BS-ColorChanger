using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        public PlayerData LoadFile()
        {
            if (dlg.CheckFileExists && dlg.FileName.Length != 0)
            {
                StreamReader sr = new StreamReader(dlg.OpenFile());
                string json = sr.ReadToEnd();
                PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(json);
                sr.Close();

                return playerData;
            }
            
            return null;
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
            PlayerData playerData = LoadFile();
            if (playerData == null)
            {
                return null;
            }
            BeatSaber.Color col = new BeatSaber.Color() { r = RedSlider.Value / 255, g = GreenSlider.Value / 255, b = BlueSlider.Value / 255, a = AlphaSlider.Value / 255 };

            if (i == 1)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].saberAColor = col;
            }
            else if(i == 2)
            {
                playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].saberBColor = col;
            }
            else if(i == 3)
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
            for (int i = 0; i < overloadCount;i++)
            {
                RedSlider.Maximum += MINIMUM;
                GreenSlider.Maximum += MINIMUM;
                BlueSlider.Maximum += MINIMUM;
                AlphaSlider.Maximum += MINIMUM;
            }
        }

        
        
        private void SliderValue(int user,int i)
        {
            PlayerData playerData = LoadFile();
            if (playerData == null)
            {
                return;
            }
            var colorData = playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1];
            
                
            
            if (i == 1)
            {
                if (colorData.saberAColor.r * 255 > 255 || colorData.saberAColor.b * 255 > 255 || colorData.saberAColor.g * 255 > 255 || colorData.saberAColor.a * 255 > 255)
                {
                    RedSlider.Maximum = MAXIMUM;
                    GreenSlider.Maximum = MAXIMUM;
                    BlueSlider.Maximum = MAXIMUM;
                    AlphaSlider.Maximum = MAXIMUM;
                }
                else
                {
                    RedSlider.Maximum = MINIMUM;
                    GreenSlider.Maximum = MINIMUM;
                    BlueSlider.Maximum = MINIMUM;
                    AlphaSlider.Maximum = MINIMUM;
                }
                RedSlider.Value = Math.Round(colorData.saberAColor.r * 255);
                GreenSlider.Value = Math.Round(colorData.saberAColor.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.saberAColor.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.saberAColor.a * MINIMUM);
                
            }
            else if(i ==2)
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
                    RedSlider.Maximum = MINIMUM;
                    GreenSlider.Maximum = MINIMUM;
                    BlueSlider.Maximum = MINIMUM;
                    AlphaSlider.Maximum = MINIMUM;
                }
                RedSlider.Value = Math.Round(colorData.saberBColor.r * MINIMUM);
                GreenSlider.Value = Math.Round(colorData.saberBColor.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.saberBColor.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.saberBColor.a * MINIMUM);
                
            }    
            else if(i==3)
            {
                if (colorData.environmentColor0.r * MINIMUM > MINIMUM || colorData.environmentColor0.b * MINIMUM > MINIMUM || colorData.environmentColor0.g * MINIMUM > MINIMUM || colorData.environmentColor0.a * MINIMUM > MINIMUM)
                {
                    RedSlider.Maximum = MAXIMUM;
                    GreenSlider.Maximum = MAXIMUM;
                    BlueSlider.Maximum = MAXIMUM;
                    AlphaSlider.Maximum = MAXIMUM;
                }
                else
                {
                    RedSlider.Maximum = MINIMUM;
                    GreenSlider.Maximum = MINIMUM;
                    BlueSlider.Maximum = MINIMUM;
                    AlphaSlider.Maximum = MINIMUM;
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
                    RedSlider.Maximum = MAXIMUM;
                    GreenSlider.Maximum = MAXIMUM;
                    BlueSlider.Maximum = MAXIMUM;
                    AlphaSlider.Maximum = MAXIMUM;
                }
                else
                {
                    RedSlider.Maximum = MINIMUM;
                    GreenSlider.Maximum = MINIMUM;
                    BlueSlider.Maximum = MINIMUM;
                    AlphaSlider.Maximum = MINIMUM;
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
                    RedSlider.Maximum = MAXIMUM;
                    GreenSlider.Maximum = MAXIMUM;
                    BlueSlider.Maximum = MAXIMUM;
                    AlphaSlider.Maximum = MAXIMUM;
                }
                else
                {
                    RedSlider.Maximum = MINIMUM;
                    GreenSlider.Maximum = MINIMUM;
                    BlueSlider.Maximum = MINIMUM;
                    AlphaSlider.Maximum = MINIMUM;
                }
                RedSlider.Value = Math.Round(colorData.obstaclesColor.r * MINIMUM);
                GreenSlider.Value = Math.Round(colorData.obstaclesColor.g * MINIMUM);
                BlueSlider.Value = Math.Round(colorData.obstaclesColor.b * MINIMUM);
                AlphaSlider.Value = Math.Round(colorData.obstaclesColor.a * MINIMUM);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var file = dlg.ShowDialog();
            dlg.DefaultExt = ".dat";
            dlg.Filter = "Data Files (.dat)|*.dat";
            dlg.Multiselect = false;

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
            foreach (Button button in My_Menu.Items)
            {
                button.Background = BACKGROUND_COLOR;
            }
            b.Background = Brushes.LightGreen;
            //6 stelle = die zahl die ich benutzen will - 1
            for (int i = 1; i <= 4; i++)
            {
                if (b.Name.Contains(i.ToString()))
                {
                    selectedUser = i;
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
            PlayerData missionsStatsData = LoadFile();
            if (missionsStatsData == null)
            {
                MessageBox.Show("Please select a valid file first!");
                return;
            }
            for (int i = 0; i < missionsStatsData.localPlayers[0].missionsStatsData.Count; i++)
            {
                missionsStatsData.localPlayers[0].missionsStatsData[i].cleared = true;
            }

        }

        private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorPreview.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value));
        }

        private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorPreview.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value));
        }

        private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorPreview.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void AutoOversaturate(object sender, RoutedEventArgs e)
        {
            
            AlphaSlider.Value += MINIMUM;
            GreenSlider.Value += MINIMUM;
            RedSlider.Value += MINIMUM;
            BlueSlider.Value += MINIMUM;    
            
        }

        private void OverloadCountBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = OverloadCountBox.Text;
            overloadCount = int.TryParse(text, out overloadCount) ? overloadCount : 1;
        }
    }
}
