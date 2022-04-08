using Microsoft.EntityFrameworkCore;
using TestApp.Entities;

namespace TestApp.DataContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
        public DbSet<Report> Report { get; set; }
    }
}
