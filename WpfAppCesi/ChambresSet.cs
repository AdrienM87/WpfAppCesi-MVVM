namespace WpfAppCesi
{
    using System.Collections.Generic;
    
    public partial class ChambresSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChambresSet()
        {
            this.HotelsSet = new HashSet<HotelsSet>();
        }
    
        public int Id { get; set; }
        public string Nom { get; set; }
        public bool Climatisation { get; set; }
        public int NbLits { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HotelsSet> HotelsSet { get; set; }
    }
}
