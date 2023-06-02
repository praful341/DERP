using BLL.PropertyClasses.Master.HR;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master.HR
{
    public class HREmployeeMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(HREmployee_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int64);
            Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int64);
            Request.AddParams("@employee_code", pClsProperty.employee_code, DbType.Int64);
            Request.AddParams("@employee_name", pClsProperty.employee_name, DbType.String);
            Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
            Request.AddParams("@adharcard_no", pClsProperty.adharcard_no, DbType.String);
            Request.AddParams("@mobile_no", pClsProperty.mobile_no, DbType.String);
            Request.AddParams("@reference_by", pClsProperty.reference_by, DbType.String);
            Request.AddParams("@reference_adhar_no", pClsProperty.reference_adhar_no, DbType.Int64);
            Request.AddParams("@dob", pClsProperty.DOB, DbType.Date);
            Request.AddParams("@joining_date", pClsProperty.joining_date, DbType.Date);
            Request.AddParams("@leave_date", pClsProperty.leave_date, DbType.Date);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Employee_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.HR_MST_Employee_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Fact_Dept_GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Fact_Dept_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Fact_Dept_Wise_ManagerGetData(HREmployee_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@active", pClsProperty.active_new, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.HR_Fact_Deptwise_Manager_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Fact_Dept_Wise_ManagerGetDataReport(HREmployee_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@fact_department_name", pClsProperty.fact_department_name, DbType.String);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@active", pClsProperty.active_new, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.HR_Fact_Deptwise_ManagerRep_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Dtab_Distint_BookNo(int Year, int Month)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@year", Year, DbType.Int32);
            Request.AddParams("@month", Month, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.HR_Distinct_BookNo_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        //public string ISExistsCode(string EmpCode, Int64 EmpId)
        //{
        //    Validation Val = new Validation();
        //    //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
        //    //{
        //    return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Employee", "employee_code", "AND employee_code = '" + EmpCode + "' AND NOT employee_id =" + EmpId));
        //    //}
        //    //else
        //    //{
        //    //    return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_Employee", "employee_code", "AND employee_code = '" + EmpCode + "' AND NOT employee_id =" + EmpId));
        //    //}
        //}
    }
}
