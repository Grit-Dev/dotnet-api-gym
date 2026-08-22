namespace BasicRestApi.Models
{
    public class Platform
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }

        public List<Game> Games { get; set; } = [];
    }
}
