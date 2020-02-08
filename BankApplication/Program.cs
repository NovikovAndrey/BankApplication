using System;
using BankLibrary;
namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("SATBank");
            bool active = true;
            while(active)
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1 - Открыть счет.");
                Console.WriteLine("2 - Вывести средства.");
                Console.WriteLine("3 - Пополнить счет.");
                Console.WriteLine("4 - Закрыть счет.");
                Console.WriteLine("5 - Пропустить день.");
                Console.WriteLine("6 - Выйти из программы.");
                Console.WriteLine("Введите номер пункта!");

                Console.ForegroundColor = consoleColor;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            {
                                OpenAccount(bank);
                                break;
                            }
                        case 2:
                            {
                                Withdraw(bank);
                                break;
                            }
                        case 3:
                            {
                                Put(bank);
                                break;
                            }
                        case 4:
                            {
                                CloseAccount(bank);
                                break;
                            }
                        case 5:
                            {
                                break;
                            }
                        case 6:
                            {
                                active = false;
                                break;
                            }
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = consoleColor;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите начальную сумма счета");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета: 1-до востребования; 2 - депозит.");
            AccountType accountType=0;
            int type = Convert.ToInt32(Console.ReadLine());
            switch(type)
            {
                case 1:
                    {
                        accountType = AccountType.Ordinary;
                        break;
                    }
                case 2:
                    {
                        accountType = AccountType.Deposit;
                        break;
                    }
            }
            bank.Open(accountType, sum, AddSumHandler, WithdrawSumHandler, (o, e) => Console.WriteLine(e.Message), CloseAccountHandler, OpenAccountHandler);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
            if (accountEventArgs.Sum > 0)
                Console.WriteLine("Идем тратить деньги");
        }

        private static void AddSumHandler(object sender, AccountEventArgs accountEventArgs)
        {
            Console.WriteLine(accountEventArgs.Message);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумуа для вывода со счета");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Укажите Id счета");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумуа для пополнения счета");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Укажите Id счета");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите Id счета для закрытия");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Close(id);
        }
    }
}
