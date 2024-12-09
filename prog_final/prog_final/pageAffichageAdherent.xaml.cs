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
    public sealed partial class pageAffichageAdherent : Page
    {
        int index = -1;
        string matricule, nom, prenom, adresse, dateNaissance;

        public pageAffichageAdherent()
        {
            this.InitializeComponent();
            SingletonAdherent.getInstance().getToutAdherents();
            gv_adherent.ItemsSource = SingletonAdherent.getInstance().getListe_des_adherents();
        }
        
                private void gv_adherent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_adherent.SelectedItem = button.DataContext;

                Adherent selectedProduit = gv_adherent.SelectedItem as Adherent;

                if (selectedProduit != null)
                {
                    string matricule = selectedProduit.Matricule;

                    SingletonAdherent.getInstance().supprimerAdherent(matricule);
                }
            }
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_adherent.SelectedItem = button.DataContext;

                Adherent selectedProduit = gv_adherent.SelectedItem as Adherent;

                if (selectedProduit != null)
                {
                    titre.Text = "Modifier l'adherent: " + selectedProduit.Matricule;
                    matricule = selectedProduit.Matricule;

                    tbx_nom.Text = selectedProduit.Nom;
                    nom = selectedProduit.Nom;
                    tbx_prenom.Text = selectedProduit.Prenom;
                    prenom = selectedProduit.Prenom;
                    tbx_adresse.Text = selectedProduit.Adresse;
                    adresse = selectedProduit.Adresse;
                    tbx_date_naissance.Text = selectedProduit.Date_naissance.ToString("yyyy-mm-dd");
                    dateNaissance = selectedProduit.Date_naissance.ToString("yyyy-mm-dd");
                }
            }
        }
        private void btn_modifier_bd_Click(object sender, RoutedEventArgs e)
        {
            if (!infoAdherentModifier())
            {
                if (validationInput())
                {
                    SingletonAdherent.getInstance().modifierAdherent(matricule, tbx_nom.Text, tbx_prenom.Text, tbx_adresse.Text, tbx_date_naissance.Text);
                    tbx_nom.Text = "";
                    tbx_prenom.Text = "";
                    tbx_adresse.Text = "";
                    tbx_date_naissance.Text = "";

                    nomErr.Text = "";
                    prenomErr.Text = "";
                    adresseErr.Text = "";
                    date_naissanceErr.Text = "";
                }
            }
            else
            {
                // mettre un message d'erreur qui dit que rien a changé    
            }
            
        }

        private bool infoAdherentModifier()
        {
            if (nom.Equals(tbx_nom.Text) && prenom.Equals(tbx_prenom.Text) && adresse.Equals(tbx_adresse.Text) && dateNaissance.Equals(tbx_date_naissance.Text) )
            {
                return true; // toutes les valeurs sont pareil
            }
            else
            {
                return false; // un changement dans les valeurs
            }
        }
        private bool validationInput()
        {
            bool validation = true;
            //1111 nom complet de votre rue
            string adressePattern = @"^[1-9]{1,5} (?:\s(rue|avenue|boulevard|place|impasse|chemin|allée|quai|voie|cours|route))? [Aa-Zz]*$";
            // mm-dd-yyyy
            string datePattern = @"^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$";

            if (string.IsNullOrWhiteSpace(tbx_nom.Text))
            {
                nomErr.Text = "Remplir ce champs";
                validation = false;
            }

            if (string.IsNullOrWhiteSpace(tbx_prenom.Text))
            {
                prenomErr.Text = "Remplir ce champs";
                validation = false;
            }

            if (string.IsNullOrWhiteSpace(tbx_adresse.Text))
            {
                adresseErr.Text = "Remplir ce champs";
                validation = false;
            }
            /*
            else if(!Regex.IsMatch(tbx_adresse.Text, adressePattern))
            {
                adresseErr.Text = "Veillez avoir ce format: 1111 nom complet de votre rue";
                validation = false;
            }*/


            if (string.IsNullOrWhiteSpace(tbx_date_naissance.Text))
            {
                date_naissanceErr.Text = "Remplir ce champs";
                validation = false;
            }
            /*
            else if (!Regex.IsMatch(tbx_date_naissance.Text, datePattern))
            {
                date_naissanceErr.Text = "Format ou date invalide (mm-dd-yyyy)";
                validation = false;
            }*/

            return validation;
        }

    }
}
