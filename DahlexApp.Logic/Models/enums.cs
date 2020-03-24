namespace DahlexApp.Logic.Models
{
    public enum GameMode { Random, Campaign };

    public enum Sound { Bomb, Teleport, Crash };

    public enum PieceType { None, Heap, Professor, Robot };

    public enum MoveDirection { Ignore, None, North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };

    public enum GameStatus { BeforeStart, LevelOngoing, LevelComplete, GameWon, GameLost, GameStarted };

}
