using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class SingletonAdherent
    {
        MySqlConnection con;
        ObservableCollection<Adherent> liste_des_adherents;
        static SingletonAdherent instance = null;

        public SingletonAdherent()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420335ri_gr00001_2366599-mac-donald-etienne;Uid=2366599;Pwd=2366599;");
            liste_des_adherents = new ObservableCollection<Adherent>();
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

        public ObservableCollection<Adherent> getListe_des_adherents()
        {
            return liste_des_adherents;
        }

        // ajoute les adherents dans la liste (va les chercher dans la bd))
        public void getToutAdherents()
        {
            liste_des_adherents.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM adherent;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    string adresse = r.GetString("adresse");
                    string date_naissance = r.GetString("date_naissance");
                    int age= r.GetInt32("age");

                    Adherent adherent = new Adherent(matricule, nom, prenom, adresse, date_naissance, age);
                    liste_des_adherents.Add(adherent);
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

        // ajoute les adherents dans la bd
        public void addAdherent(string nom, string prenom, string adresse, string date_naissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO adherent (nom, prenom, adresse, date_naissance) VALUES (@nom, @prenom, @adresse, @date_naissance);";

                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@date_naissance", date_naissance);

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
            getToutAdherents();
        }


        // CONNEXION
        public bool validationAdherentLogIn(string inputNumIdentification)
        {
            bool validationConnexion = false;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM adherent;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string matricule = r.GetString("matricule");

                    // est-ce qu'il existe
                    if (matricule == inputNumIdentification)
                    {
                        validationConnexion = true;
                        break;
                    }
                }

                con.Close();
                return validationConnexion;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                Console.WriteLine(ex.Message);
                return validationConnexion;
            }
        }
        // get son identifiant
        public void getIdentifiantAdherent(string matricule)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM adherent WHERE matricule = @matricule;";

                commande.Parameters.AddWithValue("@matricule", matricule);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    SingletonUtilisateur.getInstance().UsernameAdherent = r.GetString("prenom");
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
