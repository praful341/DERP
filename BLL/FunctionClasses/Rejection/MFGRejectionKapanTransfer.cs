using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRejectionKapanTransfer
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRejectionKapanTransferProperty pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int64);
                Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
                Request.AddParams("@from_kapan_id", pClsProperty.from_kapan_id, DbType.Int64);
                Request.AddParams("@to_kapan_id", pClsProperty.to_kapan_id, DbType.Int64);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Decimal);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Kapan_Transfer_Save;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RejKapan_Trf_GetData(MFGRejectionKapanTransferProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rej_Kapan_Trf_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int Delete(Int64 Id)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.AddParams("@transfer_id", Id, DbType.Int64);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Kapan_Transfer_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
    }
}
