using DLL;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGEmployeeHistoryView
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public DataTable EmployeeData(string fromDate, string toDate, string fromTime, string toTime)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_OperatorHistory_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@from_date", fromDate, DbType.Date);
            Request.AddParams("@to_date", toDate, DbType.Date);
            Request.AddParams("@from_time", fromTime, DbType.String);
            Request.AddParams("@to_time", toTime, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
