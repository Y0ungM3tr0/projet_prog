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
    public sealed partial class pageAffichageActivité : Page
    {
        int index = -1;
        public pageAffichageActivité()
        {
            this.InitializeComponent();
            SingletonActivite.getInstance().getToutActivite();
            gv_activite.ItemsSource = SingletonActivite.getInstance().getListe_des_activites(); ;
        }

        private void gv_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_inscription(object sender, RoutedEventArgs e)
        {

        }
    }
}
