using System.Collections.Generic;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.ElistaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(Data.ElistaDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            

            context.Users.AddOrUpdate(new User()
            {
                Id = 1,
                Firstname = "£ukasz",
                Lastname = "Zimnoch",
                TechDate = DateTime.Now,
                Password = "qwe123",
                Email = "asd@wp.pl",
                IsActive = true
            });

            context.Users.AddOrUpdate(new User()
            {
                Id = 2,
                Firstname = "£ukasz",
                Lastname = "Zimnoch",
                TechDate = DateTime.Now,
                Password = "qwe123",
                Email = "asdc@wp.pl",
                IsActive = true
            });

            context.SaveChanges();
            

            context.UserGroups.AddOrUpdate(new UserGroup()
            {
                Id = 1,
                User = new List<User>() {context.Users.Find(1)},
                GroupName = "Admin"
            });

            context.UserGroups.AddOrUpdate(new UserGroup()
            {
                Id = 2,
                User = new List<User>() {context.Users.Find(2)},
                GroupName = "Leader"
            });

            context.SaveChanges();
        }
    }
}
