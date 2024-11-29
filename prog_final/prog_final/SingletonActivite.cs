using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public SingletonActivite()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420335ri_gr00001_2366599-mac-donald-etienne;Uid=2366599;Pwd=2366599;");
            liste_des_activites = new ObservableCollection<Activite>();
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

                    Activite activite = new Activite(idActivite, nom_activite, idCategorie, description, cout_organisation, prix_vente_client);
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

        // ajoute les Activites dans la bd
        public void addActivite(string _nom, string _description, int _idCategorie, double _coutOrganisation, double _prixVente)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO activite (nomActivite, description, idCategorie, cout_organisation, prix_vente_client) VALUES (@nomActivite, @description, @idCategorie, @cout_organisation, @prix_vente_client);";

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
    }
}
