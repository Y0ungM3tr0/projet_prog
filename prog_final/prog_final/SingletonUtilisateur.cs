using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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


        bool statutUtilisateur; // false = adherent, true = admin
        bool connecter; // est ce que quelqu'un est connecté? false = non, true = oui

        string usernameAdmin;
        string mdp;
        string usernameAdherent;

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












        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
