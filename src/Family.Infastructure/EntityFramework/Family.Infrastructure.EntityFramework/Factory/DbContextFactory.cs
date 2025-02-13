using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Infrastructure.EntityFramework.Factory
{
    public class DbContextFactory : IDesignTimeDbContextFactory<FamilyDbContext>
    {
        public FamilyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FamilyDbContext>();
            optionsBuilder.UseNpgsql("Host=postgres;Port=5432;Database=FamilyDb;Username=postgres;Password=postgres");

            return new FamilyDbContext(optionsBuilder.Options);
        }
    }
}
