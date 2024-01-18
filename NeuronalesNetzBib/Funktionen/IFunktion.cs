using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib.Funktionen
{
    // ANMERKUNG: Dieses Interface existiert nur zur Verdeutlichung, was das IFunktion-Interface aus CKI machen soll.
    public interface IFunktion
    {
        double BerechneWert(double x);
        void AktiverungsFunktion(Neuron n);
        void AusgabeFunktion(Neuron n);
    }
}
