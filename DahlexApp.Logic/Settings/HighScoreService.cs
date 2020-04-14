﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;
using Newtonsoft.Json;

namespace DahlexApp.Logic.Settings
{
    public interface IHighScoreService
    {
        Task AddHighScore(GameMode mode, string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, Size boardSize);
        List<HighScore> LoadLocalHighScores();
        void SaveLocalHighScores();
    }

    public class HighScoreService : IHighScoreService
    {

        public HighScoreService(IPreferencesService preferences)
        {
            _preferences = preferences;
            _scores = new List<HighScore>();
            _scores = LoadLocalHighScores();
        }

        private string key = "HighScores";

        private readonly IPreferencesService _preferences;
        private List<HighScore> _scores;//= new List<HighScore>();

        public async Task AddHighScore(GameMode mode, string name, int level, int bombsLeft, int teleportsLeft, int moves, DateTime startTime, Size boardSize)
        {
            if (mode == GameMode.Random)
            {
                var hs = new HighScore(name, level, bombsLeft, teleportsLeft, moves, startTime, boardSize);
                _scores.Add(hs);
            }

            await foreach (HighScore hs in GetHighScoreAsync())
            {

            }
        }

        private async IAsyncEnumerable<HighScore> GetHighScoreAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            for (var i = 0; i < 20; i++)
            {
                var item = await Task.FromResult(new HighScore("nikl", 1, 1, 1, 1, DateTime.Now, new Size(1, 1)));

                yield return item;
            }
        }

        public List<HighScore> LoadLocalHighScores()
        {
            try
            {
                var hsList = _preferences.LoadPreference(key);
                //var settings = ApplicationData.Current.LocalSettings;
                // string highScores = settings.Values["HighScores"].ToString();
                //  byte[] bytes = new byte[0];// = Encoding.Unicode.GetBytes(highScores.ToCharArray());

                if (!string.IsNullOrEmpty(hsList))
                {
                    _scores = JsonConvert.DeserializeObject<List<HighScore>>(hsList);
                }

                // var serializer = new DataContractSerializer(typeof(List<HighScore>));

                // _scores = (List<HighScore>)serializer.ReadObject(new MemoryStream(bytes));

                if (_scores.Count == 0)
                {
                    _scores.Add(new HighScore("Niklas (Beat me, I wanna be last)", 1, 1, 1, 1, DateTime.Now.AddMinutes(-3), new Size(12, 12)));
                }

                _scores.Sort(new HighScoreComparer());
                _scores = _scores.GetRange(0, Math.Min(_scores.Count, 20));
            }
            catch
            {
                _scores.Clear();
            }

            return _scores;
        }

        public void SaveLocalHighScores()
        {
            string output = JsonConvert.SerializeObject(_scores);
            _preferences.SavePreference(key, output);
            // var serializer = new DataContractSerializer(typeof(List<HighScore>));
            //var sb = new StringBuilder();
            //using (var writer = XmlWriter.Create(sb))
            //{
            //   serializer.WriteObject(writer, _scores);
            // writer.Flush();

            // var settings = ApplicationData.Current.LocalSettings;
            //  settings.Values["HighScores"] = sb.ToString();
            //}
        }

        private class HighScoreComparer : IComparer<HighScore>
        {
            public int Compare(HighScore x, HighScore y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null && y != null)
                {
                    return 1;
                }
                if (x != null && y == null)
                {
                    return -1;
                }
                int cmp = y.Score.CompareTo(x.Score);
                if (cmp == 0)
                {
                    return x.GameDuration.CompareTo(y.GameDuration);
                }
                else
                {
                    return cmp;
                }
            }
        }
    }
}