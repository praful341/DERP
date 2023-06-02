using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgProfitCenterMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgProfitCenter_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@profit_center_id", pClsProperty.profit_center_id, DbType.Int32);
            Request.AddParams("@profit_center", pClsProperty.profit_center, DbType.String);
            Request.AddParams("@profit_center_type", pClsProperty.profit_center_type, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_ProfitCenter_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_ProfitCenter_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string ProfitCenter, Int64 ProfitCenterId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_ProfitCenter", "profit_center", "AND profit_center = '" + ProfitCenter + "' AND NOT profit_center_id =" + ProfitCenterId));
        }
    }
}
