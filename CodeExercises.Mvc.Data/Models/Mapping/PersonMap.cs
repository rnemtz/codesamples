using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CodeExercises.Mvc.Data.Models.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            HasKey(t => t.PersonId);

            // Properties
            Property(t => t.PersonId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Person");
            Property(t => t.PersonId).HasColumnName("PersonId");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.Age).HasColumnName("Age");
        }
    }
}