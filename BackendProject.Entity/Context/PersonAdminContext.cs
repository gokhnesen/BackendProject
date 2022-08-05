using BackendProject.Entity.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace BackendProject.Entity.Context
{
    public class PersonAdminContext:DbContext
    {
        public PersonAdminContext(DbContextOptions<PersonAdminContext> options):base(options)
        {


        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<KPI> KPI { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<User> User { get; set; }

    }
}
