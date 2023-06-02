using BLL.PropertyClasses.Master.MFG;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgPriceSettingMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgPrice_SettingProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@setting_id", pClsProperty.setting_id, DbType.Int32);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
            Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@setting_name", pClsProperty.setting_name, DbType.String);
            Request.AddParams("@setting_column", pClsProperty.setting_column, DbType.String);
            Request.AddParams("@per", pClsProperty.per, DbType.Decimal);
            Request.AddParams("@days", pClsProperty.days, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_Price_Setting_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_Price_Setting_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
