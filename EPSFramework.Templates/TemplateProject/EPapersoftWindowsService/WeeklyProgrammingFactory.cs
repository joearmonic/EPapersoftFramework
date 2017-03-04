//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService
{
    using EPapersoftWindowsService.Interfaces;

    public class WeeklyProgrammingFactory : ProgrammingFactory
    {
		public WeeklyProgrammingFactory() : base()
        {
            this.AssignedPeriodicity = "WeeklyInterval";
        }

        public override Programming CreateProgramming(ProcessWorkerBase worker)
        {
            return new WeeklyProgramming(worker);
        }
    }
}