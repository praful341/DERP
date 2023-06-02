using DLL;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MfgCutWiseView
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();
        public DataTable GetData(int CutId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_Trn_Sawable_GetData;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@cut_id", CutId, DbType.Int32, ParameterDirection.Input);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetShowData(int cutId, string clarityId, string roughSieve)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Cut_View_List_GetData;
            Request.AddParams("@cut_id", cutId, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@clarity_id", clarityId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@sieve_id", roughSieve, DbType.String, ParameterDirection.Input);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetIssuePendingData(string KapanID, string cutId, string From_Date, string To_Date)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_ProcessIssue_Pending_Summary;
            Request.AddParams("@company_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@branch_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@location_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@department_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@cut_id", cutId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@kapan_id", KapanID, DbType.String, ParameterDirection.Input);
            Request.AddParams("@fromDate", From_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@toDate", To_Date, DbType.Date, ParameterDirection.Input);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetShineIssueRecieveData(string KapanID, string cutId, string From_Date, string To_Date)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_ShineIssue_GetData;
            Request.AddParams("@company_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@branch_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@location_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@department_id", 0, DbType.String, ParameterDirection.Input);
            Request.AddParams("@cut_id", cutId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@kapan_id", KapanID, DbType.String, ParameterDirection.Input);
            Request.AddParams("@fromDate", From_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@toDate", To_Date, DbType.Date, ParameterDirection.Input);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetCutWiseShadeView(int cutId, string clarityId, string purityId, string quality_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_RPT_Shadewise_View;
            Request.AddParams("@cut_id", cutId, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@clarity_id", clarityId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@purity_id", purityId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@quality_id", quality_id, DbType.String, ParameterDirection.Input);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetCutWiseLSAssortView(int cutId, string clarityId, string purityId, string quality_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_RPT_LSAssort_Final_View;
            Request.AddParams("@cut_id", cutId, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@clarity_id", clarityId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@purity_id", purityId, DbType.String, ParameterDirection.Input);
            Request.AddParams("@quality_id", quality_id, DbType.String, ParameterDirection.Input);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetCutSplitData(int cutId, string QualityId, string SieveId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Receive_Split_GetData;
            Request.AddParams("@cut_id", cutId, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@quality_id", QualityId, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@sieve_id", SieveId, DbType.Int32, ParameterDirection.Input);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetRoughKapanWise(int KapanId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetKapanWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32, ParameterDirection.Input);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
