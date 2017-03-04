//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [InheritedExport]
    public abstract class Programming
    {
        protected bool isSynchronized = true;

        protected DateTime lastSetup;

        /// <summary>
        /// The worker that executes its action on programming Wakes up.
        /// </summary>
        protected ProcessWorkerBase worker;

        /// <summary>
        /// The timer that controls the frequency of the executions made by the processWorker.
        /// <para>For more information about how to schedule it see <see cref="ISchedule" />.</para>
        /// </summary>
        private Timer timer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgrammableWorker" /> class.
        /// </summary>
        /// <param name="worker">The worker.</param>
        [ImportingConstructor]
        public Programming(ProcessWorkerBase worker)
        {
            this.worker = worker;
        }

        public IEnumerable<DayOfWeek> DaysOfWeek { get; protected set; }
        public TimeSpan DueTime { get; protected set; }
        public TimeSpan EndTime { get; protected set; }
        public TimeSpan Interval { get; set; }
        public bool MustWaitUntilDone { get; set; }
        public TimeSpan NextDue { get; protected set; }
        public TimeSpan NextInterval { get; protected set; }
        public DateTime NextSchedule { get { return this.lastSetup; } }

        public string WorkerName { get { return this.worker.Name; } }

        public static int DifferenceProgrammedDaysWithToday(IEnumerable<DayOfWeek> DaysOfWeek)
        {
            if (DaysOfWeek.Count() == 0)
                return 0;

            int lastDayDiffPosition = 6; // we started thinking that the max difference between today and the day to execute is 1 week.
            foreach (var day in DaysOfWeek)
            {
                int partialDayDiffPosition = 0;
                if (day.CompareTo(DateTime.Today.DayOfWeek) < 0)
                {
                    partialDayDiffPosition = 6;// It must add one week difference.
                }

                var lastDayDiffPositionTemp = (((int)day) - ((int)DateTime.Today.DayOfWeek) + partialDayDiffPosition);
                if (lastDayDiffPositionTemp < lastDayDiffPosition)
                {
                    lastDayDiffPosition = lastDayDiffPositionTemp;// Always the closest to the current day.
                }
            }

            return lastDayDiffPosition;
        }

        /// <summary>
        /// Instance Programmation Goes. It instantiates the internal timer with the scheduled due and interval times calculated during programmation step.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">A programmed timer is currently executing, cannot relaunch a timer in course. Have you tried to reset it?</exception>
        public void Go()
        {
            if (timer != null)
            {
                throw new InvalidOperationException("A programmed timer is currently executing, cannot relaunch a timer in course. Have you tried to reset it?");
            }

            TimerCallback AlarmCallback = new TimerCallback(Wakeup);
            SetupSchedule();

            if (this.MustWaitUntilDone)
            {
                this.isSynchronized = false;
            }

            this.lastSetup = DateTime.Now.Add(NextDue);
            timer = new Timer(AlarmCallback, null, NextDue, NextInterval);            
        }

        public void SetSchedule(TimeSpan dueTime, TimeSpan endTime, TimeSpan interval, params DayOfWeek[] daysOfWeek)
        {
            SetTime(dueTime, endTime);
            Interval = interval;
            DaysOfWeek = daysOfWeek;

            NextDue = TimeSpan.Zero;
            NextInterval = TimeSpan.Zero;

            SetSynchronization();
        }

        public void SetTime(TimeSpan dueTime, TimeSpan? endTime = null)
        {
            if ((endTime.HasValue) && endTime != System.Threading.Timeout.InfiniteTimeSpan
                && endTime.Value.CompareTo(dueTime) < 0)
            {
                Interval = Timeout.InfiniteTimeSpan;
                dueTime = Timeout.InfiniteTimeSpan;
                EndTime = Timeout.InfiniteTimeSpan;
                throw new ArgumentOutOfRangeException(endTime.ToString(), "Programmation end time cannot be less than initTime in total net hours.");
            }

            DueTime = dueTime;
            EndTime = endTime ?? Timeout.InfiniteTimeSpan;

            SetSynchronization();
        }

        /// <summary>
        /// Stops the alarm.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Timer not set. Thus It cannot reset it</exception>
        public void Reset()
        {
            if (this.timer == null)
            {
                throw new InvalidOperationException("Timer not set. Thus It cannot reset it");
            }

            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
            this.NextDue = Timeout.InfiniteTimeSpan;
            this.NextInterval = Timeout.InfiniteTimeSpan;
            this.lastSetup = DateTime.MinValue;
        }

        public void Stop()
        {
            this.Reset();

            if(!this.worker.Stopped)
                this.worker.Stop();         
        }

        protected abstract void AdjustSchedule();

        protected int DifferenceProgrammedDaysWithToday()
        {          
            return Programming.DifferenceProgrammedDaysWithToday(this.DaysOfWeek);
        }

        /// <summary>
        /// Sets the synchronization state of the programming based on its internal state. When invoked the synchronization pass to Synchronized unless
        /// there's an end time, the alarm must wait until the worker did the work, or there are more days in the schedule so that needs recalculate alarm's due time).
        /// </summary>
        protected void SetSynchronization()
        {
            isSynchronized = true;

            // If must wait until task is done
            // or when there's and end time or needs to recalculate for other day, It can't be synchronized to calculate every time it fires alarm.
            if (MustWaitUntilDone || EndTime != System.Threading.Timeout.InfiniteTimeSpan || this.DaysOfWeek != null && this.DaysOfWeek.Count() > 1)
            {
                isSynchronized = false;
            }
        }

        protected abstract void SetupSchedule();

        /// <summary>
        /// Fires the and forget.
        /// </summary>
        /// <param name="mainProcess">The main process.</param>
        /// <returns></returns>
        private Task FireAndForget(Func<Task> mainProcess)
        {
            // Invokes the task but it doen't awaits it.
            return Task.Factory.StartNew(async () => 
            {
                await mainProcess();
        }, TaskCreationOptions.LongRunning);
        }

        private void ResumeTimer(Task leadingTask)
        {
            if (this.timer == null)
            {
                throw new InvalidOperationException("Timer not set. Thus It cannot reset it");
            }

            if (leadingTask.IsCanceled || leadingTask.Status == TaskStatus.Faulted)
            {
                return;
            }

            this.AdjustSchedule();
            this.lastSetup = DateTime.Now.Add(NextDue);
            this.timer.Change(NextDue, NextInterval);
        }

        private void ScheduleContinuation(Task task, Action<Task> contTask)
        {
            // Nothing to do. Previous chained action determines that this task is irrelevant.
            if (task == null)
                return;

            this.worker.CurrentTasks.Add(task);

            if (this.MustWaitUntilDone)
            {
                TaskContinuationOptions continueOptions =
               TaskContinuationOptions.NotOnCanceled |
               TaskContinuationOptions.ExecuteSynchronously |
               TaskContinuationOptions.AttachedToParent;

                task.ContinueWith(contTask, continueOptions);
            }
            else if (!isSynchronized)
            {
                contTask(task);
            }
        }

        /// <summary>
        /// Stops the alarm if.
        /// </summary>
        /// <param name="IsContinuation">if set to <c>true</c> [is continuation].</param>
        /// <returns></returns>
        private Programming StopAlarmIf(bool IsContinuation)
        {
            if (IsContinuation)
                this.Reset();

            return this;
        }

        /// <summary>
        /// Wakeups the programming instance to call the worker to work.
        /// </summary>
        /// <param name="state">The state.</param>
        private void Wakeup(object state)
        {
            this.lastSetup = DateTime.Now.Add(this.Interval);// Updates next schedule only for synchonized programmings

            this.StopAlarmIf(!this.isSynchronized)
                .ScheduleContinuation(
                    FireAndForget(worker.DoWorkAsync),
                    ResumeTimer
                );
        }
    }
}