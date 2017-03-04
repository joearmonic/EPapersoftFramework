//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.IoC.ConfigurationHandler
{
    using System;
    using System.Configuration;

    internal class MappingElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the interface.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        [ConfigurationProperty("interface")]
        public String Interface 
        {
            get { return (String)this["interface"]; }
            set { this["interface"] = value; }
        }

        /// <summary>
        /// Gets or sets the implemented by.
        /// </summary>
        /// <value>
        /// The implemented by.
        /// </value>
        [ConfigurationProperty("implementedBy")]
        public String ImplementedBy
        {
            get { return (String)this["implementedBy"]; }
            set { this["implementedBy"] = value; }
        }
    }
}
