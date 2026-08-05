using BasicRestApi.Data;
using BasicRestApi.Models;

namespace BasicRestApi.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;

        public GameService(GameDbContext dbContext)
        {
            _context = dbContext;
        }

        public Game? GetGameById(int id) => _context.Games.Find(id);

        public Game CreateGame(Game game)
        {
            var createGame = new Game
            {
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                Developer = game.Developer
            };

            _context.Games.Add(createGame);
            _context.SaveChanges();

            return createGame;
        }

        public bool DeleteGame(int id)
        {
            var gameDelete = _context.Games.Find(id);

            if (gameDelete == null)
            {
                return false;
            }

            _context.Games.Remove(gameDelete);
            _context.SaveChanges();

            return true;
        }

        public IReadOnlyList<Game> GetGames()
        {
            return _context.Games.ToList();
        }

        public bool UpdateGame(int id, Game game)
        {
            var gameFound = _context.Games.Find(id);

            if (gameFound is null)
            {
                return false;
            }

            gameFound.Title = game.Title;
            gameFound.Genre = game.Genre;
            gameFound.ReleaseYear = game.ReleaseYear;
            gameFound.Developer = game.Developer;

            _context.SaveChanges();

            return true;
        }
    }
}
