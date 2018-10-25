namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificationUsersToClients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientsSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prenom = c.String(nullable: false),
                        Nom = c.String(nullable: false),
                        DateNaissance = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.UsersSet");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersSet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ClientsSet");
        }
    }
}
