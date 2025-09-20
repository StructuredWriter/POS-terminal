namespace PointOfSale.Models
{
    /// <summary>
    /// Represents a volume price for product, such as "3 for $5.00"
    /// </summary>
    /// <param name="Quantity">The number of units in the volume deal.</param>
    /// <param name="Price">The total price for the volume deal.</param>
    public readonly record struct VolumePrice(int Quantity, decimal Price);
}