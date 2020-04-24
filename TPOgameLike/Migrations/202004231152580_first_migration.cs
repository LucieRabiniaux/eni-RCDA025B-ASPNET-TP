namespace TPOgameLike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        CaseNb = c.Int(),
                        SolarSystem_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SolarSystems", t => t.SolarSystem_Id)
                .Index(t => t.SolarSystem_Id);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Level = c.Int(),
                        Planet_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.Planet_Id)
                .Index(t => t.Planet_Id);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastQuantity = c.Int(),
                        LastUpdate = c.DateTime(nullable: false),
                        Planet_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.Planet_Id)
                .Index(t => t.Planet_Id);
            
            CreateTable(
                "dbo.SolarSystems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Planets", "SolarSystem_Id", "dbo.SolarSystems");
            DropForeignKey("dbo.Resources", "Planet_Id", "dbo.Planets");
            DropForeignKey("dbo.Buildings", "Planet_Id", "dbo.Planets");
            DropIndex("dbo.Resources", new[] { "Planet_Id" });
            DropIndex("dbo.Buildings", new[] { "Planet_Id" });
            DropIndex("dbo.Planets", new[] { "SolarSystem_Id" });
            DropTable("dbo.SolarSystems");
            DropTable("dbo.Resources");
            DropTable("dbo.Buildings");
            DropTable("dbo.Planets");
        }
    }
}
