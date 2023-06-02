using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class RateTypeMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(RateType_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@ratetype_id", pClsProperty.ratetype_id, DbType.Int32);
            Request.AddParams("@rate_type", pClsProperty.ratetype, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@from_currency_id", pClsProperty.from_currency_id, DbType.Int32);
            Request.AddParams("@to_currency_id", pClsProperty.to_currency_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_RateType_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_RateType_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetRateData(string date, int RateType, int Currency)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Rate_DateWise_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@date", date, DbType.Date);
            Request.AddParams("@rate_type_id", RateType, DbType.Int32);
            Request.AddParams("@currency_id", Currency, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetPartyOpeningData(string date)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Party_Opening_DateWise_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@date", date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string RateType, Int64 RateTypeId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_RateType", "rate_type", "AND rate_type = '" + RateType + "' AND NOT ratetype_id =" + RateTypeId));
        }
        public DataTable GetDate(RateType_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_RateType_GetDate;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rate_type_id", pClsProperty.ratetype_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetDate_Weekly(RateType_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_RateType_Weekly_GetDate;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rate_type_id", pClsProperty.ratetype_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable HR_GetRateDate()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_HR_RateType_GetDate;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetRateDateWiseData(string date, int RateType, int Currency, string Color_Name)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Rate_DateWise_Data;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@date", date, DbType.Date);
            Request.AddParams("@rate_type_id", RateType, DbType.Int32);
            Request.AddParams("@currency_id", Currency, DbType.Int32);
            Request.AddParams("@color_name", Color_Name, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetRangeData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Range_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
    }
}
