using Listify.Domain.Entities;
using Listify.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemFotoMap());
            modelBuilder.ApplyConfiguration(new ItemMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        public DbSet<ItemFoto> ItemFoto { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
