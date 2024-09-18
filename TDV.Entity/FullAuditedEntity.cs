
namespace TDV.Core.Entities
{
    public abstract class FullAuditedEntity : BaseEntity
    {
        /// <summary>
        /// Creator user's Id, if this entity is create,
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// Is this entity Updated?
        /// </summary>
        public bool IsUpdated { get; set; }

        /// <summary>
        /// Updated user's Id, if this entity is deleted,
        /// </summary>
        public long? IsUpdatedUser { get; set; }

                /// <summary>
        /// Updated time, if this entity is deleted,
        /// </summary>
        public DateTime? UpdatedTime { get; set; }


        /// <summary>
        /// Deleter user's Id, if this entity is deleted,
        /// </summary>
        public long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time, if this entity is deleted,
        /// </summary>
        public DateTime? DeletionTime { get; set; }
    }
}
