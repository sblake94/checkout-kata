using CheckoutKata;
using CheckoutKata.Enums;
using CheckoutKata.Interfaces;
using CheckoutKata.PricingStrategies;
using Moq;
using System.ComponentModel;

namespace CheckoutKataTests;

[TestFixture, TestOf(typeof(Checkout))]
public class CheckoutTests
{
    [Test]
    public void Constructor_WhenGivenEmptyPricingStrategyIndex_StoresTheIndexInternally()
    {
        var pricingStrategyIndex = Mock.Of<IPricingStrategyIndex>();
        var sut = new Checkout(pricingStrategyIndex);

        // Using reflection to access the private field _pricingStrategyIndex
        // This is not ideal in production code, but can be acceptable for unit tests
        // As it allows us to verify internal state without exposing it publicly
        Assert.That(sut.GetType().GetField("_pricingStrategyIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(sut), Is.EqualTo(pricingStrategyIndex), "The pricing strategy index should be stored correctly.");
    }

    [Test]
    public void Constructor_WhenGivenNonEmptyPricingStrategyIndex_StoresTheIndexInternally()
    {
        var pricingStrategyIndexMock = new Mock<IPricingStrategyIndex>();
        pricingStrategyIndexMock.Setup(psi => psi.GetStrategyForStockKeepingUnit(StockKeepingUnit.A)).Returns(Mock.Of<IPricingStrategy>());
        pricingStrategyIndexMock.Setup(psi => psi.GetStrategyForStockKeepingUnit(StockKeepingUnit.B)).Returns(Mock.Of<IPricingStrategy>());

        var sut = new Checkout(pricingStrategyIndexMock.Object);

        // Using reflection to access the private field _pricingStrategyIndex
        // This is not ideal in production code, but can be acceptable for unit tests
        // As it allows us to verify internal state without exposing it publicly
        Assert.That(sut.GetType().GetField("_pricingStrategyIndex", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(sut), Is.EqualTo(pricingStrategyIndexMock.Object), "The pricing strategy index should be stored correctly.");
    }

    [Test]
    public void Constructor_WhenGivenNullPricingStrategyIndex_ThrowsArgumentNullException()
    {
        Checkout? sut = null;

        Assert.That(() => sut = new Checkout(null), Throws.ArgumentNullException);
    }

    [TestCase(StockKeepingUnit.A)]
    [TestCase(StockKeepingUnit.B)]
    [TestCase(StockKeepingUnit.C)]
    [TestCase(StockKeepingUnit.D)]
    public void Scan_WhenGivenValidStockKeepingUnit_AddsItemToBasket(StockKeepingUnit item)
    {
        var sut = new Checkout(Mock.Of<IPricingStrategyIndex>());

        sut.Scan(item);

        Assert.That(sut.ScannedItemQuantities[item], Is.EqualTo(1));
    }

    [Test]
    public void Scan_WhenGivenInvalidStockKeepingUnit_ThrowsArgumentException()
    {
        var sut = new Checkout(Mock.Of<IPricingStrategyIndex>());
        var item = StockKeepingUnit.INVALID;

        Assert.That(() => sut.Scan(item), Throws.TypeOf<InvalidEnumArgumentException>());
    }

    [Test]
    public void GetTotalPrice_WhenCalledOnCheckoutWithEmptyBasket_ReturnsZero()
    {
        var sut = new Checkout(Mock.Of<IPricingStrategyIndex>());

        Assert.That(sut.GetTotalPrice(), Is.EqualTo(0), "The total price should be zero when the basket is empty.");
    }

    [Test]
    public void GetTotalPrice_CallsCalculatePrice_OnAllPricingStrategies_OncePerMatchingItemScanned()
    {
        var pricingStrategyAMock = new Mock<IPricingStrategy>();
        var pricingStrategyBMock = new Mock<IPricingStrategy>();
        var pricingStrategyIndexMock = new Mock<IPricingStrategyIndex>();
        pricingStrategyIndexMock.Setup(psi => psi.GetStrategyForStockKeepingUnit(StockKeepingUnit.A)).Returns(pricingStrategyAMock.Object);
        pricingStrategyIndexMock.Setup(psi => psi.GetStrategyForStockKeepingUnit(StockKeepingUnit.B)).Returns(pricingStrategyBMock.Object);
        var sut = new Checkout(pricingStrategyIndexMock.Object);

        // TODO: This is a potential point of failure if the Scan method does not correctly update the ScannedItemQuantities dictionary.
        // We should probably make a way to add items to the basket without relying on a testable method like Scan.
        sut.Scan(StockKeepingUnit.A);
        sut.Scan(StockKeepingUnit.B);

        var _ = sut.GetTotalPrice();

        pricingStrategyAMock.Verify(ps => ps.CalculatePrice(1), Times.Once, "The pricing strategy for StockKeepingUnit A should be called with the correct quantity.");
        pricingStrategyBMock.Verify(ps => ps.CalculatePrice(1), Times.Once, "The pricing strategy for StockKeepingUnit B should be called with the correct quantity.");
    }

    [Test]
    public void GetTotalPrice_WhenCalledWithItemsThatDoNotHavePricingStrategy_ThrowsNullReferenceException()
    {
        var sut = new Checkout(Mock.Of<IPricingStrategyIndex>());

        sut.Scan(StockKeepingUnit.A);

        Assert.That(sut.GetTotalPrice, Throws.TypeOf<NullReferenceException>());
    }
}