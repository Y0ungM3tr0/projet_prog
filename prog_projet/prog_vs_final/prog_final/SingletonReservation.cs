using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class SingletonReservation
    {
        MySqlConnection con;
        ObservableCollection<Reservation> liste_des_reservations;
        static SingletonReservation instance = null;

        ObservableCollection<Reservation> liste_nbr_participant_par_activite;

        public SingletonReservation()
        {
            con = new MySqlConnection(
                SingletonUtilisateur.getInstance().getLienBd()
                );
            liste_des_reservations = new ObservableCollection<Reservation>();
            liste_nbr_participant_par_activite = new ObservableCollection<Reservation>();
        }

        // SINGLETON
        public static SingletonReservation getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonReservation();
            }
            return instance;
        }

        public ObservableCollection<Reservation> getListe_des_reservations()
        {
            return liste_des_reservations;
        }
        public ObservableCollection<Reservation> getListe_nbr_participant_par_activite()
        {
            return liste_nbr_participant_par_activite;
        }



        // voir toutes les reservations DE L'UTILISATEUR
        public void getReservationsUtilisateurs()
        {
            liste_des_reservations.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL infos_reservations_de_utilisateur(@matricule);";
                string matriculeAdherent = SingletonUtilisateur.getInstance().MatriculeAdherent;
                commande.Parameters.AddWithValue("@matricule", matriculeAdherent);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string nomActivite = r.GetString("nomActivite");
                    DateTime date_seance = r.GetDateTime("date_seance");
                    string heure = r.GetString("heure");

                    Reservation reservation = new Reservation(nomActivite, date_seance, heure);
                    liste_des_reservations.Add(reservation);
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

        // voir toutes les reservations dans la bd
        public void getToutReservations()
        {
            liste_des_reservations.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM reservation;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    int idReservation = r.GetInt32("idReservation");
                    int idSeance = r.GetInt32("idSeance");
                    string matricule = r.GetString("matricule");

                    Reservation reservation= new Reservation(idReservation, idSeance, matricule);
                    liste_des_reservations.Add(reservation);
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

        // ajouter une reservation (INSCRIPTION A UNE SCEANCE) dans la bd
        public void addReservation(int idSeance, string matricule)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Ajouter_reservation(@idSeance, @matricule);";

                commande.Parameters.AddWithValue("@idSeance", idSeance);
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
        }


        // validation pour réserver (seulement une fois)
        public bool validationReservation(int idSeance, string matricule)
        {
            bool validation = false;
            int validation_reservation = -1;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "CALL Adherent_inscrit(@matricule, @idSeance) AS validation_reservation";

                commande.Parameters.AddWithValue("@idSeance", idSeance);
                commande.Parameters.AddWithValue("@matricule", matricule);

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    validation_reservation = r.GetInt32("validation_reservation");
                    break;
                }

                if (validation_reservation == 1)
                {
                    validation = false;
                }
                else if (validation_reservation == 0)
                {
                    validation = true;
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

            return validation;
        }


        // voir toutes les reservations DE L'UTILISATEUR
        public void getNbr_participant_par_activite()
        {
            liste_nbr_participant_par_activite.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM nb_participant_chq_activite;";

                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                while (r.Read())
                {
                    string nomActivite = r.GetString("nomActivite");
                    int nbr_participant_par_activite = r.GetInt32("nombre_participant");

                    Reservation reservation = new Reservation(nomActivite, nbr_participant_par_activite);
                    liste_nbr_participant_par_activite.Add(reservation);
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
    }
}
