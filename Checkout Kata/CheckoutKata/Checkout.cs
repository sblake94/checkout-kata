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
        int totalPrice = 0;
        foreach (var itemTypeAndQuantity in ScannedItemQuantities)
        {
            if (!_pricingStrategyIndex.TryGetValue(itemTypeAndQuantity.Key, out IPricingStrategy pricingStrategy)) 
                throw new InvalidOperationException($"Pricing Strategy not found for {itemTypeAndQuantity.Key}");

            totalPrice += pricingStrategy.CalculatePrice(itemTypeAndQuantity.Value);
        }

        return totalPrice;
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
