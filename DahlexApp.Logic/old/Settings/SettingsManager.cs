using Dahlex.Logic.Contracts;

namespace Dahlex.Logic.Settings
{
    public class SettingsManager : ISettingsManager
    {
        //private static bool _hasAlreadyRun;
        private IntSize _canvasSize;

        public SettingsManager(IntSize canvasSize)
        {
            _canvasSize = canvasSize;
        }

        /// <summary>Will return false only the first time a user ever runs this.
        /// Everytime thereafter, a placeholder file will have been written to disk
        /// and will trigger a value of true.</summary>
        public static bool IsFirstRun()
        {
           
                return false;
           
        }


        public static int MaxLevelIndicator
        { get { return 100; } }

        public static int MinLevelIndicator
        { get { return 0; } }

        public GameSettings LoadLocalSettings()
        {
            var settings = new GameSettings(_canvasSize);
          

            return settings;
        }

        public void SaveLocalSettings(GameSettings settings)
        {
            
        }
    }
}