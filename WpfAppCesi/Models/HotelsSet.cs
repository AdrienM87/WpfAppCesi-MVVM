namespace WpfAppCesi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("HotelsSet")]
    public partial class HotelsSet : INotifyPropertyChanged
    {
        #region Attributs privés
        private int id;
        private string nom;
        private int capacite;
        private string localisation;
        private string pays;
        private ICollection<ChambresSet> chambresSet;
        #endregion

        #region Propriétés
        public int Id { get => id; set => id = value; }

        [Required]
        public string Nom { get => nom; set => nom = value; }

        public int Capacite { get => capacite; set => capacite = value; }

        public string Localisation { get => localisation; set => localisation = value; }

        public string Pays { get => pays; set => pays = value; }

        public virtual ICollection<ChambresSet> ChambresSet { get => chambresSet; set => chambresSet = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Méthodes
        public bool SupprimerHotel()
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    var resultQuery = (from hotel in db.HotelsSet
                                       where hotel.Id == Id
                                       select hotel);

                    if (resultQuery.Any())
                    {
                        db.HotelsSet.Remove(resultQuery.First());
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public HotelsSet GetHotel(int idHotel, ModelBooking db)
        {
            try
            {
                var resultQuery = (from hotels in db.HotelsSet
                                   where hotels.Id == idHotel
                                   select hotels);

                if (resultQuery.Any())
                {
                    return resultQuery.First();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ValiderHotel(int idHotel, string nom, int capacite, string localisation, string pays)
        {
            try
            {
                using (var db = new ModelBooking())
                {
                    HotelsSet hotel;

                    if (idHotel != 0) //si id!=0 alors l'hotel existe déjà
                    {
                        hotel = GetHotel(idHotel, db);
                    }
                    else
                    {
                        hotel = new HotelsSet();
                    }


                    hotel.Nom = nom;
                    hotel.Capacite = capacite;
                    hotel.Localisation = localisation;
                    hotel.Pays = pays;

                    if (idHotel == 0)   //nouvel hotel
                    {
                        db.HotelsSet.Add(hotel);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
