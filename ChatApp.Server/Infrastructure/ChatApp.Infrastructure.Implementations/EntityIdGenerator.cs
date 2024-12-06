using ChatApp.Application.Services;

namespace ChatApp.Infrastructure.Implementations
{
    /// <summary>
    /// Unique identifier generation service for entities.
    /// </summary>
    public class EntityIdGenerator : IEntityIdGenerator
    {
        /// <summary>
        /// Genereates a new Guid unique identifier.
        /// </summary>
        /// <returns>New Guid.</returns>
        public Guid Generate()
        {
            var id = Guid.NewGuid();
            return id;
        }
    }
}
