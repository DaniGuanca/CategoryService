using CategoryService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CategoryService.Data
{
    public class CategoryContext : DbContext
    {
        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options) 
        {
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreator != null )
            {
                if (!dbCreator.CanConnect())
                    dbCreator.Create();

                if(!dbCreator.HasTables())
                    dbCreator.CreateTables();
            }

        }
        public DbSet<Category> Categories { get; set; }
    }
}
