using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class HSV
    {
        public int majorVersion { get; set; }
        public int minorVersion { get; set; }
        public int patchVersion { get; set; }
        public bool isDefaultConfig { get; set; }
        public string displayMode { get; set; }
        public bool doIntermediateUpdates { get; set; }
        public int timeDependencyDecimalPrecision { get; set; }
        public int timeDependencyDecimalOffset { get; set; }
        public Judgments[] judgments { get; set; }
        public cutAngleJudgments[] beforeCutAngleJudgments { get; set; }
        public cutAngleJudgments[] acurracyJugdments { get; set; }
        public cutAngleJudgments[] afterCutAngleJudgments { get; set; }
    }

    public class Judgments
    {
        public int threshold { get; set; }
        public string text { get; set; }
        public Color color { get; set; }
        public bool fade { get; set; }

    }
    public class cutAngleJudgments
    {
        public int theshold { get; set; }
        public string text { get; set; }

    }

    public class Color
    {
        public double r { get; set; }
        public double g { get; set; }
        public double b { get; set; }
        public double a { get; set; }
    }
}