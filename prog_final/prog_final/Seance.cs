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
        string date_seance;
        string heure;
        int nbr_place_disponible;
        int nbr_inscription;
        double moyenne_appreciation;

        public Seance()
        {
            this.IdSeance = -1;
            this.IdActivite = -1;
            this.Date_seance = "date_seance";
            this.Heure = "heure";
            this.Nbr_place_disponible = -1;
            this.Nbr_inscription = -1;
            this.Moyenne_appreciation = 0.00;
        }

        public Seance(int idSeance, int idActivite, string date_seance, string heure, int nbr_place_disponible, int nbr_inscription, double moyenne_appreciation)
        {
            this.IdSeance = idSeance;
            this.IdActivite = idActivite;
            this.Date_seance = date_seance;
            this.Heure = heure;
            this.Nbr_place_disponible = nbr_place_disponible;
            this.Nbr_inscription = -1;
            this.Moyenne_appreciation = moyenne_appreciation;
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

        public string Date_seance
        {
            get => date_seance;
            set
            {
                date_seance = value;
                this.OnPropertyChanged(nameof(Date_seance));
            }
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


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
