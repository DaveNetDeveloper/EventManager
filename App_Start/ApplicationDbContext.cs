using EventManager;
using MySql.Data.Entity;
using System.Data.Entity;

[DbConfigurationType(typeof(MySqlEFConfiguration))]
public class ApplicationDbContext : DbContext
{
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Event> Event { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<Language> Language { get; set; }

    public ApplicationDbContext() : base("DefaultConnection")
    {
    }
}