using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MfgRejectionRateEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public MFGRejectionRateEntryProperty Save(MFGRejectionRateEntryProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@rate_id", pClsProperty.rate_id, DbType.Int64);
                Request.AddParams("@date", pClsProperty.date, DbType.Date);
                Request.AddParams("@time", pClsProperty.time, DbType.String);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Rate_Entry_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbRejection = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbRejection, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbRejection, Request);

                if (p_dtbRejection != null)
                {
                    if (p_dtbRejection.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbRejection.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Rate_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@active", active, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Rate_Exist_GetData(MFGRejectionRateEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Rate_ExistGetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@date", pClsProperty.date, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable RejEntry_GetData(MFGRejectionRateEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Entry_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetDetail_Data(Int64 Union_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Rate_Detail_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@union_id", Union_ID, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
