namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionNommageRelationChambreHotel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChambresSet", "ChambresSetId", "dbo.ChambresSet");
            DropForeignKey("dbo.ChambresSet", "HotelsSet_Id", "dbo.HotelsSet");
            DropIndex("dbo.ChambresSet", new[] { "ChambresSetId" });
            DropIndex("dbo.ChambresSet", new[] { "HotelsSet_Id" });
            RenameColumn(table: "dbo.ChambresSet", name: "HotelsSet_Id", newName: "HotelsSetId");
            AlterColumn("dbo.ChambresSet", "HotelsSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.ChambresSet", "HotelsSetId");
            AddForeignKey("dbo.ChambresSet", "HotelsSetId", "dbo.HotelsSet", "Id", cascadeDelete: true);
            DropColumn("dbo.ChambresSet", "ChambresSetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChambresSet", "ChambresSetId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ChambresSet", "HotelsSetId", "dbo.HotelsSet");
            DropIndex("dbo.ChambresSet", new[] { "HotelsSetId" });
            AlterColumn("dbo.ChambresSet", "HotelsSetId", c => c.Int());
            RenameColumn(table: "dbo.ChambresSet", name: "HotelsSetId", newName: "HotelsSet_Id");
            CreateIndex("dbo.ChambresSet", "HotelsSet_Id");
            CreateIndex("dbo.ChambresSet", "ChambresSetId");
            AddForeignKey("dbo.ChambresSet", "HotelsSet_Id", "dbo.HotelsSet", "Id");
            AddForeignKey("dbo.ChambresSet", "ChambresSetId", "dbo.ChambresSet", "Id");
        }
    }
}
