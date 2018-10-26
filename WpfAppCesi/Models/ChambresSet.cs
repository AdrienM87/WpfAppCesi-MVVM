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
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public bool Climatisation { get; set; }

        public int NbLits { get; set; }

        //Foreign key
        public int keyHotel { get; set; }

        public HotelsSet Hotel { get; set; }

        public virtual ICollection<ReservationSet> ReservationsSet { get; set; }
    }
}
