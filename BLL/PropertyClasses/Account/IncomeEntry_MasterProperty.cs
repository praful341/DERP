using System;

namespace BLL.PropertyClasses.Account
{
    public class IncomeEntry_MasterProperty
    {
        public Int64 income_id { get; set; }
        public string income_date { get; set; }
        public Int64 ledger_id { get; set; }
        public Int64 loan_id { get; set; }
        public Int64 bank_id { get; set; }
        public Int64 head_id { get; set; }
        public string transaction_type { get; set; }
        public decimal exchange_rate { get; set; }
        public Int64 currency_id { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public string special_remarks { get; set; }
        public string client_remarks { get; set; }
        public string payment_remarks { get; set; }
        public Int64 invoice_id { get; set; }
        public Int64 against_ledger_id { get; set; }
        public Int64 form_id { get; set; }
    }
}
