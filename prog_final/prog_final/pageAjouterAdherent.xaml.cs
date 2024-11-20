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
    public sealed partial class pageAjouterAdherent : Page
    {
        public pageAjouterAdherent()
        {
            this.InitializeComponent();
        }

        private void addAdherentbtn_Click(object sender, RoutedEventArgs e)
        {
            SingletonAdherent.getInstance().ajoutCSV();
        }
    }
}
