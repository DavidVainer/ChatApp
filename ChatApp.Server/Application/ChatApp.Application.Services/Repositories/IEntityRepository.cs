namespace ChatApp.Application.Services
{
    /// <summary>
    /// Defines data methods for domain entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IEntityRepository<T> : IValueObjectRepository<T>
        where T : class
    {
        /// <summary>
        /// Updates a record within a data source.
        /// </summary>
        /// <param name="item">The record with the up-to-date properties.</param>
        /// <returns>The updated record.</returns>
        T Update(T item);
    }
}
