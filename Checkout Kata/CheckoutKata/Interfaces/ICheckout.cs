using CheckoutKata.Enums;

namespace CheckoutKata.Interfaces;

public interface ICheckout
{
    void Scan(StockKeepingUnit item);
    int GetTotalPrice();
}
