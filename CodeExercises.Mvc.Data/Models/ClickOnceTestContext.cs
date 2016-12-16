using System.Data.Entity;
using CodeExercises.Mvc.Data.Models.Mapping;

namespace CodeExercises.Mvc.Data.Models
{
    public class ClickOnceTestContext : DbContext
    {
        static ClickOnceTestContext()
        {
            Database.SetInitializer<ClickOnceTestContext>(null);
        }

        public ClickOnceTestContext()
            : base("Name=ClickOnceTestContext")
        {
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
        }
    }
}