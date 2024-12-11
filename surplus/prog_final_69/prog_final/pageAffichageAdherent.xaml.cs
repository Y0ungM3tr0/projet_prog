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
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace prog_final
{
    public sealed partial class pageAffichageAdherent : Page
    {
        int index = -1;
        string matricule, nom, prenom, adresse, dateNaissance;

        public pageAffichageAdherent()
        {
            this.InitializeComponent();
            SingletonAdherent.getInstance().getToutAdherents();
            gv_adherent.ItemsSource = SingletonAdherent.getInstance().getListe_des_adherents();
        }
        
        private void gv_adherent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_adherent.SelectedItem = button.DataContext;

                Adherent selectedProduit = gv_adherent.SelectedItem as Adherent;

                if (selectedProduit != null)
                {
                    string matricule = selectedProduit.Matricule;

                    SingletonAdherent.getInstance().supprimerAdherent(matricule);
                }
            }
        }

        private void btn_modifier_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_adherent.SelectedItem = button.DataContext;

                Adherent selectedProduit = gv_adherent.SelectedItem as Adherent;

                if (selectedProduit != null)
                {
                    titre.Text = "Modifier l'adherent: " + selectedProduit.Matricule;
                    matricule = selectedProduit.Matricule;

                    tbx_nom.Text = selectedProduit.Nom;
                    nom = selectedProduit.Nom;
                    tbx_prenom.Text = selectedProduit.Prenom;
                    prenom = selectedProduit.Prenom;
                    tbx_adresse.Text = selectedProduit.Adresse;
                    adresse = selectedProduit.Adresse;
                    datepicker_date_seance.Date = selectedProduit.Date_naissance;
                    dateNaissance = selectedProduit.Date_naissance.ToString("yyyy-mm-dd");
                }
            }
        }
        private void btn_modifier_bd_Click(object sender, RoutedEventArgs e)
        {
            if (!infoAdherentModifier())
            {
                if (validationInput())
                {
                    DateTime date_seance = datepicker_date_seance.Date.DateTime;
                    SingletonAdherent.getInstance().modifierAdherent(matricule, tbx_nom.Text, tbx_prenom.Text, tbx_adresse.Text, date_seance);

                    titre.Text = "Modifier l'adherent:";

                    tbx_nom.Text = "";
                    tbx_prenom.Text = "";
                    tbx_adresse.Text = "";
                    datepicker_date_seance.SelectedDate = null;

                    nomErr.Text = "";
                    prenomErr.Text = "";
                    adresseErr.Text = "";
                    datepicker_date_seanceErr.Text = "";
                }
            }
            else
            {
                // mettre un message d'erreur qui dit que rien a changé    
            }
            
        }

        private bool infoAdherentModifier()
        {
            if (nom.Equals(tbx_nom.Text) && prenom.Equals(tbx_prenom.Text) && adresse.Equals(tbx_adresse.Text) && dateNaissance.Equals(datepicker_date_seance.SelectedDate) )
            {
                return true; // toutes les valeurs sont pareil
            }
            else
            {
                return false; // un changement dans les valeurs
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


            if (string.IsNullOrWhiteSpace(datepicker_date_seance.SelectedDate.ToString()))
            {
                datepicker_date_seanceErr.Text = "Choisir la date de la seance";
                validation = false;
            }
            else
            {
                datepicker_date_seanceErr.Text = "";
            }
            /*
            else if (!Regex.IsMatch(tbx_date_naissance.Text, datePattern))
            {
                date_naissanceErr.Text = "Format ou date invalide (mm-dd-yyyy)";
                validation = false;
            }*/

            return validation;
        }

    }
}

