using PointOfSale.Calculators;
using PointOfSale.Models;
using PointOfSale.Services;
using static NUnit.Framework.Assert;

namespace PointsOfSale.Tests
{
    [TestFixture]
    public class TerminalTests
    {
        private PointOfSaleTerminal _terminal;

        [SetUp]
        public void SetUp()
        {
            _terminal = new PointOfSaleTerminal(new PriceCalculator());
            _terminal.SetPricing([
                new("A", 1.25m, new VolumePriceModel(3, 3m)),
                new("B", 4.25m),
                new("C", 1m, new VolumePriceModel(6, 5m)),
                new("D", 0.75m)
            ]);
        }

        [TestCase("ABCDABA", 13.25)]
        [TestCase("CCCCCCC", 6.00)]
        [TestCase("ABCD", 7.25)]
        public void Calculation_IsTermialWorksCorrectly(string input, decimal expectedTotal)
        {
            foreach (var code in input)
                _terminal.Scan(code.ToString());

            var total = _terminal.GetTotal();
            AreEqual(expectedTotal, total);
        }

        [Test]
        public void Scan_ShouldThrowException_WhenProductCodeInvalid()
        {
            var ex = Throws<ArgumentException>(() => _terminal.Scan("X"));
            StringAssert.Contains("not found", ex.Message);
        }

        [TestCase("A")]
        public void Reset_ShouldClearScannedProducts(string code)
        {
            _terminal.Scan(code);

            _terminal.Reset();

            var total = _terminal.GetTotal();
            AreEqual(0m, total);
        }
    }
}