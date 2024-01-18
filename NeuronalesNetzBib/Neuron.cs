using NeuronalesNetzBib.Funktionen;
using CKI.Interfaces;
using System;
namespace NeuronalesNetzBib
{
    public class Neuron
    {
        /// <summary>
        /// 
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// </summary>
        public double Nettoinput { get; set; }
        
        /// <summary>
        /// </summary>
        public double Aktivierung { get; set; }

        /// <summary>
        /// Ausgabewert des Neurons
        /// </summary>
        public double Ausgabe { get; set; }

        /// <summary>
        /// änderbare Aktivierungsfunktion durch Interfaces
        /// </summary>
        NeuronalesNetzBib.Funktionen.IFunktion AktivierungsFunktion { get; set; }

        /// <summary>
        /// änderbare Ausgabefunktion durch Interfaces
        /// </summary>
        NeuronalesNetzBib.Funktionen.IFunktion AusgabeFunktion { get; set; }

        /// <summary>
        /// Konstruktor zum initialisierung des Neurons
        /// </summary>
        /// <param name="index"></param>
        /// <param name="aktivF"></param>
        /// <param name="ausgabeF"></param>
        public Neuron(int index, NeuronalesNetzBib.Funktionen.IFunktion aktivF, NeuronalesNetzBib.Funktionen.IFunktion ausgabeF)
        {
            this.Index = index;
            AktivierungsFunktion = aktivF;
            AusgabeFunktion = ausgabeF;
        }

        /// <summary>
        /// Methode um Funktionen auf die gewünchte Funktionen von Konstruktor zu setzen
        /// </summary>
        /// <param name="aktivF"></param>
        /// <param name="ausgabeF"></param>
        public void SetzeFunktionen(CKI.Interfaces.IFunktion aktivF, CKI.Interfaces.IFunktion ausgabeF)
        {
            AktivierungsFunktion = (NeuronalesNetzBib.Funktionen.IFunktion)aktivF;
            AusgabeFunktion = (NeuronalesNetzBib.Funktionen.IFunktion)ausgabeF;
        }

        /// <summary>
        /// Methode zur Berechnung des Aktivierungswertes
        /// </summary>
        public void BerechneAktivierung()
        {
            AktivierungsFunktion.AktiverungsFunktion(this);
        }

        /// <summary>
        /// Methode zur Berechnung des Ausgabeswertes
        /// </summary>
        public void BerechneAusgabe()
        {
            AusgabeFunktion.AusgabeFunktion(this);
        }

        /// <summary>
        /// Methode zur Berechnung des Ableitungswertes 
        /// </summary>
        /// <returns></returns>
        public double BerechneAbleitung()
        {
            return ((CKI.Interfaces.IFunktion)AktivierungsFunktion).BerechneAbleitungswert(this.Nettoinput);
        }
    }
}
