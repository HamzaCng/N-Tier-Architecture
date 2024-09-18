namespace TDV.Core.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identity Number.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Is this entity created Date?
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Is this entity deleted?
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
