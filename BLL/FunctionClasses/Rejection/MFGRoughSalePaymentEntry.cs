using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRoughSalePaymentEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRejectionSale_PaymentProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_Rejection_Sale_Payment_Save;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@payment_id", pClsProperty.payment_id, DbType.Int32);
                Request.AddParams("@sale_id", pClsProperty.sale_id, DbType.Int32);
                Request.AddParams("@slip_no", pClsProperty.slip_no, DbType.String);
                Request.AddParams("@date", pClsProperty.date, DbType.Date);
                Request.AddParams("@rejection_party_id", pClsProperty.rejection_party_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@sale_date", pClsProperty.sale_date, DbType.Date);
                Request.AddParams("@total_payment", pClsProperty.total_payment, DbType.Decimal);
                Request.AddParams("@total_receive", pClsProperty.total_receive, DbType.Decimal);
                Request.AddParams("@pending_amount", pClsProperty.pending_amount, DbType.Decimal);
                Request.AddParams("@pay_amount", pClsProperty.pay_amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int64);
                Request.AddParams("@payment_type", pClsProperty.payment_type, DbType.String);

                IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSaleData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Rejection_Sale_Payment_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSaleDataEntry(Int64 Rejection_Party_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Get_SaleNo;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@rejection_party_id", Rejection_Party_ID, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetPaymentList()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Rejection_Sale_Payment_List_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int FindNewSrNo()
        {
            int IntSrnoA = 0;
            IntSrnoA = Ope.FindNewID(DBConnections.ConnectionString, DBConnections.ProviderName, "MFG_TRN_Rough_Payment", "MAX(sr_no)", "");
            return IntSrnoA;
        }
        public int Delete(int Id)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.AddParams("@payment_id", Id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Payment_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
    }
}
