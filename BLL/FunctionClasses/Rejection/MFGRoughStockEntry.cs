using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRoughStockEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRoughStock_EntryProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@kapan_date", pClsProperty.kapan_date, DbType.Date);
            Request.AddParams("@kapan_no", pClsProperty.kapan_no, DbType.String);
            Request.AddParams("@pcs", pClsProperty.pcs, DbType.Decimal);
            Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@type", pClsProperty.type, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughStockEntry_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(string FromDate, string ToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.AddParams("@fromdate", FromDate, DbType.Date);
            Request.AddParams("@todate", ToDate, DbType.Date);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughStockEntry_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Delete(int Id)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.AddParams("@kapan_id", Id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Kapan_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public DataTable Kapan_GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughKapan_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public bool ISExists_Kapan_No(string Kapan_No)
        {
            string p_Kapan_No = string.Empty;

            p_Kapan_No = Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Rejection_Kapan", "kapan_no", " And kapan_no = '" + Kapan_No.ToString() + "' "));

            if (p_Kapan_No == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
