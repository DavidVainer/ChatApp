namespace ChatApp.Infrastructure.Models.RepositorySettings
{
    /// <summary>
    /// Represents the settings and configurations to be used in a repository.
    /// </summary>
    public class RepositorySettings : IRepositorySettings
    {
        /// <summary>
        /// The name of the database table associated with the repository.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// The query used to retrieve all records from the table.
        /// </summary>
        public string GetAllQuery { get; set; }

        /// <summary>
        /// The query used to retrieve a record by its unique identifier.
        /// </summary>
        public string GetByIdQuery { get; set; }

        /// <summary>
        /// The query used to insert a new record into the table.
        /// </summary>
        public string InsertQuery { get; set; }

        /// <summary>
        /// The query used to delete a record from the table.
        /// </summary>
        public string DeleteQuery { get; set; }
    }
}
