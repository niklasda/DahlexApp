using System.Drawing;
using DahlexApp.Logic.Interfaces;
using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Settings
{
    public class SettingsManager : ISettingsManager
    {
        //private static bool _hasAlreadyRun;
        private readonly Size _canvasSize;

        public SettingsManager(Size canvasSize)
        {
            _canvasSize = canvasSize;
        }

        /// <summary>Will return false only the first time a user ever runs this.
        /// Every time thereafter, a placeholder file will have been written to disk
        /// and will trigger a value of true.</summary>
        public static bool IsFirstRun()
        {
           
                return false;
           
        }


        //public static int MaxLevelIndicator
        //{ get { return 100; } }

        //public static int MinLevelIndicator
        //{ get { return 0; } }

        public GameSettings LoadLocalSettings()
        {
            var settings = new GameSettings(_canvasSize);

            IPreferencesService prf = new PreferencesService();
            string playerName = prf.LoadPreference(key1);
            if (string.IsNullOrEmpty(playerName))
            {
                settings.PlayerName = "Dr. Who";
            }
            else
            {
                settings.PlayerName = playerName;
            }

            string lessSound = prf.LoadPreference(key2);
            
            bool.TryParse(lessSound, out bool less);

            settings.LessSound = less;

            return settings;
        }

        private readonly string key1 = "SettingsName";
        private readonly string key2 = "SettingsMute";

        public void SaveLocalSettings(GameSettings settings)
        {
            IPreferencesService prf = new PreferencesService();
            prf.SavePreference(key1, settings.PlayerName);
            prf.SavePreference(key2, settings.LessSound.ToString());
        }
    }
}
