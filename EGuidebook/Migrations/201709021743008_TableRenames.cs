namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableRenames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SpotGradeModels", newName: "SpotGrades");
            RenameTable(name: "dbo.SpotModels", newName: "Spots");
            RenameTable(name: "dbo.SpotCategoryModels", newName: "SpotCategories");
            RenameTable(name: "dbo.SystemSettingsModels", newName: "SystemSettings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SystemSettings", newName: "SystemSettingsModels");
            RenameTable(name: "dbo.SpotCategories", newName: "SpotCategoryModels");
            RenameTable(name: "dbo.Spots", newName: "SpotModels");
            RenameTable(name: "dbo.SpotGrades", newName: "SpotGradeModels");
        }
    }
}
