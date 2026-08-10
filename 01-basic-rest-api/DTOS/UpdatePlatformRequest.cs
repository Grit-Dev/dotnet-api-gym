using System.ComponentModel.DataAnnotations;

namespace BasicRestApi.Dtos
{
    public class UpdatePlatformRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; } = string.Empty;

        [Range(1970, 2100)]
        public int ReleaseYear { get; set; }
    }
}
