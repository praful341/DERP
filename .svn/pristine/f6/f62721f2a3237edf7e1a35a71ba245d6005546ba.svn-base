using BLL.PropertyClasses.Account;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGRoughSaleStatus
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRoughSaleStatus_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rough_purchase_id", pClsProperty.rough_purchase_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@invoice_date", pClsProperty.invoice_date, DbType.Date);
                Request.AddParams("@rough_party_id", pClsProperty.rough_party_id, DbType.Int64);
                Request.AddParams("@rough_broker_id", pClsProperty.rough_broker_id, DbType.Int64);
                Request.AddParams("@mfg_carat", pClsProperty.mfg_carat, DbType.Decimal);
                Request.AddParams("@mfg_pending", pClsProperty.mfg_pending, DbType.Decimal);
                Request.AddParams("@mfg_working", pClsProperty.mfg_working, DbType.Decimal);
                Request.AddParams("@assort_sale", pClsProperty.assort_sale, DbType.Decimal);
                Request.AddParams("@assort_pending", pClsProperty.assort_pending, DbType.Decimal);
                Request.AddParams("@assort_working", pClsProperty.assort_working, DbType.Decimal);
                Request.AddParams("@sale_carat", pClsProperty.sale_carat, DbType.Decimal);
                Request.AddParams("@sale_rate", pClsProperty.sale_rate, DbType.Decimal);
                Request.AddParams("@net_rate", pClsProperty.net_rate, DbType.Decimal);
                Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.Decimal);
                Request.AddParams("@cl_carat", pClsProperty.cl_carat, DbType.Decimal);
                Request.AddParams("@terms_days", pClsProperty.term_days, DbType.Int32);
                Request.AddParams("@due_date", pClsProperty.due_date, DbType.Date);
                Request.AddParams("@is_delivery", pClsProperty.is_delivery, DbType.Int32);
                Request.AddParams("@delivery_date", pClsProperty.delivery_date, DbType.Date);
                Request.AddParams("@is_final_dispatch", pClsProperty.is_final_dispatch, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@inr_amount", pClsProperty.inr_amount, DbType.Decimal);
                Request.AddParams("@closing_amount", pClsProperty.closing_amount, DbType.Decimal);

                Request.CommandText = BLL.TPV.SProc.TRN_RoughSale_Status_Save;
                Request.CommandType = CommandType.StoredProcedure;

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
                }
                else
                {
                    if (Conn != null)
                        IntRes = Conn.Inter2.ExecuteNonQuery(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, Request);
                }
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_RoughSaleStatus_GetData;
            Request.CommandType = CommandType.StoredProcedure;

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
    }
}
