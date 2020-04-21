namespace Module6TP1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REQUIRED_CONSTRAINTS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Armes", "Nom", c => c.String(nullable: false));
            AlterColumn("dbo.Samourais", "Nom", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Samourais", "Nom", c => c.String());
            AlterColumn("dbo.Armes", "Nom", c => c.String());
        }
    }
}
