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
    public sealed partial class pageModifierSeance : Page
    {
        public pageModifierSeance()
        {
            this.InitializeComponent();

            SingletonSeance.getInstance().getToutSeances();
            gv_seance.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();
        }

        private void gv_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            //SingletonSeance.getInstance().supprimerSeance();
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
