namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgProcessSequence_MasterProperty
    {
        public int sequence_id { get; set; }
        public string sequence_name { get; set; }
        public int? process_id { get; set; }
        public int? sub_process_id { get; set; }
        public int? sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
