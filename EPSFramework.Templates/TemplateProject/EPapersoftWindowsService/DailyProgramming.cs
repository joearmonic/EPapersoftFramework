//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Infraestructure;
    using Interfaces;
    using System.Linq;

    public class DailyProgramming : Programming
    {
        public DailyProgramming(ProcessWorkerBase worker) : base(worker)
        {
        }

        protected override void SetupSchedule()
        {
            if (DueTime == System.Threading.Timeout.InfiniteTimeSpan)
            {
                throw new InvalidOperationException("An appointment must be defined for Daily programming");
            }

            Interval = TimeSpan.FromDays(1);

            int lastDayDiffPosition = DifferenceProgrammedDaysWithToday();

            // in case Due time is not reached, calculates the interval to launch the alarm, othewise see below.
            var nextDueTimeProvisional = DueTime.Subtract(DateTime.Now.TimeOfDay);

            // Current time of day should be between due time and end time (if defined).
            if (DueTime.CompareTo(DateTime.Now.TimeOfDay) <= 0)
            {
                if (EndTime.CompareTo(DateTime.Now.TimeOfDay) > 0)
                {
                    nextDueTimeProvisional = TimeSpan.Zero.Add(TimeSpan.FromDays(lastDayDiffPosition));
                }
                else if (EndTime.CompareTo(DateTime.Now.TimeOfDay) <= 0)
                {
                    // if the duetime and end time (if defined) has passed, then it will be launched the next interval.
                    nextDueTimeProvisional = DueTime.Add(Interval).Subtract(DateTime.Now.TimeOfDay);
                    if (lastDayDiffPosition > 0)
                    {
                        nextDueTimeProvisional = NextDue.Add(TimeSpan.FromDays(lastDayDiffPosition));
                    }
                }
            }

            NextDue = nextDueTimeProvisional;

            // The interval time will defined how long it waits until launch an alarm in regular intervals.
            NextInterval = Interval;
        }

        protected override void AdjustSchedule()
        {
            if(EndTime != Timeout.InfiniteTimeSpan)
            {
                if(EndTime.CompareTo(DateTime.Now.TimeOfDay) < 0)
                {
                    NextDue = EndTime.Add(Interval).Subtract(DateTime.Now.TimeOfDay);
                }
                else
                {
                    NextDue = Interval;
                }
            }
            else
            {
                NextDue = DueTime.Add(Interval).Subtract(DateTime.Now.TimeOfDay);
            }

            // Recalculates the next execution day based on that today it has been executed yet, so It substract the current day.
            int lastDayDiffPosition = Programming.DifferenceProgrammedDaysWithToday(this.DaysOfWeek.Except(new[] { DateTime.Now.DayOfWeek }));
            if (lastDayDiffPosition > 0)
            {
                NextDue = NextDue.Add(TimeSpan.FromDays(lastDayDiffPosition));
            }
        }
    }
}