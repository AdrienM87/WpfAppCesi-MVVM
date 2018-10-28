using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCesi.ViewModel
{
    public class ClientsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool VMsupprimerClient(ClientsSet client)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.SupprimerClient(client);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool VMvaliderClient(int idClient, string prenom, string nom, DateTime dateNaissance)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    return db.ValiderClient(idClient, prenom, nom, dateNaissance);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
