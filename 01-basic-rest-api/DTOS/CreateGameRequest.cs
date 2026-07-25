using System.ComponentModel.DataAnnotations;

namespace BasicRestApi.Dtos
{
    public class CreateGameRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Range(1950, 2100)]   
        public int ReleaseYear { get; set; }
    }
}