/*
 <?xml version="1.0" encoding="utf-8"?>
<Page
    x:Class="prog_final.pageAffichageAdherent"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:prog_final"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid Background="LightGray">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>


        <GridView x:Name="gv_adherent" SelectionChanged="gv_adherent_SelectionChanged" HorizontalAlignment="Center">
            <GridView.ItemTemplate>
                <DataTemplate x:DataType="local:Adherent" >
                    <StackPanel x:Name="produitCard" Margin="15" Background="White" CornerRadius="5" Width="650" HorizontalAlignment="Center">
                        <Grid Margin="5">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="4*" />
                                <ColumnDefinition Width="4*" />
                                <ColumnDefinition Width="3*" />
                            </Grid.ColumnDefinitions>
                            <Grid.RowDefinitions>
                                <RowDefinition Height="2*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="2*" />
                            </Grid.RowDefinitions>

                            <StackPanel Grid.Column="0" Grid.Row="0" HorizontalAlignment="Stretch">
                                <TextBlock Text="{x:Bind Matricule}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Nom}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Prenom}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Date_naissance}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Age}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Adresse}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                            </StackPanel>

                            <Button 
                            Click="btn_modifier_Click"
                            HorizontalAlignment="Center"
                            VerticalAlignment="Center"
                            Grid.Column="2"
                            Grid.Row="0"
                            FontSize="19"
                            Width="150" Height="40"
                            >
                                modifier
                            </Button>
                            <Button 
                            Click="btn_Supp_Click"
                            HorizontalAlignment="Center"
                            VerticalAlignment="Center"
                            Grid.Column="2"
                            Grid.Row="1"
                            FontSize="19"
                            Width="150" Height="40"
                            >
                                Supprimer
                            </Button>
                        </Grid>
                    </StackPanel>
                </DataTemplate>
            </GridView.ItemTemplate>
        </GridView>

        <Grid Grid.Column="1">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
                <RowDefinition Height="*"/>
                <RowDefinition Height="*"/>
                <RowDefinition Height="*"/>
                <RowDefinition Height="*"/>
                <RowDefinition Height="*"/>
            </Grid.RowDefinitions>

            <TextBlock 
                x:Name="titre"
                FontSize="40"
                FontWeight="Light"
                HorizontalAlignment="Center"
                Grid.Row="0"
                >
                modifier l'adherent: 
            </TextBlock>

            <StackPanel Grid.Row="1">
                <TextBox
                    x:Name="tbx_nom"
                    Header="nom:"
                    Margin="5"
                    Width="240" Height="75"
                    FontSize="18" 
                    PlaceholderText="votre nom de famille"
                    PlaceholderForeground="DarkGray"
                    >   
                </TextBox>
                <TextBlock
                    x:Name="nomErr"
                    FontSize="15"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Bottom"
                    Foreground="DarkRed"
                    >
                </TextBlock>
            </StackPanel>

            <StackPanel Grid.Row="2">
                <TextBox
                    x:Name="tbx_prenom"
                    Header="prenom:"
                    Margin="5"
                    Width="240" Height="75"
                    FontSize="18" 
                    PlaceholderText="votre premier nom"
                    PlaceholderForeground="DarkGray"
                    >
                </TextBox>
                <TextBlock
                    x:Name="prenomErr"
                    FontSize="15"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Bottom"
                    Foreground="DarkRed"
                    >
                </TextBlock>
            </StackPanel>

            <StackPanel Grid.Row="3">
                <TextBox
                    x:Name="tbx_adresse"
                    Header="adresse:"
                    Margin="5"
                    Width="240" Height="75"
                    FontSize="18" 
                    PlaceholderText="1111 nom complet de votre rue"
                    PlaceholderForeground="DarkGray"
                    >
                </TextBox>
                <TextBlock
                    x:Name="adresseErr"
                    FontSize="15"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Bottom"
                    Foreground="DarkRed"
                    >
                </TextBlock>
            </StackPanel>

            <StackPanel Grid.Row="4">
                <TextBox
                    x:Name="tbx_date_naissance"
                    Header="date de naissance:"
                    Margin="5"
                    Width="240" Height="75"
                    FontSize="18" 
                    PlaceholderText="votre date de naissance"
                    PlaceholderForeground="DarkGray"
                    >
                </TextBox>
                <TextBlock
                    x:Name="date_naissanceErr"
                    FontSize="15"
                    HorizontalAlignment="Center"
                    VerticalAlignment="Bottom"
                    Foreground="DarkRed"
                    >
                </TextBlock>
            </StackPanel>

            <Button 
                Grid.Row="6"
                x:Name="btn_modifier_bd"
                Margin="5"
                Width="240" Height="75"
                FontSize="18"
                HorizontalAlignment="Center"
                Click="btn_modifier_bd_Click"
                >
                modifier l'adherent
            </Button>
        </Grid>





    </Grid>
</Page>

 */