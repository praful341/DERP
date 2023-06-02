using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGRoughSale
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGRough_SaleProperty Save(MFGRough_SaleProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@invoice_id", pClsProperty.invoice_id, DbType.Int32);
                Request.AddParams("@invoice_no", pClsProperty.invoice_no, DbType.String);
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.String);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@broker_id", pClsProperty.broker_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.total_carat, DbType.String);
                Request.AddParams("@rate", pClsProperty.total_rate, DbType.String);
                Request.AddParams("@amount", pClsProperty.total_amount, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@due_days", pClsProperty.due_days, DbType.Int16);
                Request.AddParams("@brokerage_per", pClsProperty.brokerage_per, DbType.String);
                Request.AddParams("@brokerage_amount", pClsProperty.brokerage_amount, DbType.String);
                Request.AddParams("@discount_per", pClsProperty.discount_per, DbType.String);
                Request.AddParams("@discount_amount", pClsProperty.discount_amount, DbType.String);
                Request.AddParams("@premium_per", pClsProperty.premium_per, DbType.String);
                Request.AddParams("@premium_amount", pClsProperty.premium_amount, DbType.String);
                Request.AddParams("@invoice_date", pClsProperty.invoice_date, DbType.Date);
                Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.String);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughSale_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbSaleInvoiceNo = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbSaleInvoiceNo, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbSaleInvoiceNo, Request);

                if (p_dtbSaleInvoiceNo != null)
                {
                    if (p_dtbSaleInvoiceNo.Rows.Count > 0)
                    {
                        pClsProperty.invoice_no = Val.ToInt(p_dtbSaleInvoiceNo.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.invoice_no = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, string invoice_no, int partyId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughSale_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@invoice_no", invoice_no, DbType.String);
            Request.AddParams("@party_id", partyId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetDataDetails(int p_InvoiceNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughSale_GetDetailData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@invoice_no", p_InvoiceNo, DbType.Int32);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetKapan(int IsSale)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_SaleKapan_GetData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@is_sale", IsSale, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
    }
}
