namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfile_Avatar_FirstName_LastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: true));
            AddColumn("dbo.AspNetUsers", "AvatarImagePath", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AvatarImagePath");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
