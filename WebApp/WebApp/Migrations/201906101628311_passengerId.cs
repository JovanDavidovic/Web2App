namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passengerId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tickets", new[] { "Passenger_Id" });
            DropColumn("dbo.Tickets", "PassengerId");
            RenameColumn(table: "dbo.Tickets", name: "Passenger_Id", newName: "PassengerId");
            AlterColumn("dbo.Tickets", "PassengerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tickets", "PassengerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tickets", new[] { "PassengerId" });
            AlterColumn("dbo.Tickets", "PassengerId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Tickets", name: "PassengerId", newName: "Passenger_Id");
            AddColumn("dbo.Tickets", "PassengerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "Passenger_Id");
        }
    }
}
