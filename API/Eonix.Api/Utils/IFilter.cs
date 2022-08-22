namespace Eonix.Api.Utils
{
    public interface IFilter<T>
    {
        public IQueryable<T> ApplyTo(IQueryable<T> query);
    }
}
