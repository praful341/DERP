using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGRoughRateUpdate
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public DataTable RoughRate_GetData(int CutId, int KapanId, int ProcessId, string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughRate_GetData;

            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", ProcessId, DbType.Int32);
            Request.AddParams("@type", type, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable RoughRateProcessIssue_GetData(int CutId, int KapanId, int ProcessId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughRateIssue_GetData;

            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", ProcessId, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable MakablePcsProcessIssue_GetData(Int64 KapanId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Makable_Pcs_GetData;

            Request.AddParams("@kapan_id", KapanId, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataSet RoughRateAll_GetData(string Date, string KapanId, string CutId)
        {
            DataSet DS = new DataSet();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Final_RoughRate_GetData;

            // Request.AddParams("@entry_date", Date, DbType.Date);
            Request.AddParams("@kapan_id", KapanId, DbType.String);
            Request.AddParams("@cut_id", CutId, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DS, "", Request);
            return DS;
        }
        public DataTable ChipyoIssue_GetData(int CutId, int KapanId, int ProcessId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_ChipyoIssue_GetData;

            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", ProcessId, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
