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

        int idAdherent;
        string nom;
        string prenom;
        string adresse;
        string dateNaissance;

        public Adherent()
        {
            this.idAdherent = -1;
            this.nom = "nom";
            this.prenom = "prenom";
            this.adresse = "adresse";
            this.dateNaissance = "dateNaissance";
        }

        public Adherent(int idAdherent, string nom, string prenom, string adresse, string dateNaissance)
        {
            this.idAdherent = idAdherent;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissance = dateNaissance;
        }
        public int IdAdherent
        {
            get => idAdherent;
            set
            {
                idAdherent = value;
                this.OnPropertyChanged(nameof(IdAdherent));
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

        public string DateNaissance
        {
            get => dateNaissance;
            set
            {
                dateNaissance = value;
                this.OnPropertyChanged(nameof(DateNaissance));
            }

        }








        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
