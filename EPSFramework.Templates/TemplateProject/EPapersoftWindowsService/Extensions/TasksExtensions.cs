//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.Extensions
{
    using System;
    using System.Threading.Tasks;

    public static class TasksExtensions
    {
        public static void ScheduleIfContinuation(this Task task, Action<Task> continuationAction, bool continueAction)
        {
            // Nothing to do. Previous chained action determines that this task is irrelevant.
            if (task == null)
                return;

            if (continueAction)
            {
                TaskContinuationOptions continueOptions =
               TaskContinuationOptions.NotOnCanceled |
               TaskContinuationOptions.ExecuteSynchronously |
               TaskContinuationOptions.AttachedToParent;

                task.ContinueWith(continuationAction, continueOptions);
            }
        }
    }
}