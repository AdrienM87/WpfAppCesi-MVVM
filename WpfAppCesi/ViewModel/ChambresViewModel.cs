using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCesi.ViewModel
{
    public class ChambresViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool VMsupprimerChambre(ChambresSet chambre)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.SupprimerChambre(chambre);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool VMvaliderChambre(int idChambre, string nom, bool climatisation, int nbLits, int keyHotel)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.ValiderChambre(idChambre, nom, climatisation, nbLits, keyHotel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
