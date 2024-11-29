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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace prog_final
{
    public sealed partial class pageAjouterActivite : Page
    {
        public pageAjouterActivite()
        {
            this.InitializeComponent();


            //ajout des catégories
            SingletonCategorie.getInstance().getToutCategories();
            foreach (var cat in SingletonCategorie.getInstance().getListe_des_categories())
            {
                cbx_type.Items.Add(cat.Type);
            }
            
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInput())
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
            }
        }

        private bool validationInput()
        {
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
                typeErr.Text = "Choisir la catégorie";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbx_type.SelectedValue.ToString()))
            {
                typeErr.Text = "Choisir la catégorie";
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
                cout_organisationErr.Text = "Doit être un nombre positif, dans le format 0 ou 0.00";
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
                prix_vente_clientErr.Text = "Doit être un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                prix_vente_clientErr.Text = "";
            }

            return validation;
        }
    }
}
