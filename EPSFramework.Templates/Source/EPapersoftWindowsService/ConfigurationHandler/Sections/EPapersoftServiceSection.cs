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
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System;
    using System.Configuration;

    internal class EPapersoftServiceSection : ConfigurationSection
    {
        [ConfigurationProperty("xmlns:xsi", IsRequired = false)]
        public String Xmlns
        {
            get
            {
                return this["xmlns:xsi"] != null ? this["xmlns:xsi"].ToString() : string.Empty;
            }
        }

        [ConfigurationProperty("xsi:noNamespaceSchemaLocation", IsRequired = false)]
        public String Xsi
        {
            get
            {
                return this["xsi:noNamespaceSchemaLocation"] != null ? this["xsi:noNamespaceSchemaLocation"].ToString() : string.Empty;
            }
        }

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