using DahlexApp.Logic.Interfaces;

namespace DahlexApp.Logic.Models
{
    public class GameState : IGameState
    {
        public int Level { get; set; }

        public int MoveCount { get; set; }

        public int BombCount { get; set; }

        public int TeleportCount { get; set; }

        public int GameStatus { get; set; }

        public int ElapsedTimeInSeconds { get; set; }

        public string TheBoard { get; set; }

        public int Mode { get; set; }

        public string Message { get; set; }
    }
}
