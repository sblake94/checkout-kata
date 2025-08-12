using CheckoutKata.Enums;
using CheckoutKata.Interfaces;

namespace CheckoutKata.PricingStrategies;

// Custom Collection wrapper type allows for:
//    - Mocking via IPricingStrategyIndex
//    - DependencyInjection
public class PricingStrategyIndex : IPricingStrategyIndex
{
    public void SetStrategy(StockKeepingUnit sku, IPricingStrategy pricingStrategy)
    {
        throw new NotImplementedException(nameof(SetStrategy));
    }

    public IPricingStrategy GetStrategyForStockKeepingUnit(StockKeepingUnit sku)
    {
        throw new NotImplementedException(nameof(GetStrategyForStockKeepingUnit));
    }
}
