using System;
using System.Collections.Generic;

namespace StationeryManagementSystem
{
    class Program
    {
        private static Employee_UI empUI;
        static void Main(string[] args)
        {
            InitialiseData();
            DisplayMenu();
            int choice = GetMenuChoice();

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                        AddToStock();
                        break;
                    case 2:
                        TakeFromStock();
                        break;
                    case 3:
                        ViewInventoryReport();
                        break;
                    case 4:
                        ViewFinancialReport();
                        break;
                    case 5:
                        DisplayTransactionLog();
                        break;
                    case 6:
                        ReportPersonalUsage();
                        break;
                }
                DisplayMenu();
                choice = GetMenuChoice();
            }

        }

        private static void InitialiseData()
        {
            StockManager stockMgr = new StockManager();
            TransactionManager transactoinMgr = new TransactionManager();

            empUI = new Employee_UI(stockMgr, transactoinMgr);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n1. Add to stock");
            Console.WriteLine("2. Take from stock");
            Console.WriteLine("3. View inventory report");
            Console.WriteLine("4. View financial report");
            Console.WriteLine("5. Display transaction log");
            Console.WriteLine("6. Repeort personal usage");
            Console.WriteLine("7. Exit");
        }

        private static int GetMenuChoice()
        {
            int option = ReadInteger("\nOption");
            while (option < 1 || option > 7)
            {
                Console.WriteLine("\nChoice not recognised. Please try again");
                option = ReadInteger("\nOption");
            }
            return option;
        }

        private static int ReadInteger(string prompt)
        {
            try
            {
                Console.Write(prompt + ": > ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private static decimal ReadPrice(string prompt)
        {
            decimal price;
            Console.Write(prompt + ": >£ ");
            try
            {
                string priceString = Console.ReadLine();
                price = Convert.ToDecimal(priceString);
                int indexOfFullStop = priceString.IndexOf(".");
                if (indexOfFullStop != -1)
                {
                    int lengthOfPrice = priceString.Length;

                    if (lengthOfPrice - 3 != indexOfFullStop && lengthOfPrice - 3 != indexOfFullStop - 1)
                    {
                        throw new System.Exception("ERROR: Not a valid price");
                    } else if (lengthOfPrice - 3 == indexOfFullStop - 1)
                    {
                        price = Convert.ToDecimal(price.ToString("N2"));
                    }
                }
                else
                {
                    price = Convert.ToDecimal(price.ToString("N2"));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("\nNot a valid price. Please try again");
                decimal properPrice = Convert.ToDecimal(ReadPrice("Stock price paid").ToString("N2"));
                return properPrice;
            }
            return price;
        }


        private static int intValidator(int intToValidate, string prompt) { 
            while(intToValidate < 1)
            {
                Console.WriteLine("\nNot a valid {0}. Please try again", prompt.ToLower());
                intToValidate = ReadInteger($"\n{prompt}");
            }
            return intToValidate;
        }

        private static void AddToStock()
        {
            int stockCode = ReadInteger("\nStock code");
            stockCode = intValidator(stockCode, "Stock code");

            Console.Write("Stock name: >");
            string stockName = Console.ReadLine();
            while (stockName.Length == 0)
            {
                Console.WriteLine("\nNot a valid stock name. Please try again");
                Console.Write("Stock name: >"); 
                stockName = Console.ReadLine();
            }
            int stockQuantity = ReadInteger("Stock quantity");
            stockQuantity = intValidator(stockQuantity, "Stock quantity");
   
            decimal stockPricePaid = ReadPrice("Stock price paid");
            empUI.AddToStock(stockCode, stockName.ToLower(), stockQuantity, stockPricePaid, DateTime.Now);
            Console.WriteLine("\nAdded to stock");
        }

        private static void TakeFromStock()
        {
            int stockCode = ReadInteger("\nStock code");
            stockCode = intValidator(stockCode, "Stock code");
            int stockQuantity = ReadInteger("Stock quantity");
            stockQuantity = intValidator(stockQuantity, "Stock quantity");
            string personName = ReadPersonName("Person name");
            try
            {
                empUI.TakeFromStock(stockCode, stockQuantity, personName.ToLower(), DateTime.Now);
                Console.WriteLine("\nStock taken");

            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }
        }

        private static void ViewInventoryReport()
        {

            Dictionary<int, Stock> inventory = empUI.ViewInventoryReport();
            if (inventory.Count == 0)
            {
                Console.WriteLine("\nNothing in stock");
            }
            else
            {
                Console.WriteLine("\nInventory report");
                Console.WriteLine("\n\t{0, -5} {1, -15} {2}", "Code", "Name", "Quantity");
                foreach (Stock s in inventory.Values)
                {

                    DisplayStock(s);
                }
            }

        }
        private static void DisplayStock(Stock s)
        {
            Console.WriteLine("\t{0, -5} {1, -15} {2}",
                s.Code,
                CapitaliseName(s.Name),
                s.Quantity);
        }

        private static void ViewFinancialReport()
        {
            List<Transaction> transactions = empUI.ViewFinancialReport();
            if (transactions.Count == 0)
            {
                Console.WriteLine("\nNo money spent");
            }
            else
            {
                Console.WriteLine("\nFinancial report");
                Console.WriteLine("\n\t{0, -5}   {1}", "Code", "Expenditure");
                Dictionary<int, decimal> expenditure = CalcExpenditure(transactions);
                foreach (KeyValuePair<int, decimal> exp in expenditure)
                {
                    if (exp.Key != 0)
                    {
                        DisplayFinancialReport(exp);
                    }
                }
                Console.WriteLine("\t{0}", new String('-', 20));
                Console.WriteLine("\t{0, -7} £{1}", "Total", expenditure[0]);
            }
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

        private static void DisplayFinancialReport(KeyValuePair<int, decimal> exp)
        {
            Console.WriteLine("\t{0, -5}   £{1}", exp.Key,exp.Value);
        }

        private static void DisplayTransactionLog()
        {
            List<Transaction> transactions = empUI.DisplayTransactionLog();
            if (transactions.Count == 0)
            {
                Console.WriteLine("\nNo transaction logs");
            }
            else
            {
                Console.WriteLine("\n{0, 42}", "Transaction logs");
                foreach (Transaction t in transactions)
                {
                    ShowTransactionLog(t);
                }
            }
            
        }

        private static void ShowTransactionLog(Transaction t)
        {   
            DateTime dateAdded = t.DateAdded;
            int code = t.Code;
            string name = t.Name;
            if (!(dateAdded == DateTime.MinValue))
            {
                Console.WriteLine("{0}", new String('-', 68));
                Console.WriteLine("Type: Add (Date Added: {0})", dateAdded.ToString("dd/MM/yyyy HH:mm:ss"));
                Console.WriteLine("\tCode: {0,-5} Name: {1,-15}  Price paid : £{2}", code, CapitaliseName(name), t.PricePaid);
            }
            else
            {
                Console.WriteLine("{0}", new String('-', 68));
                Console.WriteLine("Type: Remove (Date Removed: {0})", t.DateTaken.ToString("dd/MM/yyyy HH:mm:ss"));
                Console.WriteLine("\tCode: {0,-5} Name: {1,-15} Person name : {2}", code, CapitaliseName(name), CapitaliseName(t.PersonName));
            }
        }

        private static void ReportPersonalUsage()
        {
            string personName = ReadPersonName("\nPerson name");
            List<Transaction> transactions = empUI.ReportPersonalUsage(personName.ToLower());
            if (transactions.Count == 0)
            {
                Console.WriteLine("\nNo transaction logs with {0}", CapitaliseName(personName.ToLower()));
            }
            else
            {
                Console.WriteLine("\n{0}'s personal usage", CapitaliseName(personName.ToLower()));
                Console.WriteLine("\n\t{0,-5} {1, -15} {2}", "Code", "Name", "Date taken");
                foreach (Transaction t in transactions)
                {
                    DisplayPersonalUsage(t);
                }
            }
        }

        private static string ReadPersonName(string prompt)
        {
            Console.Write(prompt + ": > ");
            string personName = Console.ReadLine();
            bool nameViolation = false;
            string[] fnameSname = personName.Split(" ");
            if (fnameSname.Length < 2)
            {
                nameViolation = true;
            }
            else
            {
                for (int x = 0; !(nameViolation) && x < fnameSname.Length; x++)
                {
                    string name = fnameSname[x].ToLower();
                    for (int i = 0; !(nameViolation) && i < name.Length; i++)
                    {
                        if (!(name[i] >= 'a' && name[i] <= 'z'))
                        {

                            nameViolation = true;
                        }
                    }

                }

            }
            while (nameViolation)
            {
                Console.WriteLine("\nNot a valid name. Please try again");
                personName = ReadPersonName("Person name");
                nameViolation = false;
            }
            return personName;

        }
            
        private static string CapitaliseName(string fullName)
        {
            string[] fullNameArr = fullName.Split(" ");
            string capitalised = string.Empty;
            for (int index = 0; index < fullNameArr.Length; index++)
            {
                if(index == 0)
                {
                    capitalised += char.ToUpper(fullNameArr[index][0]) + fullNameArr[index].Substring(1);
                }
                else
                {
                    capitalised += " " + char.ToUpper(fullNameArr[index][0]) + fullNameArr[index].Substring(1);
                }
               
            }
            return capitalised;
        }

        private static void DisplayPersonalUsage(Transaction t)
        {
            Console.WriteLine("\t{0,-5} {1, -15} {2}", t.Code, CapitaliseName(t.Name), t.DateTaken.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}
