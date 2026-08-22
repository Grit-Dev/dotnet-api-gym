using BasicRestApi.Data;
using BasicRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicRestApi.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;

        public GameService(GameDbContext dbContext)
        {
            _context = dbContext;
        }
        public IReadOnlyList<Platform> GetPlatformsByIds(List<int> platformIds)
        {
            return _context.Platforms
                .Where(platform => platformIds.Contains(platform.Id))
                .ToList();
        }

        public Game? GetGameById(int id)
        {
            Console.WriteLine($"REQUESTED GAME ID: {id}");
            Console.WriteLine($"TOTAL GAMES: {_context.Games.Count()}");

            var game = _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Platforms)
                .FirstOrDefault(g => g.Id == id);

            Console.WriteLine($"FOUND GAME: {game?.Id}");

            return game;
        }

        public Game CreateGame(Game game)
        {
            var createGame = new Game
            {
                Title = game.Title,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                DeveloperId = game.DeveloperId,
                Platforms = game.Platforms
            };

            _context.Games.Add(createGame);
            _context.SaveChanges();

            return GetGameById(createGame.Id)!;
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
            return _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Platforms)
                .ToList();
        }

        public bool UpdateGame(int id, Game game)
        {
            var gameFound = _context.Games
                .Include(g => g.Platforms)
                .FirstOrDefault(g => g.Id == id);

            if (gameFound is null)
            {
                return false;
            }

            gameFound.Title = game.Title;
            gameFound.Genre = game.Genre;
            gameFound.ReleaseYear = game.ReleaseYear;
            gameFound.DeveloperId = game.DeveloperId;

            gameFound.Platforms.Clear();

            var platforms = _context.Platforms
                .Where(p => game.Platforms
                    .Select(x => x.Id)
                    .Contains(p.Id))
                .ToList();

            foreach (var platform in platforms)
            {
                gameFound.Platforms.Add(platform);
            }

            _context.SaveChanges();

            return true;
        }
    }
}
