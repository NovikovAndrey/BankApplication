using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage):base(sum, percentage)
        {

        }
        public override void Open()
        {
            base.OnOpen(new AccountEventArgs($"Открыт новый счет до востребования {this.Id}", this.Sum));
        }
    }
}
