using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Activite : INotifyPropertyChanged
    {
        int idActivite;
        string nom_activite;
        int idCategorie;
        string description;
        double cout_organisation;
        double prix_vente_client;

        string typeCategorie;

        // statistique
        // le nombre d'activités total
        int nb_activites;
        // nbr_seance_par_activite
        int nbr_seance;

        public Activite()
        {
            this.IdActivite = -1;
            this.Nom_activite = "nom_activite";
            this.IdCategorie = -1;
            this.Description = "description";
            this.Cout_organisation = -1.00;
            this.Prix_vente_client = -1.00;
        }
        public Activite(string nom_activite, int idCategorie, string description, double cout_organisation, double prix_vente_client)
        {
            this.Nom_activite = nom_activite;
            this.IdCategorie = idCategorie;
            this.Description = description;
            this.Cout_organisation = cout_organisation;
            this.Prix_vente_client = prix_vente_client;
        }

        public Activite(int idActivite, string nom_activite, int idCategorie, string description, double cout_organisation, double prix_vente_client)
        {
            this.IdActivite = idActivite;
            this.Nom_activite = nom_activite;
            this.IdCategorie = idCategorie;
            this.Description = description;
            this.Cout_organisation = cout_organisation;
            this.Prix_vente_client = prix_vente_client;
        }
        public Activite(int idActivite, string nom_activite, int idCategorie, string description, double cout_organisation, double prix_vente_client, string typeCategorie)
        {
            this.IdActivite = idActivite;
            this.Nom_activite = nom_activite;
            this.IdCategorie = idCategorie;
            this.Description = description;
            this.Cout_organisation = cout_organisation;
            this.Prix_vente_client = prix_vente_client;

            this.TypeCategorie = typeCategorie;
        }
        // statistique
        public Activite(int nb_activites)
        {
            this.nb_activites = nb_activites;
        }

        public Activite(int idActivite, string nom_activite, int nbr_seance)
        {
            this.idActivite = idActivite;
            this.nom_activite = nom_activite;
            this.nbr_seance = nbr_seance;
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
        public string Nom_activite
        {
            get => nom_activite;
            set
            {
                nom_activite = value;
                this.OnPropertyChanged(nameof(Nom_activite));
            }
        }
        public int IdCategorie
        {
            get => idCategorie;
            set
            {
                idCategorie = value;
                this.OnPropertyChanged(nameof(IdCategorie));
            }
        }
        public string TypeCategorie
        {
            get => typeCategorie;
            set
            {
                typeCategorie = value;
                this.OnPropertyChanged(nameof(TypeCategorie));
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                this.OnPropertyChanged(nameof(Description));
            }
        }
        public double Cout_organisation
        {
            get => cout_organisation;
            set
            {
                cout_organisation = value;
                this.OnPropertyChanged(nameof(Cout_organisation));
            }
        }
        public double Prix_vente_client
        {
            get => prix_vente_client;
            set
            {
                prix_vente_client = value;
                this.OnPropertyChanged(nameof(Prix_vente_client));
            }
        }
        // le nombre d'activités total
        public int Nb_activites
        {
            get => nb_activites;
            set
            {
                nb_activites = value;
                this.OnPropertyChanged(nameof(Nb_activites));
            }
        }
        // nbr_seance_par_activite
        public int Nbr_seance
        {
            get => nbr_seance;
            set
            {
                nbr_seance = value;
                this.OnPropertyChanged(nameof(Nbr_seance));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
