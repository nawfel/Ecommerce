using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Reflection;
namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //                           Microsoft.EntityFrameworkCore.Sqlite      
            if(Database.ProviderName == "Microsoft.EntityFrameWorkCore.Sqlite"){
                Directory.CreateDirectory(@"C:\Users\nsghaier\Downloads\StudentAssets (1)\StudentAssets\ApiImages\khra");
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var props = entityType.ClrType.GetProperties().Where(p=>p.PropertyType==typeof(decimal));
                   
                    foreach(var prop in props){
                        modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion<double>();
                    }
                }   
            }
        }

    }
}