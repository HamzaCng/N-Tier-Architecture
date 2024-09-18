
using System.ComponentModel.DataAnnotations;

namespace TDV.Application.Shared.Users.Dtos
{
    public class CreateOrEditUserDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 128, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 128, MinimumLength = 1)]
        public string SurName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
