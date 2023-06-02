using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class OpeningStock
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(OpeningStockProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@opening_date", pClsProperty.opening_date, DbType.Int32);
                Request.AddParams("@packet_type_id", 2, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@assort_id", (object)pClsProperty.assort_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@shape_id", (object)pClsProperty.shape_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@color_id", (object)pClsProperty.color_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@purity_id", (object)pClsProperty.purity_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@cut_id", (object)pClsProperty.cut_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@polish_id", (object)pClsProperty.polish_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@symmetry_id", (object)pClsProperty.symmetry_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@flurosence_id", (object)pClsProperty.fluorescence_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@parakhrate", pClsProperty.parakhrate, DbType.Decimal);
                Request.AddParams("@parakhamount", pClsProperty.parakhamount, DbType.Decimal);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@status_id", (object)pClsProperty.status_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_Opening_Stock_Save;
                Request.CommandType = CommandType.StoredProcedure;
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);  
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable Check_Opening_Stock()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_Opening_CheckData;
            Request.CommandType = CommandType.StoredProcedure;            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Opening_GetData(OpeningStockProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@opening_date", pClsProperty.opening_date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_Opening_GetData;
            Request.CommandType = CommandType.StoredProcedure;           
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Check_RateDetail(int AssortId, int SieveId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@assort_id", AssortId, DbType.Int32);
            Request.AddParams("@sieve_id", SieveId, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@active", 1, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.TRN_RateDetail_CheckData;
            Request.CommandType = CommandType.StoredProcedure;           
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
