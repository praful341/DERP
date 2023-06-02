using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class BranchTransferConfirm
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Update(BranchTransfer_ConfirmProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@bt_id", pClsProperty.bt_id, DbType.Int64);
                Request.AddParams("@bt_detail_id", pClsProperty.bt_detail_id, DbType.Int64);
                Request.AddParams("@from_company_id", pClsProperty.from_company_id, DbType.Int64);
                Request.AddParams("@from_branch_id", pClsProperty.from_branch_id, DbType.Int64);
                Request.AddParams("@from_location_id", pClsProperty.from_location_id, DbType.Int64);
                Request.AddParams("@from_department_id", pClsProperty.from_department_id, DbType.Int64);
                Request.AddParams("@to_company_id", pClsProperty.to_company_id, DbType.Int64);
                Request.AddParams("@to_branch_id", pClsProperty.to_branch_id, DbType.Int64);
                Request.AddParams("@to_location_id", pClsProperty.to_location_id, DbType.Int64);
                Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int64);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int64);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@discount", pClsProperty.discount, DbType.Decimal);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int64);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int64);
                Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int64);
                Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@current_rate", pClsProperty.current_rate, DbType.Decimal);
                Request.AddParams("@current_amount", pClsProperty.current_amount, DbType.Decimal);

                Request.CommandText = BLL.TPV.SProc.TRN_BT_Confirm_Details;
                Request.CommandType = CommandType.StoredProcedure;               
                    if (Conn != null)
                        return Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@type", "Master", DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.TRN_BT_Confirm;
            Request.CommandType = CommandType.StoredProcedure;
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public DataTable BTGetData(int Bt_Id, string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@bt_id", Bt_Id, DbType.Int32);
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int64);
            Request.AddParams("@ratetype_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.TRN_BT_Confirm;
            Request.CommandType = CommandType.StoredProcedure;
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);          
            return DTab;
        }
    }
}
