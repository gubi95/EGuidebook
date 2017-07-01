namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpotModels",
                c => new
                {
                    SpotID = c.Guid(nullable: false),
                    Name = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    CoorX = c.Single(nullable: false),
                    CoorY = c.Single(nullable: false),
                    IsApproved = c.Boolean(nullable: false, defaultValue: false),
                    RecommendationCount = c.Int(nullable: false, defaultValue: 0),
                    ApprovedByUserID = c.String(nullable: true, maxLength: 128),
                    ApprovalDate = c.DateTime(nullable: true),
                })
                .PrimaryKey(t => t.SpotID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedByUserID)
                .Index(t => t.ApprovedByUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpotModels", "ApprovedByUserID", "dbo.AspNetUsers");
            DropIndex("dbo.SpotModels", new[] { "ApprovedByUserID" });
            DropTable("dbo.SpotModels");
        }
    }
}
