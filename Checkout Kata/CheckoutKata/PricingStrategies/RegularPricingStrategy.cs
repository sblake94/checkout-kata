using CheckoutKata.Interfaces;

namespace CheckoutKata.PricingStrategies;

public class RegularPricingStrategy(int unitPrice) : IPricingStrategy
{
    public int CalculatePrice(int quantity) => unitPrice * quantity;
}
