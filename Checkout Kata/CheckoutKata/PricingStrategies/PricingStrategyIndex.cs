using CheckoutKata.Interfaces;
using System.ComponentModel;

namespace CheckoutKata.PricingStrategies;

// Custom Collection wrapper type allows for:
//    - Mocking via IPricingStrategyIndex
//    - DependencyInjection
public class PricingStrategyIndex : IPricingStrategyIndex
{
    private readonly Dictionary<string, IPricingStrategy> _strategies;

    public PricingStrategyIndex()
    {
        _strategies = [];
    }
    public void SetStrategy(string itemIdentifier, IPricingStrategy pricingStrategy)
    {
        if (string.IsNullOrWhiteSpace(itemIdentifier))
            throw new InvalidEnumArgumentException(nameof(itemIdentifier));

        _strategies[itemIdentifier] = pricingStrategy;
    }

    public IPricingStrategy GetStrategyForStockKeepingUnit(string itemIdentifier)
    {
        if (string.IsNullOrWhiteSpace(itemIdentifier))
            throw new InvalidEnumArgumentException(nameof(itemIdentifier));

        if (!_strategies.TryGetValue(itemIdentifier, out var strategy))
            throw new KeyNotFoundException($"No pricing strategy found for StockKeepingUnit: {itemIdentifier}");

        return strategy;
    }
}