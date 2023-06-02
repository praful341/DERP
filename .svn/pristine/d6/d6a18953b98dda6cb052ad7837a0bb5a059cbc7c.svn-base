using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRejectionSale
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        private Request AddRejectionSaleSaveParams(MFGRejectionSaleProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@sale_id", pClsProperty.sale_id, DbType.Int64);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
            Request.AddParams("@currency_id", pClsProperty.Currency_ID, DbType.Int64);
            Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
            Request.AddParams("@invoice_no", pClsProperty.invoice_no, DbType.Int64);
            Request.AddParams("@invoice_date", pClsProperty.invoice_date, DbType.Date);
            Request.AddParams("@due_days", pClsProperty.due_days, DbType.Int32);
            Request.AddParams("@due_date", pClsProperty.due_date, DbType.Date);
            Request.AddParams("@rejection_party_id", pClsProperty.rejection_party_id, DbType.Int64);
            Request.AddParams("@rejection_broker_id", pClsProperty.rejection_broker_id, DbType.Int64);
            Request.AddParams("@rejection_broker_name", pClsProperty.rejection_broker_name, DbType.String);
            Request.AddParams("@type", pClsProperty.type, DbType.String);
            Request.AddParams("@total_pcs", pClsProperty.total_pcs, DbType.Decimal);
            Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
            Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.Decimal);
            Request.AddParams("@gross_rate", pClsProperty.gross_rate, DbType.Decimal);
            Request.AddParams("@gross_amount", pClsProperty.gross_amount, DbType.Decimal);
            Request.AddParams("@add_per", pClsProperty.add_per, DbType.Decimal);
            Request.AddParams("@add_amount", pClsProperty.add_amount, DbType.Decimal);
            Request.AddParams("@discount_per", pClsProperty.less_per, DbType.Decimal);
            Request.AddParams("@discount_amount", pClsProperty.less_amount, DbType.Decimal);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@allow_type", pClsProperty.allow_type, DbType.String);

            Request.AddParams("@currency_type", pClsProperty.Currency_Type, DbType.String);
            Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
            Request.AddParams("@sale_type", pClsProperty.sale_type, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Sale_Master_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Request;
        }
        public MFGRejectionSaleProperty Save(MFGRejectionSaleProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                DataTable p_dtbSaleInvoiceNo = new DataTable();
                Request Request = new Request();

                Request = AddRejectionSaleSaveParams(pClsProperty);

                p_dtbSaleInvoiceNo = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbSaleInvoiceNo, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbSaleInvoiceNo, Request);

                if (p_dtbSaleInvoiceNo != null)
                {
                    if (p_dtbSaleInvoiceNo.Rows.Count > 0)
                    {
                        pClsProperty.sale_id = Val.ToInt64(p_dtbSaleInvoiceNo.Rows[0][0]);
                        pClsProperty.union_id = Val.ToInt64(p_dtbSaleInvoiceNo.Rows[0][1]);
                    }
                }
                else
                {
                    pClsProperty.sale_id = 0;
                    pClsProperty.union_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        private Request AddRejectionSaleSaveDetailParams(MFGRejectionSaleProperty pClsProperty)
        {
            Request RequestDetails = new Request();
            RequestDetails.AddParams("@sale_det_id", pClsProperty.sale_detail_id, DbType.Int64);
            RequestDetails.AddParams("@sale_id", pClsProperty.sale_id, DbType.Int64);
            RequestDetails.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int64);
            RequestDetails.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
            RequestDetails.AddParams("@purity_id", pClsProperty.rej_purity_id, DbType.Int64);
            RequestDetails.AddParams("@group_name", pClsProperty.group_name, DbType.String);
            RequestDetails.AddParams("@type", pClsProperty.type, DbType.String);
            RequestDetails.AddParams("@pcs", pClsProperty.pcs, DbType.Decimal);
            RequestDetails.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
            RequestDetails.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            RequestDetails.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            RequestDetails.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            RequestDetails.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            RequestDetails.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            RequestDetails.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            RequestDetails.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
            RequestDetails.AddParams("@old_carat", pClsProperty.old_carat, DbType.Decimal);

            RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Sale_Detail_Save;
            RequestDetails.CommandType = CommandType.StoredProcedure;

            return RequestDetails;
        }
        public MFGRejectionSaleProperty Save_Detail(MFGRejectionSaleProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                DataTable p_dtbSaleInvoiceNo = new DataTable();
                Request RequestDetails = new Request();

                RequestDetails = AddRejectionSaleSaveDetailParams(pClsProperty);

                p_dtbSaleInvoiceNo = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbSaleInvoiceNo, RequestDetails, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbSaleInvoiceNo, RequestDetails);

                if (p_dtbSaleInvoiceNo != null)
                {
                    if (p_dtbSaleInvoiceNo.Rows.Count > 0)
                    {
                        pClsProperty.sr_no = Val.ToInt64(p_dtbSaleInvoiceNo.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.sr_no = 0;
                }

                return pClsProperty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, string invoice_no, int RejectionpartyId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionSale_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@invoice_no", invoice_no, DbType.String);
            Request.AddParams("@rejection_party_id", RejectionpartyId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@allow_type", "A", DbType.String);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetSaleData(string SlipNo)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Get_Rej_SlipNo_Detail;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@slip_no", SlipNo, DbType.String);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetDataDetails(Int64 p_Sale_ID)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionSale_GetDetailData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@sale_id", p_Sale_ID, DbType.Int64);
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }

        public int GetDeleteJanged_ID(MFGRejectionSaleProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Sale_Delete;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@sale_id", pClsProperty.sale_id, DbType.Int64);
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }
        public int GetUpdateJanged_ID(MFGRejectionSaleProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Sale_Update;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@purity_id", pClsProperty.rej_purity_id, DbType.Int64);
            Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }

        public Int64 FindRejPurityID(string PurityName)
        {
            Int64 IntRejPurityID = 0;

            IntRejPurityID = Ope.FindSrNo(DBConnections.ConnectionString, DBConnections.ProviderName, "MFG_MST_Rejection_Purity", "purity_id", " AND purity_name = '" + PurityName + "'");

            return IntRejPurityID;
        }

        public string ISExists(string InvoiceNo, Int64 SaleId)
        {
            string Str = string.Empty;

            Str = Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Rejection_Sale", "invoice_no", "AND invoice_no = '" + InvoiceNo + "' AND NOT sale_id =" + SaleId));

            return Str;
        }
    }
}
