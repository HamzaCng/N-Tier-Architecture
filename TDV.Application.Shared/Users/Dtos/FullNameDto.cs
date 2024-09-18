using System.ComponentModel.DataAnnotations;

namespace TDV.Application.Shared.Users.Dtos
{
    public class FullNameDto
    {
        [Required]
        [StringLength(128, MinimumLength = 1)]
        public string UserFullName { get; set; } = String.Empty;
    }
}
