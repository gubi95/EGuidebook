namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModelIsPasswordChangeEnforced : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsPasswordChangeEnforced", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsPasswordChangeEnforced");
        }
    }
}
