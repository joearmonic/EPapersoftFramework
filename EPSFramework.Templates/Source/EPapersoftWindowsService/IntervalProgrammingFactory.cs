//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService
{
    using EPapersoftWindowsService.Interfaces;

    public class IntervalProgrammingFactory : ProgrammingFactory
    {
		public IntervalProgrammingFactory() : base()
        {
            this.AssignedPeriodicity = "DefinedInterval";
        }

        public override Programming CreateProgramming(ProcessWorkerBase worker)
        {
            return new IntervalProgramming(worker);
        }
    }
}