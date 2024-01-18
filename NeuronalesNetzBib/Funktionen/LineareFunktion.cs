using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    public class LineareFunktion : FunktionBase
    {
        public override double BerechneWert(double x)
        {
            return x;
        }
        public override double BerechneAbleitungswert(double x)
        {
            return 1;
        }
    }
}
