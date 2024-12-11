using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Seance : INotifyPropertyChanged
    {
        int idSeance;
        int idActivite;
        DateTimeOffset date_seance;
        string heure;
        int nbr_place_disponible;
        int nbr_inscription;
        double moyenne_appreciation;


        string nomActivite;
        string descriptionActivite;

        int idReservation;
        string matricule;

        public Seance()
        {
            this.IdSeance = 0;
            this.IdActivite = -1;
            this.Date_seance = new DateTime();
            this.Heure = "heure";
            this.Nbr_place_disponible = 0;
            this.Nbr_inscription = 0;
            this.Moyenne_appreciation = 0.00;

            this.NomActivite = "nomActivite";
        }
        public Seance(int idSeance, int idActivite, DateTime date_seance, string heure, int nbr_place_disponible, int nbr_inscription, double moyenne_appreciation)
        {
            this.IdSeance = idSeance;
            this.IdActivite = idActivite;
            this.Date_seance = date_seance;
            this.Heure = heure;
            this.Nbr_place_disponible = nbr_place_disponible;
            this.Nbr_inscription = nbr_inscription;
            this.Moyenne_appreciation = moyenne_appreciation;
        }

        public Seance(int idSeance, int idActivite, DateTime date_seance, string heure, int nbr_place_disponible, int nbr_inscription, double moyenne_appreciation, string nomActivite)
        {
            this.IdSeance = idSeance;
            this.IdActivite = idActivite;
            this.Date_seance = date_seance;
            this.Heure = heure;
            this.Nbr_place_disponible = nbr_place_disponible;
            this.Nbr_inscription = nbr_inscription;
            this.Moyenne_appreciation = moyenne_appreciation;

            this.NomActivite = nomActivite;
        }
        public Seance(int idSeance, int idActivite, DateTime date_seance, string heure, int nbr_place_disponible, int nbr_inscription, double moyenne_appreciation, string nomActivite, string descriptionActivite, int idReservation, string matricule)
        {
            this.idSeance = idSeance;
            this.idActivite = idActivite;
            this.date_seance = date_seance;
            this.heure = heure;
            this.nbr_place_disponible = nbr_place_disponible;
            this.nbr_inscription = nbr_inscription;
            this.moyenne_appreciation = moyenne_appreciation;

            this.nomActivite = nomActivite;
            this.descriptionActivite = descriptionActivite;


            this.idReservation = idReservation;
            this.matricule = matricule;
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

        public int IdActivite
        {
            get => idActivite;
            set
            {
                idActivite = value;
                this.OnPropertyChanged(nameof(IdActivite));
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
            get => date_seance.ToString("d");
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

        public int Nbr_place_disponible
        {
            get => nbr_place_disponible;
            set
            {
                nbr_place_disponible = value;
                this.OnPropertyChanged(nameof(Nbr_place_disponible));
            }
        }

        public int Nbr_inscription
        {
            get => nbr_inscription;
            set
            {
                nbr_inscription = value;
                this.OnPropertyChanged(nameof(Nbr_inscription));
            }
        }

        public double Moyenne_appreciation
        {
            get => moyenne_appreciation;
            set
            {
                moyenne_appreciation = value;
                this.OnPropertyChanged(nameof(Moyenne_appreciation));
            }
        }

        public string DescriptionActivite
        {
            get => descriptionActivite;
            set
            {
                descriptionActivite = value;
                this.OnPropertyChanged(nameof(DescriptionActivite));
            }
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

        public string Matricule
        {
            get => matricule;
            set
            {
                matricule = value;
                this.OnPropertyChanged(nameof(Matricule));
            }
        }



        public Visibility Afficher {
            get
            {
                if(SingletonUtilisateur.getInstance().Connecter == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
