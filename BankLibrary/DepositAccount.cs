using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DepositAccount:Account
    {
        public DepositAccount(decimal sum, int percentage):base(sum, percentage)
        {

        }
        protected internal override void Open()
        {
            base.OnOpen(new AccountEventArgs($"Открыт новый депозитный вклад {this.Id}", this.Sum));
        }
        public override void Put(decimal sum)
        {
            if (days % 30 == 0)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs($"Счет можно пополнить только по истечении 30-ти дней", 0));
        }
        public override decimal Withdraw(decimal sum)
        {
            if (days % 30 == 0)
                base.Withdraw(sum);
            else
                base.OnWithdrawed(new AccountEventArgs($"Со счета можно снять только по истечении 30-ти дней", 0));
            return 0;
        }
        protected internal override void Calculate()
        {
            if (days%30 ==0)
                base.Calculate();
        }

    }
}
