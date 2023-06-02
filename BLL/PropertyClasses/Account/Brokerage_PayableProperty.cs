using System;

namespace BLL.PropertyClasses.Account
{
    public class Brokerage_PayableProperty
    {
        public int ledger_id { get; set; }
        public decimal payable { get; set; }
        public string expense_date { get; set; }
        public string payment_type { get; set; }
        public string remarks { get; set; }
        public decimal exchange_rate { get; set; }
        public Int64 against_ledger_id { get; set; }
        public Int64 form_id { get; set; }
    }
}
