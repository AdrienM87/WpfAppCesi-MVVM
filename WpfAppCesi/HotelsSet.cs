namespace WpfAppCesi
{  
    public partial class HotelsSet
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Capacite { get; set; }
        public int ChambresSetId { get; set; }
        public string Localisation { get; set; }

        public virtual ChambresSet ChambresSet { get; set; }
    }
}
