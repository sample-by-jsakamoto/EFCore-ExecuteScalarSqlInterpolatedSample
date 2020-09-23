using ExecuteScalarInterpolatedSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ExecuteScalarInterpolatedSample.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}
