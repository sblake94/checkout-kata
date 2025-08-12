using CheckoutKata.Enums;
using CheckoutKata.Interfaces;
using System.ComponentModel;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    public Dictionary<StockKeepingUnit, int> ScannedItemQuantities { get; } = [];
    private readonly IPricingStrategyIndex _pricingStrategyIndex;

    public Checkout(IPricingStrategyIndex pricingStrategies)
    {
        ArgumentNullException.ThrowIfNull(pricingStrategies);

        _pricingStrategyIndex = pricingStrategies;
    }

    public int GetTotalPrice()
    {
        int totalPrice = 0;
        foreach (var itemTypeAndQuantity in ScannedItemQuantities)
        {
            var pricingStrategy = _pricingStrategyIndex.GetStrategyForStockKeepingUnit(itemTypeAndQuantity.Key);

            totalPrice += pricingStrategy.CalculatePrice(itemTypeAndQuantity.Value);
        }

        return totalPrice;
    }

    public void Scan(StockKeepingUnit item)
    {
        if(item.Equals(StockKeepingUnit.INVALID))
        {
            throw new InvalidEnumArgumentException($"{nameof(item)} was {item}");
        }

        if (!ScannedItemQuantities.TryGetValue(item, out int value))
        {
            value = 0;
            ScannedItemQuantities[item] = value;
        }

        ScannedItemQuantities[item] = ++value;
    }
}
