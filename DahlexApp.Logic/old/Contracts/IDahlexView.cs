using System;
using Dahlex.Logic.Game;

namespace Dahlex.Logic.Contracts
{
    public interface IDahlexView
    {
        void AddLineToLog(string log);

        // void DrawGrid(int width, int height, int xSize, int ySize);
        void DrawBoard(IBoard board, int xSize, int ySize);

        void ShowStatus(int level, int bombCount, int teleportCount, int robotCount, int moveCount, int maxLevel);

        //   Control GetControlAt(IntPoint p);
        void Clear(bool all);

        // void SetBoardSizeControls();
        void PlaySound(Sound effect);

        void Animate(BoardPosition bp, IntPoint oldPosition, IntPoint newPosition, Guid roundId);

        // void RemoveAnimate(BoardPosition position);
        void RemoveImage(string imageName);

        void StartTheRobots(Guid roundId);

        void DrawLines();
    }
}