using System;

namespace BLL.PropertyClasses.Account
{
    public class ExpenseEntry_MasterProperty
    {
        public Int64 expense_id { get; set; }
        public int purchase_id { get; set; }
        public int invoice_id { get; set; }
        public int bt_id { get; set; }
        public string expense_date { get; set; }
        public Int64 bank_id { get; set; }
        public Int64 head_id { get; set; }
        public Int64 loan_id { get; set; }
        public int currency_id { get; set; }
        public decimal exchange_rate { get; set; }
        public Int64 ledger_id { get; set; }
        public Int64 to_ledger_id { get; set; }
        public string transaction_type { get; set; }
        public string to_transaction_type { get; set; }
        public decimal amount { get; set; }
        public string remarks { get; set; }
        public string special_remarks { get; set; }
        public Int64 against_ledger_id { get; set; }
        public Int64 from_against_ledger_id { get; set; }
        public Int64 to_against_ledger_id { get; set; }
        public decimal payable { get; set; }
        public int form_id { get; set; }
    }
}
