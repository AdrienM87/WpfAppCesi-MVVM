namespace WpfAppCesi
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelBooking : DbContext
    {
        public ModelBooking()
            : base("name=ModelBooking")
        {
        }

        public virtual DbSet<ChambresSet> ChambresSet { get; set; }
        public virtual DbSet<HotelsSet> HotelsSet { get; set; }
        public virtual DbSet<UsersSet> UsersSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChambresSet>()
                .HasMany(e => e.HotelsSet)
                .WithRequired(e => e.ChambresSet)
                .WillCascadeOnDelete(false);
        }
    }
}
