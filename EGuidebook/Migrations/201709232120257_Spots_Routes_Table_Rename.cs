namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Spots_Routes_Table_Rename : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SpotModelRouteModels", newName: "Spots_Routes");
            RenameColumn(table: "dbo.Spots_Routes", name: "SpotModel_SpotID", newName: "RouteID");
            RenameColumn(table: "dbo.Spots_Routes", name: "RouteModel_RouteID", newName: "SpotID");
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_RouteModel_RouteID", newName: "IX_SpotID");
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_SpotModel_SpotID", newName: "IX_RouteID");
            DropPrimaryKey("dbo.Spots_Routes");
            AddPrimaryKey("dbo.Spots_Routes", new[] { "SpotID", "RouteID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Spots_Routes");
            AddPrimaryKey("dbo.Spots_Routes", new[] { "SpotModel_SpotID", "RouteModel_RouteID" });
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_RouteID", newName: "IX_SpotModel_SpotID");
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_SpotID", newName: "IX_RouteModel_RouteID");
            RenameColumn(table: "dbo.Spots_Routes", name: "SpotID", newName: "RouteModel_RouteID");
            RenameColumn(table: "dbo.Spots_Routes", name: "RouteID", newName: "SpotModel_SpotID");
            RenameTable(name: "dbo.Spots_Routes", newName: "SpotModelRouteModels");
        }
    }
}
