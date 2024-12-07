namespace ChatApp.Infrastructure.Models
{
    /// <summary>
    /// Represents the settings and configurations to be used in a repository.
    /// </summary>
    public interface IRepositorySettings
    {
        /// <summary>
        /// The name of the database table associated with the repository.
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// The query used to retrieve all records from the table.
        /// </summary>
        string GetAllQuery { get; }

        /// <summary>
        /// The query used to insert a new record into the table.
        /// </summary>
        string InsertQuery { get; }

        /// <summary>
        /// The query used to delete a record from the table.
        /// </summary>
        string DeleteQuery { get; }
    }
}
