using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class CurrencyMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(Currency_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
            Request.AddParams("@currency", pClsProperty.currency, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Currency_Save;
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
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Currency_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
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
        public DataTable GetCurrencyID(string p_strCurrencyType)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Currency_GetID;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@CurrencyType", p_strCurrencyType, DbType.String);
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
        public DataTable GetCurrencyRate(int p_currency_Id, int p_Secondary_Currency_Id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Currency_GetRate;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Currency_Id", p_currency_Id, DbType.Int32);
            Request.AddParams("@Secondary_Id", p_Secondary_Currency_Id, DbType.Int32);
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
        public string ISExists(string Curr, Int64 CurrId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Currency", "currency", "AND currency = '" + Curr + "' AND NOT currency_id =" + CurrId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_Currency", "currency", "AND currency = '" + Curr + "' AND NOT currency_id =" + CurrId));
            }
        }
    }
}
