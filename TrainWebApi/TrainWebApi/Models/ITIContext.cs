using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TrainWebApi.Models
{
    public partial class ITIContext : DbContext
    {
        public ITIContext()
            : base("name=ITIContext")
        {
        }

        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<department>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<department>()
                .Property(e => e.location)
                .IsFixedLength();

            modelBuilder.Entity<department>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.department)
                .HasForeignKey(e => e.deptid);

            modelBuilder.Entity<Student>()
                .Property(e => e.name)
                .IsFixedLength();
        }
    }
}
