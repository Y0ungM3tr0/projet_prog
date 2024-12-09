using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class SingletonSeance
    {
        MySqlConnection con;
        ObservableCollection<Seance> liste_des_seances;
        static SingletonSeance instance = null;

        public SingletonSeance()
        {
            con = new MySqlConnection(
                SingletonUtilisateur.getInstance().getLienBd()
                );
            liste_des_seances = new ObservableCollection<Seance>();
        }

        // SINGLETON
        public static SingletonSeance getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonSeance();
            }
            return instance;
        }

        public ObservableCollection<Seance> getListe_des_seances()
        {
            return liste_des_seances;
        }

        // ajoute les Activites dans la liste (va les chercher dans la bd))
        public void getToutSeances()
        {
            liste_des_seances.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM seance;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idSeance = r.GetInt32("idSeance");
                    int idActivite= r.GetInt32("idActivite");
                    //string nomActivite = r.GetString("nomActivite");
                    DateTime date_seance = r.GetDateTime("date_seance");
                    string heure = r.GetString("heure");
                    int nbr_place_disponible = r.GetInt32("nbr_place_disponible");
                    int nbr_inscription = r.GetInt32("nbr_inscription");
                    double moyenne_appreciation = r.GetDouble("moyenne_appreciation");

                    Seance seance = new Seance(idSeance, idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation/*, nomActivite*/);
                    liste_des_seances.Add(seance);
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

        // get les seance DE L'UTILISATEUR 
        public void getSeancesUtilisateur()
        {
            liste_des_seances.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL infos_seance_reservations_utilisateur_pour_appreciation(@p_matricule);";
                string matriculeAdherent = SingletonUtilisateur.getInstance().MatriculeAdherent;
                commande.Parameters.AddWithValue("@p_matricule", matriculeAdherent);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                { 
                    int idSeance = r.GetInt32("idSeance");
                    int idActivite = r.GetInt32("idActivite");
                    DateTime date_seance = r.GetDateTime("date_seance");
                    string heure = r.GetString("heure");
                    int nbr_place_disponible = r.GetInt32("nbr_place_disponible");
                    int nbr_inscription = r.GetInt32("nbr_inscription");
                    double moyenne_appreciation = r.GetDouble("moyenne_appreciation");

                    string nomActivite = r.GetString("nomActivite");
                    string descriptionActivite = r.GetString("description");

                    int idReservation = r.GetInt32("idReservation");
                    string matricule = r.GetString("matricule");


                    Seance seance = new Seance(idSeance, idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation, nomActivite, descriptionActivite, idReservation, matricule);
                    liste_des_seances.Add(seance);
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


        // ajouter une seance dans la bd
        public void addSeance(int idActivite, DateTime date_seance, string heure, int nbr_place_disponible)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Ajouter_seance(@idActivite, @date_seance, @heure, @nbr_place_disponible);";

                commande.Parameters.AddWithValue("@idActivite", idActivite);
                commande.Parameters.AddWithValue("@date_seance", date_seance);
                commande.Parameters.AddWithValue("@heure", heure);
                commande.Parameters.AddWithValue("@nbr_place_disponible", nbr_place_disponible);

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
            getToutSeances();
        }

    }
}
