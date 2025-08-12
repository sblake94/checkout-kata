using CheckoutKata.Interfaces;
using CheckoutKata.PricingStrategies;
using CheckoutKataTests.Utils;
using Moq;
using System.ComponentModel;

namespace CheckoutKataTests.PricingStrategies;

[TestFixture, TestOf(typeof(PricingStrategyIndex))] 
public class PricingStrategyIndexTests
{
    [TestCase(TestStockItemIdentifiers.A)]
    [TestCase(TestStockItemIdentifiers.B)]
    [TestCase(TestStockItemIdentifiers.C)]
    [TestCase(TestStockItemIdentifiers.D)]
    public void SetStrategy_WhenGivenValidStockKeepingUnit_AndPricingStrategy_AddsToIndex(string sku)
    {
        var pricingStrategyIndex = new PricingStrategyIndex();
        var pricingStrategy = Mock.Of<IPricingStrategy>();

        pricingStrategyIndex.SetStrategy(sku, pricingStrategy);

        Assert.That(pricingStrategyIndex.GetStrategyForItem(sku), Is.EqualTo(pricingStrategy));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void SetStrategy_WhenGivenInValidStockKeepingUnit_AndPricingStrategy_ThrowsInvalidEnumArgumentException(string? invalidItemIdentifier)
    {
        var pricingStrategyIndex = new PricingStrategyIndex();
        var pricingStrategy = Mock.Of<IPricingStrategy>();

        Assert.That(() => pricingStrategyIndex.SetStrategy(invalidItemIdentifier, pricingStrategy), Throws.TypeOf<InvalidEnumArgumentException>());
    }
}
