
using System;

namespace StationeryManagementSystem
{
    public class Transaction
    {
        public int Code { get;}
        public string Name { get;}
        public int Quantity { get;}
        public decimal PricePaid { get;}
        public DateTime DateAdded { get;}
        public string PersonName { get; }
        public DateTime DateTaken { get;}

        
        public Transaction(int code, string name, int quantity, string personName, decimal pricePaid, DateTime dateAdded, DateTime dateTaken)
        {
            this.Code = code;
            this.Name = name;
            this.Quantity = quantity;
            this.PersonName = personName;
            this.PricePaid = pricePaid;
            this.DateAdded = dateAdded;
            this.DateTaken = dateTaken;
        }


        public Transaction(int code, string name, int quantity, decimal pricePaid, DateTime dateAdded) : this(code, name, quantity, "", pricePaid, dateAdded, DateTime.MinValue)
        {

        }

        public Transaction(int code, string name, string personName, DateTime dateTaken) : this(code, name, 0, personName, 0, DateTime.MinValue, dateTaken)
        {

        }
    }
}
