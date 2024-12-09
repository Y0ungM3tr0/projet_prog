using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace prog_final
{
    public sealed partial class pageAjouterAppreciation : Page
    {
        int idSeance;
        string matricule;
        double note_appreciation;
        public pageAjouterAppreciation()
        {
            this.InitializeComponent();
            SingletonSeance.getInstance().getSeancesUtilisateur();
            gv_seanceReservation.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();
        }
        private void gv_seanceReservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        private void btn_noter_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_seanceReservation.SelectedItem = button.DataContext;

                Seance selectedSeance = gv_seanceReservation.SelectedItem as Seance;

                if (selectedSeance != null)
                {
                    noterGrid.Visibility = Visibility.Visible;
                    tbx_nomActivite.Text = selectedSeance.NomActivite;
                    tbx_date.Text = selectedSeance.Date_seance_string;

                    idSeance = selectedSeance.IdSeance;
                    matricule = selectedSeance.Matricule;
                }
            }
        }

        private void rating_slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            sliderValue.Text = "Votre appréciation:  " + rating_slider.Value.ToString("0.0");
        }

        private void btn_ajouterAppreciation_click(object sender, RoutedEventArgs e)
        {
            SingletonAppreciation.getInstance().ajouterAppreciation(idSeance, matricule, Math.Round(rating_slider.Value, 2));

            tbx_nomActivite.Text = "";
            tbx_date.Text = "";
        }
    }
}
