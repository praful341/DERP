namespace BLL.PropertyClasses.Master.MFG
{
    public class PartyLock_MasterProperty
    {
        public int lock_id { get; set; }
        public int party_id { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public int total_pcs { get; set; }
        public int? company_id { get; set; }
        public int? branch_id { get; set; }
        public int? location_id { get; set; }
        public int? department_id { get; set; }
    }
}
