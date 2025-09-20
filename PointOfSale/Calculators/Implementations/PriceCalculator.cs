using PointOfSale.Models;

namespace PointOfSale.Calculators;

/// <summary>
/// Provides logic to calculate the total price for a scanned product,
/// applying volume pricing if the specified quantity qualifies.
/// </summary>
public sealed class PriceCalculator : IPriceCalculator
{
    /// <summary>
    /// Calculates the total price of a product based on its unit price and volume pricing rules.
    /// If the product has a volume discount and the quantity meets the threshold, 
    /// the discounted price is applied once; the remaining units are charged at the regular price.
    /// </summary>
    /// <param name="product">The product to calculate the total price for.</param>
    /// <param name="quantity">The total number of units scanned for this product.</param>
    /// <returns>Total price as a decimal, including any applicable discounts.</returns>
    public decimal CalculateTotal(ProductModel product, int quantity)
    {
        var volume = product.Volume;

        if (volume is null || volume.Value.Quantity <= 0)
            return quantity * product.Price;

        if (quantity >= volume.Value.Quantity)
        {
            var discountedUnits = volume.Value.Quantity;
            var fullPriceUnits = quantity - discountedUnits;

            return volume.Value.Price + fullPriceUnits * product.Price;
        }

        return quantity * product.Price;
    }
}