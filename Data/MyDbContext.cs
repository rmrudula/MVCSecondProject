using Microsoft.EntityFrameworkCore;
using MVCSecondProject.Models.Domain;

namespace MVCSecondProject.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
    }

}
