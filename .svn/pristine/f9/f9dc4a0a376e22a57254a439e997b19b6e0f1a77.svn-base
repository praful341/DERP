using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRejectionInternalTransfer
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRejectionInternalTransferProperty pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int64);
                Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
                Request.AddParams("@from_purity_id", pClsProperty.from_purity_id, DbType.Int64);
                Request.AddParams("@to_purity_id", pClsProperty.to_purity_id, DbType.Int64);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Decimal);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
                Request.AddParams("@diff_carat", pClsProperty.diff_carat, DbType.Decimal);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Internal_Transfer_Save;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RejInternal_Trf_GetData(MFGRejectionInternalTransferProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rej_Internal_Trf_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable Rej_Purity_Stock_Data(Int64 Purity_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Purity_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@purity_id", Purity_ID, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int GetDeleteRejectionInternal_Transfer_ID(MFGRejectionInternalTransferProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Internal_Trf_Delete;

            Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);
            Request.AddParams("@from_purity_id", pClsProperty.from_purity_id, DbType.Int64);
            Request.AddParams("@to_purity_id", pClsProperty.to_purity_id, DbType.Int64);
            Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);

            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
    }
}
