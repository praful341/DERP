using System;

namespace BLL.PropertyClasses.Master.HR
{
    public class HRFactory_MasterProperty
    {
        public Int64 factory_id { get; set; }
        public string factory_name { get; set; }
        public string factory_short_name { get; set; }
        public bool? active { get; set; }
    }
}
