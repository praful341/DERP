using BLL.PropertyClasses.Master.MFG;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgDepartmentWiseSalary
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgDepartmentWiseSalaryProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@salary_id", pClsProperty.salary_id, DbType.Int64);
            Request.AddParams("@salary_date", pClsProperty.salary_date, DbType.Date);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int64);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@total_salary", pClsProperty.total_salary, DbType.Decimal);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_DepartmentWise_Salary_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_DepartmentWise_Salary_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
    }
}
