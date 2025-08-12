using CheckoutKata.Interfaces;

namespace CheckoutKata.PricingStrategies;

public class MultiItemDiscountPricingStrategy : IPricingStrategy
{
    public MultiItemDiscountPricingStrategy(int unitPrice, int qualifyingQuantity, int bundlePrice)
    {
    }

    public int CalculatePrice(int quantity)
    {
        throw new NotImplementedException();
    }
}
