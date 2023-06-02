using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgMachineMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgMachine_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@machine_id", pClsProperty.machine_id, DbType.Int32);
            Request.AddParams("@machine_name", pClsProperty.machine_name, DbType.String);
            Request.AddParams("@manual_code", pClsProperty.manual_code, DbType.String);
            Request.AddParams("@model_no", pClsProperty.model_no, DbType.String);
            Request.AddParams("@serial_no", pClsProperty.serial_no, DbType.String);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@team_id", pClsProperty.team_id, DbType.Int32);
            Request.AddParams("@group_id", pClsProperty.group_id, DbType.Int32);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@vendor_name", pClsProperty.vendor_name, DbType.String);
            Request.AddParams("@category_name", pClsProperty.category_name, DbType.String);
            Request.AddParams("@machine_type_id", pClsProperty.machine_type_id, DbType.Int32);
            Request.AddParams("@date_acquired", pClsProperty.date_acquired, DbType.Date);
            Request.AddParams("@installation_date", pClsProperty.installation_date, DbType.Date);
            Request.AddParams("@purchase_date", pClsProperty.purchase_date, DbType.Date);
            Request.AddParams("@purchase_rate", pClsProperty.purchase_rate, DbType.Decimal);
            Request.AddParams("@robert_kit_date", pClsProperty.robert_kit_date, DbType.Date);
            Request.AddParams("@depriciable_life", pClsProperty.depriciable_life, DbType.Decimal);
            Request.AddParams("@electricity_per_hrs", pClsProperty.electricity_per_hrs, DbType.Decimal);
            Request.AddParams("@machine_activity", pClsProperty.machine_activity, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_Machine_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public DataTable GetData(int active = 0, int ProcId = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_Machine_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
            Request.AddParams("@process_id", ProcId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public string ISExists(string MacName, Int64 MacId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Machine", "machine_name", "AND machine_name = '" + MacName + "' AND NOT machine_id =" + MacId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MFG_MST_Machine", "machine_name", "AND machine_name = '" + MacName + "' AND NOT machine_id =" + MacId));
            }
        }
    }
}
