using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00001_2366599-etienne-mac-donald;Uid=2366599;Pwd=2366599;");
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

        // ajoute les Activites dans la liste (va les chercher dans la bd)
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
                    string nom = r.GetString("nom");
                    string description = r.GetString("description");
                    string type = r.GetString("type");
                    double coutOrganisation = r.GetDouble("coutOrganisation");
                    double prixVente = r.GetDouble("prixVente");

                    Activite activite = new Activite(nom, description, type, coutOrganisation, prixVente);
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

                addActivite(v[0], v[1], v[2], v[3]);
            }
        }

        // ajoute les Activites dans la bd
        public void addActivite(string _nom, string _prenom, string _adresse, string _dateNaissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO Activite (nom, prenom, adresse, dateNaissance) VALUES (@nom, @prenom, @adresse, @dateNaissance);";

                commande.Parameters.AddWithValue("@nom", _nom);
                commande.Parameters.AddWithValue("@prenom", _prenom);
                commande.Parameters.AddWithValue("@adresse", _adresse);
                commande.Parameters.AddWithValue("@dateNaissance", _dateNaissance);

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
