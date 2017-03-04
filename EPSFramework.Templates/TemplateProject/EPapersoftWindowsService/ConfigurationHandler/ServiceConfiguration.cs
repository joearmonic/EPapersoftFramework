//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.ConfigurationHandler
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Infraestructure;

    public class ServiceConfiguration
    {
        private static ServiceConfiguration instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="ServiceConfiguration"/> class from being created.
        /// </summary>
        private ServiceConfiguration()
        {
            // Workers generic section.
            EPapersoftServiceSection configurationSection = (EPapersoftServiceSection)ConfigurationManager.GetSection(Cons.SettingNames.SVC_SECTION);

            // Activities Section group with its children sections.
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ActivitiesSectionGroup group = config.GetSectionGroup(Cons.SettingNames.ACTIVITIES_SECTION_GROUP) as ActivitiesSectionGroup;
            SettingsActivities = group.SettingsSection;

            this.WorkersConfiguration = configurationSection.Workers
                .Cast<WorkerElement>()
                .Select(
                    e => new WorkerSettings()
                    {
                        Name = e.Name,
                        Id = e.Id,
                        FactoryName = e.Factory,
                        DueTime = ToTimeSpan(e.Programming.Appointment),
                        EndTime = ToTimeSpan(e.Programming.UntilAppointment),
                        IntervalTime = ToTimeSpan(e.Programming.Cycle),
                        Period = e.Programming.Period,
                        DaysOftheWeek = e.Programming.DaysOfTheWeekConfiguration
                        .Cast<DayOfWeekElement>()
                        .Select(d => ((DayOfWeekElement)d).Name).ToList(),
                        ActivitiesSettings = ActivitiesSettings()
                    })
                .ToList();
        }

        /// <summary>
        /// Creates the configuration.
        /// </summary>
        /// <returns></returns>
        public static ServiceConfiguration CreateConfiguration()
        {
            if (ServiceConfiguration.instance == null)
            {
                instance = new ServiceConfiguration();
            }

            return ServiceConfiguration.instance;
        }

        /// <summary>
        /// Gets the settings activities.
        /// </summary>
        /// <value>
        /// The settings activities.
        /// </value>
        internal ActivitiesSettingSection SettingsActivities { get; private set; }

        /// <summary>
        /// Activitieses the settings.
        /// </summary>
        /// <returns></returns>
        public ActivitiesSettings ActivitiesSettings()
        {
            var ActivitiesSettings = new ActivitiesSettings()
            {
                OnStopWaitTimeout = SettingsActivities.ActivitiesSettings.Cast<ActivitiesSettingElement>().Where(set => set.Key == "OnStopWaitTimeout").Select(set => Convert.ToInt32(set.Value)).Single(),
                OnReloadWaitTimeout = SettingsActivities.ActivitiesSettings.Cast<ActivitiesSettingElement>().Where(set => set.Key == "OnReloadWaitTimeout").Select(set => Convert.ToInt32(set.Value)).Single()
            };

            return ActivitiesSettings;
        }

        /// <summary>
        /// Gets the workers.
        /// </summary>
        /// <value>
        /// The workers.
        /// </value>
        public List<WorkerSettings> WorkersConfiguration { get; private set; }

        /// <summary>
        /// To the time span.
        /// </summary>
        /// <param name="timeElement">The time element.</param>
        /// <returns></returns>
        private static TimeSpan ToTimeSpan(TimeElement timeElement)
        {
            TimeSpan retTimeSpan = DateTime.Now.TimeOfDay;

            switch (timeElement.Unit)
            {
                case TimeElement.UnitType.hours:
                    retTimeSpan = TimeSpan.FromHours(timeElement.Time);
                    break;

                case TimeElement.UnitType.minutes:
                    retTimeSpan = TimeSpan.FromMinutes(timeElement.Time);
                    break;

                case TimeElement.UnitType.seconds:
                    retTimeSpan = TimeSpan.FromSeconds(timeElement.Time);
                    break;

                case TimeElement.UnitType.milliseconds:
                    retTimeSpan = TimeSpan.FromMilliseconds(timeElement.Time);
                    break;
            }

            return retTimeSpan;
        }

        private static TimeSpan ToTimeSpan(AppointmentElement appointment)
        {
            if(appointment.Hour == 0 && appointment.Minute == 0)
            {
                return System.Threading.Timeout.InfiniteTimeSpan;
            }

            return new TimeSpan(appointment.Hour, appointment.Minute, 0);
        }
    }
}