
namespace CheckoutKata.Interfaces;

public interface IPricingStrategyIndex
{
    IPricingStrategy GetStrategyForStockKeepingUnit(string itemIdentifier);
    void SetStrategy(string itemIdentifier, IPricingStrategy pricingStrategy);
}
