using HttpRequestLoggingHandler.EFCore.BaseEntities;
using HttpRequestLoggingHandler.EFCore.DBEntities;
using System.Linq.Expressions;

namespace HttpRequestLoggingHandler.EFCore;

public class AppDbContext : DbContext
{
    public DbSet<JsonServerApiLog> JsonServerApiLogs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplySoftDeleteQueryFilters(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private void ApplySoftDeleteQueryFilters(ModelBuilder modelBuilder)
    {
        const string softDeletePropertyName = nameof(BaseEntity.DeleteAt);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var propertyMethodInfo = typeof(EF).GetMethod(nameof(EF.Property))!.MakeGenericMethod(typeof(DateTime?));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant(softDeletePropertyName));
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(null));
                var lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }
}
