namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SystemSettingsModelSMTP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemSettingsModels",
                c => new
                    {
                        SystemSettingsId = c.Guid(nullable: false),
                        SMTP_Email = c.String(),
                        SMTP_Password = c.String(),
                        SMTP_Host = c.String(),
                        SMTP_Port = c.Int(nullable: false),
                        SMTP_EnableSSL = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SystemSettingsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemSettingsModels");
        }
    }
}
