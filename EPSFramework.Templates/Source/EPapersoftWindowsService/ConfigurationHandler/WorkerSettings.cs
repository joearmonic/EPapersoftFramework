//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
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