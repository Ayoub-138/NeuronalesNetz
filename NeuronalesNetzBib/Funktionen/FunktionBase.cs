using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    /// <summary>
    /// Basisklasse für alle Funktionen. Nur die "abstract" members müssen dann noch implementiert werden.
    /// </summary>
    public abstract class FunktionBase : IFunktion, CKI.Interfaces.IFunktion
    {
        public void AktiverungsFunktion(Neuron n)
        {
            n.Aktivierung = BerechneWert(n.Nettoinput);
        }

        public void AusgabeFunktion(Neuron n)
        {
            n.Ausgabe = BerechneWert(n.Aktivierung);
        }

        public abstract double BerechneAbleitungswert(double x);

        public abstract double BerechneWert(double x);

        double CKI.Interfaces.IFunktion.BerechneFunktionswert(double x)
        {
            // ANMERKUNG: Da Ihr Interface ja auch "BerechneWert" definiert,
            // muss das nicht doppelt implementiert werden. BerechneFunktionswert aus CKI greift dann einfach auf BerechneWert zu.
            return BerechneWert(x);
        }
    }
}
