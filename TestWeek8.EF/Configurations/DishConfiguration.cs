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
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(d => d.Description)
                   .HasMaxLength(400)
                   .IsRequired();
            builder.Property(d => d.Price)
                   .IsRequired();
            builder.Property(d=>d.Type)
                    .IsRequired()
                    .HasConversion(
                    v => v.ToString(),
                    v => (Typology)Enum.Parse(typeof(Typology), v));

            builder.HasOne(d => d.Menu)
                   .WithMany(m => m.Dishes);

            builder.HasData(
                new Dish
                {
                    Id=1,
                    Name="Tortiglioni alla norma",
                    Description="Tortiglioni conditi con un sugo di pomodoro, melanzane fritte e formaggio a cubetti",
                    Type=Typology.First,
                    Price=9.90m,
                    MenuId=1
                },
                  new Dish
                  {
                      Id = 2,
                      Name = "Calamari fritti",
                      Description = "Calamari freschi tagliati ad anelli e fritti",
                      Type = Typology.Second,
                      Price = 15.50m,
                      MenuId = 2
                  },
                    new Dish
                    {
                        Id = 3,
                        Name = "Patate al forno",
                        Description = "Patae al forno con rosmarino",
                        Type = Typology.Side,
                        Price = 4.85m,
                        MenuId = 1
                    }
                );
        }
    }
}
