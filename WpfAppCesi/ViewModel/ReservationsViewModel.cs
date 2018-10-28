using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCesi.ViewModel
{
    public class ReservationsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool VMsupprimerReservation(ReservationSet reservation)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.SupprimerReservation(reservation);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool VMvaliderReservation(int idReservation, DateTime dateDebut, DateTime dateFin, int keyClient, int keyChambre)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.ValiderReservation(idReservation, dateDebut, dateFin, keyClient, keyChambre);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public HashSet<ReservationSet> VMgetReservations()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.GetAllReservations();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
