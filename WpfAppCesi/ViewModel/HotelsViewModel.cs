using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
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

        #region Constructor
        public HotelsViewModel()
        {
            HotelsCollection = getAllHotels();
        }
        #endregion

        #region Properties
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

        public ObservableCollection<HotelsSet> getAllHotels()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    HashSet<HotelsSet> hotelHashSet = db.GetAllHotels();

                    return new ObservableCollection<HotelsSet>(hotelHashSet);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
        #endregion

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

        private ICommand btAjouterHotel_Click;
        public ICommand BtAjouterHotel_Click
        {
            get
            {
                if (btAjouterHotel_Click == null)
                {
                    btAjouterHotel_Click = new RelayCommand<string>((obj) =>
                    {
                        try
                        {
                            if (obj != null)
                            {
                                HotelsSet h = new HotelsSet();
                                h.Id = 0;
                                h.Nom = obj;
                                h.Capacite = 0;
                                h.Localisation = "";
                                h.Pays = "";

                                using (var db = new ModelBooking())
                                {
                                    db.ValiderHotel(h.Id, h.Nom, h.Capacite, h.Localisation, h.Pays);
                                }
                                HotelsCollection.Add(h);
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
                return btAjouterHotel_Click;
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

        #region Value Conversion
        //Partie non terminée : essai pour récupérer de multiples champs depuis la vue
        [ValueConversion(typeof(string), typeof(String))]
        public class MyConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return string.Format("{0}:{1}", (string)value, (string)parameter);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {

                return DependencyProperty.UnsetValue;
            }
        }
        #endregion
    }
}
