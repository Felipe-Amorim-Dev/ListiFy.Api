using Listify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Infra.Data.Mappings
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("ITEM");

            builder.Property(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("ID");
            builder.Property(i => i.Titulo).HasColumnName("TITULO").HasMaxLength(100).IsRequired();
            builder.Property(i => i.Descricao).HasColumnName("DESCRICAO").HasMaxLength(200).IsRequired();
            builder.Property(i => i.Categoria).HasColumnName("CATEGORIA").HasMaxLength(50).IsRequired();
            builder.Property(i => i.Tipo).HasColumnName("TIPO").HasMaxLength(50).IsRequired();            
            builder.Property(i => i.DataLancamento).HasColumnName("DATALANCAMENTO");
            builder.Property(i => i.DataCriacao).HasColumnName("DATACRIACAO").IsRequired();            

            builder.HasOne(i => i.Usuario).WithMany(u => u.Items).HasForeignKey(i => i.UsuarioID);
            builder.HasMany(i => i.Galeria).WithOne(f => f.Item).HasForeignKey(f => f.ItemId);
        }
    }
}
