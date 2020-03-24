using DahlexApp.Logic.Interfaces;
using Xamarin.Essentials;

namespace DahlexApp.Logic.HighScores
{
    public class PreferencesService : IPreferencesService
    {
        public void RemovePreference(string key)
        {
            Preferences.Remove(key);
        }

        public void SavePreference(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public string LoadPreference(string key)
        {
            var value = Preferences.Get(key, "");
            return value;
        }
    }
}
