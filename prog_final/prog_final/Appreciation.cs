using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Appreciation : INotifyPropertyChanged
    {
        int idAppreciation;
        int idSeance;
        string matricule;
        double note_appreciation;

        public Appreciation()
        {
            this.IdAppreciation = -1;
            this.IdSeance = -1;
            this.Matricule = "matricule";
            this.Note_appreciation = -1;
        }

        public Appreciation(int idAppreciation, int idSeance, string matricule, double note_appreciation)
        {
            this.IdAppreciation = idAppreciation;
            this.IdSeance = idSeance;
            this.Matricule = matricule;
            this.Note_appreciation = note_appreciation;
        }

        public int IdAppreciation
        {
            get => idAppreciation;
            set
            {
                idAppreciation = value;
                this.OnPropertyChanged(nameof(IdAppreciation));
            }
        }

        public int IdSeance
        {
            get => idSeance;
            set
            {
                idSeance = value;
                this.OnPropertyChanged(nameof(IdSeance));
            }
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

        public double Note_appreciation
        {
            get => note_appreciation;
            set
            {
                note_appreciation = value;
                this.OnPropertyChanged(nameof(Note_appreciation));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
