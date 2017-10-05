namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotGrade_CreationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpotGradeModels", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpotGradeModels", "CreationDate");
        }
    }
}
