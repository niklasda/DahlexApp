using System.Runtime.Serialization;

namespace Dahlex.Logic.Contracts
{
    public interface IGameState
    {
        int Level { get; set; }

        int MoveCount { get; set; }

        int BombCount { get; set; }

        int TeleportCount { get; set; }

        int GameStatus { get; set; }

        int ElapsedTimeInSeconds { get; set; }

        string TheBoard { get; set; }

        int Mode { get; set; }

        [IgnoreDataMember]
        string Message { get; set; }
    }
}
