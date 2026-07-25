using BasicRestApi.Models;

namespace BasicRestApi.Services
{
    public class GameService : IGameService
    {
        private static readonly List<Game> _games =
        [
            new() { Id = 1, Title = "Witcher 3", Genre = "Action RPG", ReleaseYear = 2020},
                    new() { Id = 2, Title = "Cyberpunk 2077", Genre = "Action RPG", ReleaseYear = 2020},
                    new() { Id = 3, Title = "Crimson Desert", Genre = "Action RPG", ReleaseYear = 2020}
        ];

        public Game CreateGame(Game game)
        {
            var newGameId = _games.Count == 0 ? 1 : _games.Max(g => g.Id) + 1;

            var createGame = new Game
            {
                Id = newGameId,
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear
            };

            _games.Add(createGame);

            return createGame;
        }

        public bool DeleteGame(int id)
        {
            var gameFound = _games.FirstOrDefault(g => g.Id == id);

            if (gameFound == null)
            {
                return false;
            }

            _games.Remove(gameFound);

            return true;
        }

        public Game? GetGameById(int id)
        {
            var game = _games.FirstOrDefault(x => x.Id == id);

            return game;
        }

        public IReadOnlyList<Game> GetGames()
        {
            return (_games);
        }

        public bool UpdateGame(int id, Game game)
        {
            var gameFound = _games.FirstOrDefault(g => g.Id == id);

            if (gameFound is null)
            {
                return false;
            }

            gameFound.Title = game.Title;
            gameFound.Genre = game.Genre;
            gameFound.ReleaseYear = game.ReleaseYear;

            return true;
        }
    }
}
