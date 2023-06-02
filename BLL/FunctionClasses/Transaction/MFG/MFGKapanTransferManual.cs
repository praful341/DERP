using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGKapanTransferManual
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGKapanTransfer_ManualProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);
            Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
            Request.AddParams("@from_kapan_id", pClsProperty.from_kapan_id, DbType.Int32);
            Request.AddParams("@to_kapan_id", pClsProperty.to_kapan_id, DbType.String);
            Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_Kapan_Transfer_Manual_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(string FromDate, string ToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Kapan_Transfer_Manual_GetData;
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
        public DataTable KapanCarat(int kapanId = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Kapan_Carat_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", kapanId, DbType.Int32);
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
        public int Delete(MFGKapanTransfer_ManualProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_Kapan_Transfer_Manual_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public string ISExists(int FKapan, Int64 TranferId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Kapan_Transfer_Menual", "from_kapan_id", "AND from_kapan_id = '" + FKapan + "' AND NOT transfer_id =" + TranferId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MFG_TRN_Kapan_Transfer_Menual", "from_kapan_id", "AND from_kapan_id = '" + FKapan + "' AND NOT transfer_id =" + TranferId));
            }
        }
    }
}
