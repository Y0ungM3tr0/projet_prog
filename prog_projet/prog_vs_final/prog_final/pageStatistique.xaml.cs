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
using static System.Net.Mime.MediaTypeNames;


namespace prog_final
{
    public sealed partial class pageStatistique : Page
    {
        public pageStatistique()
        {
            this.InitializeComponent();

            SingletonAdherent.getInstance().getAdherentPlusSeance();
            gv_adherent_plus_seance.ItemsSource = SingletonAdherent.getInstance().getListe_adherent_plus_de_seance();

            SingletonAdherent.getInstance().getPrixMoyenPay�ParAdherent();
            gv_adherent_prix_moy_par_activite.ItemsSource = SingletonAdherent.getInstance().getListe_prix_moyen_pay�_par_adherent();

            validation_listes();
        }

        private void validation_listes()
        {
            if (SingletonAdherent.getInstance().getListe_prix_moyen_pay�_par_adherent().Count == 0)
            {
                header_prix_moy_par_activite.Text = "Prix moyen des activite par adherent, (aucune r�servation)";
            }
            else
            {
                header_prix_moy_par_activite.Text = "Prix moyen des activite par adherent:";
            }

            if (SingletonAdherent.getInstance().getListe_adherent_plus_de_seance().Count == 0)
            {
                header_adherent_plus_seance.Text = "L'adherent qui a le plus de s�ances, (aucune r�servation)";
            }
            else
            {
                header_adherent_plus_seance.Text = "L'adherent qui a le plus de s�ances:";
            }
        }

        private void btn_prev_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique3));

        }

        private void btn_next_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique2));
        }

        private void gv_adherent_plus_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gv_adherent_prix_moy_par_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}