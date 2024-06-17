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
    public class ItemFotoMap : IEntityTypeConfiguration<ItemFoto>
    {
        public void Configure(EntityTypeBuilder<ItemFoto> builder)
        {
            builder.ToTable("ITEMFOTO");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).HasColumnName("ID");
            builder.Property(f => f.Foto).HasColumnName("FOTO");

            builder.HasOne(f => f.Item).WithMany(i => i.Galeria).HasForeignKey(f => f.ItemId);
        }
    }
}
