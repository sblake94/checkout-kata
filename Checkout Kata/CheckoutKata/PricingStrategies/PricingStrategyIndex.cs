using CheckoutKata.Enums;
using CheckoutKata.Interfaces;
using System.ComponentModel;

namespace CheckoutKata.PricingStrategies;

// Custom Collection wrapper type allows for:
//    - Mocking via IPricingStrategyIndex
//    - DependencyInjection
public class PricingStrategyIndex : IPricingStrategyIndex
{
    private readonly Dictionary<StockKeepingUnit, IPricingStrategy> _strategies;

    public PricingStrategyIndex()
    {
        _strategies = [];
    }
    public void SetStrategy(StockKeepingUnit sku, IPricingStrategy pricingStrategy)
    {
        if (sku == StockKeepingUnit.INVALID)
            throw new InvalidEnumArgumentException(nameof(sku));

        _strategies[sku] = pricingStrategy;
    }

    public IPricingStrategy GetStrategyForStockKeepingUnit(StockKeepingUnit sku)
    {
        if (sku == StockKeepingUnit.INVALID)
            throw new InvalidEnumArgumentException(nameof(sku));

        if (!_strategies.TryGetValue(sku, out var strategy))
            throw new KeyNotFoundException($"No pricing strategy found for SKU: {sku}");

        return strategy;
    }
}