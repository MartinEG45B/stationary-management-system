using System;

namespace StationeryManagementSystem
{
    public class Stock
    {
        public int Code { get; }
        public string Name { get; }
        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity += value;
            }

        }

        public Stock(int code, string name, int quantity)
        {
            this.Code = code;
            this.Name = name;
            this.quantity = quantity;
        }

    }
}
