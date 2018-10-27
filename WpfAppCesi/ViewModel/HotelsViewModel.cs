using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCesi.ViewModel
{
    public class HotelsViewModel : INotifyPropertyChanged
    {
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
            set { hotelsCollection = value; NotifyPropertyChanged(); }
        }

        private HotelsSet hotel;

        public HotelsSet Hotel
        {
            get { return hotel; }
            set { hotel = value; NotifyPropertyChanged(); }
        }
    }
}
