//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System;
    using System.Configuration;

    internal class DayOfWeekElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the day as established by the system enum DayOfWeek
        /// </summary>
        /// <value>
        /// The name of the day
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.NAME_ATTR)]
        public DayOfWeek Name
        {
            get { return (DayOfWeek)this[Cons.SettingNames.NAME_ATTR]; }
            set { this[Cons.SettingNames.NAME_ATTR] = value; }
        }
    }
}