//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.IoC
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