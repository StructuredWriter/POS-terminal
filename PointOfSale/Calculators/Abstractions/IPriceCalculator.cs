using PointOfSale.Models;

namespace PointOfSale.Calculators;

public interface IPriceCalculator
{
    decimal CalculateTotal(ProductModel product, int quantity);
}