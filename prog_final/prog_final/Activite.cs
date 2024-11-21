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
        string nom;
        string description;
        string type;
        double coutOrganisation;
        double prixVente;

        public Activite()
        {
            this.idActivite = -1;
            this.nom = "nom";
            this.description = "description";
            this.type = "type";
            this.coutOrganisation = 0.00;
            this.prixVente = 0.00;
        }
        public Activite(string nom, string description, string type, double coutOrganisation, double prixVente)
        {
            this.nom = nom;
            this.description = description;
            this.type = type;
            this.coutOrganisation = coutOrganisation;
            this.prixVente = prixVente;
        }

        public Activite(int idActivite, string nom, string description, string type, double coutOrganisation, double prixVente)
        {
            this.idActivite = idActivite;
            this.nom = nom;
            this.description = description;
            this.type = type;
            this.coutOrganisation = coutOrganisation;
            this.prixVente = prixVente;
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

        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                this.OnPropertyChanged(nameof(Nom));
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

        public string Type
        {
            get => type;
            set
            {
                type = value;
                this.OnPropertyChanged(nameof(Type));
            }

        }

        public double CoutOrganisation
        {
            get => coutOrganisation;
            set
            {
                coutOrganisation = value;
                this.OnPropertyChanged(nameof(CoutOrganisation));
            }

        }

        public double PrixVente
        {
            get => prixVente;
            set
            {
                prixVente = value;
                this.OnPropertyChanged(nameof(PrixVente));
            }

        }






        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
