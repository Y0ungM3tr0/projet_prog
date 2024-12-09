using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Appreciation : INotifyPropertyChanged
    {
        int idAppreciation;
        int idSeance;
        string matricule;
        double note_appreciation;

        // statistique
        // Afficher moyenne des notes par activité
        double moy_note_par_activite;
        int idActivite;
        string nomActivite;
        // Afficher la moyenne des notes d'appréciation pour toutes les activités
        double moy_note_toutes_activites;


        public Appreciation()
        {
            this.IdAppreciation = -1;
            this.IdSeance = -1;
            this.Matricule = "matricule";
            this.Note_appreciation = -1;
        }

        public Appreciation(int idAppreciation, int idSeance, string matricule, double note_appreciation)
        {
            this.IdAppreciation = idAppreciation;
            this.IdSeance = idSeance;
            this.Matricule = matricule;
            this.Note_appreciation = note_appreciation;
        }

        // statistique
        // Afficher moyenne des notes par activité
        public Appreciation(int idAppreciation, double moy_note_par_activite, int idActivite, string nomActivite)
        {
            this.idAppreciation = idAppreciation;
            this.moy_note_par_activite = moy_note_par_activite;
            this.idActivite = idActivite;
            this.nomActivite = nomActivite;
        }
        // Afficher la moyenne des notes d'appréciation pour toutes les activités
        public Appreciation(double moy_note_toutes_activites )
        {
            this.moy_note_toutes_activites = moy_note_toutes_activites;
        }

        public int IdAppreciation
        {
            get => idAppreciation;
            set
            {
                idAppreciation = value;
                this.OnPropertyChanged(nameof(IdAppreciation));
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

        public double Note_appreciation
        {
            get => note_appreciation;
            set
            {
                note_appreciation = value;
                this.OnPropertyChanged(nameof(Note_appreciation));
            }
        }

        // statistique
        // Afficher moyenne des notes par activité
        public double Moy_note_par_activite
        {
            get => moy_note_par_activite;
            set
            {
                moy_note_par_activite = value;
                this.OnPropertyChanged(nameof(Moy_note_par_activite));
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
        // Afficher la moyenne des notes d'appréciation pour toutes les activités
        public double Moy_note_toutes_activites
        {
            get => moy_note_toutes_activites;
            set
            {
                moy_note_toutes_activites = value;
                this.OnPropertyChanged(nameof(Moy_note_toutes_activites));
            }
        }

        public string Moy_note_toutes_activites_string
        {
            get => moy_note_toutes_activites.ToString("0.0");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
