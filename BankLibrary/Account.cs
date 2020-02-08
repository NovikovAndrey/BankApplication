using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountSateHandler Withdrawed;
        protected internal event AccountSateHandler Added;
        protected internal event AccountSateHandler Opened;
        protected internal event AccountSateHandler Closed;
        protected internal event AccountSateHandler Calculated;
        static int counter = 0;
        protected int days = 0;

        public decimal Sum { get; private set; }
        public int Percentage { get; private set; }
        public int Id { get; private set; }

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }
        private void CallEvent(AccountEventArgs accountEventArgs, AccountSateHandler accountSateHandler)
        {
            if (accountEventArgs != null)
                accountSateHandler?.Invoke(this, accountEventArgs);
        }
        protected virtual void OnOpen(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Withdrawed);
        }

        protected virtual void OnAdded(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Added);
        }
        protected virtual void OnClosed(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs accountEventArgs)
        {
            CallEvent(accountEventArgs, Calculated);
        }

        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs($"На счет поступило {sum}", sum));
        }

        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum>=sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Снято со счета {sum}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"На балансе {Id} сумма {Sum}, не достаточно для снятия {sum}", sum));
            }
            return result;
        }
        protected internal virtual void Open()
        {
            OnOpen(new AccountEventArgs($"Открытие счета номер {Id}", Sum));
        }
        protected internal virtual void Close()
        {
            if (Sum > 0)
                Withdraw(Sum);
            OnClosed(new AccountEventArgs($"Произошло закрытие счета номер {Id}", 0));
        }
        protected internal void IncrementDays()
        {
            days++;
        }
        protected internal virtual void Calculate()
        {
            Sum += (Sum * Percentage / 100);
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере {Math.Round(Sum * Percentage / 100, 2)}", Math.Round(Sum * Percentage / 100, 2)));
        }
    }
}
