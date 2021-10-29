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
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasMany(m => m.Dishes)
                   .WithOne(d => d.Menu);

            builder.HasData(
                new Menu
                {
                    Id=1,
                    Name= "Turistico"
                },
                 new Menu
                 {
                     Id = 2,
                     Name = "Assaggi di pesce"
                 });
        }
    }
}
