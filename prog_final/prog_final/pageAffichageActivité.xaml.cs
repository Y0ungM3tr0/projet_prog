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
using Windows.Media.Core;


namespace prog_final
{
    public sealed partial class pageAffichageActivité : Page
    {
        int index = -1;
        public pageAffichageActivité()
        {
            this.InitializeComponent();
            SingletonSeance.getInstance().getToutSeances();
            gv_seance.ItemsSource = SingletonSeance.getInstance().getListe_des_seances();
        }

        private void gv_seance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void btn_inscription_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                gv_seance.SelectedItem = button.DataContext;

                Seance selectedSeance= gv_seance.SelectedItem as Seance;

                if (selectedSeance != null)
                {
                    int idSeance= selectedSeance.IdActivite;

                    SingletonReservation.getInstance().addReservation(idSeance, 
                        SingletonUtilisateur.getInstance().MatriculeAdherent
                        );
                }
            }
        }

        
    }
}

/*
<Page
    x:Class="prog_final.pageAffichageActivité"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:prog_final"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d"
    Background="{ThemeResource ApplicationPageBackgroundThemeBrush}"
    >

    <Grid Background="LightGray">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>


        <GridView x:Name="gv_activite" SelectionChanged="gv_activite_SelectionChanged" HorizontalAlignment="Center">
            <GridView.ItemTemplate>
                <DataTemplate x:DataType="local:Activite" >
                    <StackPanel x:Name="produitCard" Margin="15" Background="White" CornerRadius="5" Width="700" HorizontalAlignment="Stretch">
                        <Grid Margin="5">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="2*" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Grid.RowDefinitions>
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                                <RowDefinition Height="*" />
                            </Grid.RowDefinitions>

                            <StackPanel Grid.Column="0" Grid.Row="0" HorizontalAlignment="Stretch">
                                <TextBlock Text="{x:Bind Nom_activite}" FontSize="25" Margin="15,15,0,0" FontFamily="Arial rouded" TextTrimming="WordEllipsis"/>
                            </StackPanel>


                            <StackPanel Grid.Column="0" Grid.ColumnSpan="2" Grid.Row="1" Margin="15,20,0,0" HorizontalAlignment="Stretch">
                                <TextBlock FontSize="20" Margin="5">
                                    <Run Text="Type d'activité:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Description}"/>
                                </TextBlock>
                            </StackPanel>
                            <StackPanel Grid.Column="0" Grid.Row="2" Margin="15,20,0,0" HorizontalAlignment="Stretch">
                                <TextBlock FontSize="20" Margin="5">
                                    <Run Text="Type d'activité:"/>
                                    <Run FontWeight="Light" Text="{x:Bind IdCategorie}"/>
                                </TextBlock>
                            </StackPanel>
                            <StackPanel Grid.Column="0" Grid.Row="3" Margin="15,20,0,0" HorizontalAlignment="Stretch">
                                <TextBlock FontSize="20" Margin="5">
                                    <Run Text="Type d'activité:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Cout_organisation}"/>
                                </TextBlock>
                            </StackPanel>
                            <StackPanel Grid.Column="0" Grid.Row="4" Margin="15,20,0,0" HorizontalAlignment="Stretch">
                                <TextBlock FontSize="20" Margin="5">
                                    <Run Text="Prix vente client:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Prix_vente_client}"/>
                                </TextBlock>
                            </StackPanel>

                            <Button 
                                Click="btn_inscription"
                                HorizontalAlignment="Center"
                                VerticalAlignment="Center"
                                Grid.Column="2"
                                Grid.Row="3"
                                FontSize="19"
                                Width="150" Height="40"
                                >           
                                S'inscrire
                            </Button>
                            <Button 
                                Click="btn_Supp_Click"
                                HorizontalAlignment="Center"
                                VerticalAlignment="Center"
                                Grid.Column="2"
                                Grid.Row="4"
                                FontSize="19"
                                Width="150" Height="40"
                                >
                                Noter
                            </Button>
                        </Grid>
                    </StackPanel>
                </DataTemplate>
            </GridView.ItemTemplate>
        </GridView>
    </Grid>
</Page>

 */