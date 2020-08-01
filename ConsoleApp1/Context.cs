using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            modelBuilder.Entity<Namad>()
                .HasMany(c => c.Moamelatss)
                .WithRequired(p => p.Namad)
                .HasForeignKey(k => k.NamadId);
            modelBuilder.Entity<Roz>()
                .HasMany(c => c.Moamelatss)
                .WithRequired(p => p.Roz)
                .HasForeignKey(k => k.RozId);
            modelBuilder.Entity<Moamelat>().HasKey(sc => new { sc.RozId, sc.NamadId });
        }
    }

    public static class DbSetExtensions
    {
        //public static void AddRangeIfNotExists<TEnt, TKey>(this DbSet<TEnt> dbSet, IEnumerable<TEnt> entities, Func<TEnt, TKey> predicate) where TEnt : class
        //{
        //    var entitiesExist = from ent in dbSet
        //                        where entities.Any(add => predicate(ent).Equals(predicate(add)))
        //                        select ent;

        //    dbSet.AddRange(entities.Except(entitiesExist));
        //}
        public static bool AddIfNotExists<T>(this DbSet<T> dbSet, T entities, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {


            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            if (!exists)
            {
                dbSet.Add(entities);
            }
            return exists;
        }

    }
}
