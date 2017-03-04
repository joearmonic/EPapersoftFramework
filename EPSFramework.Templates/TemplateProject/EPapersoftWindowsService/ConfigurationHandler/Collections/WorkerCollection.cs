//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.ConfigurationHandler
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    internal class WorkerCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Creates the new element.
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new WorkerElement();
        }

        /// <summary>
        /// Gets the element key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WorkerElement)element).Id;
        }
    }
}
