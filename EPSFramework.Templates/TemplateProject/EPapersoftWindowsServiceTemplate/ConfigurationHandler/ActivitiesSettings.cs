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