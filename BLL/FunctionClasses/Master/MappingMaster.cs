using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;
using System.Windows.Forms;

namespace BLL.FunctionClasses.Master
{
    public class MappingMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(Mapping_MasterProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mapping_id", (object)pClsProperty.mapping_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@shape_id", (object)pClsProperty.shape_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@color_id", (object)pClsProperty.color_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@purity_id", (object)pClsProperty.purity_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@cut_id", (object)pClsProperty.cut_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@color_group_id", (object)pClsProperty.color_group_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@assort_id", (object)pClsProperty.assort_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MST_Mapping_Save;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        public int GetData(Mapping_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Mapping_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@shape_id", (object)pClsProperty.shape_id ?? DBNull.Value, DbType.Int32);
            Request.AddParams("@color_id", (object)pClsProperty.color_id ?? DBNull.Value, DbType.Int32);
            Request.AddParams("@clarity_id", (object)pClsProperty.purity_id ?? DBNull.Value, DbType.Int32);
            Request.AddParams("@cut_id", (object)pClsProperty.cut_id ?? DBNull.Value, DbType.Int32);
            Request.AddParams("@color_group_id", (object)pClsProperty.color_group_id ?? DBNull.Value, DbType.Int32);
            Request.AddParams("@assort_id", (object)pClsProperty.assort_id ?? DBNull.Value, DbType.Int32);
            Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int32);
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            if (DTab.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public DataTable GetMappingData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_MappingShow_GetData;
            Request.CommandType = CommandType.StoredProcedure;
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
    }
}
