//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$
{
    using ConfigurationHandler;
    using Interfaces;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using System.ServiceProcess;

    /// <summary>
    /// Represents a windows service customized for a development team.
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    [Export(typeof(ServiceBase))]
    public partial class EPapersoftService : ServiceBase
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IEnumerable<Lazy<ProcessWorkerFactory, IDictionary<string, object>>> workerfactories;

        private IEnumerable<ProgrammingFactory> alarmClockFactories;

        private List<Programming> programmingWorkers = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EPapersoftService" /> class. The composition container injects the
        /// complete existing list of ProcessWorkerFactory composables to select only those marked with the metadata LeadProcess
        /// that are going to be launched at this step.
        /// </summary>
        /// <param name="factories">The factories for process workers.</param>
        /// <param name="programmingfactories">The programming factories.</param>
        [ImportingConstructor]
        public EPapersoftService([ImportMany] IEnumerable<Lazy<ProcessWorkerFactory, IDictionary<string, object>>> factories,
            [ImportMany] IEnumerable<ProgrammingFactory> programmingfactories
            ) : base()
        {
            InitializeComponent();
            workerfactories = factories;
            alarmClockFactories = programmingfactories;
        }

        /// <summary>
        /// Starts the services with specified arguments by executing the overrided OnStart method.
        /// </summary>
        /// <param name="args">The arguments.</param>
        internal void Start(string[] args)
        {
            OnStart(args);
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            logger.Info("Starting application. Configuring workers...");
            programmingWorkers = new List<Programming>();
            ServiceConfiguration adConfig = ServiceConfiguration.CreateConfiguration();

            // Instantiates each worker defined in config.
            // It gets worker whose factory name starts with factory 'attribute', or whatever worker it
            // encounters if 'factory' is not defined.
            adConfig.WorkersConfiguration.ForEach(workerSettings =>
            {
                if (logger.IsDebugEnabled) logger.DebugFormat("Programming {0} worker process", workerSettings.Name);

                var pwFactory = workerfactories
                    .Where(f => workerSettings.FactoryName == string.Empty
                    || nameof(f).StartsWith(workerSettings.FactoryName))
                    .Select(f => f.Value)
                    .FirstOrDefault();

                if (pwFactory == null)
                {
                    throw new CompositionException("There are no worker factories defined to start the service's work");
                }

                ProcessWorkerBase currentWorker = pwFactory.CreateWorker();
                currentWorker.Name = workerSettings.Name;

                try
                {
                    var currentfactory = alarmClockFactories
                        .Where(factory => workerSettings.Period == factory.AssignedPeriodicity)
                        .FirstOrDefault();
                    if (currentfactory == null)
                    {
                        throw new System.Configuration.ConfigurationErrorsException(
                            $"There is no programming factory for the periodicity {workerSettings.Period} indicated." +
                            " Have you mispelled the configuration or Have you forgotten register the assembly?");
                    }

                    Programming alarmClock = currentfactory.CreateProgramming(currentWorker);
                    alarmClock.SetSchedule(workerSettings.DueTime, workerSettings.EndTime, workerSettings.IntervalTime, workerSettings.DaysOftheWeek.ToArray());
                    alarmClock.Go();
                    programmingWorkers.Add(alarmClock);
                    logger.Info($"Next schedule for {currentWorker.Name}: {alarmClock.NextSchedule.ToString()}");
                }
                catch (Exception ex)
                {
                    logger.Error($"{currentWorker.Name} failed to start.", ex);
                }
            });

            if (programmingWorkers.Count == 0)
            {
                logger.WarnFormat("There are no workers programmed. No activity will be done!{0}", Environment.NewLine);
            }
            else
            {
                logger.InfoFormat("Workers configured and programmed to work.{0}", Environment.NewLine);
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            logger.Info("Service Stop has been commanded.");
            bool StopWithException = false;

            // Check all threads and stop them all.
            if (programmingWorkers != null)
            {
                logger.InfoFormat("There are {0} programmed workers to stop. Proceding...", programmingWorkers.Count);
                // Sends a cancellation signal. Threads in Tasks must watch for this.
                programmingWorkers.ForEach(workerProgram =>
                {
                    try
                    {
                        workerProgram.Stop();
                    }
                    catch (Exception ex)
                    {
                        logger.ErrorFormat("The worker {0} has been stopped with exception.",
                        workerProgram.WorkerName,
                        ex);
                        StopWithException = true;
                    }
                });

                logger.InfoFormat("All workers have been stopped.");
                if (StopWithException)
                    logger.Warn("However, one or more stopped unexpectly. see previous messages for more info.");
            }

            logger.Info("Service Stop command finished");
        }
    }
}