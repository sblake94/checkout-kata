using CheckoutKata.Interfaces;

namespace CheckoutKata.PricingStrategies;

public class RegularPricingStrategy : IPricingStrategy
{
    readonly int _unitPrice;

    public RegularPricingStrategy(int unitPrice)
    {
        _unitPrice = unitPrice;
    }

    public int CalculatePrice(int quantity) => _unitPrice * quantity;
}
