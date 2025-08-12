using CheckoutKata.Enums;
using CheckoutKata.Interfaces;

namespace CheckoutKata
{
    struct ScannedItem
    {
        public StockKeepingUnit StockKeepingUnit { get; }
        public int Quantity { get; }
        public int IPricingStrategy { get; }
    }
}
