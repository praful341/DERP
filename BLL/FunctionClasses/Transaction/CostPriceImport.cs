using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class CostPriceImport
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int MasterSave(CostPrice_ImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@cost_rate_date", pClsProperty.cost_rate_date, DbType.Date);
                Request.AddParams("@rate_type_id", (object)pClsProperty.rate_type_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@action_by", GlobalDec.gEmployeeProperty.user_name, DbType.String);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.CommandText = BLL.TPV.SProc.TRN_CostPrice_Import_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbMasterId = new DataTable();
                
                    if (Conn != null)
                        Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbMasterId, Request, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);
               
                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.cost_rate_id = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.cost_rate_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty.cost_rate_id;
        }

        public int DetailSave(CostPrice_ImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@cost_rate_id", (object)pClsProperty.cost_rate_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@assort_id", (object)pClsProperty.assort_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sequence_no", (object)pClsProperty.sequence_no ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@rate_type_id", (object)pClsProperty.rate_type_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.TRN_CostPrice_Import_Detail_Save;
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
        public DataTable GetData(string p_dtpDate, int rate_type_id, int curr_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_CostPrice_Import_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Date", p_dtpDate, DbType.Int32);
            Request.AddParams("@rate_type_id", rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", curr_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
    }
}
