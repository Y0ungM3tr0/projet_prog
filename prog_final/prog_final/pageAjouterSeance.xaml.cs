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
    public sealed partial class pageAjouterSeance : Page
    {
        public pageAjouterSeance()
        {
            this.InitializeComponent();

            datepicker_date_seance.MinYear = DateTimeOffset.Now;
            datepicker_date_seance.MaxYear = DateTimeOffset.Now.AddYears(5);

            //ajout des cat�gories
            SingletonActivite.getInstance().getToutActivite();
            foreach (var activite in SingletonActivite.getInstance().getListe_des_activites())
            {
                cbx_Activite.Items.Add(activite.Nom_activite);
            }
        }

        private void add_seance_btn_click(object sender, RoutedEventArgs e)
        {
            if (validationInputAjouter())
            {
                int idActivite = SingletonActivite.getInstance().getIdActivite(cbx_Activite.SelectedValue.ToString());
                DateTime date_seance = datepicker_date_seance.Date.DateTime;

                SingletonSeance.getInstance().addSeance(
                    idActivite,
                    date_seance,
                    tbx_heure.Text,
                    Convert.ToInt32(tbx_nbrPlaceDispo.Text)
                    );

                cbx_Activite.SelectedValue = null;
                tbx_heure.Text = "";
                datepicker_date_seance.SelectedDate = null;
                tbx_nbrPlaceDispo.Text = "";
            }
        }
        private bool validationInputAjouter()
        {
            bool validation = true;

            string nbrPattern = @"^[0-9]*$";
            string heurePattern = @"^[0-2][0-2]:[0-5][0-9]*$";

            if (cbx_Activite.SelectedValue == null)
            {
                cbx_ActiviteErr.Text = "Choisir l'activit�";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbx_Activite.SelectedValue.ToString()))
            {
                cbx_ActiviteErr.Text = "Choisir l'activit�";
                validation = false;
            }
            else
            {
                cbx_ActiviteErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbx_heure.Text))
            {
                tbx_heureErr.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbx_heure.Text, heurePattern))
            {
                tbx_heureErr.Text = "Doit �tre une heure dans le format 00:00";
                validation = false;
            }
            else
            {
                tbx_heureErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(datepicker_date_seance.SelectedDate.ToString()))
            {
                datepicker_date_seanceErr.Text = "Choisir la date de la seance";
                validation = false;
            }
            else
            {
                datepicker_date_seanceErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbx_nbrPlaceDispo.Text))
            {
                tbx_nbrPlaceDispoErr.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbx_nbrPlaceDispo.Text, nbrPattern))
            {
                tbx_nbrPlaceDispoErr.Text = "Doit �tre un nombre positif";
                validation = false;
            }
            else
            {
                tbx_nbrPlaceDispoErr.Text = "";
            }

            return validation;
        }
    }
}
