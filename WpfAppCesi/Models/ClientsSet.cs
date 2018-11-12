using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace WpfAppCesi
{
    [Table("ClientsSet")]
    public partial class ClientsSet
    {
        #region Attributs privés
        private int id;
        private string prenom;
        private string nom;
        private DateTime dateNaissance;
        private ICollection<ReservationSet> reservationsSet;
        #endregion

        #region Propriétés
        public int Id { get => id; set => id = value; }

        [Required]
        public string Prenom { get => prenom; set => prenom = value; }

        [Required]
        public string Nom { get => nom; set => nom = value; }

        [Required]
        public DateTime DateNaissance { get => dateNaissance; set => dateNaissance = value; }

        public virtual ICollection<ReservationSet> ReservationsSet { get => reservationsSet; set => reservationsSet = value; }
        #endregion
    }
}
