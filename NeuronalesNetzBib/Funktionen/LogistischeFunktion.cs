using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    public class LogistischeFunktion : FunktionBase
    {
        public override double BerechneWert(double x)
        {
            return SigmoidAktivierung(x);
        }

        private double SigmoidAktivierung(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        public override double BerechneAbleitungswert(double x)
        {
            return 1 / (1 + Math.Exp(-x)) * (1 - 1 / (1 + Math.Exp(-x)));
        }
    }
}
