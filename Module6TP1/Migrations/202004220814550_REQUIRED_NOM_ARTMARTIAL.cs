namespace Module6TP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REQUIRED_NOM_ARTMARTIAL : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArtMartials", "Nom", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArtMartials", "Nom", c => c.String());
        }
    }
}
