namespace DahlexApp.Logic.HighScores
{
    public interface IPreferencesService
    {
        void RemovePreference(string key);
        void SavePreference(string key, string value);
        string LoadPreference(string key);
    }
}
