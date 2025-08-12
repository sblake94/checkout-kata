using CheckoutKata.Enums;

namespace CheckoutKata.Interfaces;

public interface IPricingStrategyIndex
{
    IPricingStrategy GetStrategyForStockKeepingUnit(StockKeepingUnit sku);
    void SetStrategy(StockKeepingUnit sku, IPricingStrategy pricingStrategy);
}
