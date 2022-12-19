using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BeatSaber;
using ColorChanger;
using ColorChanger.HSV;
using Microsoft.Win32;
using Newtonsoft.Json;
using Color = System.Windows.Media.Color;

namespace FirstWpfAPPColorPicker;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const int MAXIMUM = 510;
    private const int MINIMUM = 255;
    private readonly SolidColorBrush BACKGROUND_COLOR = new(Color.FromArgb(255, 33, 33, 56)); //	212138 
    public OpenFileDialog dlg = new();
    public int leftOrRight = 1;
    public int overloadCount = 1;
    public PlayerData playerData;
    public int selectedUser = 1;


    public MainWindow()
    {
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        InitializeComponent();
        StartScreen start = new StartScreen();
        start.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        start.Show();
        InputColorsFromFileIntoButtons();
        dlg.Filter = "Data Files (.dat)|*.dat";
        dlg.DefaultExt = ".dat";
    }

    public void WriteEntireJsonFile()
    {
        var path = "Presets.json";
        var DataToWrite = new ColorImport
        {
            colors = new[]
            {
                new()
                {
                    name = "Default",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default1",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default2",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default3",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default4",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default5",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default6",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default7",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default8",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default9",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default10",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default11",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default12",
                    r = 255,
                    g = 255,
                    b = 255
                },
                new CustomColorPreset
                {
                    name = "Default13",
                    r = 255,
                    g = 255,
                    b = 255
                }
            }
        };
        File.WriteAllText(path, JsonConvert.SerializeObject(DataToWrite, Formatting.Indented));
        using (var file = File.CreateText(path))
        {
            new JsonSerializer().Serialize(file, DataToWrite);
        }
    }

    public ColorImport LoadColorsFromFile()
    {
        var path = "Presets.json";
        return TryReadCustomColorPreset(path);
    }

    public void InputColorsFromFileIntoButtons()
    {
        ChangeColorsAndName(Preset1, 0);
        ChangeColorsAndName(Preset2, 1);
        ChangeColorsAndName(Preset3, 2);
        ChangeColorsAndName(Preset4, 3);
        ChangeColorsAndName(Preset5, 4);
        ChangeColorsAndName(Preset6, 5);
        ChangeColorsAndName(Preset7, 6);
        ChangeColorsAndName(Preset8, 7);
        ChangeColorsAndName(Preset9, 8);
        ChangeColorsAndName(Preset10, 9);
        ChangeColorsAndName(Preset11, 10);
        ChangeColorsAndName(Preset12, 11);
        ChangeColorsAndName(Preset13, 12);
        ChangeColorsAndName(Preset14, 13);
    }

    public void ChangeColorsAndName(Button button, int arrayPlace)
    {
        ColorImport loadedColors = LoadColorsFromFile();
        if (loadedColors.colors[arrayPlace] == null || loadedColors.colors.Length < 14)
        {
            MessageBox.Show(
                "Sorry the software had a problem running. Have you checked your presets file? (Needs to be exactly 14 presets)");
            Close();
        }

        button.Name = loadedColors.colors[arrayPlace].name;
        button.Background = new SolidColorBrush(Color.FromRgb(
            loadedColors.colors[arrayPlace].r < 255 ? (byte)loadedColors.colors[arrayPlace].r : byte.MaxValue,
            loadedColors.colors[arrayPlace].g < 255 ? (byte)loadedColors.colors[arrayPlace].g : byte.MaxValue,
            loadedColors.colors[arrayPlace].b < 255 ? (byte)loadedColors.colors[arrayPlace].b : byte.MaxValue));
    }

    public ColorImport TryReadCustomColorPreset(string path)
    {
        if (!File.Exists(path)) WriteEntireJsonFile();

        var sr = new StreamReader(path);
        var customColorPreset = JsonConvert.DeserializeObject<ColorImport>(sr.ReadToEnd());
        sr.Close();
        if (customColorPreset == null) return null;

        return customColorPreset;
    }

    public void LoadFile()
    {
        if (dlg.CheckFileExists && dlg.FileName.Length != 0)
            try
            {
                var streamReader = new StreamReader(dlg.OpenFile());
                var playerDataOneLoad = JsonConvert.DeserializeObject<PlayerData>(streamReader.ReadToEnd());
                streamReader.Close();
                playerData = playerDataOneLoad;
                SliderValue(selectedUser, leftOrRight);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
    }

    public PlayerData ChangeFileId(int user, int i)
    {
        if (playerData == null) return null;

        var col = new BeatSaber.Color
        {
            r = RedSlider.Value / 255.0,
            g = GreenSlider.Value / 255.0,
            b = BlueSlider.Value / 255.0,
            a = AlphaSlider.Value / 255.0
        };
        if (i == 1)
            playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].saberAColor = col;
        else if (i == 2)
            playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].saberBColor = col;
        else if (i == 3)
            playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].environmentColor0 = col;
        else if (i == 4)
            playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].environmentColor1 = col;
        else if (i == 5) 
            playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1].obstaclesColor = col;

        return playerData;
    }

    public void SaveFile(PlayerData playerData)
    {
        if (playerData != null)
        {
            Stream stream = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
            var result = JsonConvert.SerializeObject(playerData);
            var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(result);
            streamWriter.Close();
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        for (var i = 0; i < overloadCount; i++)
        {
            RedSlider.Maximum += 255.0;
            GreenSlider.Maximum += 255.0;
            BlueSlider.Maximum += 255.0;
            AlphaSlider.Maximum += 255.0;
        }
    }

    private void SliderValue(int user, int i)
    {
        if (playerData == null) return;

        var colorData = playerData.localPlayers[0].colorSchemesSettings.colorSchemes[user - 1];
        if (i == 1)
        {
            if (colorData.saberAColor.r * 255.0 > 255.0 || colorData.saberAColor.b * 255.0 > 255.0 ||
                colorData.saberAColor.g * 255.0 > 255.0 || colorData.saberAColor.a * 255.0 > 255.0)
                SetMaximum();
            else
                SetMinimum();

            RedSlider.Value = Math.Round(colorData.saberAColor.r * 255.0);
            GreenSlider.Value = Math.Round(colorData.saberAColor.g * 255.0);
            BlueSlider.Value = Math.Round(colorData.saberAColor.b * 255.0);
            AlphaSlider.Value = Math.Round(colorData.saberAColor.a * 255.0);
            return;
        }

        if (i == 2)
        {
            if (colorData.saberBColor.r * 255.0 > 255.0 || colorData.saberBColor.b * 255.0 > 255.0 ||
                colorData.saberBColor.g * 255.0 > 255.0 || colorData.saberBColor.a * 255.0 > 255.0)
            {
                RedSlider.Maximum = 510.0;
                GreenSlider.Maximum = 510.0;
                BlueSlider.Maximum = 510.0;
                AlphaSlider.Maximum = 510.0;
            }
            else
            {
                SetMinimum();
            }

            RedSlider.Value = Math.Round(colorData.saberBColor.r * 255.0);
            GreenSlider.Value = Math.Round(colorData.saberBColor.g * 255.0);
            BlueSlider.Value = Math.Round(colorData.saberBColor.b * 255.0);
            AlphaSlider.Value = Math.Round(colorData.saberBColor.a * 255.0);
            return;
        }

        if (i == 3)
        {
            if (colorData.environmentColor0.r * 255.0 > 255.0 || colorData.environmentColor0.b * 255.0 > 255.0 ||
                colorData.environmentColor0.g * 255.0 > 255.0 || colorData.environmentColor0.a * 255.0 > 255.0)
                SetMaximum();
            else
                SetMinimum();

            RedSlider.Value = Math.Round(colorData.environmentColor0.r * 255.0);
            GreenSlider.Value = Math.Round(colorData.environmentColor0.g * 255.0);
            BlueSlider.Value = Math.Round(colorData.environmentColor0.b * 255.0);
            AlphaSlider.Value = Math.Round(colorData.environmentColor0.a * 255.0);
            return;
        }

        if (i == 4)
        {
            if (colorData.environmentColor1.r * 255.0 > 255.0 || colorData.environmentColor1.b * 255.0 > 255.0 ||
                colorData.environmentColor1.g * 255.0 > 255.0 || colorData.environmentColor1.a * 255.0 > 255.0)
                SetMaximum();
            else
                SetMinimum();

            RedSlider.Value = Math.Round(colorData.environmentColor1.r * 255.0);
            GreenSlider.Value = Math.Round(colorData.environmentColor1.g * 255.0);
            BlueSlider.Value = Math.Round(colorData.environmentColor1.b * 255.0);
            AlphaSlider.Value = Math.Round(colorData.environmentColor1.a * 255.0);
            return;
        }

        if (i == 5)
        {
            if (colorData.obstaclesColor.r * 255.0 > 255.0 || colorData.obstaclesColor.b * 255.0 > 255.0 ||
                colorData.obstaclesColor.g * 255.0 > 255.0 || colorData.obstaclesColor.a * 255.0 > 255.0)
                SetMaximum();
            else
                SetMinimum();

            RedSlider.Value = Math.Round(colorData.obstaclesColor.r * 255.0);
            GreenSlider.Value = Math.Round(colorData.obstaclesColor.g * 255.0);
            BlueSlider.Value = Math.Round(colorData.obstaclesColor.b * 255.0);
            AlphaSlider.Value = Math.Round(colorData.obstaclesColor.a * 255.0);
        }
    }

    private void SetMinimum()
    {
        RedSlider.Maximum = 255.0;
        GreenSlider.Maximum = 255.0;
        BlueSlider.Maximum = 255.0;
        AlphaSlider.Maximum = 255.0;
    }

    private void SetMaximum()
    {
        RedSlider.Maximum = 510.0;
        GreenSlider.Maximum = 510.0;
        BlueSlider.Maximum = 510.0;
        AlphaSlider.Maximum = 510.0;
    }

    private void SelectFile(object sender, RoutedEventArgs e)
    {
        dlg.ShowDialog();
        dlg.Multiselect = false;
        LoadFile();
    }

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        RedSlider.Maximum = 255.0;
        GreenSlider.Maximum = 255.0;
        BlueSlider.Maximum = 255.0;
        AlphaSlider.Maximum = 255.0;
        if (AlphaSlider.Value > 255.0) AlphaSlider.Value = 255.0;

        if (RedSlider.Value > 255.0) RedSlider.Value = 255.0;

        if (GreenSlider.Value > 255.0) GreenSlider.Value = 255.0;

        if (BlueSlider.Value > 255.0) BlueSlider.Value = 255.0;
    }

    private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
    }

    private void SaveToUserClick(object sender, RoutedEventArgs e)
    {
        var playerData = ChangeFileId(selectedUser, leftOrRight);
        if (playerData == null)
        {
            MessageBox.Show("Please select a valid file first", "Invalid File", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        SaveFile(playerData);
        MessageBox.Show("Saved to user " + selectedUser, "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Button_Click_4(object sender, RoutedEventArgs e)
    {
    }

    private void Color_Click(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        SetBackgroundColors();
        b.Background = new SolidColorBrush(Color.FromRgb(86, 255, 255));
        b.Foreground = new SolidColorBrush(Color.FromRgb(39, 34, 34));
        for (var i = 1; i <= 4; i++)
            if (b.Name.Contains(i.ToString()))
            {
                selectedUser = i;
                SliderValue(selectedUser, leftOrRight);
            }
    }

    private void Click_ColorLeftOrRight(object sender, RoutedEventArgs e)
    {
        var b = (Button)sender;
        SetBackground();
        b.Background = new SolidColorBrush(Color.FromRgb(86, 255, 255));
        b.Foreground = new SolidColorBrush(Color.FromRgb(39, 34, 34));
        if (b.Name.Contains("Left"))
            leftOrRight = 1;
        else if (b.Name.Contains("Right"))
            leftOrRight = 2;
        else if (b.Name.Contains("EnvironmentOuter"))
            leftOrRight = 3;
        else if (b.Name.Contains("EnvironmentInner"))
            leftOrRight = 4;
        else if (b.Name.Contains("Wall")) leftOrRight = 5;

        SliderValue(selectedUser, leftOrRight);
    }

    public void SetBackground()
    {
        LeftSaber.Background = BACKGROUND_COLOR;
        RightSaber.Background = BACKGROUND_COLOR;
        EnvironmentInner.Background = BACKGROUND_COLOR;
        EnvironmentOuter.Background = BACKGROUND_COLOR;
        Wall.Background = BACKGROUND_COLOR;
        LeftSaber.Foreground = Brushes.White;
        RightSaber.Foreground = Brushes.White;
        EnvironmentInner.Foreground = Brushes.White;
        EnvironmentOuter.Foreground = Brushes.White;
        Wall.Foreground = Brushes.White;
    }

    public void SetBackgroundColors()
    {
        Color1.Background = BACKGROUND_COLOR;
        Color2.Background = BACKGROUND_COLOR;
        Color3.Background = BACKGROUND_COLOR;
        Color4.Background = BACKGROUND_COLOR;
        Color1.Foreground = Brushes.White;
        Color2.Foreground = Brushes.White;
        Color3.Foreground = Brushes.White;
        Color4.Foreground = Brushes.White;
    }

    private void ClearAllCampaigns(object sender, RoutedEventArgs e)
    {
        if (playerData == null)
        {
            MessageBox.Show("Please select a valid file first!");
            return;
        }

        for (var i = 0; i < playerData.localPlayers[0].missionsStatsData.Count; i++)
            playerData.localPlayers[0].missionsStatsData[i].cleared = true;
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
        Popup popup = new Popup();
        popup.Show();
    }


    private void ColorPreviewChange()
    {
        var redSliderValue = (int)RedSlider.Value;
        var greenSliderValue = (int)GreenSlider.Value;
        var blueSliderValue = (int)BlueSlider.Value;
        if (redSliderValue > 255) redSliderValue = 255;

        if (greenSliderValue > 255) greenSliderValue = 255;

        if (blueSliderValue > 255) blueSliderValue = 255;

        ColorPreview.Background = new SolidColorBrush(Color.FromRgb((byte)redSliderValue,
            (byte)greenSliderValue, (byte)blueSliderValue));
    }

    private void OverloadCountBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var text = OverloadCountBox.Text;
        overloadCount = int.TryParse(text, out overloadCount) ? overloadCount : 1;
    }

    private void CheckHex(object sender, TextChangedEventArgs e)
    {
        var text = HexadecimalBox.Text.ToUpper();
        if (text.Length >= 2) RedSlider.Value = HexToInt(text, 0, 1);

        if (text.Length >= 4) GreenSlider.Value = HexToInt(text, 2, 3);

        if (text.Length >= 6) BlueSlider.Value = HexToInt(text, 4, 5);

        if (text.Length == 8) AlphaSlider.Value = HexToInt(text, 6, 7);
    }

    public int HexToInt(string text, int index1, int index2)
    {
        var text2places = string.Empty;
        text2places = text[index1].ToString().ToUpper() + text[index2].ToString().ToUpper();
        for (var i = 0; i < text2places.Length; i++)
            if (!char.IsDigit(text2places[i]) && text2places[i] != 'F' && text2places[i] != 'E' &&
                text2places[i] != 'D' && text2places[i] != 'C' && text2places[i] != 'B' && text2places[i] != 'A')
                return 0;

        return Convert.ToInt32(text2places, 16);
    }

    private void RandomColor(object sender, RoutedEventArgs e)
    {
        var randoInt = new Random();
        RedSlider.Value = randoInt.Next(0, 255);
        GreenSlider.Value = randoInt.Next(0, 255);
        BlueSlider.Value = randoInt.Next(0, 255);
    }

    private void SerColorFromPreset(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var background = button.Background;
        int redBrush = ((SolidColorBrush)background).Color.R;
        int greenBrush = ((SolidColorBrush)background).Color.G;
        int blueBrush = ((SolidColorBrush)background).Color.B;
        if (button.Name.Contains("Overload"))
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
        AlphaSlider.Value = 510.0;
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {

        Close();
    }

    private void MainWindow1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void Minimize(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void PlayTutorial(object sender, RoutedEventArgs e)
    {
        TutorialWindow tutorial = new TutorialWindow();
        tutorial.Show();
    }
}