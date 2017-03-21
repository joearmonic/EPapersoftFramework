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

    internal sealed class ActivitiesSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty(Cons.SettingNames.ACTIVITIES_SETTINGS_SECTION)]
        internal ActivitiesSettingSection SettingsSection
        {
            get { return (ActivitiesSettingSection)base.Sections[Cons.SettingNames.ACTIVITIES_SETTINGS_SECTION]; }
        }        
    }
}
