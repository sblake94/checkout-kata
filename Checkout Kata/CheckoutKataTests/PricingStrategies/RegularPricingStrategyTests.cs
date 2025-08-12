using CheckoutKata.PricingStrategies;

namespace CheckoutKataTests.PricingStrategies;

[TestFixture, TestOf(typeof(RegularPricingStrategy))]
public class RegularPricingStrategyTests
{
    // Parameterizable constants to define the range of unit costs and quantities for testing
    // These are here for the sake of the exercise
    // In a real-world scenario, these would likely be defined in a test configuration file or similar
    const int minUnitCost = 0;
    const int maxUnitCost = 10;
    const int minUnitQuantity = 0;
    const int maxUnitQuantity = 40;

    [Test]
    public void CalculatePrice_WhenCalledWithItems_ReturnsUnitCostMultipliedByUnitQuantity([Range(minUnitCost, maxUnitCost)]int unitCost, [Range(minUnitQuantity, maxUnitQuantity)] int unitQuantity)
    {
        var sut = new RegularPricingStrategy();
        var expectedPrice = unitQuantity * unitCost; // Assuming each item costs 10

        var actualPrice = sut.CalculatePrice(unitQuantity);
        Assert.That(actualPrice, Is.EqualTo(expectedPrice), "The calculated price should be the sum of the item prices.");
    }
}
