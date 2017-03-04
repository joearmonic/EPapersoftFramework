//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$
{
	using $safeprojectname$.Interfaces;

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