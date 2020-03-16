using System.Drawing;

namespace DahlexApp.Logic.Settings
{
    //[DataContract]
    public class GameSettings
    {
        private Size _canvasSize;
        public GameSettings(Size canvasSize)
        {
            _canvasSize = canvasSize;
        }
     //   [DataMember]
     public string PlayerName { get; set; } = "Dr. Who";

     //   [DataMember]
        public bool LessSound;

        /// <summary>
        /// Number of squares on the board
        /// </summary>
        //   [IgnoreDataMember]
        public Size BoardSize
        {
            get
            {
                int h = (_canvasSize.Height / SquareSize.Height);
                int w = (_canvasSize.Width / SquareSize.Width); 
                return new Size(w, h);
            }
        }

        // w, h

        /// <summary>
        /// The size of the squares on the board, TODO with or without margin ???
        /// </summary>
     //   [IgnoreDataMember]
        public readonly Size SquareSize = new Size(37, 37); // image size 42 x 42

        /// <summary>
        /// The offset to apply to get the images inside the squares
        /// </summary>
    //    [IgnoreDataMember]
        public readonly Point ImageOffset = new Point(1, 1); // w, h

        /// <summary>
        /// The distance between squares
        /// </summary>
    //    [IgnoreDataMember]
        public readonly Point LineWidth = new Point(0, 0);

        //[IgnoreDataMember]
        //public bool IsFirstRun;
    //    [IgnoreDataMember]
        public int MaxNumberOfLevel
        {
            get { return (BoardSize.Width * BoardSize.Height) / 4 + 10; }
        }
    }
}
