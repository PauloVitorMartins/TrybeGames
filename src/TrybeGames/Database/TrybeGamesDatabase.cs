namespace TrybeGames;
public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        var gamesDeveloped = from game in Games
                             where gameStudio.Id == game.DeveloperStudio
                             select game;
        List<Game> gamesDevFiltered = new(gamesDeveloped);
        return gamesDevFiltered;
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        var gamesPlayed = from game in Games
                          where game.Players.Contains(player.Id)
                          select game;
        List<Game> gamesDevFiltered = new(gamesPlayed);
        return gamesDevFiltered;
    }

    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        var gamesOwned = from game in Games
                         join gameId in playerEntry.GamesOwned on game.Id equals gameId
                         select game;
        List<Game> gamesDevFiltered = new(gamesOwned);
        return gamesDevFiltered;
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        var gameWithStudi = from game in Games
                            from gameStudio in GameStudios
                            where game.DeveloperStudio == gameStudio.Id
                            select new GameWithStudio { GameName = game.Name, StudioName = gameStudio.Name, NumberOfPlayers = game.Players.Count };
        return gameWithStudi.ToList();


    }

    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        var gameTypes = from game in Games
                        select game.GameType;
        return gameTypes.Distinct().ToList();
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        GamePlayer gameInfo(Game game)
        {
            var listOfPlayersId = game.Players;
            var listOfPlayers = from id in listOfPlayersId
                                from player in Players
                                where id == player.Id
                                select player;
            return new GamePlayer { GameName = game.Name, Players = listOfPlayers.ToList() };
        }
        var studiosWithGames = from studio in GameStudios
                                   //    from game in Games
                                   //    where game.DeveloperStudio == studio.Id
                               select new StudioGamesPlayers
                               {
                                   GameStudioName = studio.Name,
                                   Games = (from game in Games
                                            where game.DeveloperStudio == studio.Id
                                            select gameInfo(game)).ToList()
                               };
        return studiosWithGames.ToList();
    }

}
