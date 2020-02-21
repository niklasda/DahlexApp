﻿using System.Drawing;
using System.Runtime.Serialization;
using Dahlex.Logic.Contracts;

namespace Dahlex.Logic.Settings
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
        public string PlayerName;

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
                int h = (int)(_canvasSize.Height / SquareSize.Height);
                int w = (int)(_canvasSize.Width / SquareSize.Width); 
                return new Size(w, h);
            }
        }

        // w, h

        /// <summary>
        /// The size of the squares on the board, TODO with or without margin ???
        /// </summary>
     //   [IgnoreDataMember]
        public readonly Size SquareSize = new Size(42, 42); // image size 42 x 42

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
