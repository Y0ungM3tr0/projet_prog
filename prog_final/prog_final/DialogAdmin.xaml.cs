using Google.Protobuf.WellKnownTypes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Mysqlx.Notice;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Frame = Microsoft.UI.Xaml.Controls.Frame;


namespace prog_final
{
    public sealed partial class DialogAdmin : ContentDialog
    {
        bool annulerBtn = false;

        public DialogAdmin()
        {
            this.InitializeComponent();
            cbxAdherent.IsChecked = true;
        }

        public bool AnnulerBtn
        {
            get => annulerBtn;
            set
            {
                annulerBtn = value;
            }

        }

        // une fois les inputs validés
        private void connexionbtn(object sender, RoutedEventArgs e)
        {
            if (validationInputsConnexion())
            {
                if (cbxAdmin.IsChecked == true)
                {
                    switch (SingletonAdmin.getInstance().validationAdminLogIn(tbx_userAdmin.Text, pwd_userAdmin.Password))
                    {
                        case -1:
                            // -1 -> rien de bon (pas username dans bd)
                            messageErr_tbx_userAdmin.Text = "Numéro d'identification inexistant.";
                            break;
                        case 0:
                            // 0 -> username bon mais pas mot de passe 
                            messageErr_tbx_userAdmin.Text = "Erreur de mot de passe.";
                            break;
                        case 1:
                            // 1 -> username bon + mot de passe bon -> doit log in
                            this.Hide();


                            SingletonUtilisateur.getInstance().Connecter = true;

                            SingletonUtilisateur.getInstance().StatutUtilisateur = true;
                            SingletonUtilisateur.getInstance().UsernameAdmin = tbx_userAdmin.Text;
                            SingletonUtilisateur.getInstance().Mdp = tbx_userAdmin.Text;
                            // jsp trop quoi faire
                            break;
                        default:
                            messageErr_tbx_userAdmin.Text = "Une erreur s'est produite.";
                            break;
                    }

                }
                else 
                {
                    switch (SingletonAdherent.getInstance().validationAdherentLogIn(tbx_userAdherent.Text))
                    {

                        case false:
                            // false -> num identification = mauvais (pas username dans bd)
                            messageErr_tbx_userAdherent.Text = "Matricule inexistant.";
                            break;
                        case true:
                            // true -> num identification = bon 
                            messageErr_tbx_userAdherent.Text = "";
                            AnnulerBtn = false;
                            this.Hide();


                            SingletonUtilisateur.getInstance().Connecter = true;

                            SingletonUtilisateur.getInstance().StatutUtilisateur = false;
                            SingletonUtilisateur.getInstance().UsernameAdherent = tbx_userAdherent.Text;
                            // jsp trop quoi faire
                            break;
                        default:
                            messageErr_tbx_userAdherent.Text = "Une erreur s'est produite.";
                            break;
                    }
                    
                }    
            }
        }

        // validation des inputs avant de vérifier pour log in
        private bool validationInputsConnexion()
        {
            bool validation = true;

            // checkbox selectionné?
            if (cbxAdherent.IsChecked == false && cbxAdmin.IsChecked == false)
            {
                messageErr_checkbx.Text = "Selectionner un des choix";
                validation = false;
            }

            // adherent
            // matricule input?
            if (cbxAdherent.IsChecked == true && cbxAdmin.IsChecked == false)
            {
                if (string.IsNullOrWhiteSpace(tbx_userAdherent.Text))
                {
                    messageErr_tbx_userAdherent.Text = "Remplir ce champs";
                    validation = false;
                }
            }

            // admin
            // num identification input?
            if (cbxAdherent.IsChecked == false && cbxAdmin.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(tbx_userAdmin.Text))
                {
                    messageErr_tbx_userAdmin.Text = "Remplir tout les champs";
                    validation = false;
                }

                if (string.IsNullOrWhiteSpace(pwd_userAdmin.Password))
                {
                    messageErr_tbx_userAdmin.Text = "Remplir tout les champs";
                    validation = false;
                }
            }

            return validation;
        }




        // fermer dialog
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            AnnulerBtn = true;
            this.Hide();
        }

        // gestion checkbox
        private void gestion_checkbox_admin(object sender, RoutedEventArgs e)
        {
            if (cbxAdmin.IsChecked == true)
            {
                stckpnl_admin.Visibility = Visibility.Visible;
                stckpnl_adherent.Visibility = Visibility.Collapsed;
                messageErr_tbx_userAdherent.Text = "";
                tbx_userAdmin.Text = "";
                pwd_userAdmin.Password = "";
                cbxAdherent.IsChecked = false;
            }
            else
            {
                cbxAdherent.IsChecked = true;
            }
        }
        private void gestion_checkbox_adherent(object sender, RoutedEventArgs e)
        {
            if (cbxAdherent.IsChecked == true)
            {
                stckpnl_adherent.Visibility = Visibility.Visible;
                stckpnl_admin.Visibility = Visibility.Collapsed;
                messageErr_tbx_userAdmin.Text = "";
                tbx_userAdherent.Text = "";
                cbxAdmin.IsChecked = false;
            }
            else
            {
                cbxAdmin.IsChecked = true;
            }
        }


    }
}
