//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.ConfigurationHandler
{
    using System;
    using System.Configuration;
    using Infraestructure;
    internal class ProgrammingElement : ConfigurationElement
    {      
        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.PERIOD_ATTR, IsRequired = true)]
        public String Period
        {
            get { return (String)this[Cons.SettingNames.PERIOD_ATTR]; }
            set { this[Cons.SettingNames.PERIOD_ATTR] = value; }
        }

        /// <summary>
        /// Gets or sets the cycle.
        /// </summary>
        /// <value>
        /// The cycle.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.CYCLE, IsRequired = false)]
        public TimeElement Cycle
        {
            get { return (TimeElement)this[Cons.SettingNames.CYCLE]; }
            set { this[Cons.SettingNames.CYCLE] = value; }
        }

        [ConfigurationProperty(Cons.SettingNames.APPOINTMENT, IsRequired = false)]
        public AppointmentElement Appointment
        {
            get { return (AppointmentElement)this[Cons.SettingNames.APPOINTMENT]; }
            set { this[Cons.SettingNames.APPOINTMENT] = value; }
        }

        [ConfigurationProperty(Cons.SettingNames.UNTIL_APPOINTMENT, IsRequired = false)]
        public AppointmentElement UntilAppointment
        {
            get { return (AppointmentElement)this[Cons.SettingNames.UNTIL_APPOINTMENT]; }
            set { this[Cons.SettingNames.UNTIL_APPOINTMENT] = value; }
        }

        [ConfigurationProperty(Cons.SettingNames.DAYOFWEEK_COLLECTION, IsRequired =false)]
        [ConfigurationCollection(typeof(DaysOfTheWeekCollection), AddItemName = Cons.SettingNames.DAYOFWEEK_ELEMENT)]
        public DaysOfTheWeekCollection DaysOfTheWeekConfiguration
        {
            get { return (DaysOfTheWeekCollection)base[Cons.SettingNames.DAYOFWEEK_COLLECTION]; }
            set { base[Cons.SettingNames.DAYOFWEEK_COLLECTION] = value; }
        }
    }
}
