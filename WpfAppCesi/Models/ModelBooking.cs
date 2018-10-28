namespace WpfAppCesi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelBooking : DbContext
    {
        public ModelBooking() : base("name=ModelBooking") { }

        public virtual DbSet<ChambresSet> ChambresSet { get; set; }
        public virtual DbSet<HotelsSet> HotelsSet { get; set; }
        public virtual DbSet<ClientsSet> ClientsSet { get; set; }
        public virtual DbSet<ReservationSet> ReservationsSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }

        #region Méthodes Hotel
        public bool SupprimerHotel(HotelsSet hotel)
        {
            try
            {
                var resultQuery = (from hotels in this.HotelsSet
                                   where hotels.Id == hotel.Id
                                   select hotels);

                if (resultQuery.Any())
                {
                    this.HotelsSet.Remove(resultQuery.First());
                    this.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public HotelsSet GetHotel(int idHotel)
        {
            try
            {
                var resultQuery = (from hotels in this.HotelsSet
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
                HotelsSet hotel;

                if (idHotel != 0) //si id!=0 alors l'hotel existe déjà
                {
                    hotel = GetHotel(idHotel);
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
                    this.HotelsSet.Add(hotel);
                }
                this.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
