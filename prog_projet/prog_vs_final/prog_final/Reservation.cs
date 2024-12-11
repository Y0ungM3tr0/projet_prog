using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Reservation : INotifyPropertyChanged
    {
        int idReservation;
        int idSeance;
        string matricule;

        string nomActivite;
        DateTimeOffset date_seance;
        string heure;

        // statistique
        // Afficher le nombre de participants pour chaque activité
        int nbr_participant_par_activite;

        public Reservation()
        {
            this.IdReservation = -1;
            this.IdSeance = -1;
            this.Matricule = "matricule";


            this.nomActivite = "nomActivite";
            this.date_seance = new DateTime();
            this.heure = "heure";
        }

        public Reservation(int idReservation, int idSeance, string matricule)
        {
            this.IdReservation = idReservation;
            this.IdSeance = idSeance;
            this.Matricule = matricule;
        }

        public Reservation(int idReservation, int idSeance, string matricule, string nomActivite, DateTime date_seance, string heure)
        {
            this.IdReservation = idReservation;
            this.IdSeance = idSeance;
            this.Matricule = matricule;

            this.nomActivite = nomActivite;
            this.date_seance = date_seance;
            this.heure = heure;
        }

        public Reservation(string nomActivite, DateTime date_seance, string heure)
        {
            this.nomActivite = nomActivite;
            this.date_seance = date_seance;
            this.heure = heure;
        }

        // statistique
        // Afficher le nombre de participants pour chaque activité
        public Reservation(string nomActivite, int nbr_participant_par_activite)
        {
            this.nomActivite = nomActivite;
            this.nbr_participant_par_activite = nbr_participant_par_activite;
        }

        public int IdReservation
        {
            get => idReservation;
            set
            {
                idReservation = value;
                this.OnPropertyChanged(nameof(IdReservation));
            }
        }

        public int IdSeance
        {
            get => idSeance;
            set
            {
                idSeance = value;
                this.OnPropertyChanged(nameof(IdSeance));
            }
        }

        public string Matricule
        {
            get => matricule;
            set
            {
                matricule = value;
                this.OnPropertyChanged(nameof(Matricule));
            }
        }
        
        public string NomActivite
        {
            get => nomActivite;
            set
            {
                nomActivite = value;
                this.OnPropertyChanged(nameof(NomActivite));
            }
        }
        public DateTimeOffset Date_seance
        {
            get => date_seance;
            set
            {
                date_seance = value;
                this.OnPropertyChanged(nameof(Date_seance));
            }
        }
        public string Date_seance_string
        {
            get => date_seance.ToString("yyyy-mm-dd");
        }
        public string Heure
        {
            get => heure;
            set
            {
                heure = value;
                this.OnPropertyChanged(nameof(Heure));
            }
        }

        // statistique
        // Afficher le nombre de participants pour chaque activité
        /*
        public int IdActivite
        {
            get => idActivite;
            set
            {
                idActivite = value;
                this.OnPropertyChanged(nameof(IdActivite));
            }
        }
        */

        public int Nbr_participant_par_activite
        {
            get => nbr_participant_par_activite;
            set
            {
                nbr_participant_par_activite = value;
                this.OnPropertyChanged(nameof(Nbr_participant_par_activite));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
