using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repository.RInterface;

namespace Repository.Models
{
    //ApplicationUser Czytelnik
    //ApplicationDbContext CzytelniaContext
    public class CzytelniaContext : IdentityDbContext, ICzytelniaContext
    {
        public CzytelniaContext()
            : base("DefaultConnection")
        {
        }

        public static CzytelniaContext Create()
        {
            return new CzytelniaContext();
        }
        public DbSet<Rodzaj> Rodzaje { get; set; }
        public DbSet<Ksiazka> Ksiazki { get; set; }
        public DbSet<Czytelnik> Czytelnik { get; set; }
        public DbSet<Ksiazka_Rodzaj> Ksiazka_Rodzaj { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Potrzebne dla klas Identity
            base.OnModelCreating(modelBuilder);

            // using System.Data.Entity.ModelConfiguration.Conventions;
            // Brak automatycznego tworzenia liczby mnogiej dla nazw tabel w bazie danych (Rodzajes)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Wyłącza konwencję CascadeDelete
            // CascadeDelete zostanie włączone za pomocą Fluent API
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Używa się Fluent API, aby ustalić powiązanie pomiędzy tabelami 
            // i włączyć CascadeDelete dla tego powiązania
            modelBuilder.Entity<Ksiazka>().HasRequired(x => x.Czytelnik)
                .WithMany(x => x.Ksiazki)
                .HasForeignKey(x => x.CzytelnikId)
                .WillCascadeOnDelete(true);
        }
    }
}