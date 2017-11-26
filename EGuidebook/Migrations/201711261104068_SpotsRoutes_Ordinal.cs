namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotsRoutes_Ordinal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Spots_Routes", "RouteID", "dbo.Routes");
            DropForeignKey("dbo.Spots_Routes", "SpotID", "dbo.Spots");
            DropIndex("dbo.Spots_Routes", new[] { "RouteID" });
            DropIndex("dbo.Spots_Routes", new[] { "SpotID" });
            CreateTable(
                "dbo.SpotsRoutes",
                c => new
                    {
                        SpotID = c.Guid(nullable: false),
                        RouteID = c.Guid(nullable: false),
                        Ordinal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpotID, t.RouteID })
                .ForeignKey("dbo.Routes", t => t.RouteID, cascadeDelete: true)
                .ForeignKey("dbo.Spots", t => t.SpotID, cascadeDelete: true)
                .Index(t => t.SpotID)
                .Index(t => t.RouteID);
            
            DropTable("dbo.Spots_Routes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Spots_Routes",
                c => new
                    {
                        RouteID = c.Guid(nullable: false),
                        SpotID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RouteID, t.SpotID });
            
            DropForeignKey("dbo.SpotsRoutes", "SpotID", "dbo.Spots");
            DropForeignKey("dbo.SpotsRoutes", "RouteID", "dbo.Routes");
            DropIndex("dbo.SpotsRoutes", new[] { "RouteID" });
            DropIndex("dbo.SpotsRoutes", new[] { "SpotID" });
            DropTable("dbo.SpotsRoutes");
            CreateIndex("dbo.Spots_Routes", "SpotID");
            CreateIndex("dbo.Spots_Routes", "RouteID");
            AddForeignKey("dbo.Spots_Routes", "SpotID", "dbo.Spots", "SpotID", cascadeDelete: true);
            AddForeignKey("dbo.Spots_Routes", "RouteID", "dbo.Routes", "RouteID", cascadeDelete: true);
        }
    }
}
