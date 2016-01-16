using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Repository.Models
{
    // You can add profile data for the user by adding more properties to your Uzytkownik class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Czytelnik : IdentityUser
    {
        public Czytelnik()
        {
            this.Ksiazki = new HashSet<Ksiazka>();
        }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int? Wiek { get; set; }//nullable (może być puste)

        //pola notmapped nie będzie w bazie danych
        #region dodatkowe pole notmapped

        [NotMapped]     // using System.ComponentModel.DataAnnotations.Schema;
        [Display(Name = "Pan/Pani:")]
        public string PelneNazwisko
        {
            get { return Imie + " " + Nazwisko; }
        }

        #endregion

        public virtual ICollection<Ksiazka> Ksiazki { get; private set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Czytelnik> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}