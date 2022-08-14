using Microsoft.EntityFrameworkCore;
using MSA.Phase2.OfficialAPI.Models;

namespace MSA.Phase2.OfficialAPI.Data
{
    public class TeamAPIDbContext : DbContext
    {
        public TeamAPIDbContext(DbContextOptions options): base(options)
        {
           
        }
        public DbSet<Party> Party { get; set; }
    }
}
