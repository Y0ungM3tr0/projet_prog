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
    public sealed partial class pageAjouterCategorie : Page
    {
        int index = -1;
        int idCategorie;
        string typeModifier;
        public pageAjouterCategorie()
        {
            this.InitializeComponent();
            SingletonCategorie.getInstance().getToutCategories();
            gv_categorie.ItemsSource = SingletonCategorie.getInstance().getListe_des_categories();
        }



        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInput())
            {
                SingletonCategorie.getInstance().addCategorie(tbx_type.Text);

                tbx_type.Text = "";
                message_reussite_ajout.Visibility = Visibility.Visible;
            }
        }

        private bool validationInput()
        {
            message_reussite_ajout.Visibility = Visibility.Collapsed;
            bool validation = true;

            if (string.IsNullOrWhiteSpace(tbx_type.Text))
            {
                typeErr.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                typeErr.Text = "";
            }            

            return validation;
        }

        private void gv_categorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            message_reussite.Visibility = Visibility.Collapsed;
            btn_modifier_grid.Visibility = Visibility.Visible;

            Button button = sender as Button;
            if (button != null)
            {
                gv_categorie.SelectedItem = button.DataContext;

                Categorie selectedCategorie = gv_categorie.SelectedItem as Categorie;

                if (selectedCategorie != null)
                {
                    typeModifier = selectedCategorie.Type;
                    idCategorie = selectedCategorie.IdCategorie;

                    tbxModifier_type.Text = selectedCategorie.Type;
                }
            }
        }

        private void btn_modifier_grid_click(object sender, RoutedEventArgs e)
        {
            if (validationInputModifier() && idCategorie != null)
            {
                SingletonCategorie.getInstance().modifierCategorie(idCategorie, tbxModifier_type.Text);

                tbx_type.Text = "";
                tbxModifier_type.Text = "";
                message_reussite.Visibility = Visibility.Visible;
            }
        }

        private bool validationInputModifier()
        {
            message_reussite.Visibility = Visibility.Collapsed;
            bool validation = true;

            if (string.IsNullOrWhiteSpace(tbxModifier_type.Text))
            {
                typeModifierErr.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                typeModifierErr.Text = "";
            }

            return validation;
        }



        /*
        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_categorie.SelectedItem = button.DataContext;

                Categorie selectedCategorie = gv_categorie.SelectedItem as Categorie;

                if (selectedCategorie != null)
                {
                    int idCategorie = selectedCategorie.IdCategorie;
                    SingletonCategorie.getInstance().supprimerCategorie(idCategorie);
                }
            }
        }
        */
    }
}
