//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$
{
    using Interfaces;
    using System;
    using System.Linq;

    public class IntervalProgramming : Programming
    {
        public IntervalProgramming(ProcessWorkerBase worker) : base(worker)
        {
            MustWaitUntilDone = false;
        }

        /// <summary>
        /// Adjusts the schedule to the changing situation depending on the actual context. Programmings that
        /// should wait to the action, usually invoke this method, or those that need rescheduling when one alarm
        /// was fired.
        /// </summary>
        protected override void AdjustSchedule()
        {
            // When It reaches or passes the end time (if exists) OR, when we want the programming certain days a week
            // and the next leap will be out of today, the adjust is like calculating the starting duetime.
            // otherwise the interval is the next due (no days are programmed, or only pure interval is desired)
            if (EndTime != System.Threading.Timeout.InfiniteTimeSpan && EndTime.Add(NextInterval).CompareTo(DateTime.Now.TimeOfDay) <= 0
                || DateTime.Now.Add(NextInterval).DayOfWeek > DateTime.Now.DayOfWeek && this.DaysOfWeek.Count() > 0)
            {
                // Recalculates the next execution day based on that today it has been executed yet, so It substract the current day.
                int lastDayDiffPosition = Programming.DifferenceProgrammedDaysWithToday(this.DaysOfWeek.Except(new[] { DateTime.Now.DayOfWeek }));

                NextDue = DueTime.Add(TimeSpan.FromDays(1)).Subtract(DateTime.Now.TimeOfDay);
                if (lastDayDiffPosition > 0)
                {
                    NextDue = NextDue.Add(TimeSpan.FromDays(lastDayDiffPosition));
                }

                return;
            }

            NextDue = NextInterval;
        }

        /// <summary>
        /// Setups the schedule to which the internal timer must conform. This method should
        /// sets the fields NextDue and NextInterval to the programming to work.
        /// </summary>
        protected override void SetupSchedule()
        {
            if (Interval == TimeSpan.Zero)
            {
                throw new InvalidOperationException("Interval period must be defined for Interval programming");
            }

            int lastDayDiffPosition = DifferenceProgrammedDaysWithToday();

            if (DueTime == System.Threading.Timeout.InfiniteTimeSpan || DueTime.CompareTo(DateTime.Now.TimeOfDay) <= 0 && EndTime.Subtract(Interval).CompareTo(DateTime.Now.TimeOfDay) > 0)
            {
                NextDue = TimeSpan.Zero.Add(TimeSpan.FromDays(lastDayDiffPosition));
            }
            else
            {
                // if the duetime has passed, then it will be launched the next interval.
                if (DueTime.CompareTo(DateTime.Now.TimeOfDay) <= 0 && EndTime.CompareTo(DateTime.Now.TimeOfDay) <= 0)
                {
                    NextDue = DueTime.Add(TimeSpan.FromDays(1)).Subtract(DateTime.Now.TimeOfDay);
                    if (lastDayDiffPosition > 0)
                    {
                        NextDue = NextDue.Add(TimeSpan.FromDays(lastDayDiffPosition));
                    }
                }
                else
                {
                    // Due time is not reached, calculates the interval to launch the alarm.
                    NextDue = DueTime.Subtract(DateTime.Now.TimeOfDay);
                }
            }

            // The interval time will defined how long it waits until launch an alarm in regular intervals.
            NextInterval = Interval;
        }
    }
}