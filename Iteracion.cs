using System;
using System.Collections.Generic;
using System.Text;

namespace MetodoDeFalsaPosicion
{
    public class Iteracion
    {
        private double error;

        public int I { get; set; }
        public double A { get; set; }
        public double B { get; set; }
        public double Fa { get; set; }
        public double Fb { get; set; }
        public double Xr { get; set; }
        public double Fxr { get; set; }
        public double FaFxr { get; set; }
        public double Error { get => Math.Round(error, 3); set => error = value; }
    }
}
