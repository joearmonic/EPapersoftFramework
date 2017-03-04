//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.Interfaces
{
    using System;
    using System.Collections.Concurrent;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements basic functionality for process worker in service applications to stop and start softly and let the system recover from abort exceptions.
    /// </summary>
    public abstract class ProcessWorkerBase
    {
        /// <summary>
        /// The _cancellation token source from the cancel action can be throwed <see cref="Task"/>
        /// </summary>
        protected CancellationTokenSource _cancellationTokenSource = null;

        /// <summary>
        /// Gets the current tasks.
        /// </summary>
        /// <value>
        /// The current tasks.
        /// </value>
        public ConcurrentBag<Task> CurrentTasks { get; private set; }

        /// <summary>
        /// Allows control the transition between states when stopping.
        /// </summary>
        private readonly object stopLock = new object();

        /// <summary>
        /// Whether the process is in stopping state.
        /// </summary>
        private bool stopping = false;

        /// <summary>
        /// whether the process is stopped
        /// </summary>
        private bool stopped = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessWorkerBase"/> class.
        /// </summary>
        public ProcessWorkerBase()
        {
            this.CurrentTasks = new ConcurrentBag<Task>();
            this._cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Gets or sets the name of the process worker.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets a value indicating whether [stopping].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [stopping]; otherwise, <c>false</c>.
        /// </value>
        public bool Stopping
        {
            get
            {
                return this.stopping;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [stopped].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [stopped]; otherwise, <c>false</c>.
        /// </value>
        public bool Stopped
        {
            get
            {
                lock (this.stopLock)
                {
                    // Just in case the state is not properly updated, it can be updated by the tasks currently running.
                    if (this.CurrentTasks.Where(task => task.Status == TaskStatus.Running).Count() > 0 && this.stopped)
                        SetStarted();                        

                    return this.stopped;
                }
            }
        }

        public void DoWork()
        {
            SetStarted();
            DoWorkInternal();
        }

        /// <summary>
        /// Starts the work.
        /// </summary>
        protected abstract void DoWorkInternal();

        protected abstract Task DoWorkInternalAsync();

        /// <summary>
        /// Does the work in asynchronous mode.
        /// </summary>
        /// <returns></returns>
        public async Task DoWorkAsync()
        {
            SetStarted();
            await DoWorkInternalAsync();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public virtual void Stop()
        {
            lock (this.stopLock)
            {
                this.stopping = true;
                this._cancellationTokenSource.Cancel();

                try
                {
                    Task.WaitAll(this.CurrentTasks.ToArray());
                }
                catch (AggregateException ae)
                {
                    TreatAggregateException(ae);
                }

                this._cancellationTokenSource.Dispose();
                this.stopped = true;
            }
        }

        /// <summary>
        /// Sets the stopped.
        /// </summary>
        protected void SetStopped()
        {
            lock (this.stopLock)
            {
                this.stopped = true;
            }
        }

        protected void SetStarted()
        {
            lock (this.stopLock)
            {
                this.stopped = false;
            }
        }

        /// <summary>
        /// Treats the aggregate exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void TreatAggregateException(AggregateException ex)
        {
            foreach (Exception singleEx in ex.InnerExceptions)
            {
                if (singleEx is AggregateException)
                {
                    TreatAggregateException((AggregateException)singleEx);
                }
                else if(singleEx is TaskCanceledException)
                {
                    Debug.WriteLine($"Tasks canceled for worker {this.Name}");
                    Debug.Flush();
                }
                else
                {
                    throw singleEx;
                }
            }
        }
    }
}