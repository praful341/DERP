using System;

namespace BLL.PropertyClasses.Master
{
    public class Sieve_MasterProperty
    {
        public Int64 Sieve_Id { get; set; }
        public string Sieve_Name { get; set; }
        public int Active { get; set; }
        public string Remark { get; set; }
        public double From_Pcs { get; set; }
        public double To_Pcs { get; set; }
        public double From_Carat { get; set; }
        public double To_Carat { get; set; }
        public int Sequence_No { get; set; }
    }
}
