using System.Data.Entity;
using Persistence.Model;

namespace Persistence
{
    public class StoreContext : DbContext
    {
        // Your context has been configured to use a 'StoreContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'App.StoreContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'StoreContext' 
        // connection string in the application configuration file.
        public StoreContext()
            : base("name=StoreContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<BasketEntry> BasketEntries { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}