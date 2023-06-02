using BLL.PropertyClasses.Master.HR;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master.HR
{
    public class HRManagerMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(HRManager_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
            Request.AddParams("@manager_name", pClsProperty.manager_name, DbType.String);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@is_manager", pClsProperty.is_manager, DbType.String);
            Request.AddParams("@adharcard_no", pClsProperty.adharcard_no, DbType.String);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Manager_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.HR_MST_Manager_GetData;
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
