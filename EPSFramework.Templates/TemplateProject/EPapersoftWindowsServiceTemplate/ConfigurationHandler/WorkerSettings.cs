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
    using System.Collections.Generic;
    using Infraestructure;
    using Interfaces;

    public class WorkerSettings : IWorkerSettings
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        public string FactoryName { get; set; }

        /// <summary>
        /// Gets or sets the due time.
        /// </summary>
        /// <value>
        /// The due time.
        /// </value>
        public TimeSpan DueTime { get; set; }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets or sets the interval time.
        /// </summary>
        /// <value>
        /// The interval time.
        /// </value>
        public TimeSpan IntervalTime { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public String Period { get; set; }


        /// <summary>
        /// Gets or sets the days of the week when the worker works
        /// </summary>
        /// <value>
        /// The days ofthe week.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public IEnumerable<DayOfWeek> DaysOftheWeek { get; set;}

        /// <summary>
        /// Gets the activities settings.
        /// </summary>
        /// <value>
        /// The activities settings.
        /// </value>
        public ActivitiesSettings ActivitiesSettings { get; internal set; }
    }
}