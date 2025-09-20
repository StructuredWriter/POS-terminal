# PointOfSaleTerminal

-- Simple .NET class library for grocery terminal checkout with volume pricing

## Usage
```csharp
var terminal = new PointOfSaleTerminal(new PriceCalculator());
terminal.SetPricing(new List<Product> { ... });
terminal.Scan("A");
terminal.Scan("B");
var total = terminal.GetTotal();