namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Spot_Route_ManyToMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpotModelRouteModels",
                c => new
                    {
                        SpotModel_SpotID = c.Guid(nullable: false),
                        RouteModel_RouteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SpotModel_SpotID, t.RouteModel_RouteID })
                .ForeignKey("dbo.Spots", t => t.SpotModel_SpotID, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteModel_RouteID, cascadeDelete: true)
                .Index(t => t.SpotModel_SpotID)
                .Index(t => t.RouteModel_RouteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpotModelRouteModels", "RouteModel_RouteID", "dbo.Routes");
            DropForeignKey("dbo.SpotModelRouteModels", "SpotModel_SpotID", "dbo.Spots");
            DropIndex("dbo.SpotModelRouteModels", new[] { "RouteModel_RouteID" });
            DropIndex("dbo.SpotModelRouteModels", new[] { "SpotModel_SpotID" });
            DropTable("dbo.SpotModelRouteModels");
        }
    }
}
