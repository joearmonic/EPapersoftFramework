//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System.Configuration;

    public class ActivitiesSettingElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.KEY_ATTR, IsRequired = true)]
        public string Key
        {
            get { return (string)this[Cons.SettingNames.KEY_ATTR]; }
            set { this[Cons.SettingNames.KEY_ATTR] = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.VALUE_ATTR)]
        public string Value
        {
            get { return (string)this[Cons.SettingNames.VALUE_ATTR]; }
            set { this[Cons.SettingNames.VALUE_ATTR] = value; }
        }
    }
}