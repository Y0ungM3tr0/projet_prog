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
    public sealed partial class pageAffichageAppréciation : Page
    {
        public pageAffichageAppréciation()
        {
            this.InitializeComponent();
            SingletonAppreciation.getInstance().getToutAppreciation();
            gv_appreciation.ItemsSource = SingletonAppreciation.getInstance().getListe_des_appreciations();
        }

        private void gv_appreciation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
