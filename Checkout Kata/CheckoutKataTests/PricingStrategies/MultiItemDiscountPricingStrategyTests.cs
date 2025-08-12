using CheckoutKata.PricingStrategies;

namespace CheckoutKataTests.PricingStrategies;

[TestFixture, TestOf(typeof(MultiItemDiscountPricingStrategy))]
public class MultiItemDiscountPricingStrategyTests
{
    [TestCase(0, 6, 2, 15, 0)] // no items purchased, should return 0
    [TestCase(0, 7, 3, 55, 0)] // no items purchased, should return 0
    [TestCase(1, 40, 3, 80, 40)] // buying 1 items at 40 each, with a discount of 3 for the price of 2
    [TestCase(2, 40, 3, 80, 80)] // buying 2 items at 40 each, with a discount of 3 for the price of 2
    [TestCase(3, 40, 3, 80, 80)] // buying 2 items at 40 each, with a discount of 3 for the price of 2
    [TestCase(4, 40, 3, 80, 120)] // buying 4 items at 40 each, with a discount of 3 for the price of 2
    [TestCase(5, 40, 3, 80, 160)] // buying 5 items at 40 each, with a discount of 3 for the price of 2
    [TestCase(6, 40, 3, 80, 160)] // buying 6 items at 40 each, with a discount of 3 for the price of 2
    [TestCase(1, 8, 2, 12, 8)] // Buy one, Get one half price on items at 8 each
    [TestCase(2, 8, 2, 12, 12)] // Buy one, Get one half price on items at 8 each
    [TestCase(3, 8, 2, 12, 20)] // Buy one, Get one half price on items at 8 each
    [TestCase(4, 8, 2, 12, 24)] // Buy one, Get one half price on items at 8 each
    [TestCase(5, 8, 2, 12, 32)] // Buy one, Get one half price on items at 8 each
    [TestCase(6, 8, 2, 12, 36)] // Buy one, Get one half price on items at 8 each
    // One could continue writing test cases here, or use the range attribute to generate a wider range of test cases
    public void CalculatePrice_WhenCalledWithEnoughItemsToQualifyForDiscount_ReturnsDiscountedPrice(int numPurchasedItems, int unitPrice, int qualifyingQuantity, int bundlePrice, int expectedPrice)
    {
        var sut = new MultiItemDiscountPricingStrategy(unitPrice, qualifyingQuantity, bundlePrice);

        var result = sut.CalculatePrice(numPurchasedItems);

        Assert.That(result, Is.EqualTo(expectedPrice));
    }
}
