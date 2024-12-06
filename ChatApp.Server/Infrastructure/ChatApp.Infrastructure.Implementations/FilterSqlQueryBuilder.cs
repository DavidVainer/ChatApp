using ChatApp.Infrastructure.Services;
using System.Text;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Builder service to construct a filter SQL query to use in a data source.
    /// </summary>
    public class FilterSqlQueryBuilder : IFilterQueryBuilder
    {
        /// <summary>
        /// Builds a filter SQL query dynamically based on non-null properties of the filter object.
        /// </summary>
        /// <param name="tableName">The name of the table to query.</param>
        /// <param name="filter">An object with properties to filter by.</param>
        /// <returns>A tuple containing the SQL query and its parameters.</returns>
        public string Build(object filter, string tableName)
        {
            var query = new StringBuilder($"SELECT * FROM {tableName} WHERE 1=1 ");

            var conditions = filter.GetType().GetProperties()
                .Where(prop => prop.GetValue(filter) != null)
                .Select(prop => $"AND {prop.Name} = @{prop.Name}");

            query.Append(' ').Append(string.Join(" ", conditions));

            return query.ToString();
        }
    }
}
