using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGLoanEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public DataTable GetDataForSearchNew(MFGLoanEntry_Property pClsProperty) // Add : Haresh : 21-04-2015
        {
            DataTable DTab = new DataTable();

            Request Request = new Request();

            Request.AddParams("@company_id", pClsProperty.Company_Multi, DbType.String);
            Request.AddParams("@branch_id", pClsProperty.Branch_Multi, DbType.String);
            Request.AddParams("@department_id", pClsProperty.Department_Multi, DbType.String);
            Request.AddParams("@location_id", pClsProperty.Location_Multi, DbType.String);
            Request.AddParams("@ledger_id", pClsProperty.Ledger_ID_Multi, DbType.String);

            Request.AddParams("@from_year_month", pClsProperty.FromYearMonth, DbType.Int32);
            Request.AddParams("@to_year_month", pClsProperty.ToYearMonth, DbType.Int32);
            Request.AddParams("@from_date", pClsProperty.FromDate, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.ToDate, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_Loan_Master_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }

        public MFGLoanEntry_Property Save(MFGLoanEntry_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@loan_id", pClsProperty.Loan_ID, DbType.Int64);
                Request.AddParams("@ledger_id", pClsProperty.Ledger_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.Company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.Branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.Location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.Department_id, DbType.Int32);
                Request.AddParams("@fyear", pClsProperty.FYear, DbType.Int32);
                Request.AddParams("@fmonth", pClsProperty.FMonth, DbType.Int32);
                Request.AddParams("@entry_date", pClsProperty.Entry_Date, DbType.Date);
                Request.AddParams("@entry_time", pClsProperty.Entry_Time, DbType.String);
                Request.AddParams("@opening_amount", pClsProperty.Opening_Amount, DbType.Double);
                Request.AddParams("@new_given", pClsProperty.New_Given, DbType.Double);
                Request.AddParams("@new_given_date", pClsProperty.New_Given_Date, DbType.Date);
                Request.AddParams("@recovery_amount", pClsProperty.Recovery_Amount, DbType.Double);
                Request.AddParams("@recovery_date", pClsProperty.Recovery_Date, DbType.Date);
                Request.AddParams("@loss_amount", pClsProperty.Loss_Amount, DbType.Double);
                Request.AddParams("@loss_date", pClsProperty.Loss_Date, DbType.Date);
                Request.AddParams("@deduct_from_salary", pClsProperty.Deduct_From_Salary, DbType.Double);
                Request.AddParams("@with_emi_amount", pClsProperty.With_EMI_Amount, DbType.Double);
                Request.AddParams("@interest_per", pClsProperty.Interest_Per, DbType.Double);
                Request.AddParams("@interest_amount", pClsProperty.Interest_Amount, DbType.Double);
                Request.AddParams("@closing_amount", pClsProperty.Closing_Amount, DbType.Double);
                Request.AddParams("@remark", pClsProperty.Remark, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@is_paid", pClsProperty.IS_Paid, DbType.Int64);
                Request.AddParams("@paid_date", pClsProperty.Paid_Date, DbType.Date);
                Request.AddParams("@yearmonth", pClsProperty.YearMonth, DbType.Int32);
                Request.AddParams("@payment_type", pClsProperty.transaction_type, DbType.String);
                Request.AddParams("@advance_amount", pClsProperty.Advance_Amount, DbType.Double);
                Request.AddParams("@advance_date", pClsProperty.Advance_Date, DbType.Date);
                Request.AddParams("@advance_rec_amount", pClsProperty.Advance_Recovery_Amount, DbType.Double);
                Request.AddParams("@advance_rec_date", pClsProperty.Advance_Recovery_Date, DbType.Date);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);

                Request.CommandText = BLL.TPV.SProc.MFG_MST_Loan_Master_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbLoanId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbLoanId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbLoanId, Request);

                if (p_dtbLoanId != null)
                {
                    if (p_dtbLoanId.Rows.Count > 0)
                    {
                        pClsProperty.Loan_ID = Val.ToInt64(p_dtbLoanId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Loan_ID = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public int Delete(string pStrLoanID, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_MST_Loan_Master_Delete;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@loan_id", pStrLoanID, DbType.String);

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
    }
}
