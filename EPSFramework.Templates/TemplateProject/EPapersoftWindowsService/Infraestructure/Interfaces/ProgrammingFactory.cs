//----------------------------------------------------------------------------------------------
// <copyright file="$fileinputname$" company="EPapersoft">
// Copyright(C) $year$ EPapersoft
// </copyright>
//<license project=$projectname$>
//  This program is free software: you can redistribute it and/or modify it under the terms of 
//  the GNU General Public License as published by the Free Software Foundation, either version 
//  3 of the License, or (at your option) any later version.
//  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
//  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//  See the LICENSE file in the project root for more information or
//  visit, see<http://www.gnu.org/licenses/>.
//</license>
//----------------------------------------------------------------------------------------------
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