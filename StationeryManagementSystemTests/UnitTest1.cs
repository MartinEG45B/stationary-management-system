using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationeryManagementSystem;
using System;
using System.Collections.Generic;

namespace StationeryManagementSystemTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ItemCodeReadAdd_UT1()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);

            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            Assert.AreEqual(1, stockMgr.FindStock(1).Code);
        }

        [TestMethod]
        public void ItemNameReadAdd_UT1()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);

            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            Assert.AreEqual("Pen", stockMgr.FindStock(1).Name);
        }

        [TestMethod]
        public void ItemQuantityReadAdd_UT1()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);

            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            Assert.AreEqual(50, stockMgr.FindStock(1).Quantity);
        }

        [TestMethod]
        public void ItemPricePaidReadAdd_UT1()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);

            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            Assert.AreEqual(720, transactoinMgr.Transaction[0].PricePaid);
        }

        [TestMethod]
        public void ItemDateAddedReadAdd_UT1()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            DateTime dateAdded = DateTime.Now;
            empUI.AddToStock(1, "Pen", 50, 720, dateAdded);
            Assert.AreEqual(dateAdded, transactoinMgr.Transaction[0].DateAdded);
        }

        [TestMethod]
        public void ItemCodeReadRemove_UT2()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.TakeFromStock(1, 20, "Euan Martin".ToLower(), DateTime.Now);
            Assert.AreEqual(1, transactoinMgr.Transaction[1].Code);
        }

        [TestMethod]
        public void PersonReadRemove_UT2()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.TakeFromStock(1, 20, "Euan Martin".ToLower(), DateTime.Now);
            Assert.AreEqual("euan martin", transactoinMgr.Transaction[1].PersonName);
        }

        [TestMethod]
        public void ItemQuantityReadRemove_UT2()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.TakeFromStock(1, 20, "Euan Martin".ToLower(), DateTime.Now);
            Assert.AreEqual(20, 50 - stockMgr.FindStock(1).Quantity);
        }

        [TestMethod]
        public void ItemExpenditurePen_UT3()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.AddToStock(2, "Notebook", 10, Convert.ToDecimal(80.25), DateTime.Now);
            empUI.AddToStock(2, "Notebook", 10, Convert.ToDecimal(80.25), DateTime.Now);
            List<Transaction> transactions = empUI.ViewFinancialReport();
            Dictionary<int, decimal> expenditure = CalcExpenditure(transactions);
            Assert.AreEqual(1440, expenditure[1]);
        }
        [TestMethod]
        public void TotalExpenditure_UT3()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.AddToStock(2, "Notebook", 10, Convert.ToDecimal(80.25), DateTime.Now);
            empUI.AddToStock(2, "Notebook", 10, Convert.ToDecimal(80.25), DateTime.Now);
            List<Transaction> transactions = empUI.ViewFinancialReport();
            Dictionary<int, decimal> expenditure = CalcExpenditure(transactions);
            Assert.AreEqual(Convert.ToDecimal(1600.5), expenditure[0]);
        }

        [TestMethod]
        public void ItemExpenditureNotebook_UT3()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.AddToStock(1, "Pen", 50, 720, DateTime.Now);
            empUI.AddToStock(2, "Notebook", 10, Convert.ToDecimal(80.25), DateTime.Now);
            empUI.AddToStock(2, "Notebook", 10, Convert.ToDecimal(80.25), DateTime.Now);
            List<Transaction> transactions = empUI.ViewFinancialReport();
            Dictionary<int, decimal> expenditure = CalcExpenditure(transactions);
            Assert.AreEqual(Convert.ToDecimal(160.5), expenditure[2]);
        }

        [TestMethod]
        public void ItemQuantityBoundary_UT4()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();
            Employee_UI empUI = new Employee_UI(stockMgr, transactoinMgr);
            empUI.AddToStock(1, "Pen", 1, Convert.ToDecimal(7.4), DateTime.Now);
            Assert.AreEqual(1, stockMgr.FindStock(1).Quantity);
        }


        private static Dictionary<int, decimal> CalcExpenditure(List<Transaction> transactions)
        {
            decimal totalMoney = 0;
            Dictionary<int, decimal> eachExpenditure = new Dictionary<int, decimal>();
            for (int index = 0; index < transactions.Count; index++)
            {
                try
                {
                    decimal currExpenditure = eachExpenditure[transactions[index].Code];
                    currExpenditure += transactions[index].PricePaid;
                    eachExpenditure[transactions[index].Code] = currExpenditure;
                    totalMoney += transactions[index].PricePaid;
                }
                catch (KeyNotFoundException)
                {
                    eachExpenditure.Add(transactions[index].Code, transactions[index].PricePaid);
                    totalMoney += transactions[index].PricePaid;
                }
            }
            eachExpenditure.Add(0, totalMoney);
            return eachExpenditure;
        }
    }
}
