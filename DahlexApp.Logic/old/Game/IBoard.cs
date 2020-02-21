using System.Drawing;
using Dahlex.Logic.Contracts;

namespace Dahlex.Logic.Game
{
    public interface IBoard
    {
        BoardPosition GetPosition(int x, int y);
        void SetPosition(int x, int y, BoardPosition pos);
        void ResetPosition(int x, int y);
        int GetPositionHeight();
        int GetPositionWidth();
        Point GetProfessor();
        BoardPosition GetTempPosition(int x, int y);
        void SetTempPosition(int x, int y, BoardPosition pos);
        void ResetTempPosition(int x, int y);
        Point GetProfessorFromTemp();
        BoardPosition[,] TheBoard { get; set; }
        int GetRobotCount();
    }
}
