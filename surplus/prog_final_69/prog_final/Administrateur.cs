using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Administrateur : INotifyPropertyChanged
    {
        string nom_utilisateur;
        string mot_de_passe;

        public Administrateur()
        {
            this.Nom_utilisateur = "";
            this.Mot_de_passe = "";
        }

        public Administrateur(string nom_utilisateur, string mot_de_passe)
        {
            this.Nom_utilisateur = nom_utilisateur;
            this.Mot_de_passe = mot_de_passe;
        }

        public string Nom_utilisateur
        {
            get => nom_utilisateur;
            set
            {
                nom_utilisateur = value;
                this.OnPropertyChanged(nameof(Nom_utilisateur));
            }
        }

        public string Mot_de_passe
        {
            get => mot_de_passe;
            set
            {
                mot_de_passe = value;
                this.OnPropertyChanged(nameof(Mot_de_passe));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
