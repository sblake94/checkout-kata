using CheckoutKata;

namespace CheckoutKataTests;

[TestFixture, TestOf(typeof(Checkout))]
public class CheckoutTests
{
    [Test]
    public void Scan_WhenGivenValidStockKeepingUnit_AddsItemToBasket()
    {
        throw new NotImplementedException("This unit test is not yet implemented");
    }

    [Test]
    public void Scan_WhenGivenInvalidStockKeepingUnit_ThrowsArgumentException()
    {
        throw new NotImplementedException("This unit test is not yet implemented");
    }

    [Test]
    public void GetTotalPrice_WhenCalledOnCheckoutWithEmptyBasket_ReturnsZero()
    {
        throw new NotImplementedException("This unit test is not yet implemented");
    }

    [Test]
    public void GetTotalPrice_CallsCalculatePrice_OnAllStockKeepingUnitsInBasket_WithCorrectQuantity()
    {
        throw new NotImplementedException("This unit test is not yet implemented");
    }
}