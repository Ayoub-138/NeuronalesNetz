using System;
using CKI.Interfaces;
using NeuronalesNetzBib;
using NeuronalesNetzBib.Funktionen;
using System.Linq;
using System.Collections.Generic;

namespace NeuronalesNetzKonsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialisierung eines Netzwerks
            NeuronalesNetz netz = new NeuronalesNetz { };
           
            //Initialisierung der Trainingsmuster Liste
            List<TrainingsMuster> muster = SiebenSegmentMuster.SiebenSegmentMusterListe;
            List<IDoubleTrainingsmuster> m = new List<IDoubleTrainingsmuster>(new IDoubleTrainingsmuster[muster.Count()]);
            for (int i = 0; i < muster.Count(); i++)
            {
                m[i] = muster[i];
            }

            //Initialisierung der Funktionen
            FunktionBase aktivierung = new LogistischeFunktion ();
            FunktionBase Ausgabe = new SchwellenwertFunktion();

            //zwischen schicht grösse und anzahl neuronen
            int[] zwischen = new int[1];
            zwischen[0] = 15;
            
            //netz grösse setzen
            netz.SetzeAnzahlNeuronenUndMatrix(7, zwischen, 4);
            for (int i = 0; i < netz.Netz.Count(); i++)
            {
                netz.SetzeFunktionenInSchicht(i, aktivierung, Ausgabe);
            }

            //trainieren
            bool endResult = netz.Trainieren(m, 0.4, 0.1, 2000, out int iteration);

            //training Ergebniss
            Console.WriteLine("end result =  " + endResult + " after: " + iteration + " iterations");

        }

    }
}
