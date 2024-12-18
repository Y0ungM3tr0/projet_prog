﻿using System;
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
        string type;
        string description;
        double cout_organisation;
        double prix_vente_client;

        public Activite()
        {
            this.IdActivite = -1;
            this.Nom_activite = "nom_activite";
            this.Type = "type";
            this.Description = "description";
            this.Cout_organisation = -1.00;
            this.Prix_vente_client = -1.00;
        }
        public Activite(string nom_activite, string type, string description, double cout_organisation, double prix_vente_client)
        {
            this.Nom_activite = nom_activite;
            this.Type = type;
            this.Description = description;
            this.Cout_organisation = cout_organisation;
            this.Prix_vente_client = prix_vente_client;
        }

        public Activite(int idActivite, string nom_activite, string type, string description, double cout_organisation, double prix_vente_client)
        {
            this.IdActivite = idActivite;
            this.Nom_activite = nom_activite;
            this.Type = type;
            this.description = description;
            this.Cout_organisation = cout_organisation;
            this.Prix_vente_client = prix_vente_client;
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

        public string Type
        {
            get => type;
            set
            {
                type = value;
                this.OnPropertyChanged(nameof(Type));
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



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
