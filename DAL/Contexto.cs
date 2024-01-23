using Microsoft.EntityFrameworkCore;

namespace TicketsMABB3.DAL
{
    public class Contexto :DbContext
    {
        public DbSet<Models.Tickets> Tickets { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    }
}
