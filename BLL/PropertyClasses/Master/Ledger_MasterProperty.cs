using System;

namespace BLL.PropertyClasses.Master
{
    public class Ledger_MasterProperty
    {
        public int? ledger_id { get; set; }
        public string ledger_name { get; set; }
        public string ledger_shortname { get; set; }
        public int? party_id { get; set; }
        public int? broker_id { get; set; }
        public int? account_type_id { get; set; }
        public int? location_id { get; set; }
        public decimal? crts { get; set; }
        public decimal? adat_per { get; set; }
        public decimal? broker_per { get; set; }
        public string opening_date { get; set; }
        public decimal? opening_balance { get; set; }
        public decimal? actual_opening_balance { get; set; }
        public string reference_by { get; set; }
        public string remarks { get; set; }
        public bool? is_inspection { get; set; }
        public bool? is_adat { get; set; }
        public bool? is_broker { get; set; }
        public bool? is_imp_exp_party { get; set; }
        public bool? factory { get; set; }
        public bool? is_checked { get; set; }
        public bool? is_party_lock_open { get; set; }
        public bool? is_outside { get; set; }
        public bool? is_expense { get; set; }
        public bool? is_pattycash { get; set; }
        public string office_address { get; set; }
        public int? office_country_id { get; set; }
        public int? office_state_id { get; set; }
        public int? office_city_id { get; set; }
        public string office_pincode { get; set; }
        public string office_email_id { get; set; }
        public string office_phone_no { get; set; }
        public string office_fax { get; set; }
        public string factory_address { get; set; }
        public int? factory_country_id { get; set; }
        public int? factory_state_id { get; set; }
        public int? factory_city_id { get; set; }
        public string factory_pincode { get; set; }
        public string factory_email_id { get; set; }
        public string factory_phone_no { get; set; }
        public string factory_fax { get; set; }
        public string contact_name { get; set; }
        public string address { get; set; }
        public int? designation_id { get; set; }
        public string email_id { get; set; }
        public string birth_date { get; set; }
        public string mobile_no { get; set; }
        public string phone_no { get; set; }
        public string account_no { get; set; }
        public string bank_email_id { get; set; }
        public string bank_birth_date { get; set; }
        public string bank_branch_name { get; set; }
        public decimal? bank_transaction_limit { get; set; }
        public string pan_no { get; set; }
        public string tin_vat_no { get; set; }
        public string tin_cst_no { get; set; }
        public string gstin { get; set; }
        public string tan_no { get; set; }
        public string tin_vat_effective_date { get; set; }
        public string tin_cst_effective_date { get; set; }
        public string gstin_effective_date { get; set; }
        public bool? active { get; set; }
        public int? sequence_no { get; set; }
        public string ledger_type { get; set; }
        public Int64 outstanding_limit { get; set; }
    }
}
