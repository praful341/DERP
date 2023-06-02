using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MfgRejectionPurityMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRejectionPurity_MasterProperty pClsProperty, Int64 Rej_Purity_ID)
        {
            Request Request = new Request();
            int IntRes = 0;
            Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
            Request.AddParams("@purity_name", pClsProperty.purity_name, DbType.String);
            Request.AddParams("@group_name", pClsProperty.group_name, DbType.String);
            Request.AddParams("@final_rate", pClsProperty.final_rate, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_RejPurity_Save;
            Request.CommandType = CommandType.StoredProcedure;

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            //Request = new Request();
            //Request.AddParams("@purity_id", Rej_Purity_ID, DbType.Int64);
            //Request.AddParams("@purity_name", pClsProperty.purity_name, DbType.String);
            //Request.AddParams("@group_name", pClsProperty.group_name, DbType.String);
            //Request.AddParams("@final_rate", pClsProperty.final_rate, DbType.String);
            //Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            //Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            //Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            //Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            //Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            //Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            //Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            //Request.CommandText = BLL.TPV.SProc.MFG_MST_RejPurity_Save;
            //Request.CommandType = CommandType.StoredProcedure;

            //IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);

            return IntRes;
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_RejPurity_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }

        public string ISExists(string Purity, Int64 PurityId)
        {
            Validation Val = new Validation();

            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Rejection_Purity", "purity_name", "AND purity_name = '" + Purity + "' AND NOT purity_id =" + PurityId));
        }

        public Int64 FindRejPurityID(string PurityName)
        {
            Int64 IntRejPurityID = 0;

            IntRejPurityID = Ope.FindSrNo(DBConnections.ConnectionString, DBConnections.ProviderName, "MFG_MST_Rejection_Purity", "purity_id", " AND purity_name = '" + PurityName + "'");
            return IntRejPurityID;
        }
    }
}
