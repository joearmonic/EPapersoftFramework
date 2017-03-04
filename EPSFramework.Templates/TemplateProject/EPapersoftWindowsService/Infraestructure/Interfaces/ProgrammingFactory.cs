//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.ComponentModel.Composition;

namespace $safeprojectname$.Interfaces
{
    [InheritedExport]
    public abstract class ProgrammingFactory
    {
        /// <summary>
        /// Gets or sets the assigned periodicity as a string useful for determine which programming implementation creates the factory. Usually it's
        /// used as a "dynamic" enum member value combined with other implementations of this abstract factory.
        /// </summary>
        /// <value>
        /// The assigned periodicity.
        /// </value>
        public String AssignedPeriodicity { get; protected set; }

        /// <summary>
        /// Creates the programming implementation defined for this factory.
        /// </summary>
        /// <param name="worker">The worker that it's controlled by the Programming instance</param>
        /// <returns></returns>
        public abstract Programming CreateProgramming(ProcessWorkerBase worker);
    }
}