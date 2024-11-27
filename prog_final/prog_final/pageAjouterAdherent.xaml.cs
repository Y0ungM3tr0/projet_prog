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
                SingletonAdherent.getInstance().addAdherent(tbx_nom.Text, tbx_prenom.Text, tbx_adresse.Text, tbx_date_naissance.Text);
                nomErr.Text = "";
                prenomErr.Text = "";
                adresseErr.Text = "";
                date_naissanceErr.Text = "";
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


            if (string.IsNullOrWhiteSpace(tbx_date_naissance.Text))
            {
                date_naissanceErr.Text = "Remplir ce champs";
                validation = false;
            }
            /*
            else if (!Regex.IsMatch(tbx_date_naissance.Text, datePattern))
            {
                date_naissanceErr.Text = "Format ou date invalide (mm-dd-yyyy)";
                validation = false;
            }*/

            return validation;
        }




        private void exporterAdherentbtn_Click(object sender, RoutedEventArgs e)
        {
            //SingletonAdherent.getInstance().ajoutCSV();
        }
        private void importerAdherentbtn_Click(object sender, RoutedEventArgs e)
        {
            //SingletonAdherent.getInstance().ajoutCSV();
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