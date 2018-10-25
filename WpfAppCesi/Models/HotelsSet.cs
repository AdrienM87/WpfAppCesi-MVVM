namespace WpfAppCesi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HotelsSet")]
    public partial class HotelsSet
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public int Capacite { get; set; }

        public string Localisation { get; set; }

        public string Pays { get; set; }
    }
}
