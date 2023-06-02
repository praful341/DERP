using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGRejectionLossEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRejectionLoss_EntryProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@rej_loss_id", pClsProperty.rej_loss_id, DbType.Int32);
            Request.AddParams("@date", pClsProperty.date, DbType.String);
            Request.AddParams("@opening_carat", pClsProperty.opening_carat, DbType.Decimal);
            Request.AddParams("@opening_rate", pClsProperty.opening_rate, DbType.Decimal);
            Request.AddParams("@opening_amount", pClsProperty.opening_amount, DbType.Decimal);
            Request.AddParams("@rej_loss_carat", pClsProperty.rej_loss_carat, DbType.Decimal);
            Request.AddParams("@rej_loss_rate", pClsProperty.rej_loss_rate, DbType.Decimal);
            Request.AddParams("@rej_loss_amount", pClsProperty.rej_loss_amount, DbType.Decimal);
            Request.AddParams("@rough_loss_carat", pClsProperty.rough_loss_carat, DbType.Decimal);
            Request.AddParams("@rough_loss_rate", pClsProperty.rough_loss_rate, DbType.Decimal);
            Request.AddParams("@rough_loss_amount", pClsProperty.rough_loss_amount, DbType.Decimal);
            Request.AddParams("@lotting_loss_carat", pClsProperty.lotting_loss_carat, DbType.Decimal);
            Request.AddParams("@lotting_loss_rate", pClsProperty.lotting_loss_rate, DbType.Decimal);
            Request.AddParams("@lotting_loss_amount", pClsProperty.lotting_loss_amount, DbType.Decimal);
            Request.AddParams("@lotting_rej_carat", pClsProperty.lotting_rej_carat, DbType.Decimal);
            Request.AddParams("@lotting_rej_rate", pClsProperty.lotting_rej_rate, DbType.Decimal);
            Request.AddParams("@lotting_rej_amount", pClsProperty.lotting_rej_amount, DbType.Decimal);
            Request.AddParams("@niravbhai_rej_carat", pClsProperty.niravbhai_rej_carat, DbType.Decimal);
            Request.AddParams("@niravbhai_rej_rate", pClsProperty.niravbhai_rej_rate, DbType.Decimal);
            Request.AddParams("@niravbhai_rej_amount", pClsProperty.niravbhai_rej_amount, DbType.Decimal);
            Request.AddParams("@labour", pClsProperty.labour, DbType.Decimal);
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.AddParams("@inward_carat", pClsProperty.inward_carat, DbType.Decimal);
            Request.AddParams("@inward_rate", pClsProperty.inward_rate, DbType.Decimal);
            Request.AddParams("@inward_amount", pClsProperty.inward_amount, DbType.Decimal);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionLossEntry_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(string FromDate, string ToDate, int KapanId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.AddParams("@fromdate", FromDate, DbType.Date);
            Request.AddParams("@todate", ToDate, DbType.Date);
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionLossEntry_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Delete(int Id)
        {
            Request Request = new Request();
            Request.AddParams("@loss_id", Id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejectionLossEntry_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
    }
}
