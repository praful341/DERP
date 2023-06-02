using BLL.PropertyClasses.Master.HR;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.HR
{
    public class HRTransactionEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        //SqlConnection con = new SqlConnection("Data Source=ADMIN-PC;Initial Catalog=TEST1;user id=sa;password=admin@123");

        public HRTransactionEntryProperty Save(HRTransactionEntryProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int64);
                Request.AddParams("@transaction_date", pClsProperty.transaction_date, DbType.Date);
                Request.AddParams("@transaction_time", pClsProperty.transaction_time, DbType.String);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
                Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
                Request.AddParams("@year", pClsProperty.year, DbType.Int32);
                Request.AddParams("@month", pClsProperty.month, DbType.Int32);
                Request.AddParams("@book_no", pClsProperty.book_no, DbType.Int64);
                Request.AddParams("@total_qty", pClsProperty.total_qty, DbType.Int32);
                Request.AddParams("@current_rate", pClsProperty.current_rate, DbType.Decimal);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int64);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.AddParams("@cl_date", pClsProperty.cl_date, DbType.Date);

                Request.CommandText = BLL.TPV.SProc.HR_TRN_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessUnionId = new DataTable();

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessUnionId, Request);

                if (p_dtbProcessUnionId != null)
                {
                    if (p_dtbProcessUnionId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Update(HRTransactionEntryProperty pClsProperty)
        {
            try
            {
                //SqlCommand cmd = new SqlCommand("HR_TRN_Update", con);
                //cmd.CommandType = CommandType.StoredProcedure;


                //cmd.Parameters.AddWithValue("@sr_no", pClsProperty.sr_no);
                //cmd.Parameters.AddWithValue("@employee_id", pClsProperty.employee_id);
                //cmd.Parameters.AddWithValue("@transaction_date", pClsProperty.transaction_date);

                //cmd.Parameters.AddWithValue("@manager_id", pClsProperty.manager_id);
                //cmd.Parameters.AddWithValue("@factory_id", pClsProperty.factory_id);
                //cmd.Parameters.AddWithValue("@fact_department_id", pClsProperty.fact_department_id);
                //cmd.Parameters.AddWithValue("@year", pClsProperty.year);
                //cmd.Parameters.AddWithValue("@month", pClsProperty.month);
                //cmd.Parameters.AddWithValue("@book_no", pClsProperty.book_no);
                //cmd.Parameters.AddWithValue("@total_qty", pClsProperty.total_qty);


                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int64);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int64);
                Request.AddParams("@transaction_date", pClsProperty.transaction_date, DbType.Date);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
                Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
                Request.AddParams("@year", pClsProperty.year, DbType.Int32);
                Request.AddParams("@month", pClsProperty.month, DbType.Int32);
                Request.AddParams("@book_no", pClsProperty.book_no, DbType.Int64);
                Request.AddParams("@total_qty", pClsProperty.total_qty, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.HR_TRN_Update;
                Request.CommandType = CommandType.StoredProcedure;

                //DataTable p_dtbProcessUnionId = new DataTable();

                //con.Open();

                //IntRes = cmd.ExecuteNonQuery();

                //con.Close();

                IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Emp_GetData(HRTransactionEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
            Request.AddParams("@year", pClsProperty.year, DbType.Int32);
            Request.AddParams("@month", pClsProperty.month, DbType.Int32);
            Request.AddParams("@book_no", pClsProperty.book_no, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.HR_TRN_GetEmpData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int FindNewID(int Year, int Month)
        {
            return Ope.FindNewID(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, BLL.TPV.Table.HR_Transaction_Entry, "MAX(book_no)", " AND year = " + Year.ToString() + " AND month = " + Month.ToString());
        }
        public int FindNewID_Search(int Year, int Month)
        {
            return Ope.FindNewID_Search(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, BLL.TPV.Table.HR_Transaction_Entry, "MAX(book_no)", " AND year = " + Year.ToString() + " AND month = " + Month.ToString());
        }
    }
}
