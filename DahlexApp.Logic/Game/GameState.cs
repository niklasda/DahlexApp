using DahlexApp.Logic.Interfaces;

namespace DahlexApp.Logic.Game
{
    public class GameState : IGameState
    {
        private int _level;
        private int _moveCount;
        private int _bombCount;
        private int _teleportCount;
        private int _gameStatus;
        private int _elapsedSeconds;
        private string _theBoard;
        private int _mode;
        private string _message;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int MoveCount
        {
            get { return _moveCount; }
            set { _moveCount = value; }
        }

        public int BombCount
        {
            get { return _bombCount; }
            set { _bombCount = value; }
        }

        public int TeleportCount
        {
            get { return _teleportCount; }
            set { _teleportCount = value; }
        }

        public int GameStatus
        {
            get { return _gameStatus; }
            set { _gameStatus = value; }
        }

        public int ElapsedTimeInSeconds
        {
            get { return _elapsedSeconds; }
            set { _elapsedSeconds = value; }
        }

        public string TheBoard
        {
            get { return _theBoard; }
            set { _theBoard = value; }
        }

        public int Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
