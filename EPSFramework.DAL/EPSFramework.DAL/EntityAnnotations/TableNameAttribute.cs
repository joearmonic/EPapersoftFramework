//-----------------------------------------------------------------------
// <copyright file="TableNameAttribute.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="12/06/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.DAL.EntityAnnotations
{
    using System;

    public class TableNameAttribute : Attribute
    {
        private string _name = null;

        public TableNameAttribute(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// Gets or sets the name of the table
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets model's name to map to. If no Model is provided is assumed the name of this class by default as the Model Name.
        /// </summary>
        /// <value>
        /// To model name.
        /// </value>
        public string ToModel { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        /// <value>
        /// The name of the schema adopted by the table in the database (optional value)
        /// </value>
        public string SchemaName { get; set; }
    }
}