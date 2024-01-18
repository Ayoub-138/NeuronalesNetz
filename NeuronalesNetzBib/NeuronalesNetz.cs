using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CKI.Interfaces;
using NeuronalesNetzBib.Funktionen;

namespace NeuronalesNetzBib
{
    public class NeuronalesNetz : IBackpropagationNetz
    {
        /// <summary>
        /// verwendet, um bei der ersten Initialisierung der Matrix random Gewichtswerte zu erzeugen
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// eine Liste von Schichten von Neuronen darstellt
        /// </summary>
        public List<List<Neuron>> Netz = new List<List<Neuron>>();
        /// <summary>
        /// Matrix, die die Gewichtswerte für das neuronale Netz enthält
        /// </summary>
        public IGewichtsmatrix Matrix { get; set; }
        /// <summary>
        /// Matrix, die die Deltawerte für Fehler enthält
        /// </summary>
        private double[,] weightsDelta;



        /// <summary>
        /// Methode, die die Anzahl der Schichten und die Anzahl der Neuronen in jeder Schicht festlegt. 
        /// Anschließend wird eine Gewichtsmatrix erstellt und initialisiert
        /// </summary>
        /// <param name="eingabeSchicht"></param>
        /// <param name="zwischenSchicht"></param>
        /// <param name="ausgabeSchicht"></param>
        public void SetzeAnzahlNeuronenUndMatrix(int eingabeSchicht, int[] zwischenSchicht, int ausgabeSchicht)
        {
            int index = 0;
            int totalNumberOfLayers;
            totalNumberOfLayers = 2 + zwischenSchicht.Length;

            
            NeuronalesNetzBib.Funktionen.IFunktion aktivF = new LogistischeFunktion();
            NeuronalesNetzBib.Funktionen.IFunktion ausgabeF = new LinearBisSaettigung();

            //adding all layers
            for (int i = 0; i < totalNumberOfLayers; i++)
            {
                Netz.Add(new List<Neuron>());
            }

            //adding neurons eingabe
            for (int i = 0; i < eingabeSchicht; i++)
            {
                Netz[0].Add(new Neuron(index, aktivF, ausgabeF));
                index++;
            }

            //adding neurons to variable hidden layer
            for (int i = 0; i < zwischenSchicht.Length; i++)
            {
                for (int j = 0; j < zwischenSchicht[i]; j++)
                {
                    Netz[1 + i].Add(new Neuron(index, aktivF, ausgabeF));
                    index++;
                }
            }

            //adding neurons ausgabe
            for (int i = 0; i < ausgabeSchicht; i++)
            {
                Netz[totalNumberOfLayers - 1].Add(new Neuron(index, aktivF, ausgabeF));
                index++;
            }

            Matrix = new Gewichtsmatrix(index, index);
            weightsDelta = new double[index, index];
            InitialisiereMatrix();

        }

        /// <summary>
        /// wird aufgerufen zur Initialisierung von Zufallswerten in der Gewichtsmatrix
        /// </summary>
        public void InitialisiereMatrix()
        {
            
            for (int layer = 0; layer < Netz.Count-1; layer++)
            {
                for (int zeile = Netz[layer][0].Index; zeile <= Netz[layer][Netz[layer].Count()-1].Index; zeile++)
                {
                    for (int spalte = Netz[layer+1][0].Index; spalte <= Netz[layer+1][Netz[layer+1].Count() - 1].Index; spalte++)
                    {
                        Matrix[zeile, spalte] = random.NextDouble() - 0.5d;
                    }
                }
            }
        }

        /// <summary>
        /// Methode ruft preformTraining auf und initialisiert die Matrix bis zu 10 Mal, wenn das Training nicht erfolgreich war.
        /// </summary>
        /// <param name="trainingsmuster"></param>
        /// <param name="lernrate"></param>
        /// <param name="toleranz"></param>
        /// <param name="maxInterationen"></param>
        /// <param name="iterationen"></param>
        /// <returns>
        /// Returns,ob das Training erfolgreich war oder nicht und die Anzahl der erforderlichen Iterationen
        /// </returns>
        public bool Trainieren(List<IDoubleTrainingsmuster> trainingsmuster, double lernrate, double toleranz, int maxInterationen, out int iterationen)
        {
            
            bool success = false;

            int maxMatrixInit = 10;
            int matrixInit = 1;
            iterationen = 0;

            while(!success  && maxMatrixInit > matrixInit)
            {
                iterationen = PerformTraining(trainingsmuster, lernrate, toleranz, maxInterationen, out success);
                matrixInit++;

                if(success)
                {
                    break;
                }

                InitialisiereMatrix();
            }            

            return success;
        }


