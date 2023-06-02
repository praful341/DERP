using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MemoInvoice
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public Memo_InvoiceProperty Save(Memo_InvoiceProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@memo_id", pClsProperty.memo_id, DbType.Int32);
                Request.AddParams("@memo_master_id", pClsProperty.memo_master_id, DbType.Int32);
                Request.AddParams("@memo_no", pClsProperty.memo_no, DbType.String);
                Request.AddParams("@memo_date", pClsProperty.memo_date, DbType.Date);
                Request.AddParams("@party_id", pClsProperty.Party_Id, DbType.Int32);
                Request.AddParams("@broker_id", pClsProperty.Broker_Id, DbType.Int32);
                Request.AddParams("@Brokerage_Per", pClsProperty.Brokerage_Per, DbType.Decimal);
                Request.AddParams("@Brokerage_Amt", pClsProperty.Brokerage_Amt, DbType.Decimal);
                Request.AddParams("@delivery_type_id", pClsProperty.delivery_type_id, DbType.Int32);
                Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
                Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
                Request.AddParams("@sub_sieve_id", pClsProperty.sub_sieve_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.Pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.Special_Remark, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.Client_Remark, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.Payment_Remark, DbType.String);
                Request.AddParams("@type", 1, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@entry_date", Val.DBDate(Val.DBDate(GlobalDec.gStr_SystemDate)), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@diff_carat", pClsProperty.diff_carat, DbType.Decimal);
                Request.AddParams("@diff_pcs", pClsProperty.diff_pcs, DbType.Int32);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@old_assort_id", pClsProperty.old_assort_id, DbType.Int32);
                Request.AddParams("@old_sieve_id", pClsProperty.old_sieve_id, DbType.Int32);
                Request.AddParams("@old_sub_sieve_id", pClsProperty.old_sub_sieve_id, DbType.Int32);
                Request.AddParams("@current_rate", pClsProperty.current_rate, DbType.Decimal);
                Request.AddParams("@current_amount", pClsProperty.current_amount, DbType.Decimal);
                Request.AddParams("@term_days", pClsProperty.term_days, DbType.Int32);
                Request.AddParams("@due_date", pClsProperty.due_date, DbType.Date);
                Request.AddParams("@final_term_days", pClsProperty.final_days, DbType.Int32);
                Request.AddParams("@final_due_date", pClsProperty.final_due_date, DbType.Date);
                Request.AddParams("@discount_per", pClsProperty.discount_per, DbType.Decimal);
                Request.AddParams("@discount_amt", pClsProperty.discount_amount, DbType.Decimal);
                Request.AddParams("@net_amt", pClsProperty.Net_Amt, DbType.Decimal);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@currency_type", pClsProperty.currency_type, DbType.String);
                Request.AddParams("@seller_id", pClsProperty.seller_id, DbType.Int64);
                Request.AddParams("@purchase_rate", pClsProperty.purchase_rate, DbType.Decimal);
                Request.AddParams("@purchase_amount", pClsProperty.purchase_amount, DbType.Decimal);
                Request.AddParams("@rej_pcs", pClsProperty.rej_pcs, DbType.Int32);
                Request.AddParams("@rej_per", pClsProperty.rej_per, DbType.Decimal);
                Request.AddParams("@rej_carat", pClsProperty.rej_carat, DbType.Decimal);
                Request.AddParams("@inspection_master_id", pClsProperty.inspection_master_id, DbType.Int32);
                Request.AddParams("@status_days", pClsProperty.status_days, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.TRN_MemoIssue_Save;
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
                        pClsProperty.memo_no = Val.ToString(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.memo_no = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, string P_MemoNo, int PartyId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_MemoSearch_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Date);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Date);
            Request.AddParams("@memo_no", P_MemoNo, DbType.String);
            Request.AddParams("@party_id", PartyId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }

        public DataTable GetMemoData(int p_numCompany_id, int p_numBranch_id, int p_numLocation_id, int p_numDepartment_id, string p_memoNo)
        {
            DataTable DTab = new DataTable();

            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_Memo_GetDetail;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@memo_no", p_memoNo, DbType.String);
                Request.AddParams("@Company_id", p_numCompany_id, DbType.Int32);
                Request.AddParams("@Branch_id", p_numBranch_id, DbType.Int32);
                Request.AddParams("@Location_id", p_numLocation_id, DbType.Int32);
                Request.AddParams("@Department_id", p_numDepartment_id, DbType.Int32);
                
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);               
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetInspectionNo()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_InspectionNo_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@Department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
           
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }

        public DataTable GetPrintData(Memo_InvoiceProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@memo_no", pClsProperty.memo_no, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.RPT_TRN_Memo_Print_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);            
            return DTab;
        }
        public Int64 FindMaxMemoMasterID()
        {
            Int64 MemoMasterID = 0;            
                MemoMasterID = Ope.FindNewIDInt64(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "TRN_MemoIssue_Receipt", "MAX(memo_master_id)", "");
           
            return MemoMasterID;
        }
        public DataTable GetData_Party_To_Broker_Map(int Party_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Party_Brokert_MapGetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@party_id", Party_ID, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public DataTable SearchInspData(string insp_no)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Search_MemoInsp_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@inspection_no", insp_no, DbType.String);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);            
            return DTab;
        }
        public Int64 FindReturnMemoID(string strMemoNo)
        {
            Int64 CntID = 0;
                CntID = Ope.FindNewIDInt64(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "TRN_MemoIssue_Receipt", "memo_no", " And  memo_no = '" + strMemoNo + "' And issue_type_id = 2 And company_id = " + GlobalDec.gEmployeeProperty.company_id + " And branch_id = " + GlobalDec.gEmployeeProperty.branch_id + " And location_id = " + GlobalDec.gEmployeeProperty.location_id);
            
            return CntID;
        }
        public bool ISAlredySaveInspData(int p_InspMasterID)
        {
            Int64 IntInvNo = 0;
                IntInvNo = Val.ToInt64(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "TRN_MemoIssue_Receipt", "inspection_master_id", " And inspection_master_id = '" + p_InspMasterID + "' And location_id = " + GlobalDec.gEmployeeProperty.location_id + ""));
            
            if (IntInvNo == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public string ISExists(string MemoNo)
        {
            Validation Val = new Validation();
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "TRN_MemoIssue_Receipt", "memo_no", "AND memo_no = '" + MemoNo + "'"));           
        }
        public Memo_InvoiceProperty Memo_Issue_Delete(Memo_InvoiceProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@memo_no", pClsProperty.memo_no, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_MemoIssue_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbMasterId = new DataTable();

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);

                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.memo_no = Val.ToString(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.memo_no = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
    }
}
