using TDV.Core.Entities;

namespace TDV.Entity.Entities.Users
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string SurName { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;

    }
}
