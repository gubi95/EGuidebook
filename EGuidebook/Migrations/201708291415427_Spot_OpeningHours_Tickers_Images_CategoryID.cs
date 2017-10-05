namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Spot_OpeningHours_Tickers_Images_CategoryID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpotModels", "SpotCategoryID", c => c.Guid(nullable: false));
            AddColumn("dbo.SpotModels", "Image1Path", c => c.String());
            AddColumn("dbo.SpotModels", "Image2Path", c => c.String());
            AddColumn("dbo.SpotModels", "Image3Path", c => c.String());
            AddColumn("dbo.SpotModels", "Image4Path", c => c.String());
            AddColumn("dbo.SpotModels", "Image5Path", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_MondayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_MondayTo", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_TuesdayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_TuesdayTo", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_WednesdayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_WednesdayTo", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_ThursdayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_ThursdayTo", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_FridayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_FridayTo", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_SaturdayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_SaturdayTo", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_SundayFrom", c => c.String());
            AddColumn("dbo.SpotModels", "OpeningHours_SundayTo", c => c.String());
            AddColumn("dbo.SpotModels", "TicketAdultPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SpotModels", "TicketStudentPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SpotModels", "TicketKidsPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.SpotModels", "SpotCategoryID");
            AddForeignKey("dbo.SpotModels", "SpotCategoryID", "dbo.SpotCategoryModels", "SpotCategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpotModels", "SpotCategoryID", "dbo.SpotCategoryModels");
            DropIndex("dbo.SpotModels", new[] { "SpotCategoryID" });
            DropColumn("dbo.SpotModels", "TicketKidsPrice");
            DropColumn("dbo.SpotModels", "TicketStudentPrice");
            DropColumn("dbo.SpotModels", "TicketAdultPrice");
            DropColumn("dbo.SpotModels", "OpeningHours_SundayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_SundayFrom");
            DropColumn("dbo.SpotModels", "OpeningHours_SaturdayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_SaturdayFrom");
            DropColumn("dbo.SpotModels", "OpeningHours_FridayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_FridayFrom");
            DropColumn("dbo.SpotModels", "OpeningHours_ThursdayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_ThursdayFrom");
            DropColumn("dbo.SpotModels", "OpeningHours_WednesdayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_WednesdayFrom");
            DropColumn("dbo.SpotModels", "OpeningHours_TuesdayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_TuesdayFrom");
            DropColumn("dbo.SpotModels", "OpeningHours_MondayTo");
            DropColumn("dbo.SpotModels", "OpeningHours_MondayFrom");
            DropColumn("dbo.SpotModels", "Image5Path");
            DropColumn("dbo.SpotModels", "Image4Path");
            DropColumn("dbo.SpotModels", "Image3Path");
            DropColumn("dbo.SpotModels", "Image2Path");
            DropColumn("dbo.SpotModels", "Image1Path");
            DropColumn("dbo.SpotModels", "SpotCategoryID");
        }
    }
}