        /// <summary>
        /// Feedforward Methode.
        /// Berechnet die Ausgabe des Netzes aus einer gegebenen Eingabe
        /// </summary>
        /// <param name="eingabeVektor"></param>
        /// <returns>
        /// Ausgabe des Netzes
        /// </returns>
        public List<double> BerechneAusgabe(double[] eingabeVektor)
        {
            List<double> ausgabeVektor = new List<double>(new double[Netz.Last().Count]);

            //Alle Neuronen Nettoinput auf 0
            foreach (List<Neuron> Layer in Netz)
            {
                foreach (Neuron neuron in Layer)
                {
                    neuron.Nettoinput = 0;
                }
            }

            //eingabe neurone ausgabewert
            for (int i = 0; i < Netz[0].Count; i++)
            {
                Netz[0][i].Ausgabe = eingabeVektor[i];
                Netz[0][i].Aktivierung = eingabeVektor[i];
            }

            
            //feedforward
            for (int layer = 0; layer < Netz.Count - 1; layer++)
            {
                foreach (Neuron recievingNeuron in Netz[layer + 1])
                {
                    foreach (Neuron sendingNeuron in Netz[layer])
                    {
                        recievingNeuron.Nettoinput += sendingNeuron.Aktivierung * Matrix[sendingNeuron.Index, recievingNeuron.Index];
                    }
                    recievingNeuron.BerechneAktivierung();
                    recievingNeuron.BerechneAusgabe();
                }
            }

            //ausgabeneuron ausgabewert
           
            for (int i = 0; i < Netz[Netz.Count - 1].Count(); i++)
            {
                
                ausgabeVektor[i] = Netz[Netz.Count - 1][i].Ausgabe;
            }

            return ausgabeVektor;


        }
        
        /// <summary>
        /// benutzt um Aktivierung- und AusgabeFunktion in Neuronen in jeder Schicht zu setzen
        /// </summary>
        /// <param name="schicht"></param>
        /// <param name="AktivierungsFunktion"></param>
        /// <param name="AusgabeFunktion"></param>
        public void SetzeFunktionenInSchicht(int schicht, CKI.Interfaces.IFunktion AktivierungsFunktion, CKI.Interfaces.IFunktion AusgabeFunktion)
        {
            foreach (Neuron n in Netz[schicht])
            {
                n.SetzeFunktionen(AktivierungsFunktion, AusgabeFunktion);
            }
        }


        /// <summary>
        /// Trainiert das Netz, indem es die Ausgabe für jede Eingabe berechnet und eine Backpropagation durchführt,
        /// um die Gewichte anzupassen, wenn die Ausgabe nicht korrekt ist,
        /// bis entweder die gewünschte Ausgabe erreicht ist oder die maximale Anzahl von Iterationen erreicht ist
        /// </summary>
        /// <param name="trainingsmuster"></param>
        /// <param name="lernrate"></param>
        /// <param name="toleranz"></param>
        /// <param name="maxInterationen"></param>
        /// <param name="success"></param>
        /// <returns>
        /// Anzahhl iterationen
        /// </returns>
        private int PerformTraining(List<IDoubleTrainingsmuster> trainingsmuster, double lernrate, double toleranz, int maxInterationen, out bool success)
        {
            success = false;
            int Ausgabefehler =1;
            int iterationen;

            for (iterationen = 0; iterationen < maxInterationen && Ausgabefehler !=0 ; iterationen++)
            {
                Console.WriteLine("iteration number " + (iterationen + 1));
                Ausgabefehler = Ausgabefehler=0;

                foreach (IDoubleTrainingsmuster doubleTrainingsmuster in trainingsmuster)
                {
                    var ausgabe = BerechneAusgabe(doubleTrainingsmuster.Eingabevektor);
                    bool toBeProp = false;
                    
                        Printing(doubleTrainingsmuster);
                    

                    for (int i = 0; i < doubleTrainingsmuster.Zielvektor.Length; i++)
                    {
                       
                        if (Math.Abs(Netz[Netz.Count() - 1][i].Aktivierung - doubleTrainingsmuster.Zielvektor[i]) > toleranz)
                        {
                            Ausgabefehler ++;
                            toBeProp= true;
                        }

                    }

                    if (toBeProp)
                    {
                        PerformBackpropagation(doubleTrainingsmuster, lernrate);
                    }
                    

                }

                if (Ausgabefehler ==0)
                {
                    success = true;
                }
            }
             
            
            return iterationen;
        }


