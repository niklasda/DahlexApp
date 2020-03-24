namespace DahlexApp.Logic.Interfaces
{
    public interface IPreferencesService
    {
        void RemovePreference(string key);
        void SavePreference(string key, string value);
        string LoadPreference(string key);
    }
}
