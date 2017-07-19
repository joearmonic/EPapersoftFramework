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
namespace $safeprojectname$
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;

    /// <summary>
    /// Provides a container of dependencies to inject everywhere the application needs it. The implementations must have [Export] attribute.
    /// </summary>
    public class ContainerBootstrap
    {
        private CompositionContainer _container;
        private AggregateCatalog _catalog;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerBootstrap" /> class by aggregating the application type to the catalog.
        /// </summary>
        /// <param name="meType">Type of the current application given from a start point like in program or in global.asax</param>
        public ContainerBootstrap(Type meType)
        {
            _catalog = new AggregateCatalog();
            ComposeMe(meType);
        }

        /// <summary>
        /// Composes the type passed as argument in the catalog.
        /// </summary>
        /// <param name="meType">Type of the memory.</param>
        public void ComposeMe(Type meType)
        {
            _catalog.Catalogs.Add(new AssemblyCatalog(meType.Assembly));
        }

        /// <summary>
        /// Gets the container by aggregating all external sources and types internal to the application.
        /// </summary>
        /// <returns>
        /// Dependency Container
        /// </returns>
        public CompositionContainer GetContainer()
        {
            if (_container == null)
            {
                _catalog.Catalogs.Add(new ExternalSourcesCatalog());
                _container = new CompositionContainer(_catalog);
                _container.ComposeParts(this);
            }

            return _container;
        }
    }
}