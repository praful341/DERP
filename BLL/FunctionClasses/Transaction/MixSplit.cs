using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MixSplit
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public DataTable GetMixStock(int flag = 0, MixSplitProperty pClsProperty = null)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_GetLiveData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
            Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
            Request.AddParams("@flag", flag, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }

        public DataTable GetMixStock_All(int flag = 0, MixSplitProperty pClsProperty = null)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_GetAllData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
            Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
            Request.AddParams("@flag", flag, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetCompanyMemoStock(string p_CompanyMemoNo)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_MemoGetLiveData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_memo_no", p_CompanyMemoNo, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public MixSplitProperty Save(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.AddParams("@from_invoice_id", pClsProperty.from_invoice_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SAVE;
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
                        pClsProperty.mixsplit_srno = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MixSplitProperty SieveWise_Save(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SieveWise_SAVE;
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
                        pClsProperty.mixsplit_srno = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MixSplitProperty Save_Mix(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.AddParams("@from_invoice_id", pClsProperty.from_invoice_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SAVE_Mix;
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
                        pClsProperty.mixsplit_srno = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MixSplitProperty SieveWise_Save_Mix(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SieveWise_SAVE_Mix;
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
                        pClsProperty.mixsplit_srno = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public MixSplitProperty Save_Split(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.AddParams("@from_invoice_id", pClsProperty.from_invoice_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SAVE_Split;
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
                        pClsProperty.mixsplit_srno = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public MixSplitProperty SieveWise_Save_Split(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SieveWise_SAVE_Split;
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
                        pClsProperty.mixsplit_srno = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public int Save_New(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.AddParams("@from_invoice_id", pClsProperty.from_invoice_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SAVE;
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

        public int SieveWise_Save_New(MixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                Request.AddParams("@mixsplit_srno", pClsProperty.mixsplit_srno, DbType.Int64);
                Request.AddParams("@mixsplit_date", pClsProperty.mixsplit_date, DbType.Date);
                Request.AddParams("@mixsplit_time", pClsProperty.mixsplit_time, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@from_sub_sieve_id", pClsProperty.from_sub_sieve_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@to_sub_sieve_id", pClsProperty.to_sub_sieve_id, DbType.Int32);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", pClsProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);
                Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
                Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_memo_no", pClsProperty.company_memo_no, DbType.String);
                Request.AddParams("@party_memo_no", pClsProperty.party_memo_no, DbType.String);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_SieveWise_SAVE;
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

        public DataTable GetMixSplitView(MixSplitProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_ViewData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@from_assort", pClsProperty.from_assort, DbType.String);
            Request.AddParams("@to_assort", pClsProperty.to_assort, DbType.String);
            Request.AddParams("@from_sieve", pClsProperty.from_sieve, DbType.String);
            Request.AddParams("@to_sieve", pClsProperty.To_sieve, DbType.String);
            Request.AddParams("@mixsplit_fromdate", pClsProperty.mixsplit_fromdate, DbType.Date);
            Request.AddParams("@mixsplit_todate", pClsProperty.mixsplit_todate, DbType.Date);
            Request.AddParams("@issue_type_id", pClsProperty.mixsplit_type_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public DataTable GetSlipNo(string SlipNo)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_SlipNo_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@slip_no", SlipNo, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);            
            return DTab;
        }

        public int Update_Invoice_No(MixSplitProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);
            Request.AddParams("@from_invoice_id", pClsProperty.from_invoice_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.TRN_MixSplit_Update_Invoice_No;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
    }
}
