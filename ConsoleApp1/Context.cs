using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Context :DbContext
    {
        public Context() : base("Bors")
        {

        }

        public DbSet<Roz> Rozs { get; set; }
        public DbSet<Namad> Namads { get; set; }
        public DbSet<Moamelat> Moamelats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moamelat>()
                .HasRequired(c => c.Roz)
                .WithMany(p => p.Moamelatss)
                .HasForeignKey(k => k.RozId);
            modelBuilder.Entity<Moamelat>()
                .HasRequired(c => c.Namad)
                .WithMany(p => p.Moamelatss)
                .HasForeignKey(k => k.NamadId);
        }
    }
}
