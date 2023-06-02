using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGAssortmentPurchase
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGAssortmentPurchaseProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@assort_purchase_id", pClsProperty.assort_purchase_id, DbType.Int64);
            Request.AddParams("@assort_purchase_date", pClsProperty.assort_purchase_date, DbType.Date);
            Request.AddParams("@assort_location_id", pClsProperty.assort_location_id, DbType.Int64);
            Request.AddParams("@purchase_carat", pClsProperty.purchase_carat, DbType.Decimal);
            Request.AddParams("@net_carat", pClsProperty.net_carat, DbType.Decimal);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@less", pClsProperty.less, DbType.Decimal);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@kapan_cut_no", pClsProperty.kapan_cut_no, DbType.String);
            Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_Assortment_Purchase_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(string FromDate, string ToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Assortment_Purchase_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@fromdate", FromDate, DbType.Date);
            Request.AddParams("@todate", ToDate, DbType.Date);
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetData_Print(string FromDate, string ToDate, string PrintDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Assortment_Purchase_PrintGetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@fromdate", FromDate, DbType.Date);
            Request.AddParams("@todate", ToDate, DbType.Date);
            Request.AddParams("@printdate", PrintDate, DbType.Date);
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetData_Print_All(string FromDate, string ToDate, string PrintDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Assortment_Purchase_PrintGetData_ALL;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@fromdate", FromDate, DbType.Date);
            Request.AddParams("@todate", ToDate, DbType.Date);
            Request.AddParams("@printdate", PrintDate, DbType.Date);
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public int Delete(MFGAssortmentPurchaseProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@assort_purchase_id", pClsProperty.assort_purchase_id, DbType.Int64);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_Assortment_Purchase_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
    }
}
