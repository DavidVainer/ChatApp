namespace ChatApp.Application.Services
{
    /// <summary>
    /// Unique identifier generation service for entities.
    /// </summary>
    public interface IEntityIdGenerator
    {
        /// <summary>
        /// Genereates a new Guid unique identifier.
        /// </summary>
        /// <returns>New Guid.</returns>
        Guid Generate();
    }
}
