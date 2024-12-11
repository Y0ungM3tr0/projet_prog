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
using Windows.Media.Core;

namespace prog_final
{
    public sealed partial class pageStatistique2 : Page
    {
        public pageStatistique2()
        {
            this.InitializeComponent();

            SingletonAppreciation.getInstance().getMoy_note_par_activite();
            gv_moy_note_par_activite.ItemsSource = SingletonAppreciation.getInstance().getListe_moy_note_par_activite();

            SingletonAppreciation.getInstance().getMoy_note_toutes_activite();
            gv_moy_note_toutes_activite.ItemsSource = SingletonAppreciation.getInstance().getListe_moy_toutes_activite();

            SingletonReservation.getInstance().getNbr_participant_par_activite();
            gv_nbr_participant_par_activite.ItemsSource = SingletonReservation.getInstance().getListe_nbr_participant_par_activite();
        }

        private void btn_prev_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique));

        }

        private void btn_next_click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(pageStatistique3));
        }

        private void gv_moy_note_par_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gv_moy_note_toutes_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gv_nbr_participant_par_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
