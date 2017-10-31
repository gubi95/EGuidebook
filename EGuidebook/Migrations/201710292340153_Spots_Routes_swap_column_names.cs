namespace EGuidebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Spots_Routes_swap_column_names : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Spots_Routes", name: "SpotID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Spots_Routes", name: "RouteID", newName: "SpotID");
            RenameColumn(table: "dbo.Spots_Routes", name: "__mig_tmp__0", newName: "RouteID");
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_SpotID", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_RouteID", newName: "IX_SpotID");
            RenameIndex(table: "dbo.Spots_Routes", name: "__mig_tmp__0", newName: "IX_RouteID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_RouteID", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Spots_Routes", name: "IX_SpotID", newName: "IX_RouteID");
            RenameIndex(table: "dbo.Spots_Routes", name: "__mig_tmp__0", newName: "IX_SpotID");
            RenameColumn(table: "dbo.Spots_Routes", name: "RouteID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Spots_Routes", name: "SpotID", newName: "RouteID");
            RenameColumn(table: "dbo.Spots_Routes", name: "__mig_tmp__0", newName: "SpotID");
        }
    }
}
