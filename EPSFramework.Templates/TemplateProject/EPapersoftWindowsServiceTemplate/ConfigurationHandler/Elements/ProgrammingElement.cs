//----------------------------------------------------------------------------------------------
// <copyright file="$fileinputname$" company="EPapersoft">
// Copyright(C) $year$ EPapersoft
// </copyright>
//<license project=$projectname$>
//  This program is free software: you can redistribute it and/or modify it under the terms of 
//  the GNU General Public License as published by the Free Software Foundation, either version 
//  3 of the License, or (at your option) any later version.
//  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
//  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//  See the LICENSE file in the project root for more information or
//  visit, see<http://www.gnu.org/licenses/>.
//</license>
//----------------------------------------------------------------------------------------------
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
