using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace WpfAppCesi
{
    [Table("ReservationSet")]
    public partial class ReservationSet
    {
        private int id;
        private DateTime dateDebut;
        private DateTime dateFin;
        private int keyClient;
        private ClientsSet client;
        private int keyChambre;
        private ChambresSet chambres;

        public int Id { get => id; set => id = value; }

        [Required]
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }

        [Required]
        public DateTime DateFin { get => dateFin; set => dateFin = value; }

        //foreign key
        public int KeyClient { get => keyClient; set => keyClient = value; }

        public ClientsSet Client { get => client; set => client = value; }

        //foreign key
        public int KeyChambre { get => keyChambre; set => keyChambre = value; }

        public ChambresSet Chambres { get => chambres; set => chambres = value; }
    }
}