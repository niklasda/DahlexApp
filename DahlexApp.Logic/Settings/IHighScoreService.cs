using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Settings
{
    public interface IHighScoreService
    {
        Task AddHighScore(GameMode mode, string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, Size boardSize);
        List<HighScore> LoadLocalHighScores();
        void SaveLocalHighScores();
    }

}
