using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.CustomAttributes;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
            //string userInfoPattern = @"^Bienvenue,";
            //if (args.SelectedItem != null && (!Regex.IsMatch(args.SelectedItem.ToString(), userInfoPattern)))

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
                    case "pageAjouterActivite":
                        mainFrame.Navigate(typeof(pageAjouterActivite));
                        break;
                    case "pageAjouterCategorie":
                        mainFrame.Navigate(typeof(pageAjouterCategorie));
                        break;
                    case "pageAjouterSeance":
                        mainFrame.Navigate(typeof(pageAjouterSeance));
                        break;
                    case "pageConnexion":
                        connexion_btn_Click();
                        break;
                    case "pageDeconnexion":
                        if (SingletonUtilisateur.getInstance().Connecter == true)
                        {
                            deconnexion_btn_Click();
                        }
                        break;
                    default:
                        break;
                }

            }

        }
        private async void connexion_btn_Click()
        {
            DialogAdmin dialog = new DialogAdmin();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.Title = "Authentification";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (dialog.AnnulerBtn != true)
            {
                
                if (SingletonUtilisateur.getInstance().StatutUtilisateur == false)
                {
                    SingletonAdherent.getInstance().getIdentifiantAdherent(
                        SingletonUtilisateur.getInstance().UsernameAdherent.ToString()
                        );

                    infoUtilisateur.Text = "Bienvenue, \n" + SingletonUtilisateur.getInstance().UsernameAdherent.ToString();
                    SingletonUtilisateur.getInstance().Connecter = true;
                }
                else 
                {
                    SingletonAdmin.getInstance().getIdentifiantAdmin(
                        SingletonUtilisateur.getInstance().UsernameAdmin.ToString()
                        );

                    infoUtilisateur.Text = "Bienvenue, \n" + SingletonUtilisateur.getInstance().UsernameAdmin.ToString();
                    SingletonUtilisateur.getInstance().Connecter = true;
                }
            }
        }
        private async void deconnexion_btn_Click()
        {
            DialogDeconnexion dialog = new DialogDeconnexion();
            dialog.XamlRoot = this.Content.XamlRoot;
            dialog.Title = "Déconnexion";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (dialog.AnnulerBtnDeco != true)
            {
                    SingletonUtilisateur.getInstance().Connecter = false;
                infoUtilisateur.Text = "Déconnecté";
            }
        }


    }
}
