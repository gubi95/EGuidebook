namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Route_CreatedByUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "CreatedByUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Routes", "CreatedByUserID");
            AddForeignKey("dbo.Routes", "CreatedByUserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "CreatedByUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Routes", new[] { "CreatedByUserID" });
            DropColumn("dbo.Routes", "CreatedByUserID");
        }
    }
}
