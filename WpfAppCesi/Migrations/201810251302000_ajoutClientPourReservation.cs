namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutClientPourReservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationSet", "ReservationSet_Id", c => c.Int());
            CreateIndex("dbo.ReservationSet", "ReservationSet_Id");
            AddForeignKey("dbo.ReservationSet", "ReservationSet_Id", "dbo.ReservationSet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationSet", "ReservationSet_Id", "dbo.ReservationSet");
            DropIndex("dbo.ReservationSet", new[] { "ReservationSet_Id" });
            DropColumn("dbo.ReservationSet", "ReservationSet_Id");
        }
    }
}
