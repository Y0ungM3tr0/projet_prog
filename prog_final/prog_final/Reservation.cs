using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_final
{
    internal class Reservation : INotifyPropertyChanged
    {
        int idReservation;
        int idSeance;
        string matricule;

        public Reservation()
        {
            this.IdReservation = -1;
            this.IdSeance = -1;
            this.Matricule = "matricule";
        }

        public Reservation(int idReservation, int idSeance, string matricule)
        {
            this.IdReservation = idReservation;
            this.IdSeance = idSeance;
            this.Matricule = matricule;
        }

        public int IdReservation
        {
            get => idReservation;
            set
            {
                idReservation = value;
                this.OnPropertyChanged(nameof(IdReservation));
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
