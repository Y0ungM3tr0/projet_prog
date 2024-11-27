using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;

namespace prog_final
{
    internal class SingletonAdmin
    {
        MySqlConnection con;
        ObservableCollection<Administrateur> liste_admin;
        static SingletonAdmin instance = null;

        public SingletonAdmin()
        {
            //420335ri_gr00001_2366599-mac-donald-etienne
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=420335ri_gr00001_2366599-mac-donald-etienne;Uid=2366599;Pwd=2366599;");
            liste_admin = new ObservableCollection<Administrateur>();
        }

        // SINGLETON
        public static SingletonAdmin getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonAdmin();
            }
            return instance;
        }

        public ObservableCollection<Administrateur> getListe_admin()
        {
            return liste_admin;
        }


        // ajoute les admins dans la liste (va les chercher dans la bd))
        public void getToutAdmin()
        {
            liste_admin.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM administrateur;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string nom_utilisateur = r.GetString("nom_utilisateur");
                    string mot_de_passe = r.GetString("mot_de_passe");

                    Administrateur administrateur = new Administrateur(nom_utilisateur, mot_de_passe);
                    liste_admin.Add(administrateur);
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

        // ajoute les Activites dans la bd
        public void addAdmin(string nom_utilisateur, string mot_de_passe)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO administrateur (nom_utilisateur, mot_de_passe) VALUES (@nom_utilisateur, @mot_de_passe);";

                commande.Parameters.AddWithValue("@nom_utilisateur", nom_utilisateur);
                commande.Parameters.AddWithValue("@mot_de_passe", mot_de_passe);

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
            getToutAdmin();
        }




        // CONNEXION
        public int validationAdminLogIn(string inputUsername, string inputPassword )
        {
            int validation = -1;
            // -1 -> rien de bon (pas username dans bd)
            // 0 -> username bon mais pas mot de passe 
            // 1 -> username bon + mot de passe bon -> doit log in
            int validationUsername = 0;
            bool validationConnexion = false;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM administrateur;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string nom_utilisateur = r.GetString("nom_utilisateur");
                    string mot_de_passe = r.GetString("mot_de_passe");

                    // est-ce qu'il existe
                    if (nom_utilisateur.Equals(inputUsername))
                    {
                        validationUsername++;
                    }

                    // si les deux inputs sont bon = connexion
                    if (nom_utilisateur == inputUsername && mot_de_passe == inputPassword)
                    {
                        validationConnexion = true;
                        break;
                    }
                }

                if (validationUsername == 0)
                {
                    validation = -1;
                }
                if (validationUsername > 0) 
                {
                    validation = 0;
                }
                if (validationConnexion == true)
                {
                    validation = 1;
                }

                con.Close();
                return validation;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
