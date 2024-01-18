using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    public class Tanh : FunktionBase
    {
        public override double BerechneWert(double x)
        {
            return Math.Tanh(x);
        }

        public override double BerechneAbleitungswert(double x)
        {
            double cosinus = Math.Cos(x);
            return 1 / (cosinus * cosinus);
        }
    }
}
