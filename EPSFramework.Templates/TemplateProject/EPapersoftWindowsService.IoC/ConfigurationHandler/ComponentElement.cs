//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.IoC.ConfigurationHandler
{
    using System;
    using System.Configuration;

    internal class ComponentElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        [ConfigurationProperty("assemblyName")]
        public String AssemblyName
        {
            get { return (String)this["assemblyName"]; }
            set { this["assemblyName"] = value; }
        }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        [ConfigurationProperty("dependencies")]
        [ConfigurationCollection(typeof(DependenciesCollection), AddItemName = "mapping")]
        public DependenciesCollection Dependencies
        {
            get { return (DependenciesCollection)this["dependencies"]; }
            set { this["dependencies"] = value; }
        }
    }
}
