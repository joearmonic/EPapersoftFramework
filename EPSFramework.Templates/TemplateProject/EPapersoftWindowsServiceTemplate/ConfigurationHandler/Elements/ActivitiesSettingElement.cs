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
    using System.Configuration;

    public class ActivitiesSettingElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.KEY_ATTR, IsRequired = true)]
        public string Key
        {
            get { return (string)this[Cons.SettingNames.KEY_ATTR]; }
            set { this[Cons.SettingNames.KEY_ATTR] = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.VALUE_ATTR)]
        public string Value
        {
            get { return (string)this[Cons.SettingNames.VALUE_ATTR]; }
            set { this[Cons.SettingNames.VALUE_ATTR] = value; }
        }
    }
}