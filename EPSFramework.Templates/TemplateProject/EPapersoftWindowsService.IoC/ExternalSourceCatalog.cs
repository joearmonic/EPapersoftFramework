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
	using System.ComponentModel.Composition.Hosting;
	using System.Reflection;
	using ConfigurationHandler;

	/// <summary>
	/// Provides a catalog created by external sources in the project output directory.
	/// </summary>
	public class ExternalSourcesCatalog : AggregateCatalog
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExternalSourcesCatalog"/> class.
		/// </summary>
		public ExternalSourcesCatalog()
		{
			DIComponentHandler configHandler = DIComponentHandler.Create();
			
			foreach (var component in configHandler.Components)
			{
				Assembly assem = Assembly.Load(new AssemblyName(component.Assembly));
				this.Catalogs.Add(new AssemblyCatalog(assem));
				
				//TODO: See how do that type by type.
				//foreach (var dependency in component.Dependencies)
				//{
				//    this.Catalogs.Add(new TypeCatalog(assem.DefinedTypes.Where(t => t.Name == dependency.Interface).FirstOrDefault().GetType()));
				//}                
			}
		}
	}
}