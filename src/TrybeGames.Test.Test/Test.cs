using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using TrybeGames;
using Moq;


namespace TrybeGames.Test.Test;

public class TestReq01
{
    public static TheoryData<string, Player> DataTestTestAddPlayer => new()
    {
        {
            "Teste",
            new Player(){ Name = "Teste", Id = 1 }
        },
        {
            "Teste1",
            new Player(){ Name = "Teste1", Id = 1 }
        }
    };

    [Trait("Category", "1. Crie a funcionalidde para adicionar uma nova pessoa jogadora ao banco de dados")]
    [Theory(DisplayName = "Deve cadastrar uma nova pessoa jogadora com sucesso!")]
    [MemberData(nameof(DataTestTestAddPlayer))]    
    public void TestAddPlayer(string name, Player expected)
    {
        var mockConsole = new Mock<IConsole>();
        mockConsole.Setup(c => c.ReadLine()).Returns(name);
        var database = new TrybeGamesDatabase();
        var controller = new TrybeGamesController(database, mockConsole.Object);

        controller.AddPlayer();
        controller.database.Players[0].Should().BeEquivalentTo(expected);
    }
}


public class TestReq02
{
    public static TheoryData<string, GameStudio> DataTestTestAddGameStudio => new()
    {
        {
            "Teste",
            new GameStudio(){ Name = "Teste", Id = 1 }
        },
        {
            "Teste1",
            new GameStudio(){ Name = "Teste1", Id = 1 }
        }
    };

    [Trait("Category", "2. Crie a funcionalidade de adicionar um novo estúdio de jogos ao banco de dados")]
    [Theory(DisplayName = "Deve cadastrar um novo estúdio de jogos com sucesso!")]
    [MemberData(nameof(DataTestTestAddGameStudio))]    
    public void TestAddGameStudio(string name, GameStudio expected)
    {
        var mockConsole = new Mock<IConsole>();
        mockConsole.Setup(c => c.ReadLine()).Returns(name);
        var database = new TrybeGamesDatabase();
        var controller = new TrybeGamesController(database, mockConsole.Object);

        controller.AddGameStudio();
        controller.database.GameStudios[0].Name.Should().BeEquivalentTo(expected.Name);
        controller.database.GameStudios[0].Id.Should().Be(expected.Id);
    }
}


public class TestReq03
{
    public static TheoryData<string, string, string, Game> DataTestTestAddGame => new ()
    {
        {
            "Teste",
            "01/01/2020",
            "0",
            new Game(){ Name = "Teste", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Action }
        },
        {
            "Teste1",
            "01/01/2020",
            "1",
            new Game(){ Name = "Teste1", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Adventure }
        },
        {
            "Teste2",
            "01/01/2020",
            "4",
            new Game(){ Name = "Teste2", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Simulation }
        },
        {
            "Teste3",
            "01/01/2020",
            "0",
            new Game(){ Name = "Teste3", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Action }
        },
        {
            "Teste4",
            "01/01/2020",
            "0",
            new Game(){ Name = "Teste4", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Action }
        },
        {
            "Teste5",
            "01/01/2020",
            "3",
            new Game(){ Name = "Teste5", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Strategy }
        },
        {
            "Teste6",
            "02/01/2020",
            "0",
            new Game(){ Name = "Teste6", Id = 1, ReleaseDate = new DateTime(2020, 01, 02), GameType = GameType.Action }
        },
        {
            "Teste7",
            "01/01/2020",
            "0",
            new Game(){ Name = "Teste7", Id = 1, ReleaseDate = new DateTime(2020, 01, 01), GameType = GameType.Action }
        },
    };

    [Trait("Category", "3. Crie a funcionalidade de adicionar novo Jogo ao Banco de dados")]
    [Theory(DisplayName = "Deve cadastrar um novo jogo com sucesso!")]
    [MemberData(nameof(DataTestTestAddGame))]    
    public void TestAddGame(string name, string releaseDate, string gameType, Game expected)
    {
        var mockConsole = new Mock<IConsole>();
        mockConsole.SetupSequence(c => c.ReadLine()).Returns(name).Returns(releaseDate).Returns(gameType);
        var database = new TrybeGamesDatabase();
        var controller = new TrybeGamesController(database, mockConsole.Object);

        controller.AddGame();
        controller.database.Games[0].Name.Should().BeEquivalentTo(expected.Name);
        controller.database.Games[0].ReleaseDate.Should().Be(expected.ReleaseDate);
        controller.database.Games[0].GameType.Should().Be(expected.GameType);
        controller.database.Games[0].Id.Should().Be(expected.Id);
    }
}

