namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgProcessGroup_MasterProperty
    {
        public int process_group_id { get; set; }
        public string process_group_name { get; set; }
        public int? sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
