using System;
using System.Collections.Generic;
using System.Windows;

namespace MetodoDeFalsaPosicion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool AreControlsValid()
        {
            if (double.TryParse(TxtIntervaloA.Text, out double a) && !string.IsNullOrEmpty(TxtIntervaloA.Text))
            {
                if (double.TryParse(TxtIntervaloB.Text, out double b) && !string.IsNullOrEmpty(TxtIntervaloB.Text))
                {
                    return true;
                }
            }

            return false;
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if (AreControlsValid())
            {
                double a, b;
                int i;
                string error, err;
                Iteracion iteracionAnterior, iteracion;
                IList<Iteracion> iteraciones;

                error = TxtError.Text;
                iteraciones = new List<Iteracion>();
                iteracionAnterior = new Iteracion();
                a = double.Parse(TxtIntervaloA.Text);
                b = double.Parse(TxtIntervaloB.Text);
                i = 0;

                do
                {
                    iteracion = new Iteracion();
                    iteracion.I = i;

                    if (iteracion.I == 0)
                    {
                        iteracion.A = a;
                        iteracion.B = b;
                        iteracion.Fa = Math.Sin(iteracion.A) + (2 * iteracion.A) - 1;
                        iteracion.Fb = Math.Sin(iteracion.B) + (2 * iteracion.B) - 1;
                        iteracion.Xr = iteracion.B - (iteracion.Fb * (iteracion.A - iteracion.B) / (iteracion.Fa - iteracion.Fb));
                        iteracion.Error = -1;
                    }
                    else
                    {
                        if (iteracionAnterior.FaFxr < 0)
                        {
                            iteracion.A = iteracionAnterior.A;
                            iteracion.B = iteracionAnterior.Xr;
                        }
                        else if (iteracionAnterior.FaFxr > 0)
                        {
                            iteracion.A = iteracionAnterior.Xr;
                            iteracion.B = iteracionAnterior.B;
                        }

                        iteracion.Fa = Math.Sin(iteracion.A) + (2 * iteracion.A) - 1;
                        iteracion.Fb = Math.Sin(iteracion.B) + (2 * iteracion.B) - 1;
                        iteracion.Xr = iteracion.B - (iteracion.Fb * (iteracion.A - iteracion.B) / (iteracion.Fa - iteracion.Fb));
                    }

                    iteracion.Fxr = Math.Sin(iteracion.Xr) + (2 * iteracion.Xr) - 1;
                    iteracion.FaFxr = iteracion.Fa * iteracion.Fxr;
                    iteracion.Error = Math.Abs(100 * (iteracion.Xr - iteracionAnterior.Xr) / iteracion.Xr);

                    iteracionAnterior = iteracion;
                    iteraciones.Add(iteracion);
                    i++;

                } while (iteracion.Error != 0.001 && iteracion.Error != 0);

                DgIteraciones.ItemsSource = iteraciones;
            }
            else
            {
                MessageBox.Show("ALGUNOS VALORES INGRESADOR POR EL USUARIO SON INCORRECTOS", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtIntervaloA.Clear();
            TxtIntervaloB.Clear();
            DgIteraciones.ItemsSource = null;
            TxtIntervaloA.Focus();
        }
    }
}
