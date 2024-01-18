using System;
using System.Collections.Generic;
using System.Text;

namespace NeuronalesNetzBib
{
    public class SiebenSegmentMuster
    {
        /// <summary>
        /// Konstruktor, der verwendet wird, um eine Reihe von Trainingsmustern zu erhalten, 
        /// die in der Training verwendet werden 
        /// </summary>
        public static List<TrainingsMuster> SiebenSegmentMusterListe { get; } = new List<TrainingsMuster>()
        {
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 0, 1, 1, 0, 0, 0, 0 },
                Zielvektor = new double[] { 0, 0, 0, 1 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 1, 0, 1, 1, 0, 1 },
                Zielvektor = new double[] { 0, 0, 1, 0 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 1, 1, 1, 0, 0, 1 },
                Zielvektor = new double[] { 0, 0, 1, 1 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 0, 1, 1, 0, 1, 1, 0 },
                Zielvektor = new double[] { 0, 1, 0, 0 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 0, 1, 1, 0, 1, 1 },
                Zielvektor = new double[] { 0, 1, 0, 1 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 0, 1, 1, 1, 1, 1 },
                Zielvektor = new double[] { 0, 1, 1, 0 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 1, 1, 0, 0, 0, 0 },
                Zielvektor = new double[] { 0, 1, 1, 1 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 1, 1, 1, 1, 1, 1 },
                Zielvektor = new double[] { 1, 0, 0, 0 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 1, 1, 1, 0, 1, 1 },
                Zielvektor = new double[] { 1, 0, 0, 1 }
            },
            new TrainingsMuster()
            {
                Eingabevektor = new double[] { 1, 1, 1, 1, 1, 1, 0 },
                Zielvektor = new double[] { 1, 0, 1, 0 }
            },
        };
    }
}
