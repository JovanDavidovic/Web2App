namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovapoljauPassengeru : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Type_Id", "dbo.PassengerTypes");
            RenameColumn(table: "dbo.AspNetUsers", name: "Type_Id", newName: "TypeId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Type_Id", newName: "IX_TypeId");
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "DateOfBirth", c => c.String());
            AddForeignKey("dbo.AspNetUsers", "TypeId", "dbo.PassengerTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "TypeId", "dbo.PassengerTypes");
            DropColumn("dbo.AspNetUsers", "DateOfBirth");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "Name");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_TypeId", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "TypeId", newName: "Type_Id");
            AddForeignKey("dbo.AspNetUsers", "Type_Id", "dbo.PassengerTypes", "Id");
        }
    }
}
