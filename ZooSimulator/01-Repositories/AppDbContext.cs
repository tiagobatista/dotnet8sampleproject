using Microsoft.EntityFrameworkCore; //Refer working with .NET Libraries
/*
Mention video Building a .NET 8 App with Entity Framework, SQL Server and Docker for more details
Refer that I will leave the video in the description
*/

public class AppDbContext : DbContext
{
    public DbSet<Animal> Animals { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Configure the root type (Animal) with discriminator
        In Entity Framework Core (EF Core), a discriminator is a concept used in Table-per-Hierarchy (TPH)
         inheritance to distinguish between different types of entities stored in the same table.
          Here's an overview of what a discriminator is and how it works:

        What is a Discriminator?
        Purpose: The discriminator is a column in the table that EF Core uses to determine the specific subclass of an entity when querying data.
         It helps EF Core understand which type of entity (e.g., Lion, Eagle, Tiger) each row in the table represents.
        Function: When querying the database, EF Core uses the discriminator column to filter and instantiate the correct subclass
         of the base class (Animal in your example).
        */
        modelBuilder.Entity<Animal>()
            .HasDiscriminator<string>("Name")
            .HasValue<Lion>("Lion")
            .HasValue<Eagle>("Eagle")
            .HasValue<Tiger>("Tiger");

        // Configure MovementType property conversion
        modelBuilder.Entity<Animal>()
            .Property(a => a.MovementType)
            .HasConversion(
                v => v.ToString(), // Store as string
                v => (MovementType)Enum.Parse(typeof(MovementType), v)); // Convert back from string

        // Configure primary key for the base type
        modelBuilder.Entity<Animal>()
            .HasKey(a => a.Id);

        // Configure properties for derived types (not needed as they inherit from Animal)
        modelBuilder.Entity<Lion>()
            .ToTable("Animals"); // Optional: specify table name if needed

        modelBuilder.Entity<Eagle>()
            .ToTable("Animals"); // Optional: specify table name if needed

        modelBuilder.Entity<Tiger>()
            .ToTable("Animals"); // Optional: specify table name if needed
    }

}