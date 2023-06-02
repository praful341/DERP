using BLL.PropertyClasses.Account;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Account
{
    public class PartyPayable
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(Party_PayableProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int64);
                Request.AddParams("@invoice_id", pClsProperty.invoice_id, DbType.Int64);
                Request.AddParams("@invoice_detail_id", pClsProperty.invoice_detail_id, DbType.Int64);
                Request.AddParams("@purchase_id", pClsProperty.purchase_id, DbType.Int64);
                Request.AddParams("@purchase_detail_id", pClsProperty.purchase_detail_id, DbType.Int64);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@term_discount_amount", pClsProperty.term_discount_amount, DbType.Decimal);
                Request.AddParams("@term_discount_per", pClsProperty.term_discount_per, DbType.Decimal);
                Request.AddParams("@term_advance_amount", pClsProperty.term_advance_amount, DbType.Decimal);
                Request.AddParams("@payable", pClsProperty.payable, DbType.Decimal);
                Request.AddParams("@payment_type", pClsProperty.payment_type, DbType.String);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@against_ledger_id", pClsProperty.against_ledger_id, DbType.Int64);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@head_id", pClsProperty.head_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_PartyPayment_Save;
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

        public int PurchaseParty_Save(Party_PayableProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int64);
                Request.AddParams("@purchase_id", pClsProperty.purchase_id, DbType.Int64);
                Request.AddParams("@purchase_detail_id", pClsProperty.purchase_detail_id, DbType.Int64);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@payable", pClsProperty.payable, DbType.Decimal);
                Request.AddParams("@payment_type", pClsProperty.payment_type, DbType.String);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@ledger_id_against", pClsProperty.against_ledger_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_PartyPurchasePayment_Save;
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
        public int SaveRemark(Party_PayableProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@income_id", pClsProperty.income_id, DbType.Int64);
                Request.AddParams("@expense_id", pClsProperty.expense_id, DbType.Int64);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_PartyPayment_Remark_Save;
                Request.CommandType = CommandType.StoredProcedure;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
                else
                {
                    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            return 0;
        }

        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_PartyPayment_GetData;
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
        public DataTable PartyPurchase_GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_PartyPurchasePayment_GetData;
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
        public DataTable PartyPurchase_GetDataList(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_PartyPurchase_Payment_GetData;
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
