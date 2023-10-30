using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCafe.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCafe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<DrinkIngredient> DrinkIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>().ToTable("Drink");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<Statistic>().ToTable("Statistic");
            modelBuilder.Entity<DrinkIngredient>().ToTable("DrinkIngredient");
            base.OnModelCreating(modelBuilder);
        }
    }
}
