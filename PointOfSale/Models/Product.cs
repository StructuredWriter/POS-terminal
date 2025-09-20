namespace PointOfSale.Models
{
    /// <summary>
    /// Represents a product with a unit price and an optional volume price.
    /// </summary>
    public sealed class Product
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="code">The unique product code.</param>
        /// <param name="price">The unit price of the product.</param>
        /// <param name="volume">Optional volume pricing for the product.</param>
        public Product(string code, decimal price, VolumePrice? volume = null)
        {
            Code = code;
            Price = price;
            Volume = volume;
        }

        /// <summary>
        /// Unique product code (e.g., "A", "B").
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Price per unit of the product.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Optional volume pricing details for the product.
        /// </summary>
        public VolumePrice? Volume { get; }
    }
}