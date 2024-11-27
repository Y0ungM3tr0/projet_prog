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
    public sealed partial class pageAffichageActivité : Page
    {
        int index = -1;
        public pageAffichageActivité()
        {
            this.InitializeComponent();
            SingletonActivite.getInstance().getToutActivite();
            gv_activite.ItemsSource = SingletonActivite.getInstance().getListe_des_activites(); ;
        }

        private void gv_activite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_Supp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_inscription(object sender, RoutedEventArgs e)
        {

        }
    }
}

/*
 <GridView x:Name="gv_activite" Grid.Row="2" Grid.RowSpan="3" Grid.Column="1" SelectionChanged="gv_activite_SelectionChanged" HorizontalAlignment="Center">
            <GridView.ItemTemplate>
                <DataTemplate x:DataType="local:Activite" >
                    <StackPanel x:Name="produitCard" Margin="15" Background="White" CornerRadius="5" Width="350">
                        <Grid Margin="5">
                            <Grid.ColumnDefinitions>
                                <ColumnDefinition Width="*" />
                                <ColumnDefinition Width="*" />
                            </Grid.ColumnDefinitions>
                            <Grid.RowDefinitions>
                                <RowDefinition Height="4*" />
                                <RowDefinition Height="1*" />
                            </Grid.RowDefinitions>

                            <StackPanel Grid.Column="0" Grid.ColumnSpan="2" Grid.Row="0" Padding="10" HorizontalAlignment="Stretch">
                                <TextBlock Text="{x:Bind Nom_activite}" FontSize="23" FontFamily="Arial rouded" MaxLines="2" TextTrimming="WordEllipsis"/>
                                <TextBlock FontSize="17" Margin="5">
                                    <Run Text="Type d'activité:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Type}"/>
                                </TextBlock>
                                <TextBlock FontSize="17" Margin="5">
                                    <Run Text="Description:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Description}"/>
                                </TextBlock>
                                <TextBlock FontSize="17" Margin="5">
                                    <Run Text="Cout de l'organisation:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Cout_organisation}"/>
                                </TextBlock>
                                <TextBlock FontSize="17" Margin="5">
                                    <Run Text="Prix d'inscription:"/>
                                    <Run FontWeight="Light" Text="{x:Bind Prix_vente_client}"/>
                                </TextBlock>
                            </StackPanel>

                            <Button 
                                Click="btn_inscription"
                                Grid.Column="0" 
                                Grid.Row="3" 
                                Margin="20,0,0,0"
                                HorizontalAlignment="Left"
                                >
                                S'inscrire
                            </Button>
                            <Button 
                                Click="btn_Supp_Click"
                                Grid.Column="1" 
                                Grid.Row="3" 
                                Margin="0,0,20,0"
                                HorizontalAlignment="Right"
                                >
                                Supprimer
                            </Button>
                        </Grid>
                    </StackPanel>
                </DataTemplate>
            </GridView.ItemTemplate>
        </GridView>
 */