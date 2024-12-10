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
        // statistique
        ObservableCollection<Adherent> liste_adherent_plus_de_seance;
        ObservableCollection<Adherent> liste_prix_moyen_payé_par_adherent;


        public SingletonAdherent()
        {
            con = new MySqlConnection(
                SingletonUtilisateur.getInstance().getLienBd()
                );
            liste_des_adherents = new ObservableCollection<Adherent>();
            liste_adherent_plus_de_seance = new ObservableCollection<Adherent>();
            liste_prix_moyen_payé_par_adherent = new ObservableCollection<Adherent>();
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

        public ObservableCollection<Adherent> getListe_adherent_plus_de_seance()
        {
            return liste_adherent_plus_de_seance;
        }
        public ObservableCollection<Adherent> getListe_prix_moyen_payé_par_adherent()
        {
            return liste_prix_moyen_payé_par_adherent;
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
                    DateTime date_naissance = r.GetDateTime("dateNaissance");
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

        //CRUD
        // ajoute les adherents dans la bd
        public void addAdherent(string nom, string prenom, string adresse, DateTime date_naissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                //commande.CommandText = "INSERT INTO adherent (nom, prenom, adresse, date_naissance) VALUES (@nom, @prenom, @adresse, @date_naissance);";
                //2000-02-01
                commande.CommandText = "CALL Ajouter_adherent(@nom, @prenom, @adresse, @dateNaissance);";
                
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@dateNaissance", date_naissance);

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
        // modifier un adherent dans la bd
        public void modifierAdherent(string matricule, string nom, string prenom, string adresse, string dateNaissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Modifier_adherent(@matricule, @nom, @prenom, @adresse, @dateNaissance);";

                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@dateNaissance", dateNaissance);

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
        // get adherent
        public void getUnAdherent(string _matricule)
        {
            liste_des_adherents.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM ViewAdherent WHERE matricule = @matricule;";
                commande.Parameters.AddWithValue("@matricule", _matricule);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");
                    string adresse = r.GetString("adresse");
                    string date_naissance = r.GetDateTime("dateNaissance").ToString();
                    int age = r.GetInt32("age");

                    /*
                    Adherent adherent = new Adherent(matricule, nom, prenom, adresse, date_naissance, age);
                    liste_des_adherents.Add(adherent);
                    */
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
        // supprimer un adherent dans la bd
        public void supprimerAdherent(string matricule)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Supprimer_adherent(@matricule);";

                commande.Parameters.AddWithValue("@matricule", matricule);

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
                    SingletonUtilisateur.getInstance().MatriculeAdherent = r.GetString("matricule");
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




        // STAT
        // participant ayant le nombre de séances le plus élevé
        public void getAdherentPlusSeance()
        {
            liste_adherent_plus_de_seance.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM nb_seances_plus_eleve;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string prenom = r.GetString("prenom");
                    string nom = r.GetString("nom");

                    int nb_seances = r.GetInt32("nb_seances");

                    Adherent adherent = new Adherent(matricule, nom, prenom, nb_seances);
                    liste_adherent_plus_de_seance.Add(adherent);
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

        // prix moyen par activite pour chaque participant
        public void getPrixMoyenPayéParAdherent()
        {
            liste_prix_moyen_payé_par_adherent.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM moy_prix_activite_chaque_adherent;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string matricule = r.GetString("matricule");
                    string nom = r.GetString("nom");
                    string prenom = r.GetString("prenom");

                    double moy_prix_activite = r.GetDouble("moy_prix_activite");

                    Adherent adherent = new Adherent(matricule, nom, prenom, moy_prix_activite);
                    liste_prix_moyen_payé_par_adherent.Add(adherent);
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
