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

    public class ActivitiesSettingSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the activities settings.
        /// </summary>
        /// <value>
        /// The activities settings.
        /// </value>
        [ConfigurationProperty(Cons.SettingNames.ACTIVITIES_SETTINGS)]
        internal ActivitiesSettingsCollection ActivitiesSettings
        {
            get { return (ActivitiesSettingsCollection)base[Cons.SettingNames.ACTIVITIES_SETTINGS]; }
            set { base[Cons.SettingNames.ACTIVITIES_SETTINGS] = value; }
        }
    }
}