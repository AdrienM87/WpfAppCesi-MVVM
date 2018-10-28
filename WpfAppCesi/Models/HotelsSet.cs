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
        private int id;
        private string nom;
        private int capacite;
        private string localisation;
        private string pays;
        private ICollection<ChambresSet> chambresSet;

        public int Id { get => id; set => id = value; }

        [Required]
        public string Nom { get => nom; set => nom = value; }

        public int Capacite { get => capacite; set => capacite = value; }

        public string Localisation { get => localisation; set => localisation = value; }

        public string Pays { get => pays; set => pays = value; }

        public virtual ICollection<ChambresSet> ChambresSet { get => chambresSet; set => chambresSet = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
