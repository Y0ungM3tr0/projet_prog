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

                tbx_type.Text = "";
            }
        }

        private bool validationInput()
        {
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

        private void btn_modifier_grid(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_categorie.SelectedItem = button.DataContext;

                Categorie selectedCategorie = gv_categorie.SelectedItem as Categorie;

                if (selectedCategorie != null)
                {
                    titreModifier.Text = "Modifier la categorie: " + selectedCategorie.Type;
                    typeModifier = selectedCategorie.Type;

                    tbxmodifier_type.Text = selectedCategorie.Type;
                }
            }
        }

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
    }
}
