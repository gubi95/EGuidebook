namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteModel_IsSystemRoute_Flag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "IsSystemRoute", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "IsSystemRoute");
        }
    }
}
