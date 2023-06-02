using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class CompanyMemoIssueReceipt
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public int Save(Company_MemoIssueReceiptProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@company_memo_no", pClsProperty.Company_Memo_No, DbType.String);
                Request.AddParams("@company_memo_date", pClsProperty.company_memo_date, DbType.Date);
                Request.AddParams("@party_memo_no", pClsProperty.Party_Memo_No, DbType.String);
                Request.AddParams("@from_company_id", pClsProperty.from_company_id, DbType.Int32);
                Request.AddParams("@from_branch_id", pClsProperty.from_branch_id, DbType.Int32);
                Request.AddParams("@from_location_id", pClsProperty.from_location_id, DbType.Int32);
                Request.AddParams("@from_department_id", pClsProperty.from_department_id, DbType.Int32);
                Request.AddParams("@to_company_id", pClsProperty.to_company_id, DbType.Int32);
                Request.AddParams("@to_branch_id", pClsProperty.to_branch_id, DbType.Int32);
                Request.AddParams("@to_location_id", pClsProperty.to_location_id, DbType.Int32);
                Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int32);
                Request.AddParams("@issue_type_id", pClsProperty.issue_type_id, DbType.Int32);
                Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
                Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
                Request.AddParams("@sub_sieve_id", pClsProperty.sub_sieve_id, DbType.Int32);
                Request.AddParams("@rej_pcs", pClsProperty.rej_pcs, DbType.Int32);
                Request.AddParams("@rej_carat", pClsProperty.rej_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(Val.DBDate(GlobalDec.gStr_SystemDate)), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_Company_MemoIssue_Save;
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

        public DataTable GetCompanyMemoData(int p_numCompany_id, int p_numBranch_id, int p_numLocation_id, int p_numDepartment_id, string p_PartyMemoNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_Company_Memo_GetDetail;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@party_memo_no", p_PartyMemoNo, DbType.String);
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

        public DataTable GetCompanyMemoReceiveData(int p_numCompany_id, int p_numBranch_id, int p_numLocation_id, int p_numDepartment_id, string p_CompanyMemoNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_Company_Memo_Receive_GetDetail;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@company_memo_no", p_CompanyMemoNo, DbType.String);
                Request.AddParams("@company_id", p_numCompany_id, DbType.Int32);
                Request.AddParams("@branch_id", p_numBranch_id, DbType.Int32);
                Request.AddParams("@location_id", p_numLocation_id, DbType.Int32);
                Request.AddParams("@department_id", p_numDepartment_id, DbType.Int32);
                
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);               
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }

        public DataTable GetCompanyMemoCarat(string p_PartyMemoNo, string p_CompanyMemoNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_Company_Memo_GetCarat;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@party_memo_no", p_PartyMemoNo, DbType.String);
                Request.AddParams("@company_memo_no", p_CompanyMemoNo, DbType.String);
                
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
