using BLL.PropertyClasses.Master.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.Store
{
    public class StoreDivisionMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(StoreDivision_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@division_Id", pClsProperty.division_Id, DbType.Int64);
            Request.AddParams("@division_Name", pClsProperty.division_Name, DbType.String);
            Request.AddParams("@branch_Id", pClsProperty.branch_Id, DbType.Int64);
            Request.AddParams("@department_Id", pClsProperty.department_Id, DbType.Int64);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@active", pClsProperty.Active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.Remark, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.Sequence_No, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Store_MST_Division_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.AddParams("@Active", active, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.Store_MST_Division_GetData;
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
        public string ISExists(string DivisionName, Int64 Branch_ID, Int64 Department_ID, Int64 DivisionId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "STORE_MST_Division", "division_name", "AND division_name = '" + DivisionName + "' AND branch_id = '" + Branch_ID + "' AND department_id = '" + Department_ID + "' AND NOT division_id =" + DivisionId));
        }
    }
}
