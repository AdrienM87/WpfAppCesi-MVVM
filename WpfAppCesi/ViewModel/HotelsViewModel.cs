using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppCesi.ViewModel
{
    public class HotelsViewModel : INotifyPropertyChanged
    {
        //temp constructeur
        public HotelsViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }
        }

        private ObservableCollection<HotelsSet> hotelsCollection;

        public ObservableCollection<HotelsSet> HotelsCollection
        {
            get { return hotelsCollection; }
            set
            {
                if (value != hotelsCollection)
                {
                    hotelsCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private HotelsSet hotelSelected;

        public HotelsSet HotelSelected
        {
            get { return hotelSelected; }
            set
            {
                if (value != hotelSelected)
                {
                    hotelSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand RazPageHotel
        {

            get
            {
                return razPageHotel;
            }


        }

        private ICommand razPageHotel = new RelayCommand<HotelsSet>((hotel) =>
        {
            hotel.Nom = "";
            hotel.Capacite = 0;
            hotel.Localisation = "";
            hotel.Pays = "";
        });
        //¤ les boutons ajouts ne sont pas fonctionnels (Icommand)

        public bool VMsupprimerHotel(HotelsSet hotel)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.SupprimerHotel(hotel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool VMvaliderHotel(int idHotel, string nom, int capacite, string localisation, string pays)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.ValiderHotel(idHotel, nom, capacite, localisation, pays);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public HashSet<HotelsSet> VMgetHotels()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.GetAllHotels();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
