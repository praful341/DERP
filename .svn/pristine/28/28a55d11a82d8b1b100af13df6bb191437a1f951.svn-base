using BLL.PropertyClasses.Master.HR;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master.HR
{
    public class HRFactoryMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(HRFactory_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@factory_name", pClsProperty.factory_name, DbType.String);
            Request.AddParams("@factory_short_name", pClsProperty.factory_short_name, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Factory_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Factory_GetData;
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
