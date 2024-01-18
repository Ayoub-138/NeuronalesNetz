using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    public class RechteckFunktion : FunktionBase
    {
        public override double BerechneWert(double x)
        {
            if (x < 0.25)
                return 0;
            else if (x <= 0.75)
                return 1;
            else
                return 0;
        }
        public override double BerechneAbleitungswert(double x)
        {
            if (x == 0.25d || x == 0.75d)
                return double.PositiveInfinity;
            return 0;
        }
    }
}
