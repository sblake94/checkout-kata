using CheckoutKata.Enums;
using CheckoutKata.Interfaces;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    public Dictionary<StockKeepingUnit, int> ScannedItemQuantities { get; }

    public Checkout(PricingStrategyIndex pricingStrategies)
    {

    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    public void Scan(StockKeepingUnit item)
    {
        throw new NotImplementedException();
    }
}
