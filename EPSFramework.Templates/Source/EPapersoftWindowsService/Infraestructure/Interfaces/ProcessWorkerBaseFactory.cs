//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
using System.ComponentModel.Composition;

namespace EPapersoftWindowsService.Interfaces
{
    [InheritedExport]
    public abstract class ProcessWorkerBaseFactory
    {
        public ProcessWorkerBaseFactory()
        {
        }

        public abstract ProcessWorkerBase CreateWorker();
    }
}