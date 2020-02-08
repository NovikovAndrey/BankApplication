using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public delegate void AccountSateHandler(object sender, AccountEventArgs accountEventArgs);

    public class AccountEventArgs
    {
        public string Message { get; private set;}
        public decimal Sum { get; private set; }
        public AccountEventArgs(string mess, decimal sum)
        {
            Message = mess;
            Sum = sum;
        }
    }
}
