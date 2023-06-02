using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class PriceActivation
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Rate_GetData;
            Request.CommandType = CommandType.StoredProcedure;
           
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public int Save_PriceActivate(int Active, Int32 Rate_Id, string Rate_Date, Int32 Rate_Type_Id, Int32 Currency_Id, Int64 Form_Id, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rate_id", Rate_Id, DbType.Int32);
                Request.AddParams("@rate_type_id", Rate_Type_Id, DbType.Int32);
                Request.AddParams("@currency_id", Currency_Id, DbType.Int32);
                Request.AddParams("@active", Active, DbType.String);
                Request.AddParams("@rate_date", Rate_Date, DbType.Date);
                Request.AddParams("@action_by", GlobalDec.gEmployeeProperty.user_name, DbType.String);
                Request.AddParams("@action_user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@action_ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@action_entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@action_entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", Form_Id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_Price_Activation_Save;
                Request.CommandType = CommandType.StoredProcedure;
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);                
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
