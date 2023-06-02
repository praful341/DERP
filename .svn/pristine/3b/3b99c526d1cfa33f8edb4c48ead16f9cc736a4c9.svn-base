using BLL.PropertyClasses.Master;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class SubProcessRateMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(SubProcessRate_MasterProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@rate_id", pClsProperty.rate_id, DbType.Int32);
            Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Mst_Sub_Process_Rate_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Mst_Sub_Process_Rate_Get;
            Request.CommandType = CommandType.StoredProcedure;
            //Request.AddParams("@from_date", active, DbType.Date);
            //Request.AddParams("@to_date", active, DbType.Date);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
    }
}

