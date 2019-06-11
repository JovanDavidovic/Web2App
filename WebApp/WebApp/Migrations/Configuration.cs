namespace WebApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.Models;
    using WebApp.Persistence.UnitOfWork;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.Persistence.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.Persistence.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Controller"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Controller" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser" };

                manager.Create(role);
            }

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);



            if (!context.Users.Any(u => u.UserName == "admin@yahoo.com"))
            {
                var user = new ApplicationUser() { Id = "admin", UserName = "admin@yahoo.com", Email = "admin@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Admin123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "contr"))
            {
                var user = new ApplicationUser() { Id = "contr", UserName = "contr", Email = "contr@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Contr123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Controller");
            }

            if (!context.Users.Any(u => u.UserName == "appu@yahoo.com"))
            { 
                var user = new ApplicationUser() { Id = "appu", UserName = "appu@yahoo.com", Email = "appu@yahoo.com", PasswordHash = ApplicationUser.HashPassword("Appu123!") };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "AppUser");
            }       

            if (!context.PassengerTypes.Any(pt => pt.Type == "Regular"))
            {
                var type = new PassengerType() { Id = 1, Type = "Regular", Coefficient = 100 };

                context.PassengerTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.PassengerTypes.Any(pt => pt.Type == "Student"))
            {
                var type = new PassengerType() { Id = 2, Type = "Student", Coefficient = 60 };

                context.PassengerTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.PassengerTypes.Any(pt => pt.Type == "Pensioner"))
            {
                var type = new PassengerType() { Id = 3, Type = "Pensioner", Coefficient = 70 };

                context.PassengerTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.TicketTypes.Any(pt => pt.Type == "Hour"))
            {
                var type = new TicketType() { Id = 1, Type = "Hour" };

                context.TicketTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.TicketTypes.Any(pt => pt.Type == "Day"))
            {
                var type = new TicketType() { Id = 2, Type = "Day" };

                context.TicketTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.TicketTypes.Any(pt => pt.Type == "Month"))
            {
                var type = new TicketType() { Id = 3, Type = "Month" };

                context.TicketTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.TicketTypes.Any(pt => pt.Type == "Year"))
            {
                var type = new TicketType() { Id = 4, Type = "Year"};

                context.TicketTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.DayTypes.Any(pt => pt.Type == "WorkDay"))
            {
                var type = new DayType() { Id = 1, Type = "WorkDay" };

                context.DayTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.DayTypes.Any(pt => pt.Type == "Saturday"))
            {
                var type = new DayType() { Id = 2, Type = "Saturday" };

                context.DayTypes.Add(type);
                context.SaveChanges();
            }

            if (!context.DayTypes.Any(pt => pt.Type == "Sunday"))
            {
                var type = new DayType() { Id = 3, Type = "Sunday" };

                context.DayTypes.Add(type);
                context.SaveChanges();
            }
        }
    }
}
