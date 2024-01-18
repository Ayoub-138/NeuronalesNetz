using System;
using System.Collections.Generic;
using System.Text;
using CKI.Interfaces;

namespace NeuronalesNetzBib
{   
    
    public class Gewichtsmatrix : IGewichtsmatrix
    {
        /// <summary>
        /// 2D Array, das die Gewichtsmatrix enthält
        /// </summary>
        private double[,] _matrix;

        /// <summary>
        /// Anzahl der Spalten in dem Matrix
        /// </summary>
        public int Spalten { get; set; }

        /// <summary>
        /// Anzahl der Zeilen in dem Matrix
        /// </summary>
        public int Zeilen { get; set; }

        /// <summary>
        /// Konstruktor um ein Gewichtsmatrix mit beliebige grösse zu initializieren
        /// </summary>
        /// <param name="spalten"></param>
        /// <param name="zeilen"></param>
        public Gewichtsmatrix(int spalten, int zeilen)
        {
            
            Spalten = spalten;
            Zeilen = zeilen;
            _matrix = new double[Zeilen, Spalten];
           
        }

        /// <summary>
        /// wird verwendet, um einen bestimmten Wert in der Matrix abzurufen oder diesen Wert zu ändern
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// Werte im matrix zeile x und spalte y
        /// </returns>
        public double this [int x, int y] {
            get
            {
                return _matrix[x, y];
            }
            set
            {
                _matrix[x, y] = value;
            }
        }


       
    }
}
