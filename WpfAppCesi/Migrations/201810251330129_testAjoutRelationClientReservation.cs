namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testAjoutRelationClientReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationSet", "ClientsSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.ReservationSet", "ClientsSetId");
            AddForeignKey("dbo.ReservationSet", "ClientsSetId", "dbo.ClientsSet", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationSet", "ClientsSetId", "dbo.ClientsSet");
            DropIndex("dbo.ReservationSet", new[] { "ClientsSetId" });
            DropColumn("dbo.ReservationSet", "ClientsSetId");
        }
    }
}
