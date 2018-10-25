namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutRelationChambreReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationSet", "ChambresSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReservationSet", "ChambresSetId");
            AddForeignKey("dbo.ReservationSet", "ChambresSetId", "dbo.ChambresSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationSet", "ChambresSetId", "dbo.ChambresSet");
            DropIndex("dbo.ReservationSet", new[] { "ChambresSetId" });
            DropColumn("dbo.ReservationSet", "ChambresSetId");
        }
    }
}
