using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class DeliveryTypeMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(DeliveryTypeMasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@delivery_type_id", pClsProperty.delivery_type_id, DbType.Int64);
            Request.AddParams("@delivery_type", pClsProperty.delivery_type, DbType.String);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Delivery_Type_Save;
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
            Request.CommandText = BLL.TPV.SProc.MST_Delivery_Type_GetData;
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
        public string ISExists(string DeliveryType, Int64 DeliveryID)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Delivery_Type", "delivery_type", "AND delivery_type = '" + DeliveryType + "' AND NOT delivery_type_id =" + DeliveryID));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_Delivery_Type", "delivery_type", "AND delivery_type = '" + DeliveryType + "' AND NOT delivery_type_id =" + DeliveryID));
            }
        }
    }
}
