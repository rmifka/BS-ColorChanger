
namespace CustomColorPreseter
{
    public class ColorImport
    {
        public CustomColorPreset[] colors { get; set; }
    }
    public class CustomColorPreset
    {
        public string name { get; set; }
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
    }
}
