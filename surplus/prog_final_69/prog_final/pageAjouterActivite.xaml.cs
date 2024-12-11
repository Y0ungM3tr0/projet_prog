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
using System.Text;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace prog_final
{
    public sealed partial class pageAjouterActivite : Page
    {
        int index = -1, idActivite;
        double cout_organisation, prix_vente;
        string nom, description;

        public pageAjouterActivite()
        {
            this.InitializeComponent();
            SingletonActivite.getInstance().getToutActivite();
            gv_activite.ItemsSource = SingletonActivite.getInstance().getListe_des_activites();


            //ajout des catégories
            SingletonCategorie.getInstance().getToutCategories();
            foreach (var cat in SingletonCategorie.getInstance().getListe_des_categories())
            {
                cbx_type.Items.Add(cat.Type);
                cbxModifier_type.Items.Add(cat.Type);
            }
            
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (validationInputAjouter())
            {
                int idCategorie = SingletonCategorie.getInstance().getIdCategories(cbx_type.SelectedValue.ToString());

                SingletonActivite.getInstance().addActivite(
                    tbx_nom_activite.Text,
                    tbx_description.Text,
                    idCategorie,
                    Convert.ToDouble(tbx_cout_organisation.Text),
                    Convert.ToDouble(tbx_prix_vente_client.Text));

                tbx_nom_activite.Text = "";
                tbx_description.Text = "";
                cbx_type.SelectedValue = null;
                tbx_cout_organisation.Text = "";
                tbx_prix_vente_client.Text = "";
            }
        }

        private bool validationInputAjouter()
        {
            bool validation = true;

            string nbrPattern = @"^[0-9]*$|^[0-9]*.[0-9]{2}$";

            if (string.IsNullOrWhiteSpace(tbx_nom_activite.Text))
            {
                nom_activiteErr.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                nom_activiteErr.Text = "";
            }

            if (cbx_type.SelectedValue == null)
            {
                typeErr.Text = "Choisir la catégorie";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbx_type.SelectedValue.ToString()))
            {
                typeErr.Text = "Choisir la catégorie";
                validation = false;
            }
            else
            {
                typeErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbx_description.Text))
            {
                descriptionErr.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                descriptionErr.Text = "";
            }


            if (string.IsNullOrWhiteSpace(tbx_cout_organisation.Text))
            {
                cout_organisationErr.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbx_cout_organisation.Text, nbrPattern))
            {
                cout_organisationErr.Text = "Doit être un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                cout_organisationErr.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbx_prix_vente_client.Text))
            {
                prix_vente_clientErr.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbx_prix_vente_client.Text, nbrPattern))
            {
                prix_vente_clientErr.Text = "Doit être un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                prix_vente_clientErr.Text = "";
            }

            return validation;
        }

        private void gv_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        // SUPPRIMER
        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_activite.SelectedItem = button.DataContext;

                Activite selectedActivite = gv_activite.SelectedItem as Activite;

                if (selectedActivite != null)
                {
                    int idAct = selectedActivite.IdActivite;
                    SingletonActivite.getInstance().supprimerActivite(idAct);
                }
            }
        }

        // MODIFIER
        private void modifier_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!infoAdherentModifier())
            {
               if (validationInputModifier())
               {
                    int idCategorie = SingletonCategorie.getInstance().getIdCategories(cbxModifier_type.SelectedValue.ToString());
                    //@idActivite, @nomActivite, @idCategorie, @description,  @cout_organisation, @prix_vente_client

                    SingletonActivite.getInstance().modifierActivite(
                        idActivite,
                        tbxModifier_nom_activite.Text,
                        idCategorie,
                        tbxModifier_description.Text,
                        Convert.ToDouble(tbxModifier_cout_organisation.Text),
                        Convert.ToDouble(tbxModifier_prix_vente_client.Text));

                    tbxModifier_nom_activite.Text = "";
                    tbxModifier_description.Text = "";
                    cbxModifier_type.SelectedValue = null;
                    tbxModifier_cout_organisation.Text = "";
                    tbxModifier_prix_vente_client.Text = "";
               } 
            }
            else
            {
                // faire dequoi qui dit que les valeurs sont les memes
            }

        }


        private void btn_modifier(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_activite.SelectedItem = button.DataContext;

                Activite selectedActivite = gv_activite.SelectedItem as Activite;

                if (selectedActivite != null)
                {
                    titre_modifier.Text = "Modifier l'activite: " + selectedActivite.Nom_activite;
                    idActivite = selectedActivite.IdActivite;

                    tbxModifier_nom_activite.Text = selectedActivite.Nom_activite;
                    nom = selectedActivite.Nom_activite;

                    cbxModifier_type.SelectedItem = selectedActivite.TypeCategorie;

                    tbxModifier_description.Text = selectedActivite.Description;
                    description = selectedActivite.Description;

                    tbxModifier_cout_organisation.Text = selectedActivite.Cout_organisation.ToString();
                    cout_organisation = selectedActivite.Cout_organisation;

                    tbxModifier_prix_vente_client.Text = selectedActivite.Prix_vente_client.ToString();
                    prix_vente = selectedActivite.Prix_vente_client;
                }
            }
        }
        private bool validationInputModifier()
        {
            bool validation = true;

            string nbrPattern = @"^[0-9]*$|^[0-9]*.[0-9]{2}$";

            if (string.IsNullOrWhiteSpace(tbxModifier_nom_activite.Text))
            {
                nom_activiteErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                nom_activiteErrModifier.Text = "";
            }

            if (cbxModifier_type.SelectedValue == null)
            {
                typeErrModifier.Text = "Choisir la catégorie";
                validation = false;
            }
            else if (string.IsNullOrWhiteSpace(cbxModifier_type.SelectedValue.ToString()))
            {
                typeErrModifier.Text = "Choisir la catégorie";
                validation = false;
            }
            else
            {
                typeErrModifier.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbxModifier_description.Text))
            {
                descriptionErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else
            {
                descriptionErr.Text = "";
            }


            if (string.IsNullOrWhiteSpace(tbxModifier_cout_organisation.Text))
            {
                cout_organisationErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbxModifier_cout_organisation.Text, nbrPattern))
            {
                cout_organisationErrModifier.Text = "Doit être un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                cout_organisationErrModifier.Text = "";
            }

            if (string.IsNullOrWhiteSpace(tbxModifier_prix_vente_client.Text))
            {
                prix_vente_clientErrModifier.Text = "Remplir ce champs";
                validation = false;
            }
            else if (!Regex.IsMatch(tbxModifier_prix_vente_client.Text, nbrPattern))
            {
                prix_vente_clientErrModifier.Text = "Doit être un nombre positif, dans le format 0 ou 0.00";
                validation = false;
            }
            else
            {
                prix_vente_clientErrModifier.Text = "";
            }

            return validation;
        }
        private bool infoAdherentModifier()
        {
            if (nom.Equals(tbxModifier_nom_activite.Text) && description.Equals(tbxModifier_description.Text) && cout_organisation.Equals(tbxModifier_cout_organisation.Text) && prix_vente.Equals(tbxModifier_prix_vente_client.Text))
            {
                return true; // toutes les valeurs sont pareil
            }
            else
            {
                return false; // un changement dans les valeurs
            }
        }
    }
}


