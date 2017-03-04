//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.IoC.ConfigurationHandler
{
    using System.Configuration;

    internal class DIComponentSection : ConfigurationSection
    {

        /// <summary>
        /// Gets or sets the dependency injection components.
        /// </summary>
        /// <value>
        /// The dependency injection components.
        /// </value>
        [ConfigurationProperty("DIComponents")]
        [ConfigurationCollection(typeof(DIComponentCollection), AddItemName = "component")]
        public DIComponentCollection DIComponents
        {
            get { return (DIComponentCollection)this["DIComponents"]; }
            set { this["DIComponents"] = value; }
        }
    }
}
