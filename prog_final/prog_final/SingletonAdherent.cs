using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class SingletonAdherent
    {
        MySqlConnection con; //test
        ObservableCollection<Adherent> liste_des_adherent;
        static SingletonAdherent instance = null;

        public SingletonAdherent()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420345ri_gr00001_2366599-etienne-mac-donald;Uid=2366599;Pwd=2366599;");
            liste_des_adherent = new ObservableCollection<Adherent>();
        }

        // SINGLETON
        public static SingletonAdherent getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonAdherent();
            }
            return instance;
        }

        public ObservableCollection<Adherent> getListe_des_adherent()
        {
            return liste_des_adherent;
        }

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

                addAdherent(v[0], v[1], v[2], v[3]);
            }
        }

        public void addAdherent(string _nom, string _prenom, string _adresse, string _dateNaissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO adherent (nom, prenom, adresse, dateNaissance) VALUES (@nom, @prenom, @adresse, @dateNaissance);";

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
            //getProduit();
        }



    }
}
