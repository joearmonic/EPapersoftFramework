//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.IoC.ConfigurationHandler
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    internal class DIComponentHandler
    {
        /// <summary>
        /// The _instance
        /// </summary>
        private static DIComponentHandler _instance = null;

        /// <summary>
        /// Gets or sets the components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        internal List<Component> Components { get; set; }

        /// <summary>
        /// Prevents a default instance of the <see cref="DIComponentHandler"/> class from being created.
        /// </summary>
        private DIComponentHandler()
        {
            DIComponentSection configurationSection = (DIComponentSection)ConfigurationManager.GetSection("ioCSection");
            this.Components = configurationSection.DIComponents.Cast<ComponentElement>().Select(
                e => new Component() 
                { 
                    Assembly = e.AssemblyName, 
                    Dependencies = e.Dependencies.Cast<MappingElement>().Select(m => new Component.Dependency() 
                    { Interface = m.Interface, ImplementorClass = m.ImplementedBy }).ToList() 
                }).ToList();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        internal static DIComponentHandler Create()
        {
            if (_instance == null)
            {
                _instance = new DIComponentHandler();
            }

            return _instance;
        }

        internal class Component
        {
            /// <summary>
            /// Gets or sets the assembly.
            /// </summary>
            /// <value>
            /// The assembly.
            /// </value>
            internal String Assembly { get; set; }

            /// <summary>
            /// Gets or sets the dependencies.
            /// </summary>
            /// <value>
            /// The dependencies.
            /// </value>
            internal List<Dependency> Dependencies { get; set; }

            internal class Dependency
            {
                /// <summary>
                /// Gets or sets the interface.
                /// </summary>
                /// <value>
                /// The interface.
                /// </value>
                internal String Interface { get; set; }

                /// <summary>
                /// Gets or sets the implementor class.
                /// </summary>
                /// <value>
                /// The implementor class.
                /// </value>
                internal String ImplementorClass { get; set; }
            }
        }
    }
}
