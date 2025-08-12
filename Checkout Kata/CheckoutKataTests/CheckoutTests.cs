using CheckoutKata;
using CheckoutKata.Enums;
using CheckoutKata.Interfaces;
using Moq;

namespace CheckoutKataTests;

[TestFixture, TestOf(typeof(Checkout))]
public class CheckoutTests
{
    [Test]
    public void Constructor_WhenGivenEmptyPricingStrategyIndex_StoresTheIndexInternally()
    {
        var sut = new Checkout([]);

        // Using reflection to access the private field _pricingStrategyIndex
        // This is not ideal in production code, but can be acceptable for unit tests
        // As it allows us to verify internal state without exposing it publicly
        Assert.That(sut.GetType().GetField("_pricingStrategyIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(sut), Is.EquivalentTo(new Dictionary<StockKeepingUnit, IPricingStrategy>()), "The pricing strategy index should be stored correctly.");
    }

    [Test]
    public void Constructor_WhenGivenNonEmptyPricingStrategyIndex_StoresTheIndexInternally()
    {
        var pricingStrategyIndex = new PricingStrategyIndex()
        {
            { StockKeepingUnit.A, Mock.Of<IPricingStrategy>() },
            { StockKeepingUnit.B, Mock.Of<IPricingStrategy>() },
        };
        var sut = new Checkout([]);

        // Using reflection to access the private field _pricingStrategyIndex
        // This is not ideal in production code, but can be acceptable for unit tests
        // As it allows us to verify internal state without exposing it publicly
        Assert.That(sut.GetType().GetField("_pricingStrategyIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(sut), Is.EquivalentTo(new Dictionary<StockKeepingUnit, IPricingStrategy>()), "The pricing strategy index should be stored correctly.");
    }

    [Test]
    public void Constructor_WhenGivenNullPricingStrategyIndex_ThrowsArgumentnullException()
    {
        Checkout? sut = null;

        Assert.That(() => sut = new Checkout(null), Throws.ArgumentNullException);
    }

    [Test]
    public void Scan_WhenGivenValidStockKeepingUnit_AddsItemToBasket([Values]StockKeepingUnit item)
    {
        var sut = new Checkout([]);

        sut.Scan(item);

        Assert.That(sut.ScannedItemQuantities[item], Is.EqualTo(1));
    }

    [Test]
    public void Scan_WhenGivenNullStockKeepingUnit_ThrowsArgumentException()
    {
        var sut = new Checkout([]);
        StockKeepingUnit item = default;

        Assert.That(() => sut.Scan(item), Throws.ArgumentException);
    }

    [Test]
    public void GetTotalPrice_WhenCalledOnCheckoutWithEmptyBasket_ReturnsZero()
    {
        var sut = new Checkout([]);

        Assert.That(sut.GetTotalPrice(), Is.EqualTo(0), "The total price should be zero when the basket is empty.");
    }

    [Test]
    public void GetTotalPrice_CallsCalculatePrice_OnAllPricingStrategies_OncePerMatchingItemScanned()
    {
        var pricingStrategyA = new Mock<IPricingStrategy>();
        var pricingStrategyB = new Mock<IPricingStrategy>();

        var sut = new Checkout(new PricingStrategyIndex()
        {
            { StockKeepingUnit.A, pricingStrategyA.Object },
            { StockKeepingUnit.B, pricingStrategyB.Object },
        });

        // TODO: This is a potential point of failure if the Scan method does not correctly update the ScannedItemQuantities dictionary.
        // We should probably make a way to add items to the basket without relying on a testable method like Scan.
        sut.Scan(StockKeepingUnit.A);
        sut.Scan(StockKeepingUnit.B);

        var _ = sut.GetTotalPrice();

        pricingStrategyA.Verify(ps => ps.CalculatePrice(1), Times.Once, "The pricing strategy for StockKeepingUnit A should be called with the correct quantity.");
        pricingStrategyB.Verify(ps => ps.CalculatePrice(1), Times.Once, "The pricing strategy for StockKeepingUnit B should be called with the correct quantity.");
    }
}