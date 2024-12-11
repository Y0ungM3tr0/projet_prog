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
        }

        private void addAdherentbtn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInput())
            {
                DateTime date_naissance = datepicker_date_naissance.Date.DateTime;
                SingletonAdherent.getInstance().addAdherent(tbx_nom.Text, tbx_prenom.Text, tbx_adresse.Text, date_naissance);
                message_reussite.Visibility = Visibility.Visible;

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
            message_reussite.Visibility = Visibility.Collapsed;
            bool validation = true;

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

            DateTime today = DateTime.Today.AddYears(-18);
            if (string.IsNullOrWhiteSpace(datepicker_date_naissance.SelectedDate.ToString()))
            {
                datepicker_date_naissanceErr.Text = "Choisir la date de naissance";
                validation = false;
            }
            else if (!(datepicker_date_naissance.Date < today))
            {
                datepicker_date_naissanceErr.Text = "La date de naissance doit être il y a au moins 18 ans.";
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
                message_reussite_csv_adhrent.Visibility = Visibility.Visible;
            }
            else
            {
                Console.WriteLine("Aucun fichier sélectionné.");
                message_reussite_csv_adhrent.Visibility = Visibility.Collapsed;
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
                message_reussite_csv_acvitite.Visibility = Visibility.Visible;
            }
            else
            {
                Console.WriteLine("Aucun fichier sélectionné.");
                message_reussite_csv_acvitite.Visibility = Visibility.Collapsed;
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