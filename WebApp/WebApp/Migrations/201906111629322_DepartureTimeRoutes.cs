namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartureTimeRoutes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "DayTypeId", "dbo.DayTypes");
            DropForeignKey("dbo.Routes", "DepartureTimeId", "dbo.DepartureTimes");
            DropForeignKey("dbo.StationRoutes", "Station_Name", "dbo.Stations");
            DropForeignKey("dbo.StationRoutes", "Route_Number", "dbo.Routes");
            DropIndex("dbo.Routes", new[] { "DepartureTimeId" });
            DropIndex("dbo.Routes", new[] { "DayTypeId" });
            DropIndex("dbo.StationRoutes", new[] { "Station_Name" });
            DropIndex("dbo.StationRoutes", new[] { "Route_Number" });
            AddColumn("dbo.Routes", "Stations", c => c.String());
            AddColumn("dbo.Routes", "Station_Name", c => c.String(maxLength: 128));
            AddColumn("dbo.DepartureTimes", "DayTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.DepartureTimes", "Routes", c => c.String());
            CreateIndex("dbo.Routes", "Station_Name");
            CreateIndex("dbo.DepartureTimes", "DayTypeId");
            AddForeignKey("dbo.DepartureTimes", "DayTypeId", "dbo.DayTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "Station_Name", "dbo.Stations", "Name");
            DropColumn("dbo.Routes", "DepartureTimeId");
            DropColumn("dbo.Routes", "DayTypeId");
            DropTable("dbo.StationRoutes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StationRoutes",
                c => new
                    {
                        Station_Name = c.String(nullable: false, maxLength: 128),
                        Route_Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Station_Name, t.Route_Number });
            
            AddColumn("dbo.Routes", "DayTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Routes", "DepartureTimeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Routes", "Station_Name", "dbo.Stations");
            DropForeignKey("dbo.DepartureTimes", "DayTypeId", "dbo.DayTypes");
            DropIndex("dbo.DepartureTimes", new[] { "DayTypeId" });
            DropIndex("dbo.Routes", new[] { "Station_Name" });
            DropColumn("dbo.DepartureTimes", "Routes");
            DropColumn("dbo.DepartureTimes", "DayTypeId");
            DropColumn("dbo.Routes", "Station_Name");
            DropColumn("dbo.Routes", "Stations");
            CreateIndex("dbo.StationRoutes", "Route_Number");
            CreateIndex("dbo.StationRoutes", "Station_Name");
            CreateIndex("dbo.Routes", "DayTypeId");
            CreateIndex("dbo.Routes", "DepartureTimeId");
            AddForeignKey("dbo.StationRoutes", "Route_Number", "dbo.Routes", "Number", cascadeDelete: true);
            AddForeignKey("dbo.StationRoutes", "Station_Name", "dbo.Stations", "Name", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "DepartureTimeId", "dbo.DepartureTimes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "DayTypeId", "dbo.DayTypes", "Id", cascadeDelete: true);
        }
    }
}
