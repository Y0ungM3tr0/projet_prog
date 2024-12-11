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
    public sealed partial class pageAffichageActivité : Page
    {
        int index = -1;
        public pageAffichageActivité()
        {
            this.InitializeComponent();
            SingletonSeance.getInstance().getToutSeances();
            gv_seance.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();
        }

        private void gv_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void btn_inscription_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_seance.SelectedItem = button.DataContext;

                Seance selectedSeance= gv_seance.SelectedItem as Seance;

                if (selectedSeance != null)
                {
                    int idSeance = selectedSeance.IdSeance;
                    int lenght_avant = SingletonReservation.getInstance().getListe_des_reservations().Count;

                    SingletonReservation.getInstance().addReservation(
                        idSeance, 
                        SingletonUtilisateur.getInstance().MatriculeAdherent
                        );


                    SingletonReservation.getInstance().getToutReservations();
                    int lenght_apres = SingletonReservation.getInstance().getListe_des_reservations().Count;

                    if (lenght_avant < lenght_apres)
                    {
                        message_reussite.Visibility = Visibility.Visible;
                        message_erreur.Visibility = Visibility.Collapsed;
                    }
                    if (lenght_avant == lenght_apres)
                    {
                        message_reussite.Visibility = Visibility.Collapsed;
                        message_erreur.Visibility = Visibility.Visible;
                    }

                    SingletonSeance.getInstance().getToutSeances();
                    gv_seance.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();
                }
            }
        }



    }
}