using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeek8.Core.Models;

namespace TestWeek8.EF.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email)
                   .IsRequired();
            builder.Property(u => u.Password)
                   .HasMaxLength(16)
                   .IsRequired();
            builder.Property(u => u.Role)
                   .IsRequired();

            builder.HasData(
                new User {
                    Id=1,
                    Email="mrossi@prova.it",
                    Password="12345",
                    Role = Role.Ristoratore
                },
                 new User
                 {
                     Id = 2,
                     Email = "gverdi@prova.it",
                     Password = "67890",
                     Role = Role.Cliente
                 }
                );
        }
    }
}
