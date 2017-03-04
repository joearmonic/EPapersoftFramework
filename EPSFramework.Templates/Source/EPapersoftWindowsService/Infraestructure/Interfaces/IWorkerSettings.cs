//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
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