using System.ComponentModel.DataAnnotations;

namespace FlexiHealth.API.Models.Doctor
{
    public class DoctorDto
    {
        public int DoctorId { get; set; }
        [Required]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateJoined { get; set; }

        [StringLength(250)]
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