public class TestReq04
{
    public static TheoryData<List<Game>, List<GameStudio>, List<Player>> DataTestDatabase = new () 
    {
        {
            new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Name = "Valorant",
                        DeveloperStudio = 2,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 4 }
                    },
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
                },
            new List<GameStudio>
                {
                    new GameStudio
                    {
                        Id = 1,
                        Name = "Ubisoft"
                    },
                    new GameStudio
                    {
                        Id = 2,
                        Name = "Riot Games"
                    }
                },

            new List<Player>
                {
                    new Player
                    {
                        Id = 1,
                        Name = "Joana",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    },
                    new Player
                    {
                        Id = 2,
                        Name = "Maria",
                        GamesOwned = new List<int> { 2, 3, 4 }
                    },
                    new Player
                    {
                        Id = 3,
                        Name = "Juliana",
                        GamesOwned = new List<int> { 2, 4 }
                    },
                    new Player
                    {
                        Id = 4,
                        Name = "Francisca",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    }
                }   
        }
    };

    TrybeGamesDatabase database;
    public TestReq04()
    {
        database = new TrybeGamesDatabase();
    }

    [Trait("Category", "4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos")]
    [Theory(DisplayName = "Deve retornar a busca com sucesso")]
    [MemberData(nameof(DataTestDatabase))]       
    public void GetGamesDevelopedBy(List<Game> gameList, List<GameStudio> gameStudioList, List<Player> playerList)
    {
        database.Games = gameList;
        database.GameStudios = gameStudioList;
        database.Players = playerList;

        var consult = database.GetGamesDevelopedBy(gameStudioList[0]);
        var expected = new List<Game>() {
            new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    }
        };

        consult.Should().BeEquivalentTo(expected);

        
    }
}

public class TestReq05
{
    public static TheoryData<List<Game>, List<GameStudio>, List<Player>> DataTestDatabase = new () 
    {
        {
            new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Name = "Valorant",
                        DeveloperStudio = 2,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 4 }
                    },
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
                },
            new List<GameStudio>
                {
                    new GameStudio
                    {
                        Id = 1,
                        Name = "Ubisoft"
                    },
                    new GameStudio
                    {
                        Id = 2,
                        Name = "Riot Games"
                    }
                },

            new List<Player>
                {
                    new Player
                    {
                        Id = 1,
                        Name = "Joana",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    },
                    new Player
                    {
                        Id = 2,
                        Name = "Maria",
                        GamesOwned = new List<int> { 2, 3, 4 }
                    },
                    new Player
                    {
                        Id = 3,
                        Name = "Juliana",
                        GamesOwned = new List<int> { 2, 4 }
                    },
                    new Player
                    {
                        Id = 4,
                        Name = "Francisca",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    }
                }   
        }
    };

    TrybeGamesDatabase database;
    public TestReq05()
    {
        database = new TrybeGamesDatabase();
    }

    [Trait("Category", "5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora")]
    [Theory(DisplayName = "Deve retornar a busca com sucesso")]
    [MemberData(nameof(DataTestDatabase))]       
    public void GetGamesPlayedBy(List<Game> gameList, List<GameStudio> gameStudioList, List<Player> playerList)
    {
        database.Games = gameList;
        database.GameStudios = gameStudioList;
        database.Players = playerList;

        var consult = database.GetGamesPlayedBy(playerList[1]);
        var expected = new List<Game>() {
              new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
        };
        consult.Should().BeEquivalentTo(expected);
    }

}

public class TestReq06
{
    public static TheoryData<List<Game>, List<GameStudio>, List<Player>> DataTestDatabase = new () 
    {
        {
            new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Name = "Valorant",
                        DeveloperStudio = 2,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 4 }
                    },
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
                },
            new List<GameStudio>
                {
                    new GameStudio
                    {
                        Id = 1,
                        Name = "Ubisoft"
                    },
                    new GameStudio
                    {
                        Id = 2,
                        Name = "Riot Games"
                    }
                },

            new List<Player>
                {
                    new Player
                    {
                        Id = 1,
                        Name = "Joana",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    },
                    new Player
                    {
                        Id = 2,
                        Name = "Maria",
                        GamesOwned = new List<int> { 2, 3, 4 }
                    },
                    new Player
                    {
                        Id = 3,
                        Name = "Juliana",
                        GamesOwned = new List<int> { 2, 4 }
                    },
                    new Player
                    {
                        Id = 4,
                        Name = "Francisca",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    }
                }   
        }
    };

    TrybeGamesDatabase database;
    public TestReq06()
    {
        database = new TrybeGamesDatabase();
    }


    [Trait("Category", "6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora")]
    [Theory(DisplayName = "Deve retornar a busca com sucesso")]
    [MemberData(nameof(DataTestDatabase))]       
    public void GetGamesOwnedBy(List<Game> gameList, List<GameStudio> gameStudioList, List<Player> playerList)
    {
        database.Games = gameList;
        database.GameStudios = gameStudioList;
        database.Players = playerList;

        var consulta = database.GetGamesOwnedBy(playerList[2]);
        var expected = new List<Game>() {
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
            
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
        };
        consulta.Should().BeEquivalentTo(expected);
    }
}

