//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System;
    using System.Configuration; 
       
    internal class WorkerElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.NAME_ATTR)]
        public String Name
        {
            get { return (String)this[Cons.SettingNames.NAME_ATTR]; }
            set { this[Cons.SettingNames.NAME_ATTR] = value; }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.ID, IsRequired = true)]
        public int Id
        {
            get { return (int)this[Cons.SettingNames.ID]; }
            set { this[Cons.SettingNames.ID] = value; }
        }

        [ConfigurationProperty(Cons.SettingNames.FACTORY_ATTR)]
        public String Factory
        {
            get { return (String)this[Cons.SettingNames.FACTORY_ATTR]; }
            set { this[Cons.SettingNames.FACTORY_ATTR] = value; }
        }

        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.PROGRAMMING, IsRequired = true)]
        public ProgrammingElement Programming
        {
            get { return (ProgrammingElement)this[Cons.SettingNames.PROGRAMMING]; }
            set { this[Cons.SettingNames.PROGRAMMING] = value; }
        }
    }
}
