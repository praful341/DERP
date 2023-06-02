using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class BenchMarkMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(BenchMark_MasterProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@benchmark_id", pClsProperty.benchmark_id, DbType.Int64);
            Request.AddParams("@benchmark_date", pClsProperty.benchmark_date, DbType.Date);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int64);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int64);
            Request.AddParams("@total_pcs", pClsProperty.total_pcs, DbType.Int64);
            Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_BenchMark_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.MST_BenchMark_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public string ISExists(Int64 Company_id, Int64 Branch_id, Int64 Location_id, Int64 Department_id, Int64 Process_id, Int64 Sub_Process_id, Int64 BanchMark_ID)
        {
            Validation Val = new Validation();

            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_BenchMark", "company_id", "AND company_id = '" + Company_id + "' AND branch_id = '" + Branch_id + "' AND location_id = '" + Location_id + "' AND department_id = '" + Department_id + "' AND process_id = '" + Process_id + "'AND sub_process_id = '" + Sub_Process_id + "' AND NOT benchmark_id =" + BanchMark_ID));
        }
    }
}
