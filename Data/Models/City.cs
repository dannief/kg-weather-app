using System.ComponentModel.DataAnnotations;

namespace KG.Weather.Data.Models
{
    public class City : BaseEntity
    {
        [MaxLength(32)]
        public string Name { get; set; }

        public Country Country { get; set; }

        public string FullName => $"{Name}, {Country.Value}";
    }
}
