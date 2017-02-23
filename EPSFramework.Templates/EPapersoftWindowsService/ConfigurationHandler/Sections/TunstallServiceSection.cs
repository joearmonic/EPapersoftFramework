//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System.Configuration;

    internal class EPapersoftServiceSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the workers.
        /// </summary>
        /// <value>
        /// The workers.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.WORKERS)]
        [ConfigurationCollection(typeof(WorkerCollection), AddItemName = Cons.SettingNames.WORKER)]
        public WorkerCollection Workers
        {
            get { return (WorkerCollection)base[Cons.SettingNames.WORKERS]; }
            set { base[Cons.SettingNames.WORKERS] = value; }
        }
    }
}
