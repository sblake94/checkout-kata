
namespace CheckoutKata.Interfaces;

public interface IPricingStrategyIndex
{
    IPricingStrategy GetStrategyForItem(string itemIdentifier);
    void SetStrategy(string itemIdentifier, IPricingStrategy pricingStrategy);
}
