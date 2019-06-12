namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Route : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes");
            DropPrimaryKey("dbo.Routes");
            AlterColumn("dbo.Routes", "Number", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Routes", "Number");
            AddForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes", "Number");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes");
            DropPrimaryKey("dbo.Routes");
            AlterColumn("dbo.Routes", "Number", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Routes", "Number");
            AddForeignKey("dbo.Buses", "BusRoute_Number", "dbo.Routes", "Number");
        }
    }
}
