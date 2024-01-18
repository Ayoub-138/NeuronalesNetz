using System;
using System.Collections.Generic;
using System.Text;
using CKI.Interfaces;

namespace NeuronalesNetzBib
{

    public class TrainingsMuster : IDoubleTrainingsmuster
    {

        /// <summary>
        /// Eingabewerte für das neuronale Netz
        /// </summary>
        public double[] Eingabevektor { get; set; }

        /// <summary>
        /// Zielewerte für das neuronale Netz
        /// </summary>
        public double[] Zielvektor { get; set; }

       
    }
}
