namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Route2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes");
            RenameColumn(table: "dbo.Buses", name: "BusRoute_Number", newName: "BusRoute_RouteId");
            RenameIndex(table: "dbo.Buses", name: "IX_BusRoute_Number", newName: "IX_BusRoute_RouteId");
            DropPrimaryKey("dbo.Routes");
            AddColumn("dbo.Routes", "RouteId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Routes", "RouteId");
            AddForeignKey("dbo.Buses", "BusRoute_RouteId", "dbo.Routes", "RouteId");
            DropColumn("dbo.Routes", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Routes", "Number", c => c.Int(nullable: false));
            DropForeignKey("dbo.Buses", "BusRoute_RouteId", "dbo.Routes");
            DropPrimaryKey("dbo.Routes");
            DropColumn("dbo.Routes", "RouteId");
            AddPrimaryKey("dbo.Routes", "Number");
            RenameIndex(table: "dbo.Buses", name: "IX_BusRoute_RouteId", newName: "IX_BusRoute_Number");
            RenameColumn(table: "dbo.Buses", name: "BusRoute_RouteId", newName: "BusRoute_Number");
            AddForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes", "Number");
        }
    }
}
