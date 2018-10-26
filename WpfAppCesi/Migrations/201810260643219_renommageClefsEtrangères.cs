namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renommageClefsEtrangères : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChambresSet", "HotelsSetId", "dbo.HotelsSet");
            DropForeignKey("dbo.ReservationSet", "ChambresSetId", "dbo.ChambresSet");
            DropForeignKey("dbo.ReservationSet", "ClientsSetId", "dbo.ClientsSet");
            DropIndex("dbo.ChambresSet", new[] { "HotelsSetId" });
            DropIndex("dbo.ReservationSet", new[] { "ClientsSetId" });
            DropIndex("dbo.ReservationSet", new[] { "ChambresSetId" });
            RenameColumn(table: "dbo.ChambresSet", name: "HotelsSetId", newName: "Hotel_Id");
            RenameColumn(table: "dbo.ReservationSet", name: "ChambresSetId", newName: "Chambres_Id");
            RenameColumn(table: "dbo.ReservationSet", name: "ClientsSetId", newName: "Client_Id");
            AddColumn("dbo.ChambresSet", "keyHotel", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationSet", "keyClient", c => c.Int(nullable: false));
            AddColumn("dbo.ReservationSet", "keyChambre", c => c.Int(nullable: false));
            AlterColumn("dbo.ChambresSet", "Hotel_Id", c => c.Int());
            AlterColumn("dbo.ReservationSet", "Client_Id", c => c.Int());
            AlterColumn("dbo.ReservationSet", "Chambres_Id", c => c.Int());
            CreateIndex("dbo.ChambresSet", "Hotel_Id");
            CreateIndex("dbo.ReservationSet", "Chambres_Id");
            CreateIndex("dbo.ReservationSet", "Client_Id");
            AddForeignKey("dbo.ChambresSet", "Hotel_Id", "dbo.HotelsSet", "Id");
            AddForeignKey("dbo.ReservationSet", "Chambres_Id", "dbo.ChambresSet", "Id");
            AddForeignKey("dbo.ReservationSet", "Client_Id", "dbo.ClientsSet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationSet", "Client_Id", "dbo.ClientsSet");
            DropForeignKey("dbo.ReservationSet", "Chambres_Id", "dbo.ChambresSet");
            DropForeignKey("dbo.ChambresSet", "Hotel_Id", "dbo.HotelsSet");
            DropIndex("dbo.ReservationSet", new[] { "Client_Id" });
            DropIndex("dbo.ReservationSet", new[] { "Chambres_Id" });
            DropIndex("dbo.ChambresSet", new[] { "Hotel_Id" });
            AlterColumn("dbo.ReservationSet", "Chambres_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ReservationSet", "Client_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ChambresSet", "Hotel_Id", c => c.Int(nullable: false));
            DropColumn("dbo.ReservationSet", "keyChambre");
            DropColumn("dbo.ReservationSet", "keyClient");
            DropColumn("dbo.ChambresSet", "keyHotel");
            RenameColumn(table: "dbo.ReservationSet", name: "Client_Id", newName: "ClientsSetId");
            RenameColumn(table: "dbo.ReservationSet", name: "Chambres_Id", newName: "ChambresSetId");
            RenameColumn(table: "dbo.ChambresSet", name: "Hotel_Id", newName: "HotelsSetId");
            CreateIndex("dbo.ReservationSet", "ChambresSetId");
            CreateIndex("dbo.ReservationSet", "ClientsSetId");
            CreateIndex("dbo.ChambresSet", "HotelsSetId");
            AddForeignKey("dbo.ReservationSet", "ClientsSetId", "dbo.ClientsSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReservationSet", "ChambresSetId", "dbo.ChambresSet", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ChambresSet", "HotelsSetId", "dbo.HotelsSet", "Id", cascadeDelete: true);
        }
    }
}
