using Microsoft.EntityFrameworkCore;

namespace Eonix.Api.Utils
{
    public static class DbSetExtensions
    {
        public static async Task<T> LoadAsync<T, TKey>(this DbSet<T> dbSet, TKey id, CancellationToken cancellationToken)
            where T : class
        {
            var entity = await dbSet.FindAsync(new object[] { id }, cancellationToken);

            return entity ?? throw new NullReferenceException($"Entity {typeof(T).Name} not found: {id}");
        }
    }
}
