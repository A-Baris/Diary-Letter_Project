using LetterApp.WEB.IdentityContext.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace LetterApp.WEB.IdentityContext
{
    public class WebContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=AhmetBaris\\SQLEXPRESS;database=LetterAppUsers;uid=sa;pwd=3420;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

    }
}
