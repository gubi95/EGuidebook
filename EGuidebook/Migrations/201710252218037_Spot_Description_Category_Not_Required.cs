namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Spot_Description_Category_Not_Required : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Spots", "SpotCategoryID", "dbo.SpotCategories");
            DropIndex("dbo.Spots", new[] { "SpotCategoryID" });
            AlterColumn("dbo.Spots", "SpotCategoryID", c => c.Guid());
            AlterColumn("dbo.Spots", "Description", c => c.String());
            CreateIndex("dbo.Spots", "SpotCategoryID");
            AddForeignKey("dbo.Spots", "SpotCategoryID", "dbo.SpotCategories", "SpotCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Spots", "SpotCategoryID", "dbo.SpotCategories");
            DropIndex("dbo.Spots", new[] { "SpotCategoryID" });
            AlterColumn("dbo.Spots", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Spots", "SpotCategoryID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Spots", "SpotCategoryID");
            AddForeignKey("dbo.Spots", "SpotCategoryID", "dbo.SpotCategories", "SpotCategoryId", cascadeDelete: true);
        }
    }
}
