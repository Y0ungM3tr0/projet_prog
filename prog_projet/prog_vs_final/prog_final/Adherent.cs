using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Adherent : INotifyPropertyChanged
    {

        string matricule;
        string nom;
        string prenom;
        string adresse;
        DateTimeOffset date_naissance;
        int age;

        // statistique
        // adherent au plus de séances
        int nb_seances;
        // prix moyen payé pour chaque adherent
        double moy_prix_activite;
        // le nombre total d'adhérents
        int nb_adherents;


        public Adherent()
        {
            this.matricule = "matricule";
            this.nom = "nom";
            this.prenom = "prenom";
            this.adresse = "adresse";
            this.date_naissance = new DateTime();
            this.age = -1;
        }
        
        public Adherent(string nom, string prenom, string adresse, DateTime date_naissance)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.date_naissance = date_naissance;
        }

        public Adherent(string matricule, string nom, string prenom, string adresse, DateTime date_naissance)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.date_naissance = date_naissance;
        }

        public Adherent(string matricule, string nom, string prenom, string adresse, DateTime date_naissance, int age)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.date_naissance = date_naissance;
            this.age = age;
        }

        // adherent au plus de séances
        public Adherent(string matricule, string nom, string prenom, int nb_seances)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.nb_seances = nb_seances;
        }
        // prix moyen par activité pour chaque adherent
        public Adherent(string matricule, string prenom, string nom, string nomActivite, double moy_prix_activite)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.moy_prix_activite = moy_prix_activite;
        }
        // le nombre total d'adhérents
        public Adherent(int nb_adherents)
        {
            this.nb_adherents = nb_adherents;
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

        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                this.OnPropertyChanged(nameof(Nom));
            }
        }

        public string Prenom
        {
            get => prenom;
            set
            {
                prenom = value;
                this.OnPropertyChanged(nameof(Prenom));
            }
        }

        public string Adresse
        {
            get => adresse;
            set
            {
                adresse = value;
                this.OnPropertyChanged(nameof(Adresse));
            }
        }

        public DateTimeOffset Date_naissance
        {
            get => date_naissance;
            set
            {
                date_naissance = value;
                this.OnPropertyChanged(nameof(Date_naissance));
            }
        }

        public string Date_naissance_string
        {
            get => date_naissance.ToString("yyyy-MM-dd");

        }

        public int Age
        {
            get => age;
            set
            {
                age = value;
                this.OnPropertyChanged(nameof(Age));
            }
        }

        // adherent au plus de séances
        public int Nb_seances
        {
            get => nb_seances;
            set
            {
                nb_seances = value;
                this.OnPropertyChanged(nameof(Nb_seances));
            }
        }
        // prix moyen payé pour chaque adherent 
        public double Moyenne_par_activite
        {
            get => moy_prix_activite;
            set
            {
                moy_prix_activite = value;
                this.OnPropertyChanged(nameof(moy_prix_activite));
            }
        }
        // le nombre total d'adhérents
        public int Nb_adherents
        {
            get => nb_adherents;
            set
            {
                nb_adherents = value;
                this.OnPropertyChanged(nameof(Nb_adherents));
            }
        }






        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
