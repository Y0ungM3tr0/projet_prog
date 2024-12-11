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
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace prog_final
{
    public sealed partial class pageAjouterActivite : Page
    {
        int index = -1, idActivite;
        double cout_organisation, prix_vente;
        string nom, description;

        public pageAjouterActivite()
        {
            this.InitializeComponent();
            SingletonActivite.getInstance().getToutActivite();
            gv_activite.ItemsSource = SingletonActivite.getInstance().getListe_des_activites();


            //ajout des cat�gories
            SingletonCategorie.getInstance().getToutCategories();
            foreach (var cat in SingletonCategorie.getInstance().getListe_des_categories())
            {
                cbx_type.Items.Add(cat.Type);
                cbxModifier_type.Items.Add(cat.Type);
            }
            
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInputAjouter())
            {
                int idCategorie = SingletonCategorie.getInstance().getIdCategories(cbx_type.SelectedValue.ToString());

                SingletonActivite.getInstance().addActivite(
                    tbx_nom_activite.Text,
                    tbx_description.Text,
                    idCategorie,
                    Convert.ToDouble(tbx_cout_organisation.Text),
                    Convert.ToDouble(tbx_prix_vente_client.Text));

                tbx_nom_activite.Text = "";
                tbx_description.Text = "";
                cbx_type.SelectedValue = null;
                tbx_cout_organisation.Text = "";
                tbx_prix_vente_client.Text = "";

                message_reussite.Visibility = Visibility.Visible;
                message_reussite.Text = "Ajout r�ussi (� la fin de la liste)";
            }
        }

        private bool validationInputAjouter()
        {
            message_reussite.Visibility = Visibility.Collapsed;
            bool validation = true;

            string nbrPattern = @"^[0-9]*$|^[0-9]*.[0-9]{2}$";

            if (string.IsNullOrWhiteSpace(tbx_nom_activite.Text))
            {
                nom_activiteErr.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                nom_activiteErr.Text = "";
            }

            if (cbx_type.SelectedValue == null)
            {
                typeErr.Text = "Choisir la cat�gorie";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbx_type.SelectedValue.ToString()))
            {
                typeErr.Text = "Choisir la cat�gorie";
                validation = false;
            }
            else
            {
                typeErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbx_description.Text))
            {
                descriptionErr.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                descriptionErr.Text = "";
            }


            if (string.IsNullOrWhiteSpace(tbx_cout_organisation.Text))
            {
                cout_organisationErr.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbx_cout_organisation.Text, nbrPattern))
            {
                cout_organisationErr.Text = "Doit �tre un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                cout_organisationErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbx_prix_vente_client.Text))
            {
                prix_vente_clientErr.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbx_prix_vente_client.Text, nbrPattern))
            {
                prix_vente_clientErr.Text = "Doit �tre un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                prix_vente_clientErr.Text = "";
            }

            if (!string.IsNullOrWhiteSpace(tbx_cout_organisation.Text) && !string.IsNullOrWhiteSpace(tbx_prix_vente_client.Text))
            {
                if (Regex.IsMatch(tbx_cout_organisation.Text, nbrPattern) && Regex.IsMatch(tbx_prix_vente_client.Text, nbrPattern))
                {
                    if (Convert.ToDouble(tbx_cout_organisation.Text) <= 0)
                    {
                        cout_organisationErr.Text = "Le cout de l'organisation doit �tre plus grand que z�ro.";
                        validation = false;
                    }

                    if (Convert.ToDouble(tbx_prix_vente_client.Text) <= 0)
                    {
                        prix_vente_clientErr.Text = "Le prix de vente doit �tre plus grand que z�ro.";
                        validation = false;
                    }

                    if (Convert.ToDouble(tbx_cout_organisation.Text) >= Convert.ToDouble(tbx_prix_vente_client.Text))
                    {
                        cout_organisationErr.Text = "Le cout de l'organisation doit �tre moins �lev� \n  que le prix de vente.";
                        prix_vente_clientErr.Text = "Le prix de vente doit �tre plus �lev� \n que cout de l'organisation.";
                        validation = false;
                    }
                }
            }


            return validation;
        }

        private void gv_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        // SUPPRIMER
        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            message_reussite.Visibility = Visibility.Collapsed;

            Button button = sender as Button;
            if (button != null)
            {
                gv_activite.SelectedItem = button.DataContext;

                Activite selectedActivite = gv_activite.SelectedItem as Activite;

                if (selectedActivite != null)
                {
                    int idAct = selectedActivite.IdActivite;
                    SingletonActivite.getInstance().supprimerActivite(idAct);
                }
            }
        }

        // MODIFIER
        private void modifier_btn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInputModifier())
            {
                int idCategorie = SingletonCategorie.getInstance().getIdCategories(cbxModifier_type.SelectedValue.ToString());

                SingletonActivite.getInstance().modifierActivite(
                    idActivite,
                    tbxModifier_nom_activite.Text,
                    idCategorie,
                    tbxModifier_description.Text,
                    Convert.ToDouble(tbxModifier_cout_organisation.Text),
                    Convert.ToDouble(tbxModifier_prix_vente_client.Text));

                tbxModifier_nom_activite.Text = "";
                tbxModifier_description.Text = "";
                cbxModifier_type.SelectedValue = null;
                tbxModifier_cout_organisation.Text = "";
                tbxModifier_prix_vente_client.Text = "";

                message_reussite.Visibility = Visibility.Visible;
                message_reussite.Text = "Modification r�ussi";
            } 
        }


        private void btn_modifier(object sender, RoutedEventArgs e)
        {
            message_reussite.Visibility = Visibility.Collapsed;

            Button button = sender as Button;
            if (button != null)
            {
                gv_activite.SelectedItem = button.DataContext;

                Activite selectedActivite = gv_activite.SelectedItem as Activite;

                if (selectedActivite != null)
                {
                    //titre_modifier.Text = "Modifier l'activite: " + selectedActivite.Nom_activite;
                    idActivite = selectedActivite.IdActivite;

                    tbxModifier_nom_activite.Text = selectedActivite.Nom_activite;
                    nom = selectedActivite.Nom_activite;

                    cbxModifier_type.SelectedItem = selectedActivite.TypeCategorie;

                    tbxModifier_description.Text = selectedActivite.Description;
                    description = selectedActivite.Description;

                    tbxModifier_cout_organisation.Text = selectedActivite.Cout_organisation.ToString();
                    cout_organisation = selectedActivite.Cout_organisation;

                    tbxModifier_prix_vente_client.Text = selectedActivite.Prix_vente_client.ToString();
                    prix_vente = selectedActivite.Prix_vente_client;
                }
            }
        }
        private bool validationInputModifier()
        {
            message_reussite.Visibility = Visibility.Collapsed;
            bool validation = true;

            string nbrPattern = @"^[0-9]*$|^[0-9]*.[0-9]{2}$";

            if (string.IsNullOrWhiteSpace(tbxModifier_nom_activite.Text))
            {
                nom_activiteErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                nom_activiteErrModifier.Text = "";
            }

            if (cbxModifier_type.SelectedValue == null)
            {
                typeErrModifier.Text = "Choisir la cat�gorie";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbxModifier_type.SelectedValue.ToString()))
            {
                typeErrModifier.Text = "Choisir la cat�gorie";
                validation = false;
            }
            else
            {
                typeErrModifier.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbxModifier_description.Text))
            {
                descriptionErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                descriptionErr.Text = "";
            }


            if (string.IsNullOrWhiteSpace(tbxModifier_cout_organisation.Text))
            {
                cout_organisationErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbxModifier_cout_organisation.Text, nbrPattern))
            {
                cout_organisationErrModifier.Text = "Doit �tre un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                cout_organisationErrModifier.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbxModifier_prix_vente_client.Text))
            {
                prix_vente_clientErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbxModifier_prix_vente_client.Text, nbrPattern))
            {
                prix_vente_clientErrModifier.Text = "Doit �tre un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                prix_vente_clientErrModifier.Text = "";
            }

            if (!string.IsNullOrWhiteSpace(tbxModifier_cout_organisation.Text) && !string.IsNullOrWhiteSpace(tbxModifier_prix_vente_client.Text))
            {
                if (Regex.IsMatch(tbxModifier_cout_organisation.Text, nbrPattern) && Regex.IsMatch(tbxModifier_prix_vente_client.Text, nbrPattern))
                {
                    if (Convert.ToDouble(tbxModifier_cout_organisation.Text) <= 0)
                    {
                        cout_organisationErrModifier.Text = "Le cout de l'organisation doit �tre plus grand que z�ro.";
                        validation = false;
                    }

                    if (Convert.ToDouble(tbxModifier_prix_vente_client.Text) <= 0)
                    {
                        prix_vente_clientErrModifier.Text = "Le prix de vente doit �tre plus grand que z�ro.";
                        validation = false;
                    }

                    if (Convert.ToDouble(tbxModifier_cout_organisation.Text) >= Convert.ToDouble(tbxModifier_prix_vente_client.Text))
                    {
                        cout_organisationErrModifier.Text = "Le cout de l'organisation doit �tre moins �lev� \n  que le prix de vente.";
                        prix_vente_clientErrModifier.Text = "Le prix de vente doit �tre plus �lev� \n que cout de l'organisation.";
                        validation = false;
                    }
                }
            }

            return validation;
        }
        private bool infoAdherentModifier()
        {
            if (nom.Equals(tbxModifier_nom_activite.Text) && description.Equals(tbxModifier_description.Text) && cout_organisation.Equals(tbxModifier_cout_organisation.Text) && prix_vente.Equals(tbxModifier_prix_vente_client.Text))
            {
                return true; // toutes les valeurs sont pareil
            }
            else
            {
                return false; // un changement dans les valeurs
            }
        }
    }
}