using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgLottingOutRateManual
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgLottingOutRateManual_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@out_rate_id", pClsProperty.out_rate_id, DbType.Int64);
            Request.AddParams("@out_rate_date", pClsProperty.out_rate_date, DbType.Date);
            Request.AddParams("@lotting_dept_id", pClsProperty.department_id, DbType.Int64);
            Request.AddParams("@out_rate", pClsProperty.out_rate, DbType.Decimal);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_LottingOutRate_Manual_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_LottingOutRate_Manual_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string GroupName, Int64 Rough_Sieve_ID, Int64 BenchMarkID)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Employee_BenchMark", "group_name", "AND rough_sieve_id = '" + Rough_Sieve_ID + "' AND group_name = '" + GroupName + "' AND NOT benchmark_id =" + BenchMarkID));
        }
    }
}
