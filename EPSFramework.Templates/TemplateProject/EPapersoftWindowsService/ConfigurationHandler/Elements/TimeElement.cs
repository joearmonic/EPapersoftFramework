//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.ConfigurationHandler
{
    using System.Configuration;

    internal class TimeElement : ConfigurationElement
    {
        /// <summary>
        /// 
        /// </summary>
        internal enum UnitType
        {
            hours,
            minutes,
            seconds,
            milliseconds
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.TIME, IsRequired = true)]
        public long Time
        {
            get { return (long)this[Cons.SettingNames.TIME]; }
            set { this[Cons.SettingNames.TIME] = value; }
        }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.UNIT, IsRequired = true)]
        public UnitType Unit
        {
            get { return (UnitType)this[Cons.SettingNames.UNIT]; }
            set { this[Cons.SettingNames.UNIT] = value; }
        }
    }
}