using CheckoutKata.Enums;
using CheckoutKata.Interfaces;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    public Dictionary<StockKeepingUnit, int> ScannedItemQuantities { get; } = [];
    private readonly PricingStrategyIndex _pricingStrategyIndex;

    public Checkout(PricingStrategyIndex pricingStrategies)
    {
        ArgumentNullException.ThrowIfNull(pricingStrategies);

        _pricingStrategyIndex = pricingStrategies;
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    public void Scan(StockKeepingUnit item)
    {
        if(item.Equals(StockKeepingUnit.INVALID))
        {
            throw new ArgumentException("Invalid item scanned.", nameof(item));
        }

        if (!ScannedItemQuantities.ContainsKey(item))
        {
            ScannedItemQuantities[item] = 0;
        }

        ScannedItemQuantities[item]++;
    }
}
