namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PassengerStationBus : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.AspNetUsers", "Type_Id", "dbo.PassengerTypes");
            //RenameColumn(table: "dbo.AspNetUsers", name: "Type_Id", newName: "TypeId");
            //RenameIndex(table: "dbo.AspNetUsers", name: "IX_Type_Id", newName: "IX_TypeId");
            AddColumn("dbo.Buses", "CoordinateX", c => c.Single(nullable: false));
            AddColumn("dbo.Buses", "CoordinateY", c => c.Single(nullable: false));
            AddColumn("dbo.Stations", "CoordinatesX", c => c.Single(nullable: false));
            AddColumn("dbo.Stations", "CoordinatesY", c => c.Single(nullable: false));
            //AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            //AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            //AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            //AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.String());
            //AddForeignKey("dbo.AspNetUsers", "TypeId", "dbo.PassengerTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "TypeId", "dbo.PassengerTypes");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.Stations", "CoordinatesY");
            DropColumn("dbo.Stations", "CoordinatesX");
            DropColumn("dbo.Buses", "CoordinateY");
            DropColumn("dbo.Buses", "CoordinateX");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_TypeId", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "TypeId", newName: "Type_Id");
            AddForeignKey("dbo.AspNetUsers", "Type_Id", "dbo.PassengerTypes", "Id");
        }
    }
}
