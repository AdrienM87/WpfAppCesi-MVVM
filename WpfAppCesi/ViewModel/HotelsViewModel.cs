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
        #region Interface PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
            {
                //PropertyChanged(this, new PropertyChangedEventArgs(str));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
            }
        }
        #endregion

        #region Constructeur
        public HotelsViewModel()
        {
            HotelsCollection = VMgetHotels();
        }
        #endregion


        private ObservableCollection<HotelsSet> hotelsCollection;
        public ObservableCollection<HotelsSet> HotelsCollection
        {
            //temp
            get
            {
                //return VMgetHotels();
                return hotelsCollection;
            }
            set
            {
                if (value != hotelsCollection)
                {
                    hotelsCollection = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //todo verifier utilité
        private List<HotelsSet> lesHotels;
        public List<HotelsSet> LesHotels
        {
            get
            {
                return lesHotels;
            }

            set
            {
                lesHotels = value;
                NotifyPropertyChanged();
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

        //todo verifier l'utilité
        public void HotelSelectedViewModel()
        {
            //temp really?
            //HotelsCollection = new HashSet<HotelsSet>();

            //HotelSelected = new HotelsSet()
            //{
            //    Nom = "Bristol",
            //    Capacite = 5,
            //    Localisation = "Paris",
            //    Pays = "FR"
            //};

            //HotelsCollection.Add(HotelSelected);
        }



        //todo supprimer à la fin
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

        public ObservableCollection<HotelsSet> VMgetHotels()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    HashSet<HotelsSet> hotelHashSet = db.GetAllHotels();

                    return new ObservableCollection<HotelsSet> (hotelHashSet);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region Commands

        private ICommand btModifierHotel_Click;
        public ICommand BtModifierHotel_Click
        {
            get
            {
                if (btModifierHotel_Click == null)
                {
                    btModifierHotel_Click = new RelayCommand<HotelsSet>((obj) =>
                    {
                        try
                        {
                            if (obj != null)
                            {
                                int id = obj.Id;
                                string nom = obj.Nom;
                                int capacite = obj.Capacite;
                                string localisation = obj.Localisation;
                                string pays = obj.Pays;

                                using (var db = new ModelBooking())
                                {
                                    db.ValiderHotel(id, nom, capacite, localisation, pays);
                                }
                            }
                            else
                            {
                                return;
                            }

                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    });

        }
                return btModifierHotel_Click;
            }
        }

        private ICommand btSupprimerHotel_Click;
        public ICommand BtSupprimerHotel_Click
        {
            get
            {
                if (btSupprimerHotel_Click == null)
                {
                    btSupprimerHotel_Click = new RelayCommand<HotelsSet>((obj) =>
                    {
                        try
                        {
                            if (obj != null)
                            {
                                using (var db = new ModelBooking())
                                {
                                    db.SupprimerHotel(obj);
                                }

                                foreach (HotelsSet h in HotelsCollection)
                                {
                                    if (h.Id == obj.Id)
                                    {
                                        HotelsCollection.Remove(h);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    });
                }
                return btSupprimerHotel_Click;
            }
        }

        #endregion

        
    }
}
