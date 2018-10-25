namespace WpfAppCesi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ajoutPays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelsSet", "Pays", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HotelsSet", "Pays");
        }
    }
}
