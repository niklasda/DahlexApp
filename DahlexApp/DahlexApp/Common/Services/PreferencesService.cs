using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace DahlexApp.Common.Services
{
    [Preserve]
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
