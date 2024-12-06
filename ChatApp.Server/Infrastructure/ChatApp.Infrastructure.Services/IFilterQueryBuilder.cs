namespace ChatApp.Infrastructure.Services
{
    /// <summary>
    /// Builder service to construct a filter query to use in a data source.
    /// </summary>
    public interface IFilterQueryBuilder
    {
        /// <summary>
        /// Builds a filter query dynamically based on non-null properties of the filter object.
        /// </summary>
        /// <param name="tableName">The name of the table to query.</param>
        /// <param name="filter">An object with properties to filter by.</param>
        /// <returns>A tuple containing the query and its parameters.</returns>
        string Build(object filter, string tableName);
    }
}
