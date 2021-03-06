namespace WpfAppCesi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class ModelBooking : DbContext
    {
        public ModelBooking() : base("name=ModelBooking") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }

        #region Loads des datas

        //temp
        //private void LoadDatas()
        //{
        //    LoadHotels();
        //    LoadChambres();
        //    LoadClients();
        //    LoadReservations();
        //}

        public HashSet<HotelsSet> GetAllHotels()
        {
            try
            {
                var resultQuery = (from hotels in this.HotelsSet select hotels);

                if (resultQuery.Any())
                {
                    return resultQuery.ToHashSet();
                }
                return new HashSet<HotelsSet>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public HashSet<ChambresSet> GetAllChambres()
        {
            try
            {
                var resultQuery = (from chambres in this.ChambresSet select chambres);

                if (resultQuery.Any())
                {
                    return resultQuery.ToHashSet();

                    //temp
                    //DataTable data = new DataTable();
                    //data.Columns.Add("N� chambre");
                    //data.Columns.Add("Nom de la chambre");
                    //data.Columns.Add("Climatisation");
                    //data.Columns.Add("Nombre de lits");
                    //data.Columns.Add("N� hotel de la chambre");

                    //foreach (ChambresSet ch in SetChambres)
                    //{
                    //    data.Rows.Add(new object[] { ch.Id, ch.Nom, ch.Climatisation, ch.NbLits, ch.keyHotel });
                    //}

                    //this.ChambresDataGrid.ItemsSource = (System.Collections.IEnumerable)data;
                }
                return new HashSet<ChambresSet>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public HashSet<ClientsSet> GetAllClients()
        {
            try
            {
                var resultQuery = (from clients in this.ClientsSet select clients);

                if (resultQuery.Any())
                {
                    return resultQuery.ToHashSet();
                }
                return new HashSet<ClientsSet>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public HashSet<ReservationSet> GetAllReservations()
        {
            try
            {
                var resultQuery = (from reservations in this.ReservationsSet select reservations);

                if (resultQuery.Any())
                {
                    return resultQuery.ToHashSet();
                }
                return new HashSet<ReservationSet>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region M�thodes Hotels
        public virtual DbSet<HotelsSet> HotelsSet { get; set; }
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

                if (idHotel != 0) //si id!=0 alors l'hotel existe d�j�
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

        #region M�thodes Chambres
        public virtual DbSet<ChambresSet> ChambresSet { get; set; }
        public bool SupprimerChambre(ChambresSet chambre)
        {
            try
            {
                var resultQuery = (from chambres in this.ChambresSet
                                   where chambres.Id == chambre.Id
                                   select chambres);

                if (resultQuery.Any())
                {
                    this.ChambresSet.Remove(resultQuery.First());
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
        public ChambresSet GetChambre(int idChambre)
        {
            try
            {
                var resultQuery = (from chambres in this.ChambresSet
                                   where chambres.Id == idChambre
                                   select chambres);

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
        public bool ValiderChambre(int idChambre, string nom, bool climatisation, int nbLits, int keyHotel)
        {
            try
            {
                ChambresSet chambre;

                if (idChambre != 0) //si id!=0 alors l'hotel existe d�j�
                {
                    chambre = GetChambre(idChambre);
                }
                else
                {
                    chambre = new ChambresSet();
                }
                chambre.Nom = nom;
                chambre.Climatisation = climatisation;
                chambre.NbLits = nbLits;
                chambre.KeyHotel = keyHotel;

                if (idChambre == 0)   //nouvel hotel
                {
                    this.ChambresSet.Add(chambre);
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

        #region M�thodes Clients
        public virtual DbSet<ClientsSet> ClientsSet { get; set; }
        public bool SupprimerClient(ClientsSet client)
        {
            try
            {
                var resultQuery = (from clients in this.ClientsSet
                                   where clients.Id == client.Id
                                   select clients);

                if (resultQuery.Any())
                {
                    this.ClientsSet.Remove(resultQuery.First());
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
        public ClientsSet GetClient(int idClient)
        {
            try
            {
                var resultQuery = (from clients in this.ClientsSet
                                   where clients.Id == idClient
                                   select clients);

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
        public bool ValiderClient(int idClient, string prenom, string nom, DateTime dateNaissance)
        {
            try
            {
                ClientsSet client;

                if (idClient != 0) //si id!=0 alors l'hotel existe d�j�
                {
                    client = GetClient(idClient);
                }
                else
                {
                    client = new ClientsSet();
                }
                client.Prenom = prenom;
                client.Nom = nom;
                client.DateNaissance = dateNaissance;

                if (idClient == 0)   //nouvel hotel
                {
                    this.ClientsSet.Add(client);
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

        #region M�thodes R�servations
        public virtual DbSet<ReservationSet> ReservationsSet { get; set; }
        public bool SupprimerReservation(ReservationSet reservation)
        {
            try
            {
                var resultQuery = (from reservations in this.ReservationsSet
                                   where reservations.Id == reservation.Id
                                   select reservations);

                if (resultQuery.Any())
                {
                    this.ReservationsSet.Remove(resultQuery.First());
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
        public ReservationSet GetReservation(int idReservation)
        {
            try
            {
                var resultQuery = (from reservations in this.ReservationsSet
                                   where reservations.Id == idReservation
                                   select reservations);

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
        public bool ValiderReservation(int idReservation, DateTime dateDebut, DateTime dateFin, int keyClient, int keyChambre)
        {
            try
            {
                ReservationSet reservation;

                if (idReservation != 0) //si id!=0 alors l'hotel existe d�j�
                {
                    reservation = GetReservation(idReservation);
                }
                else
                {
                    reservation = new ReservationSet();
                }
                reservation.DateDebut = dateDebut;
                reservation.DateFin = dateFin;
                reservation.KeyClient = keyClient;
                reservation.KeyChambre = keyChambre;

                if (idReservation == 0)   //nouvel hotel
                {
                    this.ReservationsSet.Add(reservation);
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
