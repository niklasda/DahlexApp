using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Game
{
    public class BoardPosition
    {
        public bool IsNew { get; set; }

        public PieceType Type { get; private set; }

        public string ImageName { get; set; }

        public BoardPosition(PieceType pType, string imgName)
        {
            Type = pType;
            ImageName = imgName;
        }

        public static BoardPosition CreateProfessorBoardPosition()
        {
            return new BoardPosition(PieceType.Professor, "imgProfessor");
        }

        public static BoardPosition CreateHeapBoardPosition(int index)
        {
            return new BoardPosition(PieceType.Heap, $"imgHeap{index}");
        }

        public static BoardPosition CreateRobotBoardPosition(int index)
        {   
            return new BoardPosition(PieceType.Robot, $"imgRobot{index}");
        }
        
        public void ConvertToNone()
        {
            Type = PieceType.None;
        }

        public void ConvertToHeap()
        {
            Type = PieceType.Heap;
            ImageName = $"imgHeap{ImageName}";
            IsNew = true;
            //TODO re-imp
        }
    }
}
