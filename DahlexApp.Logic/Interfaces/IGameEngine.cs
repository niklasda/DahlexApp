using System;
using System.Drawing;

namespace DahlexApp.Logic.Interfaces
{
    public interface IGameEngine
    {
        bool AreThereNoMoreLevels { get; }

        GameStatus Status { get; }

        int CurrentLevel { get; }

        void StartGame(GameMode mode);

        /// <summary>
        /// Continue game from tombstone, called after setState
        /// </summary>
        void ContinueGame(GameMode mode);

        /// <summary>
        /// Gather state to save to tombstone
        /// </summary>
        /// <param name="elapsed"></param>
        /// <returns></returns>
        IGameState GetState(TimeSpan elapsed);

        /// <summary>
        /// Restore state from tombstone, called before continueGame
        /// </summary>
        /// <param name="state"></param>
        void SetState(IGameState state);

        void StartNextLevel();

        void MoveHeapsToTemp();

        bool MoveProfessorToTemp(MoveDirection dir);

        void MoveRobotsToTemp();

        void CommitTemp();

        bool BlowBomb();

        bool DoTeleport();

        Point GetProfessorCoordinates();

        void AddHighScore(bool maxLevel);
    }
}
