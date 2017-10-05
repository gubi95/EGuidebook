namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotGradeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpotGradeModels",
                c => new
                    {
                        SpotGradeID = c.Guid(nullable: false),
                        Grade = c.Int(nullable: false),
                        Message = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                        SpotModel_SpotID = c.Guid(),
                    })
                .PrimaryKey(t => t.SpotGradeID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.SpotModels", t => t.SpotModel_SpotID)
                .Index(t => t.User_Id)
                .Index(t => t.SpotModel_SpotID);
            
            DropColumn("dbo.SpotModels", "RecommendationCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SpotModels", "RecommendationCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.SpotGradeModels", "SpotModel_SpotID", "dbo.SpotModels");
            DropForeignKey("dbo.SpotGradeModels", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SpotGradeModels", new[] { "SpotModel_SpotID" });
            DropIndex("dbo.SpotGradeModels", new[] { "User_Id" });
            DropTable("dbo.SpotGradeModels");
        }
    }
}
