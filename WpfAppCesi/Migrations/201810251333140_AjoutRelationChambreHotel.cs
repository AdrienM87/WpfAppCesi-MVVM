namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjoutRelationChambreHotel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChambresSet", "ChambresSetId", c => c.Int(nullable: false));
            AddColumn("dbo.ChambresSet", "HotelsSet_Id", c => c.Int());
            CreateIndex("dbo.ChambresSet", "ChambresSetId");
            CreateIndex("dbo.ChambresSet", "HotelsSet_Id");
            AddForeignKey("dbo.ChambresSet", "ChambresSetId", "dbo.ChambresSet", "Id");
            AddForeignKey("dbo.ChambresSet", "HotelsSet_Id", "dbo.HotelsSet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChambresSet", "HotelsSet_Id", "dbo.HotelsSet");
            DropForeignKey("dbo.ChambresSet", "ChambresSetId", "dbo.ChambresSet");
            DropIndex("dbo.ChambresSet", new[] { "HotelsSet_Id" });
            DropIndex("dbo.ChambresSet", new[] { "ChambresSetId" });
            DropColumn("dbo.ChambresSet", "HotelsSet_Id");
            DropColumn("dbo.ChambresSet", "ChambresSetId");
        }
    }
}