public class TestReq07
{
    public static TheoryData<List<Game>, List<GameStudio>, List<Player>> DataTestDatabase = new () 
    {
        {
            new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Name = "Valorant",
                        DeveloperStudio = 2,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 4 }
                    },
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
                },
            new List<GameStudio>
                {
                    new GameStudio
                    {
                        Id = 1,
                        Name = "Ubisoft"
                    },
                    new GameStudio
                    {
                        Id = 2,
                        Name = "Riot Games"
                    }
                },

            new List<Player>
                {
                    new Player
                    {
                        Id = 1,
                        Name = "Joana",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    },
                    new Player
                    {
                        Id = 2,
                        Name = "Maria",
                        GamesOwned = new List<int> { 2, 3, 4 }
                    },
                    new Player
                    {
                        Id = 3,
                        Name = "Juliana",
                        GamesOwned = new List<int> { 2, 4 }
                    },
                    new Player
                    {
                        Id = 4,
                        Name = "Francisca",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    }
                }   
        }
    };

    TrybeGamesDatabase database;
    public TestReq07()
    {
        database = new TrybeGamesDatabase();
    }


    [Trait("Category", "7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor")]
    [Theory(DisplayName = "Deve retornar a busca com sucesso")]
    [MemberData(nameof(DataTestDatabase))]     
    public void GetGamesWithStudio(List<Game> gameList, List<GameStudio> gameStudioList, List<Player> playerList)
    {
        database.Games = gameList;
        database.GameStudios = gameStudioList;
        database.Players = playerList;

        var consulta = database.GetGamesWithStudio();
        var expected = new List<GameWithStudio>() {
            new GameWithStudio
            {
                GameName = "Assassins Creed",
                StudioName = "Ubisoft",
                NumberOfPlayers = 2
            },
            new GameWithStudio
            {
                GameName = "Far Cry",
                StudioName = "Ubisoft",
                NumberOfPlayers = 3
            },
            new GameWithStudio
            {
                GameName = "League of Legends",
                StudioName = "Riot Games",
                NumberOfPlayers = 4
            },
            new GameWithStudio
            {
                GameName = "Valorant",
                StudioName = "Riot Games",
                NumberOfPlayers = 2
            }
        };
        consulta.Should().BeEquivalentTo(expected);
    }

}

public class TestReq08
{
    public static TheoryData<List<Game>, List<GameStudio>, List<Player>> DataTestDatabase = new () 
    {
        {
            new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Name = "Valorant",
                        DeveloperStudio = 2,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 4 }
                    },
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
                },
            new List<GameStudio>
                {
                    new GameStudio
                    {
                        Id = 1,
                        Name = "Ubisoft"
                    },
                    new GameStudio
                    {
                        Id = 2,
                        Name = "Riot Games"
                    }
                },

            new List<Player>
                {
                    new Player
                    {
                        Id = 1,
                        Name = "Joana",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    },
                    new Player
                    {
                        Id = 2,
                        Name = "Maria",
                        GamesOwned = new List<int> { 2, 3, 4 }
                    },
                    new Player
                    {
                        Id = 3,
                        Name = "Juliana",
                        GamesOwned = new List<int> { 2, 4 }
                    },
                    new Player
                    {
                        Id = 4,
                        Name = "Francisca",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    }
                }   
        }
    };

    TrybeGamesDatabase database;
    public TestReq08()
    {
        database = new TrybeGamesDatabase();
    }


    [Trait("Category", "8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados")]
    [Theory(DisplayName = "Deve retornar a busca com sucesso")]
    [MemberData(nameof(DataTestDatabase))]   
    public void GetGameTypes(List<Game> gameList, List<GameStudio> gameStudioList, List<Player> playerList)
    {
        database.Games = gameList;
        database.GameStudios = gameStudioList;
        database.Players = playerList;

        var consulta = database.GetGameTypes();
        var expected = new List<GameType>(){
            GameType.Action,
            GameType.Adventure,
            GameType.Strategy
        };

        consulta.Should().BeEquivalentTo(expected);
    }

}

