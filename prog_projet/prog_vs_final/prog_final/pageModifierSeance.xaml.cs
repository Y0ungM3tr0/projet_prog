using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Text.RegularExpressions;


namespace prog_final
{
    public sealed partial class pageModifierSeance : Page
    {
        public pageModifierSeance()
        {
            this.InitializeComponent();

            SingletonSeance.getInstance().getToutSeances();
            gv_seance.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();

            //ajout des activites
            SingletonActivite.getInstance().getToutActivite();
            foreach (var activite in SingletonActivite.getInstance().getListe_des_activites())
            {
                cbx_Activite.Items.Add(activite.Nom_activite);
            }
        }

        private void gv_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_seance.SelectedItem = button.DataContext;

                Seance selectedSeance = gv_seance.SelectedItem as Seance;

                if (selectedSeance != null)
                {
                    int idSeance = selectedSeance.IdSeance;
                    SingletonSeance.getInstance().supprimerSeance(idSeance);
                    message_reussite.Visibility = Visibility.Visible;
                }
            }
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            message_reussite.Visibility = Visibility.Collapsed;
            btn_modifier_BD.Visibility = Visibility.Visible;

            Button button = sender as Button;
            if (button != null)
            {
                gv_seance.SelectedItem = button.DataContext;

                Seance selectedSeance = gv_seance.SelectedItem as Seance;

                if (selectedSeance != null)
                {
                    cbx_Activite.SelectedItem = selectedSeance.NomActivite;
                    tbx_heure.Text = selectedSeance.Heure;
                    datepicker_date_seance.SelectedDate = selectedSeance.Date_seance;
                    tbx_nbrPlaceDispo.Text = selectedSeance.Nbr_place_disponible.ToString();
                }
            }
        }

        private void btn_modifier_BD_Click(object sender, RoutedEventArgs e)
        {
            if (validationInputModifier())
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

                SingletonSeance.getInstance().getToutSeances();
                gv_seance.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();
                message_reussite.Visibility = Visibility.Visible;

                btn_modifier_BD.Visibility = Visibility.Collapsed;
            }
        }
        private bool validationInputModifier()
        {
            message_reussite.Visibility = Visibility.Collapsed;
            bool validation = true;

            string nbrPattern = @"^[0-9]*$";
            string heurePattern = @"^[0-2][0-9]:[0-5][0-9]*$";

            if (cbx_Activite.SelectedValue == null)
            {
                cbx_ActiviteErr.Text = "Choisir l'activité";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbx_Activite.SelectedValue.ToString()))
            {
                cbx_ActiviteErr.Text = "Choisir l'activité";
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
                tbx_heureErr.Text = "Doit être une heure dans le format 00:00";
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
                tbx_nbrPlaceDispoErr.Text = "Doit être un nombre positif";
                validation = false;
            }
            else if (Convert.ToDouble(tbx_nbrPlaceDispo.Text) <= 0)
            {
                tbx_nbrPlaceDispoErr.Text = "Le nombre de place doit être plus grand que zéro";
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
