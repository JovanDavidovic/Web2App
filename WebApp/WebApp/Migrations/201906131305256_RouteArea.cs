namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RouteArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "Area", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "Area");
        }
    }
}
