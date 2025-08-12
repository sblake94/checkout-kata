using CheckoutKata.Enums;
using CheckoutKata.Interfaces;
using CheckoutKata.PricingStrategies;
using Moq;
using System.ComponentModel;

namespace CheckoutKataTests.PricingStrategies;

[TestFixture, TestOf(typeof(PricingStrategyIndex))] 
public class PricingStrategyIndexTests
{
    [TestCase(StockKeepingUnit.A)]
    [TestCase(StockKeepingUnit.B)]
    [TestCase(StockKeepingUnit.C)]
    [TestCase(StockKeepingUnit.D)]
    public void SetStrategy_WhenGivenValidStockKeepingUnit_AndPricingStrategy_AddsToIndex(StockKeepingUnit sku)
    {
        var pricingStrategyIndex = new PricingStrategyIndex();
        var pricingStrategy = Mock.Of<IPricingStrategy>();

        pricingStrategyIndex.SetStrategy(sku, pricingStrategy);

        Assert.That(pricingStrategyIndex.GetStrategyForStockKeepingUnit(sku), Is.EqualTo(pricingStrategy));
    }

    [Test]
    public void SetStrategy_WhenGivenInValidStockKeepingUnit_AndPricingStrategy_ThrowsInvalidEnumArgumentException()
    {
        var pricingStrategyIndex = new PricingStrategyIndex();
        var pricingStrategy = Mock.Of<IPricingStrategy>();

        Assert.That(() => pricingStrategyIndex.SetStrategy(StockKeepingUnit.INVALID, pricingStrategy), Throws.TypeOf<InvalidEnumArgumentException>());
    }
}
