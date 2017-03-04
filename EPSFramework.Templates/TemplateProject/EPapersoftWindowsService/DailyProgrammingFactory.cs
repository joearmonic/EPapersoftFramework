//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService
{
	using EPapersoftWindowsService.Interfaces;

	public class DailyProgrammingFactory : ProgrammingFactory
	{
		public DailyProgrammingFactory() : base()
		{
			this.AssignedPeriodicity = "DayInterval";
		}

		public override Programming CreateProgramming(ProcessWorkerBase worker)
		{
			return new DailyProgramming(worker);
		}
	}
}