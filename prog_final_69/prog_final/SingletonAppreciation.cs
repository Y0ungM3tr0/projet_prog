using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    class SingletonAppreciation
    {
        MySqlConnection con;
        ObservableCollection<Appreciation> liste_des_appreciations; 
        static SingletonAppreciation instance = null;

        ObservableCollection<Appreciation> liste_moy_note_par_activite;
        ObservableCollection<Appreciation> liste_moy_toutes_activite;

        public SingletonAppreciation()
        {
            con = new MySqlConnection(
                SingletonUtilisateur.getInstance().getLienBd()
                );
            liste_des_appreciations = new ObservableCollection<Appreciation>();
            liste_moy_note_par_activite = new ObservableCollection<Appreciation>();
            liste_moy_toutes_activite = new ObservableCollection<Appreciation>();
        }

        // SINGLETON
        public static SingletonAppreciation getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonAppreciation();
            }
            return instance;
        }

        public ObservableCollection<Appreciation> getListe_des_appreciations()
        {
            return liste_des_appreciations;
        }
        public ObservableCollection<Appreciation> getListe_moy_note_par_activite()
        {
            return liste_moy_note_par_activite;
        }
        public ObservableCollection<Appreciation> getListe_moy_toutes_activite()
        {
            return liste_moy_toutes_activite;
        }



        // voir toutes les appreciation DE L'UTILISATEUR
        public void getAppreciationsUtilisateurs()
        {
            liste_des_appreciations.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL infos_reservations_utilisateur(@matricule);";
                string matriculeAdherent = SingletonUtilisateur.getInstance().MatriculeAdherent;
                commande.Parameters.AddWithValue("@matricule", matriculeAdherent);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idReservation = r.GetInt32("idReservation");
                    int idSeance = r.GetInt32("idSeance");
                    string matricule = r.GetString("matricule");

                    string nomActivite = r.GetString("nomActivite");
                    DateTime date_seance = r.GetDateTime("date_seance");
                    string heure = r.GetString("heure");

                    Reservation reservation = new Reservation(idReservation, idSeance, matricule, nomActivite, date_seance, heure);
                    //liste_des_appreciations.Add(reservation);
                }

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
        }


        // ajoute les appreciations dans la liste (va les chercher dans la bd))
        public void getToutAppreciation()
        {
            liste_des_appreciations.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM appreciation ;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idAppreciation = r.GetInt32("idAppreciation ");
                    int idSeance = r.GetInt32("idSeance ");
                    string matricule = r.GetString("matricule ");
                    double note_appreciation = r.GetDouble("note_appreciation ");

                    Appreciation appreciation = new Appreciation(idAppreciation, idSeance, matricule, note_appreciation);
                    liste_des_appreciations.Add(appreciation);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // ajouter appreciation 
        public void ajouterAppreciation(int idSeance, string matricule, double note_appreciation)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Ajouter_appreciation(@idSeance, @matricule, @note_appreciation);";
                commande.Parameters.AddWithValue("@idSeance", idSeance);
                commande.Parameters.AddWithValue("@matricule", matricule);
                commande.Parameters.AddWithValue("@note_appreciation", note_appreciation);

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
        }

        // get les moyennes d'appréciation par activite (va les chercher dans la bd))
        public void getMoy_note_par_activite()
        {
            liste_moy_note_par_activite.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM moy_note_par_activite;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idAppreciation = r.GetInt32("idAppreciation ");
                    double moy_note_par_activite = r.GetDouble("moy_note_par_activite");
                    int idActivite = r.GetInt32("idActivite ");
                    string nomActivite = r.GetString("nomActivite ");
                    

                    Appreciation appreciation = new Appreciation(idAppreciation, moy_note_par_activite, idActivite, nomActivite);
                    liste_moy_note_par_activite.Add(appreciation);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        // get les moyennes d'appréciation par activite (va les chercher dans la bd))
        public void getMoy_note_toutes_activite()
        {
            liste_moy_toutes_activite.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM moy_note_toutes_activites;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    double moy_note_toutes_activites = r.GetDouble("moy_note_toutes_activites");


                    Appreciation appreciation = new Appreciation(moy_note_toutes_activites);
                    liste_moy_toutes_activite.Add(appreciation);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

















    }
}