public class TestReq09
{
    public static TheoryData<List<Game>, List<GameStudio>, List<Player>> DataTestDatabase = new () 
    {
        {
            new List<Game>
                {
                    new Game
                    {
                        Id = 1,
                        Name = "Valorant",
                        DeveloperStudio = 2,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 4 }
                    },
                    new Game
                    {
                        Id = 2,
                        Name = "Assassins Creed",
                        DeveloperStudio = 1,
                        GameType = GameType.Adventure,
                        Players = new List<int> { 2, 3 }
                    },
                    new Game
                    {
                        Id = 3,
                        Name = "Far Cry",
                        DeveloperStudio = 1,
                        GameType = GameType.Action,
                        Players = new List<int> { 1, 2, 4 }
                    },
                    new Game
                    {
                        Id = 4,
                        Name = "League of Legends",
                        DeveloperStudio = 2,
                        GameType = GameType.Strategy,
                        Players = new List<int> { 1, 2, 3, 4 }
                    }
                },
            new List<GameStudio>
                {
                    new GameStudio
                    {
                        Id = 1,
                        Name = "Ubisoft"
                    },
                    new GameStudio
                    {
                        Id = 2,
                        Name = "Riot Games"
                    }
                },

            new List<Player>
                {
                    new Player
                    {
                        Id = 1,
                        Name = "Joana",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    },
                    new Player
                    {
                        Id = 2,
                        Name = "Maria",
                        GamesOwned = new List<int> { 2, 3, 4 }
                    },
                    new Player
                    {
                        Id = 3,
                        Name = "Juliana",
                        GamesOwned = new List<int> { 2, 4 }
                    },
                    new Player
                    {
                        Id = 4,
                        Name = "Francisca",
                        GamesOwned = new List<int> { 1, 3, 4 }
                    }
                }   
        }
    };

    TrybeGamesDatabase database;
    public TestReq09()
    {
        database = new TrybeGamesDatabase();
    }


    [Trait("Category", "9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras")]
    [Theory(DisplayName = "Deve retornar a busca com sucesso")]
    [MemberData(nameof(DataTestDatabase))]   
    public void GetStudiosWithGamesAndPlayers(List<Game> gameList, List<GameStudio> gameStudioList, List<Player> playerList)
    {
        database.Games = gameList;
        database.GameStudios = gameStudioList;
        database.Players = playerList;

        var consulta = database.GetStudiosWithGamesAndPlayers();
        var expected = new List<StudioGamesPlayers>() {
            new StudioGamesPlayers
            {
                GameStudioName = "Riot Games",
                Games = new List<GamePlayer>() {
                    new GamePlayer
                    {
                        GameName = "League of Legends",
                        Players = new List<Player>(){
                            new Player
                            {
                                Id = 4,
                                Name = "Francisca",
                                GamesOwned = new List<int> { 1, 3, 4 }
                            },
                            new Player
                            {
                                Id = 1,
                                Name = "Joana",
                                GamesOwned = new List<int> { 1, 3, 4 }
                            },
                            new Player
                            {
                                Id = 3,
                                Name = "Juliana",
                                GamesOwned = new List<int> { 2, 4 }
                            },
                            new Player
                            {
                                Id = 2,
                                Name = "Maria",
                                GamesOwned = new List<int> { 2, 3, 4 }
                            }
                        }
                    }, 
                    new GamePlayer
                    {
                        GameName = "Valorant",
                        Players = new List<Player>(){
                            new Player
                            {
                                Id = 4,
                                Name = "Francisca",
                                GamesOwned = new List<int> { 1, 3, 4 }
                            },
                            new Player
                            {
                                Id = 1,
                                Name = "Joana",
                                GamesOwned = new List<int> { 1, 3, 4 }
                            }
                        }
                    }
                }
            }, 
            new StudioGamesPlayers
            {
                GameStudioName = "Ubisoft",
                Games = new List<GamePlayer>() {
                    new GamePlayer
                    {
                        GameName = "Assassins Creed",
                        Players = new List<Player>(){
                            new Player
                            {
                                Id = 3,
                                Name = "Juliana",
                                GamesOwned = new List<int> { 2, 4 }
                            },
                            new Player
                            {
                                Id = 2,
                                Name = "Maria",
                                GamesOwned = new List<int> { 2, 3, 4 }
                            }
                        }
                    }, 
                    new GamePlayer
                    {
                        GameName = "Far Cry",
                        Players = new List<Player>(){
                            new Player
                            {
                                Id = 4,
                                Name = "Francisca",
                                GamesOwned = new List<int> { 1, 3, 4 }
                            },
                            new Player
                            {
                                Id = 1,
                                Name = "Joana",
                                GamesOwned = new List<int> { 1, 3, 4 }
                            }, 
                            new Player
                            {
                                Id = 2,
                                Name = "Maria",
                                GamesOwned = new List<int> { 2, 3, 4 }
                            }
                        }
                    }
                }
            }
        };

        consulta.Should().BeEquivalentTo(expected);

    }

}