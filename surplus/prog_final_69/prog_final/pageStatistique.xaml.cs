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

            SingletonAdherent.getInstance().getPrixMoyenPayéParAdherent();
            gv_adherent_prix_moy_par_activite.ItemsSource = SingletonAdherent.getInstance().getListe_prix_moyen_payé_par_adherent();
        }

        private void btn_prev_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique3));

        }

        private void btn_next_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique2));
        }

        private void navigation()
        {
            if (App.Current.GetType().Name == "pageStatistique")
            {
                Frame.Navigate(typeof(pageStatistique2));
            }
            else
            {
                Frame.Navigate(typeof(pageStatistique));
            }
        }

        private void gv_adherent_plus_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gv_adherent_prix_moy_par_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
