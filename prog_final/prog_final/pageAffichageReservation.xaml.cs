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
    public sealed partial class pageAffichageReservation : Page
    {
        public pageAffichageReservation()
        {
            this.InitializeComponent();
            SingletonReservation.getInstance().getReservationsUtilisateurs();
            gv_reservation.ItemsSource = SingletonReservation.getInstance().getListe_des_reservations();
        }

        private void gv_reservation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_annuler_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
