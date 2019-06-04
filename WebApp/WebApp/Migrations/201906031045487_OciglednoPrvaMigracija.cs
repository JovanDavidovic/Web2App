namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OciglednoPrvaMigracija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buses",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        BusRouteId = c.Int(nullable: false),
                        BusRoute_Number = c.Int(),
                    })
                .PrimaryKey(t => t.Number)
                .ForeignKey("dbo.Routes", t => t.BusRoute_Number)
                .Index(t => t.BusRoute_Number);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        DepartureTimeId = c.Int(nullable: false),
                        DayTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Number)
                .ForeignKey("dbo.DayTypes", t => t.DayTypeId, cascadeDelete: true)
                .ForeignKey("dbo.DepartureTimes", t => t.DepartureTimeId, cascadeDelete: true)
                .Index(t => t.DepartureTimeId)
                .Index(t => t.DayTypeId);
            
            CreateTable(
                "dbo.DayTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DepartureTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketPriceId = c.Int(nullable: false),
                        PassengerId = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Passenger_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Passenger_Id)
                .ForeignKey("dbo.TicketPrices", t => t.TicketPriceId, cascadeDelete: true)
                .Index(t => t.TicketPriceId)
                .Index(t => t.Passenger_Id);
            
            CreateTable(
                "dbo.TicketPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        PricelistId = c.Int(nullable: false),
                        TicketTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pricelists", t => t.PricelistId, cascadeDelete: true)
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
                .Index(t => t.PricelistId)
                .Index(t => t.TicketTypeId);
            
            CreateTable(
                "dbo.Pricelists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PassengerTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Coefficient = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StationRoutes",
                c => new
                    {
                        Station_Name = c.String(nullable: false, maxLength: 128),
                        Route_Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Station_Name, t.Route_Number })
                .ForeignKey("dbo.Stations", t => t.Station_Name, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.Route_Number, cascadeDelete: true)
                .Index(t => t.Station_Name)
                .Index(t => t.Route_Number);
            
            AddColumn("dbo.AspNetUsers", "VerificationStatus", c => c.String());
            AddColumn("dbo.AspNetUsers", "Image", c => c.String());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Type_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Type_Id");
            AddForeignKey("dbo.AspNetUsers", "Type_Id", "dbo.PassengerTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Type_Id", "dbo.PassengerTypes");
            DropForeignKey("dbo.Tickets", "TicketPriceId", "dbo.TicketPrices");
            DropForeignKey("dbo.TicketPrices", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.TicketPrices", "PricelistId", "dbo.Pricelists");
            DropForeignKey("dbo.Tickets", "Passenger_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StationRoutes", "Route_Number", "dbo.Routes");
            DropForeignKey("dbo.StationRoutes", "Station_Name", "dbo.Stations");
            DropForeignKey("dbo.Routes", "DepartureTimeId", "dbo.DepartureTimes");
            DropForeignKey("dbo.Routes", "DayTypeId", "dbo.DayTypes");
            DropForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes");
            DropIndex("dbo.StationRoutes", new[] { "Route_Number" });
            DropIndex("dbo.StationRoutes", new[] { "Station_Name" });
            DropIndex("dbo.TicketPrices", new[] { "TicketTypeId" });
            DropIndex("dbo.TicketPrices", new[] { "PricelistId" });
            DropIndex("dbo.Tickets", new[] { "Passenger_Id" });
            DropIndex("dbo.Tickets", new[] { "TicketPriceId" });
            DropIndex("dbo.AspNetUsers", new[] { "Type_Id" });
            DropIndex("dbo.Routes", new[] { "DayTypeId" });
            DropIndex("dbo.Routes", new[] { "DepartureTimeId" });
            DropIndex("dbo.Buses", new[] { "BusRoute_Number" });
            DropColumn("dbo.AspNetUsers", "Type_Id");
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "Image");
            DropColumn("dbo.AspNetUsers", "VerificationStatus");
            DropTable("dbo.StationRoutes");
            DropTable("dbo.PassengerTypes");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.Pricelists");
            DropTable("dbo.TicketPrices");
            DropTable("dbo.Tickets");
            DropTable("dbo.Stations");
            DropTable("dbo.DepartureTimes");
            DropTable("dbo.DayTypes");
            DropTable("dbo.Routes");
            DropTable("dbo.Buses");
        }
    }
}
