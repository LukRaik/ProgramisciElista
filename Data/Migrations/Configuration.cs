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
        protected override void Seed(Data.ElistaDbContext db)
        {
            base.Seed(db);

            foreach (var user in db.Users)
            {
                db.Users.Remove(user);
            }

            db.SaveChanges();

            db.UserGroups.AddOrUpdate(x=>x.Id,new UserGroup()
            {
                Id = 1,
                GroupName = "Admin",
                Users = new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Firstname = "£ukasz",
                        Lastname = "Zimnoch",
                        TechDate = DateTime.Now,
                        Password = "qwe123",
                        Email = "asd@wp.pl",
                        IsActive = true
                    }
                }
            });

            db.UserGroups.AddOrUpdate(x=>x.Id,new UserGroup()
            {
                Id = 2,
                GroupName = "Leader",
                Users = new List<User>()
                {
                    new User()
                    {
                        Id = 2,
                        Firstname = "£ukasz",
                        Lastname = "Zimnoch",
                        TechDate = DateTime.Now,
                        Password = "qwe123",
                        Email = "asdc@wp.pl",
                        IsActive = true
                    }
                }
            });
            db.SaveChanges();
        }
    }
}
