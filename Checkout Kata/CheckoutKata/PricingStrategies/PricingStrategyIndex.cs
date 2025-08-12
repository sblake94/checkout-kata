using CheckoutKata.Enums;
using CheckoutKata.Interfaces;

namespace CheckoutKata;

public class PricingStrategyIndex : Dictionary<StockKeepingUnit, IPricingStrategy>
{
}
