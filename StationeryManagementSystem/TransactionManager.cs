using System;
using System.Collections.Generic;

namespace StationeryManagementSystem
{
    public class TransactionManager
    {
        public List<Transaction> Transaction { get; } = new List<Transaction>();

        public void CreateAddTransactoinLog(int code, string name, int quantity, decimal pricePaid, DateTime dateAdded)
        {
            Transaction.Add(new Transaction(code, name, quantity, pricePaid, dateAdded));
        }
        public void CreateRemoveTransactoinLog(int code, string name, string personName, DateTime dateTaken)
        {
            Transaction.Add(new Transaction(code, name, personName, dateTaken));
        }

        public List<Transaction> GetPersonTransactions(string personName)
        {
            List<Transaction> personTransactions = new List<Transaction>();
            for (int index = 0; index < Transaction.Count; index++)
            {
                if(Transaction[index].PersonName == personName)
                {
                    personTransactions.Add(Transaction[index]);
                }
            }
            return personTransactions;
        }
    }
}
