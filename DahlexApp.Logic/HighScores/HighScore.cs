﻿using System;
using System.Drawing;
using DahlexApp.Logic.Settings;

namespace DahlexApp.Logic.HighScores
{
    public class HighScore
    {
        public HighScore(string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, Size boardSize)
        {
            _name = name;
            _level = level;
            _bombsLeft = bombsLeft;
            _teleportsLeft = teleportsLeft;
            _moves = moves;
            _gameDuration = DateTime.Now - startTime;
            _boardSize = boardSize;
        }

        private string _name;
        private int _level;
        private int _bombsLeft;
        private int _teleportsLeft;
        private int _moves;
        private TimeSpan _gameDuration;
        private Size _boardSize;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Score
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Level
        {
            get { return _level; }
        }

        public int BombsLeft
        {
            get { return _bombsLeft; }
        }

        public int TeleportsLeft
        {
            get { return _teleportsLeft; }
        }

        public int Moves
        {
            get { return _moves; }
        }

        public TimeSpan GameDuration
        {
            get { return _gameDuration; }
            set { _gameDuration = value; }
        }

        public Size BoardSize
        {
            get { return _boardSize; }
        }

        public string Content
        {
            get
            {
                if (Score == SettingsManager.MaxLevelIndicator)
                {
                    return string.Format("{0} completed the game in {1}s", Name, Math.Floor(GameDuration.TotalSeconds));
                }
                else if (Score == SettingsManager.MinLevelIndicator)
                {
                    return string.Format("{0}", Name);
                }
                else
                {
                    return string.Format("{0} reached level {1} in {2}s", Name, Score, Math.Floor(GameDuration.TotalSeconds));
                }
            }
        }

        public new string ToString()
        {
            return Content;
        }
    }
}
