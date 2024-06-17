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
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");
            
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("ID");
            builder.Property(u => u.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            builder.Property(u => u.Sobrenome).HasColumnName("SOBRENOME").HasMaxLength(150).IsRequired();
            builder.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(100).IsRequired();
            builder.Property(u => u.DataNascimento).HasColumnName("DATANASCIMENTO").IsRequired();
            builder.Property(u => u.Telefone).HasColumnName("TELEFONE").HasMaxLength(15).IsRequired();
            builder.Property(u => u.Senha).HasColumnName("SENHA").HasMaxLength(50).IsRequired();
            builder.Property(u => u.FotoPerfil).HasColumnName("FOTOPERFIL");
            builder.Property(u => u.DataCriacao).HasColumnName("DATACRIACAO");
            builder.Property(u => u.DataAlteracao).HasColumnName("DATAAÇTERACAO");

            builder.Ignore(u => u.AccessToken);
        }
    }
}
