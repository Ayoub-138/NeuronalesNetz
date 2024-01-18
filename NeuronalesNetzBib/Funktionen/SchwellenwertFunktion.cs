using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    public class SchwellenwertFunktion : FunktionBase
    {
        public double Schwelle { get; private set; }

        public SchwellenwertFunktion() { Schwelle = 0.5; }

        public SchwellenwertFunktion(double schwelle) { Schwelle = schwelle; }

        public override double BerechneWert(double x)
        {
            if (x >= Schwelle)
                return 1;
            return 0;
        }

        public override double BerechneAbleitungswert(double x)
        {
            if (x == Schwelle)
                return double.PositiveInfinity;
            return 0;
        }
    }
}
