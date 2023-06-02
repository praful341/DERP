namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgMachineType_MasterProperty
    {
        public int machine_type_id { get; set; }
        public string machine_type_name { get; set; }
        public int? sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
