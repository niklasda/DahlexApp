using DahlexApp.Logic.Models;

namespace DahlexApp.Logic.Interfaces
{
    public interface ISettingsManager
    {
        GameSettings LoadLocalSettings();

        void SaveLocalSettings(GameSettings settings);
    }
}
