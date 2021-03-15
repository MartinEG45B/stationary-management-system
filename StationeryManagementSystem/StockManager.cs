using System.Collections.Generic;

namespace StationeryManagementSystem
{
    public class StockManager
    {
        public Dictionary<int, Stock> Stock { get; } = new Dictionary<int, Stock>();
        public Stock FindStock(int code)
        {
            try
            {
                return Stock[code];
            }
            catch(KeyNotFoundException)
            {
                throw new System.Exception("ERROR: Stock not found");
            }
        }
        public void UpdateStock(Stock s, int quantity)
        {
            s.Quantity = quantity;
        }

        public void CreateStock(int code, string name, int quantity)
        {
            Stock s = new Stock(code, name, quantity);
            Stock.Add(s.Code, s);
        }
    }
}
