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
        string date_naissance;
        int age;

        public Adherent()
        {
            this.matricule = "matricule";
            this.nom = "nom";
            this.prenom = "prenom";
            this.adresse = "adresse";
            this.date_naissance = "date_naissance";
            this.age = -1;
        }

        public Adherent(string nom, string prenom, string adresse, string date_naissance)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.date_naissance = date_naissance;
        }

        public Adherent(string matricule, string nom, string prenom, string adresse, string date_naissance)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.date_naissance = date_naissance;
        }

        public Adherent(string matricule, string nom, string prenom, string adresse, string date_naissance, int age)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.date_naissance = date_naissance;
            this.age = age;
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

        public string Date_naissance
        {
            get => date_naissance;
            set
            {
                date_naissance = value;
                this.OnPropertyChanged(nameof(Date_naissance));
            }

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






        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
