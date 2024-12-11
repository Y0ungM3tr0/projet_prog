using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Windows.Devices.Power;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace prog_final
{
    public sealed partial class pageAjouterAdherent : Page
    {
        public pageAjouterAdherent()
        {
            this.InitializeComponent();
            datepicker_date_naissance.MinYear = DateTimeOffset.Now.AddYears(-18);
        }

        private void addAdherentbtn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInput())
            {
                DateTime date_naissance = datepicker_date_naissance.Date.DateTime;
                SingletonAdherent.getInstance().addAdherent(tbx_nom.Text, tbx_prenom.Text, tbx_adresse.Text, date_naissance);
                tbx_nom.Text = "";
                tbx_prenom.Text = "";
                tbx_adresse.Text = "";
                datepicker_date_naissance.SelectedDate = null;

                nomErr.Text = "";
                prenomErr.Text = "";
                adresseErr.Text = "";
                datepicker_date_naissanceErr.Text = "";

                titre.Text = "test";
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


            if (string.IsNullOrWhiteSpace(datepicker_date_naissance.SelectedDate.ToString()))
            {
                datepicker_date_naissanceErr.Text = "Choisir la date de la seance";
                validation = false;
            }
            else
            {
                datepicker_date_naissanceErr.Text = "";
            }

            return validation;
        }


        private async void btn_exporter_adherent_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(GestionWindow.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "exportation_liste_adherents";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            if (monFichier != null)
            {
                SingletonAdherent.getInstance().getToutAdherents();
                var listeAdherents = SingletonAdherent.getInstance().getListe_des_adherents();

                StringBuilder csvContent = new StringBuilder();

                foreach (var adherent in listeAdherents)
                {
                    string ligne =
                        adherent.Matricule + ";" +
                        adherent.Nom + ";" +
                        adherent.Prenom + ";" +
                        adherent.Adresse + ";" +
                        adherent.Date_naissance.ToString("yyyy-MM-dd") + ";" +
                        adherent.Age;
                    csvContent.AppendLine(ligne);
                }

                await Windows.Storage.FileIO.WriteTextAsync(monFichier, csvContent.ToString());
            }
            else
            {
                Console.WriteLine("Aucun fichier sélectionné.");
            }
        }

        private async void btn_exporter_acvitite_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(GestionWindow.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "exportation_liste_activites";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            if (monFichier != null)
            {
                SingletonActivite.getInstance().getToutActivite();
                var listeActivites = SingletonActivite.getInstance().getListe_des_activites();

                StringBuilder csvContent = new StringBuilder();

                foreach (var activite in listeActivites)
                {
                    string ligne =
                        activite.IdActivite + ";" +
                        activite.Nom_activite + ";" +
                        activite.IdCategorie + ";" +
                        activite.Description + ";" +
                        activite.Cout_organisation + ";" +
                        activite.Prix_vente_client;
                    csvContent.AppendLine(ligne);
                }

                await Windows.Storage.FileIO.WriteTextAsync(monFichier, csvContent.ToString());
            }
            else
            {
                Console.WriteLine("Aucun fichier sélectionné.");
            }
        }



    }
}



/*
 <Page
    x:Class="prog_final.pageAjouterAdherent"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:prog_final"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid Background="LightGray">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="2*" />
            <ColumnDefinition Width="3*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>

        <Grid Grid.Column="0" BorderBrush="Black" BorderThickness="1,1,0,1">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
            </Grid.RowDefinitions>

            <StackPanel Grid.Row="0">
                <TextBlock 
                    FontSize="40"
                    FontWeight="Light"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Center"
                    >
                    ajouter via un fichier csv
                </TextBlock>

                <Button
                    x:Name="addAdherentbtn"
                    Height="50" Width="350"
                    FontSize="20" FontWeight="Light"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Top"
                    Click="addAdherentbtn_Click"
                    >
                    clicker pour choisir le fichier
                </Button>
            </StackPanel>

            <StackPanel Grid.Row="1">
                <Image 
                    Height="200"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Center"
                    Source="https://i.kym-cdn.com/photos/images/original/002/897/911/6ff"
                    >
                </Image>
            </StackPanel>

            <StackPanel Grid.Row="2">
                <TextBlock 
                    FontSize="40"
                    FontWeight="Light"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Center"
                    >
                    exporter via un fichier csv
                </TextBlock>

                <Button
                    x:Name="exporterAdherentbtn"
                    Height="50" Width="350"
                    FontSize="20" FontWeight="Light"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Top"
                    Click="exporterAdherentbtn_Click"
                    >
                    clicker pour exporter
                </Button>
            </StackPanel>
        </Grid>



        <Grid Grid.Column="1" BorderBrush="Black" BorderThickness="1">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="3*" />
                <ColumnDefinition Width="2*" />
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
                <RowDefinition Height="*" />
            </Grid.RowDefinitions>

            <TextBlock 
                FontSize="40"
                FontWeight="Light"
                HorizontalAlignment="Center"
                VerticalAlignment="top"
                Grid.Row="0" Grid.ColumnSpan="2"
                >
                ajouter un adherent manuellement
            </TextBlock>

            <TextBox
                x:Name="tbxPrix1"
                Header="de:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="nombffffffffffre"
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="prix1err"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>

            <TextBox
                x:Name="tbxPrix2"
                Header="de:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="nombre"
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="prix2err"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>

            <TextBox
                x:Name="tbxPrix3"
                Header="de:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="nombre"
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="prix4err"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </Grid>
    </Grid>
</Page>

 */