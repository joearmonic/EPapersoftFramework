//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System.Configuration;

    internal class AppointmentElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.HOUR_ATTR, IsRequired = true)]
        public int Hour
        {
            get { return (int)this[Cons.SettingNames.HOUR_ATTR]; }
            set { this[Cons.SettingNames.HOUR_ATTR] = value; }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.MINUTE_ATTR, IsRequired = true)]
        public int Minute
        {
            get { return (int)this[Cons.SettingNames.MINUTE_ATTR]; }
            set { this[Cons.SettingNames.MINUTE_ATTR] = value; }
        }
    }
}