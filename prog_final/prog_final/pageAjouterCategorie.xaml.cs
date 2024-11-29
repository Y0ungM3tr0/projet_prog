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
        public pageAjouterCategorie()
        {
            this.InitializeComponent();
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
    }
}
