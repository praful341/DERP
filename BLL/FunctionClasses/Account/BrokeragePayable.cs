using BLL.PropertyClasses.Account;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Account
{
    public class BrokeragePayable
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(Brokerage_PayableProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int64);
                Request.AddParams("@payable", pClsProperty.payable, DbType.Decimal);
                Request.AddParams("@payment_type", pClsProperty.payment_type, DbType.String);
                Request.AddParams("@expense_date", pClsProperty.expense_date, DbType.Date);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@against_ledger_id", pClsProperty.against_ledger_id, DbType.Int64);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.CommandText = BLL.TPV.SProc.TRN_Brokerage_Save;
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

            Request.CommandText = BLL.TPV.SProc.TRN_BrokerPayment_GetData;
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
