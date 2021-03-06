﻿//----------------------------------------------------------------------------------------------
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

    internal class ComponentElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        [ConfigurationProperty("assemblyName")]
        public String AssemblyName
        {
            get { return (String)this["assemblyName"]; }
            set { this["assemblyName"] = value; }
        }

        /// <summary>
        /// Gets or sets the dependencies.
        /// </summary>
        /// <value>
        /// The dependencies.
        /// </value>
        [ConfigurationProperty("dependencies")]
        [ConfigurationCollection(typeof(DependenciesCollection), AddItemName = "mapping")]
        public DependenciesCollection Dependencies
        {
            get { return (DependenciesCollection)this["dependencies"]; }
            set { this["dependencies"] = value; }
        }
    }
}
