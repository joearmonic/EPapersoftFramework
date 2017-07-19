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

    public interface IWorkerSettings
    {
        /// <summary>
        /// Gets or sets the name used to show in the log and trace outputs for this worker.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the worker. It's needed because several workers could be defined at the same time.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the due time when the worker starts to work.
        /// </summary>
        /// <value>
        /// The due time.
        /// </value>
        TimeSpan DueTime { get; set; }

        /// <summary>
        /// Gets or sets the interval time between worker's executions
        /// </summary>
        /// <value>
        /// The interval time.
        /// </value>
        TimeSpan IntervalTime { get; set; }

        /// <summary>
        /// Gets or sets the period established for the worker to work
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        String Period { get; set; }

        /// <summary>
        /// Gets or sets the days of the week when the worker works
        /// </summary>
        /// <value>
        /// The days ofthe week.
        /// </value>
        IEnumerable<DayOfWeek> DaysOftheWeek { get; set; }

        /// <summary>
        /// Gets the activities settings associated with the worker's work or activity.
        /// </summary>
        /// <value>
        /// The activities settings.
        /// </value>
        ActivitiesSettings ActivitiesSettings { get; }
    }
}