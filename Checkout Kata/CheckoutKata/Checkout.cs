using CheckoutKata.Interfaces;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    public Dictionary<string, int> ScannedItemQuantities { get; } = [];
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
            var pricingStrategy = _pricingStrategyIndex.GetStrategyForItem(itemTypeAndQuantity.Key);

            totalPrice += pricingStrategy.CalculatePrice(itemTypeAndQuantity.Value);
        }

        return totalPrice;
    }

    public void Scan(string item)
    {
        if(string.IsNullOrWhiteSpace(item))
        {
            throw new ArgumentException($"{nameof(item)} was {item}");
        }

        if (!ScannedItemQuantities.TryGetValue(item, out int value))
        {
            value = 0;
            ScannedItemQuantities[item] = value;
        }

        ScannedItemQuantities[item] = ++value;
    }
}
