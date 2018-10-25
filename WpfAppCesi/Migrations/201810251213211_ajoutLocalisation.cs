namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutLocalisation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChambresSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Climatisation = c.Boolean(nullable: false),
                        NbLits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HotelsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Capacite = c.Int(nullable: false),
                        ChambresSetId = c.Int(nullable: false),
                        Localisation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChambresSet", t => t.ChambresSetId)
                .Index(t => t.ChambresSetId);
            
            CreateTable(
                "dbo.UsersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotelsSet", "ChambresSetId", "dbo.ChambresSet");
            DropIndex("dbo.HotelsSet", new[] { "ChambresSetId" });
            DropTable("dbo.UsersSet");
            DropTable("dbo.HotelsSet");
            DropTable("dbo.ChambresSet");
        }
    }
}
