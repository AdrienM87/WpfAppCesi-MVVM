using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace WpfAppCesi
{
    [Table("HotelsSet")]
    public partial class HotelsSet
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Attributs privés
        private int id;
        private string nom;
        private int capacite;
        private string localisation;
        private string pays;
        private ICollection<ChambresSet> chambresSet;
        #endregion

        #region Propriétés

        public int Id
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                    }
                }
            }
        }

        [Required]
        public string Nom {
            get => nom;
            set
            {
                if (value != nom)
                {
                    nom = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Nom"));
                    }
                }
            }
        }

        public int Capacite
        {
            get => capacite;
            set
            {
                if (value != capacite)
                {
                    capacite = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Capacite"));
                    }
                }
            }
        }

        public string Localisation
        {
            get => localisation;
            set
            {
                if (value != localisation)
                {
                    localisation = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Localisation"));
                    }
                }
            }
        }

        public string Pays
        {
            get => pays;
            set
            {
                if (value != pays)
                {
                    pays = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Pays"));
                    }
                }
            }
        }

        public virtual ICollection<ChambresSet> ChambresSet { get => chambresSet; set => chambresSet = value; }

        #endregion
    }
}
