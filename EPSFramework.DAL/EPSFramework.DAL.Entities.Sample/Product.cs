//-----------------------------------------------------------------------
// <copyright file="UserKey.cs" company="EPapersoft">
//     Copyright ® EPapersoft
// </copyright>
// <Historial>
//      <Entry Date="03/02/2015" Login="JLAlamo">Created</Entry>
// </Historial>
//-----------------------------------------------------------------------
namespace EPSFramework.Entities.Sample
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// DEfines a product to be ordered
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public Decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public ICollection<Order> Orders { get; set; }
    }
}