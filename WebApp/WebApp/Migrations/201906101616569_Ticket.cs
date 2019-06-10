namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ticket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "TicketPriceId", "dbo.TicketPrices");
            DropIndex("dbo.Tickets", new[] { "TicketPriceId" });
            AddColumn("dbo.Tickets", "TicketTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "ExpirationDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Tickets", "TicketTypeId");
            AddForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Tickets", "TicketPriceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TicketPriceId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
            DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
            DropColumn("dbo.Tickets", "ExpirationDate");
            DropColumn("dbo.Tickets", "TicketTypeId");
            CreateIndex("dbo.Tickets", "TicketPriceId");
            AddForeignKey("dbo.Tickets", "TicketPriceId", "dbo.TicketPrices", "Id", cascadeDelete: true);
        }
    }
}
