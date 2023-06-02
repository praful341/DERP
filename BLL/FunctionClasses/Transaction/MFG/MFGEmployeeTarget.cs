using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGEmployeeTarget
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public int Save(MFGEmployeeTargetProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();

                Request.AddParams("@emp_performance_id", pClsProperty.emp_performance_id, DbType.Int32);
                Request.AddParams("@performance_date", pClsProperty.performance_date, DbType.Date);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@target_days", pClsProperty.target_days, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Employee_Performance_Save;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public int Delete(MFGEmployeeTargetProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();
            int IntRes = 0;
            try
            {
                Request RequestDetails = new Request();

                RequestDetails.AddParams("@performance_date", Val.DBDate(pClsProperty.performance_date), DbType.Date);
                RequestDetails.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                RequestDetails.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                RequestDetails.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                RequestDetails.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                RequestDetails.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                RequestDetails.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);


                RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Employee_Performance_Delete;
                RequestDetails.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;

        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, int p_department_id, int p_process_id, int p_manager_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_EMPTarget_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", Val.DBDate(p_dtpFromDate), DbType.Date);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Date);

            Request.AddParams("@department_id", p_department_id, DbType.Int32);
            Request.AddParams("@process_id", p_process_id, DbType.Int32);
            Request.AddParams("@manager_id", p_manager_id, DbType.Int32);

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
