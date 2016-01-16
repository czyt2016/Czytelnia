using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Repository.RInterface
{
    //Repozytorium jest zależne od kontekstu: użycie IoC (biblioteka Unity Bootstrapper for Mvc) 
    //do wstrzykiwania obiektu kontekstu dla repozytorium
    public interface ICzytelniaContext
    {
        DbSet<Rodzaj> Rodzaje { get; set; }
        DbSet<Ksiazka> Ksiazki { get; set; }
        DbSet<Czytelnik> Czytelnik { get; set; }
        DbSet<Ksiazka_Rodzaj> Ksiazka_Rodzaj { get; set; }

        int SaveChanges();
        Database Database { get; }

        DbEntityEntry Entry(object entity);
    }
}