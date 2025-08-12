using CheckoutKata.Interfaces;

namespace CheckoutKata.PricingStrategies;

public class MultiItemDiscountPricingStrategy : IPricingStrategy
{
    readonly int _unitPrice;
    readonly int _qualifyingQuantity;
    readonly int _bundlePrice;

    public MultiItemDiscountPricingStrategy(int unitPrice, int qualifyingQuantity, int bundlePrice)
    {
        _unitPrice = unitPrice;
        _qualifyingQuantity = qualifyingQuantity;
        _bundlePrice = bundlePrice;
    }

    public int CalculatePrice(int quantity)
    {
        var bundleCount = quantity / _qualifyingQuantity;
        var remainderCount = quantity % _qualifyingQuantity;
        var totalPrice = (bundleCount * _bundlePrice) + (remainderCount * _unitPrice);
        return totalPrice;
    }
}
