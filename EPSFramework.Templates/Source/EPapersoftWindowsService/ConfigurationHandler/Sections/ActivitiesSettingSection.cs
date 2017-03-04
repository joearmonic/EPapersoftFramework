//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System.Configuration;

    public class ActivitiesSettingSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the activities settings.
        /// </summary>
        /// <value>
        /// The activities settings.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.ACTIVITIES_SETTINGS)]
        internal ActivitiesSettingsCollection ActivitiesSettings
        {
            get { return (ActivitiesSettingsCollection)base[Cons.SettingNames.ACTIVITIES_SETTINGS]; }
            set { base[Cons.SettingNames.ACTIVITIES_SETTINGS] = value; }
        }
    }
}