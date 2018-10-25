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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChambresSet()
        {
            HotelsSet = new HashSet<HotelsSet>();
        }

        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        public bool Climatisation { get; set; }

        public int NbLits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelsSet> HotelsSet { get; set; }
    }
}
