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

            navUtilisateur.Text = "Déconnecté";
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
                    case "pageAffichageReservation":
                        mainFrame.Navigate(typeof(pageAffichageReservation));
                        break;
                    case "pageAjouterAppreciation":
                        mainFrame.Navigate(typeof(pageAjouterAppreciation));
                        break;
                    case "pageAffichageAppréciation":
                        mainFrame.Navigate(typeof(pageAffichageAppréciation));
                        break;
                    case "pageAjouterSeance":
                        mainFrame.Navigate(typeof(pageAjouterSeance));
                        break;
                    case "pageModifierSeance":
                        mainFrame.Navigate(typeof(pageModifierSeance));
                        break;
                    case "pageAffichageAdherent":
                        mainFrame.Navigate(typeof(pageAffichageAdherent));
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
                    case "pageStatistique":
                        mainFrame.Navigate(typeof(pageStatistique));
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
                // false = adherent, true = admin
                if (SingletonUtilisateur.getInstance().StatutUtilisateur == false)
                {
                    SingletonAdherent.getInstance().getIdentifiantAdherent(
                        SingletonUtilisateur.getInstance().UsernameAdherent.ToString()
                        );

                    infoUser.Text = "\n Bienvenue, \n" + SingletonUtilisateur.getInstance().UsernameAdherent.ToString();
                    SingletonUtilisateur.getInstance().Connecter = true;
                }
                else 
                {
                    SingletonAdmin.getInstance().getIdentifiantAdmin(
                        SingletonUtilisateur.getInstance().UsernameAdmin.ToString()
                        );

                    infoUser.Text = "\n Bienvenue, \n administrateur";
                    SingletonUtilisateur.getInstance().Connecter = true;                    
                }
                
                toggleNavigationAdminVisibility();
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
                toggleNavigationAdminVisibility();
                infoUser.Text = "Déconnecté";
            }
        }


        private void toggleNavigationAdminVisibility()
        {
            if (SingletonUtilisateur.getInstance().Connecter)
            {
                if (SingletonUtilisateur.getInstance().StatutUtilisateur)
                {
                    // render visible les page de l'administrateur
                    navUtilisateur.Visibility = Visibility.Visible;
                    navUtilisateur.Text = "Navigation Admin";

                    pageAffichageAdherent.Visibility = Visibility.Visible;
                    pageAjouterActivite.Visibility = Visibility.Visible;
                    pageAjouterSeance.Visibility = Visibility.Visible;
                    pageModifierSeance.Visibility = Visibility.Visible;
                    pageAjouterAdherent.Visibility = Visibility.Visible;
                    pageAjouterCategorie.Visibility = Visibility.Visible;

                    pageStatistique.Visibility = Visibility.Visible;

                    // render non visible les page des adhrents
                    pageAffichageReservation.Visibility = Visibility.Collapsed;
                    pageAjouterAppreciation.Visibility = Visibility.Collapsed;
                    //pageAffichageAppréciation.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // render visible les page des adhrents
                    navUtilisateur.Visibility = Visibility.Visible;
                    navUtilisateur.Text = "Navigation Adhrent";
                    pageAffichageReservation.Visibility = Visibility.Visible;
                    pageAjouterAppreciation.Visibility = Visibility.Visible;
                    //pageAffichageAppréciation.Visibility = Visibility.Visible;

                    // render non visible les page de l'administrateur
                    pageAffichageAdherent.Visibility = Visibility.Collapsed;
                    pageAjouterActivite.Visibility = Visibility.Collapsed;
                    pageAjouterSeance.Visibility = Visibility.Collapsed;
                    pageModifierSeance.Visibility = Visibility.Collapsed;
                    pageAjouterAdherent.Visibility = Visibility.Collapsed;
                    pageAjouterCategorie.Visibility = Visibility.Collapsed;

                    pageStatistique.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                navUtilisateur.Visibility = Visibility.Collapsed;

                // render non visible les page des adhrents
                pageAffichageReservation.Visibility = Visibility.Collapsed;
                pageAjouterAppreciation.Visibility = Visibility.Collapsed;
                //pageAffichageAppréciation.Visibility = Visibility.Collapsed;

                // render non visible les page de l'administrateur
                pageAffichageAdherent.Visibility = Visibility.Collapsed;
                pageAjouterActivite.Visibility = Visibility.Collapsed;
                pageAjouterSeance.Visibility = Visibility.Collapsed;
                pageModifierSeance.Visibility = Visibility.Collapsed;
                pageAjouterAdherent.Visibility = Visibility.Collapsed;
                pageAjouterCategorie.Visibility = Visibility.Collapsed;

                pageStatistique.Visibility = Visibility.Collapsed;
            }

        }



    }
}
