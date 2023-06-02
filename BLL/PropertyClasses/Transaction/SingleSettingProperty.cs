namespace BLL.PropertyClasses.Transaction
{
    public class Single_SettingProperty
    {
        public int form_id { get; set; }
        public int role_id { get; set; }
        public int field_no { get; set; }
        public string column_name { get; set; }
        public string caption { get; set; }
        public int is_visible { get; set; }
        public int is_compulsory { get; set; }
        public int tab_index { get; set; }
        public int is_editable { get; set; }
        public int is_newrow { get; set; }
        public int is_control { get; set; }
        public string column_type { get; set; }
        public string gridname { get; set; }
    }
}
