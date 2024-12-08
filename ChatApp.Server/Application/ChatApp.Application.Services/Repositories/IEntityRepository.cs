namespace ChatApp.Application.Services
{
    /// <summary>
    /// Defines CRUD operations inside a data source for entities.
    /// </summary>
    /// <typeparam name="T">The type of the value object.</typeparam>
    public interface IEntityRepository<T> : IValueObjectRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets a record by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>Matching record.</returns>
        T GetById(Guid id);
    }
}
