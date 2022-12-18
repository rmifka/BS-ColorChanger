using System.Collections.Generic;

namespace ColorChanger.Code
{
    public class Presaves
    {
        public SaveColorscheme[] colorschemesArr { get; set; }

    }
    public class SaveColorscheme
    {
        public string colorSchemeId { get; set; }
        public Color saberAColor { get; set; }
        public Color saberBColor { get; set; }
        public Color environmentColor0 { get; set; }
        public Color environmentColor1 { get; set; }
        public Color obstaclesColor { get; set; }
        //Post 1.23
        public Color environmentColor0Boost { get; set; }
        public Color environmentColor1Boost { get; set; }
        public SaveColorscheme(string colorSchemeId, Color saberAColor, Color saberBColor, Color environmentColor0, Color environmentColor1, Color obstaclesColor, Color environmentColor0Boost, Color environmentColor1Boost)
        {
            this.colorSchemeId = colorSchemeId;
            this.saberAColor = saberAColor;
            this.saberBColor = saberBColor;
            this.environmentColor0 = environmentColor0;
            this.environmentColor1 = environmentColor1;
            this.obstaclesColor = obstaclesColor;
            this.environmentColor0Boost = environmentColor0Boost;
            this.environmentColor1Boost = environmentColor1Boost;
        }
    }
    public class Color
    {
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
        public int a { get; set; }

        public Color(int r, int g, int b, int a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}

