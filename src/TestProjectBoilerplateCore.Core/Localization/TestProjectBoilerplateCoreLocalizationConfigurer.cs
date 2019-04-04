using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace TestProjectBoilerplateCore.Localization
{
    public static class TestProjectBoilerplateCoreLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(TestProjectBoilerplateCoreConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(TestProjectBoilerplateCoreLocalizationConfigurer).GetAssembly(),
                        "TestProjectBoilerplateCore.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
