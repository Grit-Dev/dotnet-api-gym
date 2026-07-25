using BasicRestApi.Models;

namespace BasicRestApi.Services
{
    public interface IGameService
    {
        IReadOnlyList<Game> GetGames();
        Game? GetGameById(int id);

        Game CreateGame(Game game);

        bool UpdateGame(int id, Game game);

        bool DeleteGame(int id);
    }
}
