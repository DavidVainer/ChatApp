namespace ChatApp.Application.Services.Repositories
{
    /// <summary>
    /// Defines CRUD operations inside a data source for value objects.
    /// </summary>
    /// <typeparam name="T">The type of the value object.</typeparam>
    public interface IValueObjectRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets all records from a data source.
        /// </summary>
        /// <returns>Collection of all existing records.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets records matching the non-null properties of the filter object.
        /// </summary>
        /// <param name="item">An object with properties to filter by.</param>
        /// <returns>Collection of matching records.</returns>
        IEnumerable<T> GetByFilter(T item);

        /// <summary>
        /// Inserts a record into a data source.
        /// </summary>
        /// <param name="item">The record to be added.</param>
        /// <returns>The inserted record.</returns>
        T Insert(T item);

        /// <summary>
        /// Deletes a record by its unique identifier.
        /// </summary>
        /// <param name="item">The record to be deleted.</param>
        /// <returns>The deleted record.</returns>
        T Delete(T item);
    }
}
