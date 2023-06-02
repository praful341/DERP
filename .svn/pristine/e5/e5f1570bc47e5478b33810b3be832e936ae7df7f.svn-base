using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class FlourescenceMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(Fluorescence_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@fluorescence_id", pClsProperty.fluorescence_id, DbType.Int32);
            Request.AddParams("@fluorescence_name", pClsProperty.fluorescence_name, DbType.String);
            Request.AddParams("@fluorescence_shortname", pClsProperty.fluorescence_shortname, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Flourescence_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Flourescence_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
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
        public string ISExists(string FLName, Int64 FLId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Fluorescence", "fluorescence_name", "AND fluorescence_name = '" + FLName + "' AND NOT fluorescence_id =" + FLId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_Fluorescence", "fluorescence_name", "AND fluorescence_name = '" + FLName + "' AND NOT fluorescence_id =" + FLId));
            }
        }
    }
}
