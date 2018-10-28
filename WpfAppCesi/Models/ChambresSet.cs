namespace WpfAppCesi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChambresSet")]
    public partial class ChambresSet
    {
        #region Attributs privés
        private int id;
        private string nom;
        private bool climatisation;
        private int nbLits;
        private int keyHotel;
        private HotelsSet hotel;
        private ICollection<ReservationSet> reservationsSet;
        #endregion

        #region Propriétés
        public int Id { get => id; set => id = value; }

        [Required]
        public string Nom { get => nom; set => nom = value; }

        public bool Climatisation { get => climatisation; set => climatisation = value; }

        public int NbLits { get => nbLits; set => nbLits = value; }

        //Foreign key
        public int KeyHotel { get => keyHotel; set => keyHotel = value; }

        public HotelsSet Hotel { get => hotel; set => hotel = value; }

        public virtual ICollection<ReservationSet> ReservationsSet { get => reservationsSet; set => reservationsSet = value; }
        #endregion
    }
}
