using BLL.PropertyClasses.Master;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class MfgWagesRateMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgWagesRate_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@wages_rate_id", pClsProperty.wages_rate_id, DbType.Int64);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@wages_type", pClsProperty.wages_type, DbType.String);
            Request.AddParams("@per_carat_rate", pClsProperty.per_carat_rate, DbType.Decimal);
            Request.AddParams("@per_pcs_rate", pClsProperty.per_pcs_rate, DbType.Decimal);
            Request.AddParams("@per_hrs_rate", pClsProperty.per_hrs_rate, DbType.Decimal);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@packet_type_id", pClsProperty.packet_type_id, DbType.Int32);
            Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MFG_MST_Wages_Rate_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_Wages_Rate_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
