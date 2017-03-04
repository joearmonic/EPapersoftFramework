//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.ConfigurationHandler
{
    public class ActivitiesSettings
    {
        /// <summary>
        /// Gets or sets the on stop wait timeout.
        /// </summary>
        /// <value>
        /// The on stop wait timeout.
        /// </value>
        public int OnStopWaitTimeout { get; set; }

        /// <summary>
        /// Gets or sets the on reload wait timeout.
        /// </summary>
        /// <value>
        /// The on reload wait timeout.
        /// </value>
        public int OnReloadWaitTimeout { get; set; }        
    }
}