namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutTableReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dateDebut = c.DateTime(nullable: false),
                        dateFin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReservationSet");
        }
    }
}
