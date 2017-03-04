﻿//-----------------------------------------------------------------------
// <copyright file="$fileinputname$.cs" company="$registeredorganization$">
//     Copyright ® $registeredorganization$
// </copyright>
//-----------------------------------------------------------------------
namespace $safeprojectname$.ConfigurationHandler
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    [ConfigurationCollection(typeof(ActivitiesSettingElement),
    CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
    internal class ActivitiesSettingsCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// The m_properties
        /// </summary>
        private static ConfigurationPropertyCollection m_properties;

        /// <summary>
        /// Initializes the <see cref="ActivitiesSettingsCollection"/> class.
        /// </summary>
        static ActivitiesSettingsCollection()
        {
            m_properties = new ConfigurationPropertyCollection();
        }

        /// <summary>
        /// Gets the type of the collection.
        /// </summary>
        /// <value>
        /// The type of the collection.
        /// </value>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return m_properties; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivitiesSettingsCollection"/> class.
        /// </summary>
        internal ActivitiesSettingsCollection()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="ActivitiesSettingElement"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="ActivitiesSettingElement"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        internal ActivitiesSettingElement this[int index]
        {
            get { return (ActivitiesSettingElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }

                base.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="ActivitiesSettingElement"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="ActivitiesSettingElement"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        internal new ActivitiesSettingElement this[string key]
        {
            get { return (ActivitiesSettingElement)base.BaseGet(key); }
        }

        /// <summary>
        /// Creates the new element.
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ActivitiesSettingElement();
        }

        /// <summary>
        /// Gets the element key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ActivitiesSettingElement)element).Key;
        }
    }
}