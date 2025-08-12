using CheckoutKata.Interfaces;

namespace CheckoutKata.PricingStrategies;

public class MultiItemDiscountPricingStrategy(int unitPrice, int qualifyingQuantity, int bundlePrice) : IPricingStrategy
{
    public int CalculatePrice(int quantity)
    {
        var bundleCount = quantity / qualifyingQuantity;
        var remainderCount = quantity % qualifyingQuantity;
        var totalPrice = (bundleCount * bundlePrice) + (remainderCount * unitPrice);
        return totalPrice;
    }
}
