namespace Repository.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repository.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repository.Models.CzytelniaContext>
    {
        public Configuration()
        {
            //w³¹czenie automatycznych migracji oraz migracji stratnych
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repository.Models.CzytelniaContext context)
        {
            // Do debugowania metody seed
            // if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
            SeedRoles(context);
            SeedUsers(context);
            //SeedKsiazki(context);
            //SeedRodzaje(context);
            //SeedKsiazka_Rodzaj(context);
        }

        private void SeedRoles(CzytelniaContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>());

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Pracownik"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pracownik";
                roleManager.Create(role);
            }
        }

        private void SeedUsers(CzytelniaContext context)
        {
            var store = new UserStore<Czytelnik>(context);
            var manager = new UserManager<Czytelnik>(store);
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {

                var user = new Czytelnik { UserName = "Admin", Wiek = 21 };

                var adminresult = manager.Create(user, "12345678");

                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
            //rola Pracownik mo¿e tylko edytowaæ i dodawaæ, nie mo¿e usuwaæ ksi¹¿ek
            if (!context.Users.Any(u => u.UserName == "Pracownik"))
            {
                var user = new Czytelnik { UserName = "pracownik@google.com" };
                var adminresult = manager.Create(user, "Aaa111!");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Pracownik");
            }
            //rola Admin mo¿e wszystko (Szczegó³y, Edytuj i Usuñ)
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var user = new Czytelnik { UserName = "admin@google.com" };
                var adminresult = manager.Create(user, "Aaa111!");
                if (adminresult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
        }

        //private void SeedKsiazki(CzytelniaContext context)
        //{

        //    var idUzytkownika = context.Set<Czytelnik>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;

        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var ogl = new Ksiazka()
        //        {
        //            Id = i,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "Autor ksi¹¿ki" + i.ToString(),
        //            Tytul = "Tytu³ ksi¹¿ki" + i.ToString(),
        //            Gatunek = "Gatunek ksi¹¿ki" + i.ToString(),
        //            RokWydania = DateTime.Now.AddDays(-i),
        //            DataDodania = DateTime.Now.AddDays(-i)
        //        };
        //        context.Set<Ksiazka>().AddOrUpdate(ogl);
        //    }
        //    context.SaveChanges();
        //}

        //private void SeedKsiazki(CzytelniaContext context)
        //{

        //    var idUzytkownika = context.Set<Czytelnik>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;

        //    context.Ksiazki.AddOrUpdate(x => x.Tytul,
        //        new Ksiazka
        //        {
        //            Id = 1,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "JRR Tolkien",
        //            Tytul = "Hobbit",
        //            Gatunek = "fantastyka",
        //            RokWydania = DateTime.Parse("1937-9-21"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 2,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "Francis Scott Fitzgerald",
        //            Tytul = "Wielki Gatsby",
        //            Gatunek = "obyczajowa",
        //            RokWydania = DateTime.Parse("1925-4-10"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 3,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "Frank Herbert",
        //            Tytul = "Diuna",
        //            Gatunek = "fantastyka",
        //            RokWydania = DateTime.Parse("1965-8-1"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 4,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "Daniel Defoe",
        //            Tytul = "Przypadki Robinsona Kruzoe",
        //            Gatunek = "przygodowa",
        //            RokWydania = DateTime.Parse("1719-4-25"),
        //            DataDodania = DateTime.Now
        //        },
        //        new Ksiazka
        //        {
        //            Id = 5,
        //            CzytelnikId = idUzytkownika,
        //            Autor = " Arthur Conan Doyle",
        //            Tytul = "Przygody Sherlocka Holmesa",
        //            Gatunek = "detektywistyczna",
        //            RokWydania = DateTime.Parse("1892-10-14"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 6,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "James Joyce",
        //            Tytul = "Ulisses",
        //            Gatunek = "obyczajowa",
        //            RokWydania = DateTime.Parse("1922-10-22"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 7,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "HG Wells",
        //            Tytul = "Wehiku³ czasu",
        //            Gatunek = "fantastyka",
        //            RokWydania = DateTime.Parse("1895-5-7"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 8,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "William Golding",
        //            Tytul = "W³adca much",
        //            Gatunek = "dramat",
        //            RokWydania = DateTime.Parse("1954-9-17"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 9,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "James Joyce",
        //            Tytul = "Portret artysty z czasów m³odoœci",
        //            Gatunek = "obyczajowa",
        //            RokWydania = DateTime.Parse("1916-12-29"),
        //            DataDodania = DateTime.Now
        //        },

        //        new Ksiazka
        //        {
        //            Id = 10,
        //            CzytelnikId = idUzytkownika,
        //            Autor = "JRR Tolkien",
        //            Tytul = "W³adca pierœcieni",
        //            Gatunek = "fantastyka",
        //            RokWydania = DateTime.Parse("1937-9-21"),
        //            DataDodania = DateTime.Now
        //        }
        //    );

        //    context.SaveChanges();
        //}

        //private void SeedRodzaje(CzytelniaContext context)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var rodz = new Rodzaj()
        //        {
        //            Id = i,
        //            Nazwa = "klasyka literatury",
        //        };
        //        context.Set<Rodzaj>().AddOrUpdate(rodz);
        //    }
        //    context.SaveChanges();
        //}

        //private void SeedKsiazka_Rodzaj(CzytelniaContext context)
        //{
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        var krodz = new Ksiazka_Rodzaj()
        //        {
        //            Id = i,
        //            KsiazkaId = i / 2 + 1,
        //            RodzajId = i / 2 + 2
        //        };

        //        context.Set<Ksiazka_Rodzaj>().AddOrUpdate(krodz);
        //    }
        //    context.SaveChanges();
        //}
    }
}
