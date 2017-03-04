//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.Composition;

namespace $safeprojectname$.Interfaces
{
    [InheritedExport]
    public abstract class ProcessWorkerFactory
    {
        public ProcessWorkerFactory()
        {
        }

        public abstract ProcessWorkerBase CreateWorker();
    }
}