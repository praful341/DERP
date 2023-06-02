using System;

namespace BLL.PropertyClasses.Master.MFG
{
    public class MfgArticle_MasterProperty
    {
        public Int64 article_id { get; set; }
        public string article_name { get; set; }
        public int sequence_no { get; set; }
        public bool? active { get; set; }
        public string remarks { get; set; }
    }
}
