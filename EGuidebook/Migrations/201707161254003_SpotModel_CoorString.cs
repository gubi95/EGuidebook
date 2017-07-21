namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpotModel_CoorString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SpotModels", "CoorX", c => c.String(nullable: false));
            AlterColumn("dbo.SpotModels", "CoorY", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpotModels", "CoorY", c => c.Double(nullable: false));
            AlterColumn("dbo.SpotModels", "CoorX", c => c.Double(nullable: false));
        }
    }
}