/*
 <?xml version="1.0" encoding="utf-8"?>
<Page
    x:Class="prog_final.pageAjouterActivite"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:prog_final"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

    <Grid Background="LightGray">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="3*"/>
            <ColumnDefinition Width="4*"/>
            <ColumnDefinition Width="3*"/>
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>        
        
        <TextBlock
            Grid.Row="0"
            Grid.Column="0"
            x:Name="titre"
            FontSize="35"
            HorizontalAlignment="Center"
            >
            ajouter une activitée
        </TextBlock>

        <StackPanel 
            Grid.Row="1" Grid.Column="0"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbx_nom_activite"
                Header="nom_activite:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="nom_activiteErr"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >   
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="2" Grid.Column="0"
            HorizontalAlignment="Center"
            >
            <ComboBox
                x:Name="cbx_type"
                Header="Categorie:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                >
            </ComboBox>
            <TextBlock
                x:Name="typeErr"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="3" Grid.Column="0"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbx_description"
                Header="description:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >   
            </TextBox>
            <TextBlock
                x:Name="descriptionErr"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>


        <StackPanel 
            Grid.Row="4" Grid.Column="0"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbx_cout_organisation"
                Header="tbx_cout_organisation:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="cout_organisationErr"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="5" Grid.Column="0"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbx_prix_vente_client"
                Header="tbx_prix_vente_client:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="prix_vente_clientErr"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <Button
            Grid.Row="6"
            Grid.Column="0"
            Width="100" Height="40"
            HorizontalAlignment="Center"
            Click="add_btn_Click"
            >
            ajouter
        </Button>

        <Button 
            Grid.Column="1" Grid.Row="0"
            x:Name="btn_exporter_adherent"
            Margin="5"
            Width="270" Height="55"
            FontSize="17"
            HorizontalAlignment="Center"
            Click="btn_exporter_adherent_Click"
            >
            exporter la liste des activites
        </Button>
        
        <GridView x:Name="gv_activite" SelectionChanged="gv_activite_SelectionChanged" HorizontalAlignment="Center" Grid.Column="1" Grid.Row="1" Grid.RowSpan="6">
            <GridView.ItemTemplate>
                <DataTemplate x:DataType="local:Activite" >
                    <StackPanel x:Name="produitCard" Margin="15" Background="White" CornerRadius="5" Width="450" HorizontalAlignment="Stretch">
                        <Grid Margin="5">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="*" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Grid.RowDefinitions>
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                            </Grid.RowDefinitions>

                            <StackPanel Grid.Column="0" Grid.ColumnSpan="2" Grid.Row="0" HorizontalAlignment="Stretch">
                                <TextBlock Text="{x:Bind Nom_activite}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Description}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind TypeCategorie}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Cout_organisation}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                                <TextBlock Text="{x:Bind Prix_vente_client}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                            </StackPanel>

                            <Button 
                                Click="btn_modifier"
                                HorizontalAlignment="Center"
                                Grid.Column="0"
                                Grid.Row="5"
                                FontSize="19"
                                Width="150" Height="40"
                                >
                                modifier
                            </Button>
                            <Button 
                                Click="btn_Supp_Click"
                                HorizontalAlignment="Center"
                                Grid.Column="1"
                                Grid.Row="5"
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

        <TextBlock
            Grid.Row="0"
            Grid.Column="2"
            x:Name="titre_modifier"
            FontSize="35"
            HorizontalAlignment="Center"
            >
            modifier une activitée
        </TextBlock>

        <StackPanel 
            Grid.Row="1" Grid.Column="2"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbxModifier_nom_activite"
                Header="nom_activite:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >   
            </TextBox>
            <TextBlock
                x:Name="nom_activiteErrModifier"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="2" Grid.Column="2"
            HorizontalAlignment="Center"
            >
            <ComboBox
                x:Name="cbxModifier_type"
                Header="Categorie:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                >
            </ComboBox>
            <TextBlock
                x:Name="typeErrModifier"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="3" Grid.Column="2"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbxModifier_description"
                Header="description:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="descriptionErrModifier"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="4" Grid.Column="2"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbxModifier_cout_organisation"
                Header="tbx_cout_organisation:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="cout_organisationErrModifier"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <StackPanel 
            Grid.Row="5" Grid.Column="2"
            HorizontalAlignment="Center"
            >
            <TextBox
                x:Name="tbxModifier_prix_vente_client"
                Header="tbx_prix_vente_client:"
                Margin="5"
                Width="240" Height="75"
                FontSize="18" 
                PlaceholderText="ex: "
                PlaceholderForeground="DarkGray"
                >
            </TextBox>
            <TextBlock
                x:Name="prix_vente_clientErrModifier"
                FontSize="15"
                HorizontalAlignment="Center"
                VerticalAlignment="Bottom"
                Foreground="DarkRed"
                >
            </TextBlock>
        </StackPanel>

        <Button
            Grid.Row="6"
            Grid.Column="2"
            Width="100" Height="40"
            HorizontalAlignment="Center"
            Click="modifier_btn_Click"
            >
            modifier
        </Button>
    </Grid>
</Page>
 */