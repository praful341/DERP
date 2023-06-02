using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MfgTrnPredictionView
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();
        public DataTable GetPrediction(int cutId, Int64 lotId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PredictionView_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", lotId, DbType.Int64);
            Request.AddParams("@cut_id", cutId, DbType.String);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
