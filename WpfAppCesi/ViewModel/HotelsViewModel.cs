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
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }
        }
        #endregion

        #region Constructeur
        public HotelsViewModel()
        {
            HotelsCollection = VMgetHotels();
        }
        #endregion


        private HashSet<HotelsSet> hotelsCollection;
        public HashSet<HotelsSet> HotelsCollection
        {
            //temp
            get
            {
                return VMgetHotels();
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
            HotelsCollection = new HashSet<HotelsSet>();

            HotelSelected = new HotelsSet()
            {
                Nom = "Bristol",
                Capacite = 5,
                Localisation = "Paris",
                Pays = "FR"
            };

            HotelsCollection.Add(HotelSelected);
        }



        //perso opérationnel
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

        //todo 
        //public ICommand VMgetHotelSelected() = new RelayCommand<HotelsSet>((int idHotel)) => {

        private ICommand btValiderHotel_Click;
        public ICommand BtValiderHotel_Click
        {
            get
            {
                if (btValiderHotel_Click == null)
                {
                    btValiderHotel_Click = new RelayCommand<HotelsSet>((obj) =>
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
                return btValiderHotel_Click;
            }
        }



        //private ICommand hotelsDataGrid_SelectionChanged;
        //public ICommand HotelsDataGrid_SelectionChanged
        //{
        //    get
        //    {
        //        hotelsDataGrid_SelectionChanged = new RelayCommand<HotelsSet>((obj) =>
        //        {
        //            if (obj != null)
        //            {
        //                HotelSelected.Id = obj.Id;
        //                HotelSelected.Nom = obj.Nom;
        //                HotelSelected.Capacite = obj.Capacite;
        //                HotelSelected.Localisation = obj.Localisation;
        //                HotelSelected.Pays = obj.Pays;
        //            }
        //        });
        //        return hotelsDataGrid_SelectionChanged;
        //    }
        //}

        //{
        //    get
        //    {
        //        try
        //        {
        //            if (MainWindow.HotelsDataGrid.SelectedItem == null)
        //            {
        //                return;
        //            }
        //            HotelsSet hotel = (HotelsSet)HotelsDataGrid.SelectedItem;
        //            if (hotel == null)
        //            {
        //                return;
        //            }

        //            this.BtAjouterHotel.IsEnabled = true;
        //            this.BtSupprimerHotel.IsEnabled = true;

        //            this.LbIdHotel.Content = hotel.Id.ToString();
        //            this.TbNomHotel.Text = hotel.Nom;
        //            this.TbCapacite.Text = hotel.Capacite.ToString();
        //            this.TbLocalisation.Text = hotel.Localisation;
        //            this.TbPays.Text = hotel.Pays;

        //            ActiverDesactiverControlesHotels(true);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}

        //en test
        //public ICommand RazPageHotel
        //{
        //    get
        //    {
        //        return razPageHotel;
        //    }
        //}

        //private ICommand razPageHotel = new RelayCommand<HotelsSet>((hotel) =>
        //{
        //    hotel.Nom = "";
        //    hotel.Capacite = 0;
        //    hotel.Localisation = "";
        //    hotel.Pays = "";
        //});
        //¤ ce boutons ajout ne sont pas fonctionnels (Icommand)

    }
}
