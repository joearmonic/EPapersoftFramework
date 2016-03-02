namespace EPSFramework.Entities.Sample
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Defines an order to the manufactures
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public Decimal Total { get; set; }

        /// <summary>
        /// Gets or sets the shipped on.
        /// </summary>
        /// <value>
        /// The shipped on.
        /// </value>
        public DateTime ShippedOn { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public ICollection<Product> Products { get; set; }
    }
}