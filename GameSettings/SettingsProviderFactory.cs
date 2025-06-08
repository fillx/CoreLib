using System.Collections.Generic;
using GameSettings.Providers;

namespace GameSettings
{
    public class SettingsProviderFactory
    {
        private readonly string _jsonPath;

        public SettingsProviderFactory(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public ISettingsProvider Create()
        {
            var list = new List<ISettingsProvider>
            {
                new ResourcesSettingsProvider(),
                new JsonFileSettingsProvider(_jsonPath)
            };

#if ADDRESSABLES_ENABLED
        list.Add(new AddressablesSettingsProvider());
#endif

            return new CompositeSettingsProvider(list.ToArray());
        }
    }
}