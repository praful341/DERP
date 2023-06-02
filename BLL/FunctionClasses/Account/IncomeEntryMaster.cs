using BLL.PropertyClasses.Account;
using DLL;
using System;
using System.Data;
namespace BLL.FunctionClasses.Account
{
    public class IncomeEntryMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Other Function

        public int IncomeEntry_Save(IncomeEntry_MasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@income_id", pClsProperty.income_id, DbType.Int64);
                Request.AddParams("@income_date", pClsProperty.income_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int64);
                Request.AddParams("@bank_id", pClsProperty.bank_id, DbType.Int64);
                Request.AddParams("@head_id", pClsProperty.head_id, DbType.Int64);
                Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@invoice_id", pClsProperty.invoice_id, DbType.String);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@against_ledger_id", pClsProperty.against_ledger_id, DbType.Int64);
                Request.AddParams("@loan_id", pClsProperty.loan_id, DbType.Int64);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_Income_Entry_Save;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Income_Entry_GetData_Search()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.TRN_Income_Entry_GetData;
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
        public DataTable GetHead()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.MST_Head_GetData;
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

        public Int64 ISLadgerName_GetData(string pLedger_Name)
        {
            Int64 IntLedgerId = 0;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                IntLedgerId = Val.ToInt64(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_LEDGER", "ledger_id", " And ledger_name = '" + pLedger_Name + "' And location_id = " + GlobalDec.gEmployeeProperty.location_id + ""));
            }
            else
            {
                IntLedgerId = Val.ToInt64(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_LEDGER", "ledger_id", " And ledger_name = '" + pLedger_Name + "' And location_id = " + GlobalDec.gEmployeeProperty.location_id + ""));
            }

            if (IntLedgerId == 0)
            {
                return 0;
            }
            else
            {
                return IntLedgerId;
            }
        }
        public Int64 ISLadgerName_LoanGetData(string pLedger_Name)
        {
            Int64 IntLedgerId = 0;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                IntLedgerId = Val.ToInt64(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_LEDGER", "ledger_id", " And ledger_name = '" + pLedger_Name + "' And location_id = " + GlobalDec.gEmployeeProperty.location_id + ""));
            }
            else
            {
                IntLedgerId = Val.ToInt64(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_LEDGER", "ledger_id", " And ledger_name = '" + pLedger_Name + "' And location_id = " + GlobalDec.gEmployeeProperty.location_id + ""));
            }

            if (IntLedgerId == 0)
            {
                return 0;
            }
            else
            {
                return IntLedgerId;
            }
        }
        #endregion
    }
}
