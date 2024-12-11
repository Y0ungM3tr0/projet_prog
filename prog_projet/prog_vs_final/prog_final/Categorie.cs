using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Categorie : INotifyPropertyChanged
    {
        int idCategorie;
        string type;

        public Categorie()
        {
            this.idCategorie = -1;
            this.Type = "type";
        }
        public Categorie(int idCategorie, string type)
        {
            this.IdCategorie = idCategorie;
            this.Type = type;
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
        public string Type
        {
            get => type;
            set
            {
                type = value;
                this.OnPropertyChanged(nameof(Type));
            }

        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
