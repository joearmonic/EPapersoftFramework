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
