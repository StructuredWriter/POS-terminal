using PointOfSale.Models;
using PointOfSale.Calculators;

namespace PointOfSale.Services
{
    /// <summary>
    /// Represents a point-of-sale terminal that handles product scanning,
    /// pricing setup, and total price calculation with discount logic.
    /// </summary>
    public sealed class PointOfSaleTerminal
    {
        private readonly Dictionary<string, ProductModel> _products = new();
        private readonly Dictionary<string, int> _productsCart = new();
        private readonly IPriceCalculator _priceCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfSaleTerminal"/> class.
        /// </summary>
        /// <param name="priceCalculator">The pricing calculator used to compute totals with discounts.</param>
        public PointOfSaleTerminal(IPriceCalculator priceCalculator)
        {
            _priceCalculator = priceCalculator;
        }

        /// <summary>
        /// Sets the available products and their pricing rules.
        /// Clears any previously scanned products.
        /// </summary>
        /// <param name="products">List of products with pricing information.</param>
        public void SetPricing(List<ProductModel> products)
        {
            Reset();
            foreach (var product in products)
                _products[product.Code] = product;
        }

        /// <summary>
        /// Scans a product by its code and adds it to the cart.
        /// </summary>
        /// <param name="productCode">The code of the product to scan.</param>
        /// <exception cref="ArgumentException">Thrown if the product code is not recognized.</exception>
        public void Scan(string productCode)
        {
            if (!_products.ContainsKey(productCode))
                throw new ArgumentException($"Product code {productCode} not found.");

            if (!_productsCart.TryAdd(productCode, 1))
                _productsCart[productCode]++;
        }

        /// <summary>
        /// Calculates the total price for all scanned products,
        /// applying any volume discounts where applicable.
        /// </summary>
        /// <returns>Total amount due as a decimal.</returns>
        public decimal GetTotal()
        {
            return _productsCart.Sum(item =>
            {
                var product = _products[item.Key];
                var quantity = item.Value;
                return _priceCalculator.CalculateTotal(product, quantity);

            });
        }

        /// <summary>
        /// Clears the current cart, removing all scanned products.
        /// </summary>
        public void Reset()
        {
            _productsCart.Clear();
        }
    }
}