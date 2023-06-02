using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MFGProcessMapping
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgDepartmentProcessMapping_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@process_mapping_id", pClsProperty.process_mapping_id, DbType.Int32);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@is_default", pClsProperty.is_default, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_Process_Mapping_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_Process_Mapping_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(int CompId, int BranchId, int LocId, int DeptId, int ProcessId, int SubProcessId, Int64 ProcMapId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Process_Mapping", "department_id", "AND company_id = " + CompId + "AND branch_id = " + BranchId + "AND location_id = " + LocId + "AND department_id = " + DeptId + "AND process_id = " + ProcessId + "AND sub_process_id = " + SubProcessId + " AND NOT process_mapping_id =" + ProcMapId));
        }
    }
}
