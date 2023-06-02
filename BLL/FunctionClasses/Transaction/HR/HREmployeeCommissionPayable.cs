using BLL.PropertyClasses.Master.HR;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.HR
{
    public class HREmployeeCommissionPayable
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(HREmployeeCommissionPayableProperty pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
                Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int64);
                Request.AddParams("@new_employee_amount", pClsProperty.new_employee_amount, DbType.Decimal);
                Request.AddParams("@refrence_employee_amount", pClsProperty.refrence_employee_amount, DbType.Decimal);
                Request.AddParams("@status", pClsProperty.status, DbType.String);
                Request.AddParams("@paid_date", pClsProperty.paid_date, DbType.Date);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);

                Request.CommandText = BLL.TPV.SProc.HR_Employee_CommissionPayable_Save;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable HR_Emp_CommissionPayableGetData(HREmployeeCommissionPayableProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);
            Request.AddParams("@book_no", pClsProperty.book_no, DbType.Int64);
            Request.AddParams("@days", pClsProperty.days, DbType.Int32);
            Request.AddParams("@emp_type", pClsProperty.employee_type, DbType.String);

            Request.CommandText = BLL.TPV.SProc.RPT_HR_Employeewise_Commision;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
