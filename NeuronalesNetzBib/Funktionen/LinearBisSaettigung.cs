using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    public class LinearBisSaettigung : FunktionBase
    {
        public override double BerechneWert(double x)
        {
            return LinearBisSattigungAktivierung(x);
        }

        private double LinearBisSattigungAktivierung(double x)
        {
            if (x >= -1 || x <= 1)
                return x;
            else if (x > 1)
                return 1;
            else
                return -1;

        }
        public override double BerechneAbleitungswert(double x)
        {
            if (x >= -1 || x <= 1)
                return 1;
            return 0;
        }
    }
}
