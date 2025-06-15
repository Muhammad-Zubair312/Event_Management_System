using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Models
{
 
    public class MyAppDB : DbContext
    {
        public DbSet<Add_Event> Add_Event { get; set; }

        public DbSet<Ticket> Ticket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EMS;Integrated Security=True");
        }
    }
}
