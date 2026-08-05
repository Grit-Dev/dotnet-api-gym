namespace BasicRestApi.Dtos
{
    public class GameResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }

        public string Developer { get; set; } = string.Empty;
    }
}
