namespace WpfAppCesi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReservationSet")]
    public partial class ReservationSet
    {
        public int Id { get; set; }

        [Required]
        public DateTime dateDebut { get; set; }

        [Required]
        public DateTime dateFin { get; set; }

        //foreign key
        public int keyClient { get; set; }

        public ClientsSet Client { get; set; }

        //foreign key
        public int keyChambre { get; set; }

        public ChambresSet Chambres { get; set; }
    }
}