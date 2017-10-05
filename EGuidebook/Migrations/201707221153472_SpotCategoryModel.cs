namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotCategoryModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpotCategoryModels",
                c => new
                    {
                        SpotCategoryId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        IconPath = c.String(),
                    })
                .PrimaryKey(t => t.SpotCategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SpotCategoryModels");
        }
    }
}
