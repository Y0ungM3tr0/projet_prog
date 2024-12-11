using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;


namespace prog_final
{
    public sealed partial class pageStatistique3 : Page
    {
        public pageStatistique3()
        {
            this.InitializeComponent();

            SingletonActivite.getInstance().get_nbr_seance_par_activite();
            gv_moy_note_par_activite.ItemsSource = SingletonActivite.getInstance().getListe_nbr_seance_par_activite();

            SingletonActivite.getInstance().getNb_total_activite();
            gv_nb_activites.ItemsSource = SingletonActivite.getInstance().getListe_nb_activites();

            SingletonAdherent.getInstance().getNb_total_adherent();
            gv_nb_total_adherent.ItemsSource = SingletonAdherent.getInstance().getListe_nb_total_adherent();
        }

        private void btn_prev_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique2));

        }

        private void btn_next_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique));
        }

        private void gv_nb_activites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gv_moy_note_par_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gv_nb_total_adherent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
