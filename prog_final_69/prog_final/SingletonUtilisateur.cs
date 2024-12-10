using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace prog_final
{    
    internal class SingletonUtilisateur : INotifyPropertyChanged
    {
        static SingletonUtilisateur instance = null;

        // SINGLETON
        public static SingletonUtilisateur getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonUtilisateur();
            }
            return instance;
        }

        // lien bd
        public string getLienBd()
        {
            string sim = "Server=cours.cegep3r.info;Database=420345ri_gr00001_2356374-simon-cartier;Uid=2356374;Pwd=2356374;";
            string etienne = "Server=cours.cegep3r.info;Database=420345ri_gr00001_2366599-etienne-mac-donald;Uid=2366599;Pwd=2366599;";
            return etienne;
        }
        


        bool statutUtilisateur; // false = adherent, true = admin
        bool connecter; // est ce que quelqu'un est connecté? false = non, true = oui

        string usernameAdmin;
        string mdp;

        string usernameAdherent;
        string matriculeAdherent;

        public bool StatutUtilisateur
        {
            get => statutUtilisateur;
            set
            {
                statutUtilisateur = value;
                this.OnPropertyChanged(nameof(StatutUtilisateur));
                // false = adherent, true = admin
            }

        }
        public bool Connecter
        {
            get => connecter;
            set
            {
                connecter = value;
                this.OnPropertyChanged(nameof(Connecter));
                // est ce que quelqu'un est connecté? false = non, true = oui
            }

        }
        public string UsernameAdmin
        {
            get => usernameAdmin;
            set
            {
                usernameAdmin = value;
                this.OnPropertyChanged(nameof(UsernameAdmin));
            }

        }
        public string Mdp
        {
            get => mdp;
            set
            {
                mdp = value;
                this.OnPropertyChanged(nameof(Mdp));
            }

        }
        public string UsernameAdherent
        {
            get => usernameAdherent;
            set
            {
                usernameAdherent = value;
                this.OnPropertyChanged(nameof(UsernameAdherent));
            }
        }
        public Visibility Afficher
        {
            get
            {
                if (SingletonUtilisateur.getInstance().Connecter == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public string MatriculeAdherent
        {
            get => matriculeAdherent;
            set
            {
                matriculeAdherent = value;
                this.OnPropertyChanged(nameof(MatriculeAdherent));
            }
        }


        // remettre toutes les informations nulle
        public void supprimerInfoUtilisateur()
        {
            StatutUtilisateur = false;
            Connecter = false;
            UsernameAdmin = "";
            Mdp = "";
            UsernameAdherent = "";
            StatutUtilisateur = false;
        }







        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
