using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRejectionTransferManual
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        private Request AddRejectionSaleSaveParams(MFGRejectionTransferManualProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int64);
            Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@kapan_carat", pClsProperty.kapan_carat, DbType.Decimal);
            Request.AddParams("@total_pcs", pClsProperty.total_pcs, DbType.Decimal);
            Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
            Request.AddParams("@total_rate", pClsProperty.total_rate, DbType.Decimal);
            Request.AddParams("@total_amount", pClsProperty.total_amount, DbType.Decimal);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Manual_Trf_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Request;
        }
        public MFGRejectionTransferManualProperty Save(MFGRejectionTransferManualProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
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
                        pClsProperty.transfer_id = Val.ToInt64(p_dtbSaleInvoiceNo.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.transfer_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        private Request AddRejectionSaleSaveDetailParams(MFGRejectionTransferManualProperty pClsProperty)
        {
            Request RequestDetails = new Request();
            RequestDetails.AddParams("@transfer_detail_id", pClsProperty.transfer_detail_id, DbType.Int64);
            RequestDetails.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int64);
            RequestDetails.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int64);
            RequestDetails.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
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

            RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Manual_Trf_Detail_Save;
            RequestDetails.CommandType = CommandType.StoredProcedure;

            return RequestDetails;
        }
        public MFGRejectionTransferManualProperty Save_Detail(MFGRejectionTransferManualProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
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
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionManualTrf_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetDataDetails(Int64 p_Transfer_ID)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionManualTrf_GetDetailData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@transfer_id", p_Transfer_ID, DbType.Int64);
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public int GetDeleteJanged_ID(MFGRejectionTransferManualProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Transfer_Manual_Delete;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int64);
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }
        public Int64 FindRejPurityID(string PurityName)
        {
            Int64 IntRejPurityID = 0;

            IntRejPurityID = Ope.FindSrNo(DBConnections.ConnectionString, DBConnections.ProviderName, "MFG_MST_Rejection_Purity", "purity_id", " AND purity_name = '" + PurityName + "'");
            return IntRejPurityID;
        }
        public int DeleteRejectionTransferManual(MFGRejectionTransferManualProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@transfer_detail_id", pClsProperty.transfer_detail_id, DbType.Int64, ParameterDirection.Input);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Manual_OneEntryDelete;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }
    }
}
