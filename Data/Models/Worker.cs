using System.ComponentModel.DataAnnotations;

namespace KG.Weather.Data.Models
{
    public class Worker: BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(32)]
        public string Phone { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public WorkerType Type { get; set; }
    }
}
