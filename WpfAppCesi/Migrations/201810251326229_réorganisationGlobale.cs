namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class réorganisationGlobale : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HotelsSet", "ChambresSetId", "dbo.ChambresSet");
            DropForeignKey("dbo.ClientsSet", "ReservationSet_Id", "dbo.ReservationSet");
            DropIndex("dbo.HotelsSet", new[] { "ChambresSetId" });
            DropIndex("dbo.ClientsSet", new[] { "ReservationSet_Id" });
            DropColumn("dbo.HotelsSet", "ChambresSetId");
            DropColumn("dbo.ClientsSet", "ReservationSet_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientsSet", "ReservationSet_Id", c => c.Int());
            AddColumn("dbo.HotelsSet", "ChambresSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.ClientsSet", "ReservationSet_Id");
            CreateIndex("dbo.HotelsSet", "ChambresSetId");
            AddForeignKey("dbo.ClientsSet", "ReservationSet_Id", "dbo.ReservationSet", "Id");
            AddForeignKey("dbo.HotelsSet", "ChambresSetId", "dbo.ChambresSet", "Id");
        }
    }
}
