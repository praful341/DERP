using BLL.PropertyClasses.Account;
using DLL;
using System.Data;
namespace BLL.FunctionClasses.Account
{
    public class ExpenseEntryMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Other Function

        public int ExpenseEntry_Save(ExpenseEntry_MasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();

            Request.AddParams("@expense_id", pClsProperty.expense_id, DbType.Int64);
            Request.AddParams("@invoice_id", pClsProperty.invoice_id, DbType.Int32);
            Request.AddParams("@purchase_id", pClsProperty.purchase_id, DbType.Int32);
            Request.AddParams("@expense_date", pClsProperty.expense_date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            //Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int32);
            Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@bank_id", pClsProperty.bank_id, DbType.Int32);
            Request.AddParams("@head_id", pClsProperty.head_id, DbType.Int32);
            Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
            Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
            Request.AddParams("@against_ledger_id", pClsProperty.against_ledger_id, DbType.Int64);
            Request.AddParams("@loan_id", pClsProperty.loan_id, DbType.Int64);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.TRN_Expence_Entry_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                if (Conn != null)
                    return Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                else
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                if (Conn != null)
                    return Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                else
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public int BankToCash_Save(ExpenseEntry_MasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();

            Request.AddParams("@expense_date", pClsProperty.expense_date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@from_ledger_id", pClsProperty.ledger_id, DbType.Int32);
            Request.AddParams("@to_ledger_id", pClsProperty.to_ledger_id, DbType.Int32);
            Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);
            Request.AddParams("@to_transaction_type", pClsProperty.to_transaction_type, DbType.String);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@bank_id", pClsProperty.bank_id, DbType.Int32);
            Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
            Request.AddParams("@from_against_ledger_id", pClsProperty.from_against_ledger_id, DbType.Int64);
            Request.AddParams("@to_against_ledger_id", pClsProperty.to_against_ledger_id, DbType.Int64);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.TRN_BankToCash_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                if (Conn != null)
                    return Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                else
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                if (Conn != null)
                    return Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                else
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public int BranchTransfer_Payment_Save(ExpenseEntry_MasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();

            Request.AddParams("@expense_id", pClsProperty.expense_id, DbType.Int64);
            Request.AddParams("@bt_id", pClsProperty.bt_id, DbType.Int32);
            Request.AddParams("@expense_date", pClsProperty.expense_date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);
            Request.AddParams("@amount", pClsProperty.payable, DbType.Decimal);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@against_ledger_id", pClsProperty.against_ledger_id, DbType.Int64);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.TRN_Branch_Transfer_Payment_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                if (Conn != null)
                    return Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                else
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                if (Conn != null)
                    return Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                else
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }

        }
        public DataTable Expense_Entry_GetData_Search()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.MST_Expence_Entry_GetData;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@Department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
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
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@to_company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@to_branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@to_location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@to_department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_Branch_Transfer_Payable;
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
        public DataTable GetBranchPaymentList(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);

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
        public DataTable LedgerBalanceGetData(int ledger_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.TRN_Ledger_Balance_GetData;
            Request.AddParams("@ledger_id", ledger_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
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
        #endregion
    }
}
