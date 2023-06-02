namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgMachineItemUsedEntryProperty
    {
        public int item_used_id { set; get; }
        public int machine_number { set; get; }
        public int machine_detail_id { set; get; }
        public int machine_item_id { set; get; }
        public string item_name { set; get; }
        public int machine_company_id { set; get; }
        public string company_name { set; get; }
        public int machine_party_id { set; get; }
        public string party_name { set; get; }
        public int available_qty { set; get; }
        public int qty { set; get; }
        public string srno { set; get; }
        public decimal rate { set; get; }
        public decimal amount { set; get; }
        public string remark { set; get; }
        public string install_date { set; get; }
    }
}
