using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class SingletonActivite
    {
        MySqlConnection con;
        ObservableCollection<Activite> liste_des_activites;
        static SingletonActivite instance = null;
        // statistique
        ObservableCollection<Activite> liste_moyenne_des_notes_par_activite;
        ObservableCollection<Activite> liste_nbr_participant_par_activite;
        ObservableCollection<Activite> liste_nb_activites;
        ObservableCollection<Activite> liste_nbr_seance_par_activite;


        public SingletonActivite()
        {
            con = new MySqlConnection(
                SingletonUtilisateur.getInstance().getLienBd()
                );
            liste_des_activites = new ObservableCollection<Activite>();
            liste_nbr_participant_par_activite = new ObservableCollection<Activite>();
            liste_nb_activites = new ObservableCollection<Activite>();
            liste_nbr_seance_par_activite = new ObservableCollection<Activite>();
        }

        // SINGLETON
        public static SingletonActivite getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonActivite();
            }
            return instance;
        }

        public ObservableCollection<Activite> getListe_des_activites()
        {
            return liste_des_activites;
        }
        public ObservableCollection<Activite> getListe_nbr_participant_par_activite()
        {
            return liste_nbr_participant_par_activite;
        }
        public ObservableCollection<Activite> getListe_nbr_seance_par_activite()
        {
            return liste_nbr_seance_par_activite;
        }
        public ObservableCollection<Activite> getListe_nb_activites()
        {
            return liste_nb_activites;
        }



        // ajoute les Activites dans la liste (va les chercher dans la bd))
        public void getToutActivite()
        {
            liste_des_activites.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM activite;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idActivite = r.GetInt32("idActivite");
                    string nom_activite = r.GetString("nomActivite");
                    int idCategorie = r.GetInt32("idCategorie");
                    string description = r.GetString("description");
                    double cout_organisation = r.GetDouble("cout_organisation");
                    double prix_vente_client = r.GetDouble("prix_vente_client");

                    Activite activite = new Activite(idActivite, nom_activite, idCategorie, description, cout_organisation, prix_vente_client, SingletonCategorie.getInstance().getTypeCategories(idCategorie));
                    liste_des_activites.Add(activite);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // vérification que la connection est ouverte, pour la fermer
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // utilise le fichier csv pour les ajouter dans la liste
        public async void ajoutCSV()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(GestionWindow.mainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();
            if (monFichier == null)
            {
                return;
            }

            var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

            foreach (var ligne in lignes)
            {
                var v = ligne.Split(";");


               addActivite(v[1], v[3], Convert.ToInt32(v[2]), Convert.ToDouble(v[4]), Convert.ToDouble(v[5]));
            }
        }

        // CRUD
        // ajoute les Activites dans la bd
        public void addActivite(string _nom, string _description, int _idCategorie, double _coutOrganisation, double _prixVente)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO activite(nomActivite, idCategorie, description, cout_organisation, prix_vente_client) VALUES(@nomActivite, @idCategorie, @description, @cout_organisation, @prix_vente_client);";

                commande.Parameters.AddWithValue("@nomActivite", _nom);
                commande.Parameters.AddWithValue("@description", _description);
                commande.Parameters.AddWithValue("@idCategorie", _idCategorie);
                commande.Parameters.AddWithValue("@cout_organisation", _coutOrganisation);
                commande.Parameters.AddWithValue("@prix_vente_client", _prixVente);

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                Console.WriteLine(ex.Message);
            }
            getToutActivite();
        }
        // ajoute les Activites dans la bd
        public void modifierActivite(int idActivite, string nomActivite, int idCategorie, string description, double cout_organisation, double prix_vente_client)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Modifier_activite(@idActivite, @nomActivite, @idCategorie, @description,  @cout_organisation, @prix_vente_client);";

                commande.Parameters.AddWithValue("@idActivite", idActivite);
                commande.Parameters.AddWithValue("@nomActivite", nomActivite);
                commande.Parameters.AddWithValue("@idCategorie", idCategorie);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@cout_organisation", cout_organisation);
                commande.Parameters.AddWithValue("@prix_vente_client", prix_vente_client);

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                Console.WriteLine(ex.Message);
            }
            getToutActivite();
        }
        // ajoute les Activites dans la bd
        public void supprimerActivite(int idActivite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Supprimer_activite(@idActivite);";
                commande.Parameters.AddWithValue("@idActivite", idActivite);

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                Console.WriteLine(ex.Message);
            }
            getToutActivite();
        }

        // get idActivite
        public int getIdActivite(string nomActivite)
        {
            int idActivite = 0;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT idActivite FROM activite WHERE nomActivite = @nomActivite;";

                commande.Parameters.AddWithValue("@nomActivite", nomActivite);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    idActivite = r.GetInt32("idActivite");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // vérification que la connection est ouverte, pour la fermer
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return idActivite;
        }

        // nombre d'activités total
        public void getNb_total_activite()
        {
            liste_nb_activites.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM nb_activites;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int nb_activite = r.GetInt32("nb_activite");

                    Activite activite = new Activite(nb_activite);
                    liste_nb_activites.Add(activite);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // vérification que la connection est ouverte, pour la fermer
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // nombre d'activités total
        public void get_nbr_seance_par_activite()
        {
            liste_nbr_seance_par_activite.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM nbr_seance_par_activite;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string nomActivite = r.GetString("nomActivite");
                    int nbr_seance_par_activite = r.GetInt32("nbr_seance_par_activite");

                    Activite activite = new Activite(nomActivite, nbr_seance_par_activite);
                    liste_nbr_seance_par_activite.Add(activite);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                // vérification que la connection est ouverte, pour la fermer
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
















    }    
}
