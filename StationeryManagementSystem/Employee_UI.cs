

using System;
using System.Collections.Generic;

namespace StationeryManagementSystem
{
     public class Employee_UI
    {
        private StockManager stockMgr;
        private TransactionManager transactionMgr;
        public Employee_UI(StockManager sm, TransactionManager tm)
        {
            this.stockMgr = sm;
            this.transactionMgr = tm;
        }

        public void AddToStock(int code, string name, int quantity, decimal pricePaid, DateTime dateAdded)
        {
            try
            {
                Stock s = stockMgr.FindStock(code);
                    stockMgr.UpdateStock(s, quantity);
            }
            catch (Exception)
            {
                stockMgr.CreateStock(code, name, quantity);
            }
            finally
            {
                transactionMgr.CreateAddTransactoinLog(code, name, quantity, pricePaid, dateAdded);
            }
        }

        public void TakeFromStock(int code, int quantity, string personName,DateTime dateTaken)
        {
                Stock s = stockMgr.FindStock(code);
            if(s.Quantity < quantity)
            {
                throw new System.Exception("ERROR: Excessive amount taken");
            }
                stockMgr.UpdateStock(s, -quantity);
                transactionMgr.CreateRemoveTransactoinLog(code, s.Name, personName, dateTaken);
        }

        public Dictionary<int, Stock> ViewInventoryReport()
        {
            return stockMgr.Stock;
        }

        public List<Transaction> ViewFinancialReport()
        {
            return transactionMgr.Transaction;
        }

        public List<Transaction> DisplayTransactionLog()
        {
            return transactionMgr.Transaction;
        }

        public List<Transaction> ReportPersonalUsage(string personName)
        {
            return transactionMgr.GetPersonTransactions(personName);
        }
    }

}
