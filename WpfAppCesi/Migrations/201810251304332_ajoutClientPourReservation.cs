namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutClientPourReservation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationSet", "ReservationSet_Id", "dbo.ReservationSet");
            DropIndex("dbo.ReservationSet", new[] { "ReservationSet_Id" });
            AddColumn("dbo.ClientsSet", "ReservationSet_Id", c => c.Int());
            CreateIndex("dbo.ClientsSet", "ReservationSet_Id");
            AddForeignKey("dbo.ClientsSet", "ReservationSet_Id", "dbo.ReservationSet", "Id");
            DropColumn("dbo.ReservationSet", "ReservationSet_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationSet", "ReservationSet_Id", c => c.Int());
            DropForeignKey("dbo.ClientsSet", "ReservationSet_Id", "dbo.ReservationSet");
            DropIndex("dbo.ClientsSet", new[] { "ReservationSet_Id" });
            DropColumn("dbo.ClientsSet", "ReservationSet_Id");
            CreateIndex("dbo.ReservationSet", "ReservationSet_Id");
            AddForeignKey("dbo.ReservationSet", "ReservationSet_Id", "dbo.ReservationSet", "Id");
        }
    }
}