        /// <summary>
        /// Methode zur Berechnung der Fehler und Anpassung der Gewichte durch Backpropagation
        /// </summary>
        /// <param name="trainingsmuster"></param>
        /// <param name="lernrate"></param>
        private void PerformBackpropagation(IDoubleTrainingsmuster trainingsmuster, double lernrate)
        {
            double[,] gamma = new double[Matrix.Zeilen, Matrix.Spalten];
            double[] error = new double[Netz[Netz.Count - 1].Count()];
            //backprop
           
              for (int layer = Netz.Count() - 1; layer > 0; layer--)
              {

                //backprop Output
                if (layer == Netz.Count() - 1)
                {
                    
                    for (int j = 0; j < Netz[layer].Count(); j++)
                    {   
                        
                        
                        error[j] = Netz[layer][j].Aktivierung - trainingsmuster.Zielvektor[j];
                    }

                    for (int j = 0; j < Netz[layer].Count(); j++)
                    {
                        
                        gamma[layer, j] = error[j] * Netz[layer][j].BerechneAbleitung();
                    }

                    
                    for (int k = 0; k < Netz[layer-1].Count(); k++)
                    {
                        for (int j = 0; j < Netz[layer].Count(); j++)
                        {
                            Neuron sender = Netz[layer - 1][k];
                            Neuron receiver = Netz[layer][j];

                            weightsDelta[sender.Index, receiver.Index] = gamma[layer, j] * sender.Aktivierung;
                            
                            

                        }
                    }
                }

                //backprop Hidden
                else
                {
                    //gamma
                    for (int k = 0; k < Netz[layer].Count(); k++)
                    {
                        gamma[layer, k] = 0;
                        for (int j = 0; j < Netz[layer + 1].Count; j++)
                        {
                            Neuron previousSender = Netz[layer][k];
                            Neuron previousReceiver = Netz[layer+1][j];

                            gamma[layer, k] += gamma[layer + 1, j] * Matrix[previousSender.Index, previousReceiver.Index];
                        }
                        gamma[layer, k] *= Netz[layer][k].BerechneAbleitung();
                    }

                    //delta
                    for (int k = 0; k < Netz[layer-1].Count; k++)
                    {
                        for (int j = 0; j < Netz[layer].Count(); j++)
                        {
                            Neuron sender = Netz[layer - 1][k];
                            Neuron receiver = Netz[layer][j];

                            weightsDelta[sender.Index, receiver.Index] = gamma[layer, j] * sender.Aktivierung;
                           
                        }
                    }
                }

            }

          
            UpdateWeights(weightsDelta, lernrate);

            

        }

        /// <summary>
        /// zeigt die Netzausgabe und die Zielausgabe für eine Musterung auf dem Bildschirm an
        /// </summary>
        /// <param name="doubleTrainingsmuster"></param>
        private void Printing(IDoubleTrainingsmuster doubleTrainingsmuster)
        {
            

            Console.WriteLine("ziel ausgabe:");
            for (int i = 0; i < doubleTrainingsmuster.Zielvektor.Count(); i++)
            {
                Console.WriteLine(doubleTrainingsmuster.Zielvektor[i]);
            }

            Console.WriteLine("tatsachlich ausgabe:");
            for (int i = 0; i < doubleTrainingsmuster.Zielvektor.Count(); i++)
            {
                Console.WriteLine(Netz[Netz.Count() - 1][i].Ausgabe);
            }
        }

        /// <summary>
        /// aktualisiert die Gewichtungsmatrix unter Verwendung der Delta-Werte und der Lernrate
        /// </summary>
        /// <param name="weightsDelta"></param>
        /// <param name="lernrate"></param>
        private void UpdateWeights(double[,] weightsDelta, double lernrate)
        {
            for (int layer = 0; layer < Netz.Count() - 1; layer++)
            {
                for (int zeile = Netz[layer][0].Index; zeile <= Netz[layer][Netz[layer].Count() - 1].Index; zeile++)
                {
                    for (int spalte = Netz[layer + 1][0].Index; spalte <= Netz[layer + 1][Netz[layer + 1].Count() - 1].Index; spalte++)
                    {
                        Matrix[zeile, spalte] -= weightsDelta[zeile, spalte] * lernrate;
                    }
                }
            }



        }


    }
}