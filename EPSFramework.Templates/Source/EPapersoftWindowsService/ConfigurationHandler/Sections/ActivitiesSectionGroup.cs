//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System.Configuration;

    internal sealed class ActivitiesSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty(Cons.SettingNames.ACTIVITIES_SETTINGS_SECTION)]
        internal ActivitiesSettingSection SettingsSection
        {
            get { return (ActivitiesSettingSection)base.Sections[Cons.SettingNames.ACTIVITIES_SETTINGS_SECTION]; }
        }        
    }
}
