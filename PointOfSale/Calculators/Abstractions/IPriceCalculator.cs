using PointOfSale.Models;

namespace PointOfSale.Calculators;

public interface IPriceCalculator
{
    decimal CalculateTotal(Product product, int quantity);
}