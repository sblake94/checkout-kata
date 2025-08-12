using CheckoutKata.Enums;
using System.Collections.ObjectModel;

namespace CheckoutKata.Interfaces;

public interface ICheckout
{
    void Scan(StockKeepingUnit item);
    int GetTotalPrice();
}
