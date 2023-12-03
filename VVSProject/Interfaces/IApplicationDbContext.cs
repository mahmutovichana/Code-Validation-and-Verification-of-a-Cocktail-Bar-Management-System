using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartCafe.Models;
using System.Threading.Tasks;
using System.Threading;

namespace VVSProject.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Drink> Drinks { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Statistic> Statistics { get; set; }
        DbSet<DrinkIngredient> DrinkIngredients { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
