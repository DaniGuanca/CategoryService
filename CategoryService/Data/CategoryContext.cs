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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: CategoryContext.cs, CategoryContext");
                Console.WriteLine(ex.Message);
            }

        }
        public DbSet<Category> Categories { get; set; }
    }
}
