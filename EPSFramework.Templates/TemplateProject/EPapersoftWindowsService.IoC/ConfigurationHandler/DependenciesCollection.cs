//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace EPapersoftWindowsService.IoC.ConfigurationHandler
{
    using System.Configuration;

    internal class DependenciesCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Cuando se reemplaza en una clase derivada, se crea un nuevo objeto <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// Una celda <see cref="T:System.Configuration.ConfigurationElement" /> recién creada.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MappingElement();
        }

        /// <summary>
        /// Gets the element key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MappingElement)element).Interface;
        }
    }
}
