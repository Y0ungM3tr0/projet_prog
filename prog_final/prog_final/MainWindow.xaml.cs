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
    public sealed partial class MainWindow : Window
    {
     
        public MainWindow()
        {
            this.InitializeComponent();
            GestionWindow.mainWindow = this;
            mainFrame.Navigate(typeof(pageAffichageActivité));
        }

        private void nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var item = (NavigationViewItem)args.SelectedItem;

                switch (item.Name)
                {
                    case "pageAffichageActivité":
                        mainFrame.Navigate(typeof(pageAffichageActivité));
                        break;
                    case "pageAjouterAdherent":
                        mainFrame.Navigate(typeof(pageAjouterAdherent));
                        break;
                    case "pageConnexion":
                        mainFrame.Navigate(typeof(pageConnexion));
                        break;
                    default:
                        break;
                }

            }

        }
    }
}
