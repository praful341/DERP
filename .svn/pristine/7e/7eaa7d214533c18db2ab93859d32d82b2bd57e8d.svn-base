using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class BrokerMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public DataTable Save(Broker_MasterProperty pClsProperty)
        {
            DataTable p_dtbBrokerId = new DataTable();
            try
            {
                Request Request = new Request();

                Request.AddParams("@broker_id", pClsProperty.broker_id, DbType.Int64);
                Request.AddParams("@broker_type", pClsProperty.broker_type, DbType.String);
                Request.AddParams("@broker_name", pClsProperty.broker_name, DbType.String);
                Request.AddParams("@brokerage", pClsProperty.brokerage, DbType.Decimal);
                Request.AddParams("@city_id", pClsProperty.city_id, DbType.Int64);
                Request.AddParams("@state_id", pClsProperty.state_id, DbType.Int32);
                Request.AddParams("@country_id", pClsProperty.country_id, DbType.Int32);
                Request.AddParams("@active", pClsProperty.active, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@address", pClsProperty.address, DbType.String);
                Request.AddParams("@pincode", pClsProperty.pincode, DbType.String);
                Request.AddParams("@phone", pClsProperty.phone, DbType.String);
                Request.AddParams("@mobile", pClsProperty.mobile, DbType.String);
                Request.AddParams("@fax", pClsProperty.fax, DbType.String);
                Request.AddParams("@email", pClsProperty.email, DbType.String);
                Request.AddParams("@aadhar_no", pClsProperty.aadhar_no, DbType.String);
                Request.AddParams("@pancard_no", pClsProperty.pan_no, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MST_Broker_Save;
                Request.CommandType = CommandType.StoredProcedure;

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbBrokerId, Request);
                }
                else
                {
                    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, p_dtbBrokerId, Request);
                }
                return p_dtbBrokerId;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            return p_dtbBrokerId;
        }
        public int Update(Broker_MasterProperty pClsProperty)
        {

            Request RequestDetails = new Request();
            RequestDetails.AddParams("@broker_id", pClsProperty.broker_id, DbType.Int64);
            RequestDetails.AddParams("@aadhar_path", pClsProperty.aadhar_path, DbType.String);
            RequestDetails.AddParams("@pancard_path", pClsProperty.pan_path, DbType.String);
            RequestDetails.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            RequestDetails.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            RequestDetails.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            RequestDetails.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            RequestDetails.CommandText = BLL.TPV.SProc.MST_Broker_Update;
            RequestDetails.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, RequestDetails);
            }
            else
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, RequestDetails);
            }
        }
        public DataTable GetData(int active = 0, int locId = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@Active", active, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Broker_GetData;
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
        public string ISExists(string BrokerName, Int64 BrokerId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Broker", "broker_name", "AND broker_name = '" + BrokerName + "' AND NOT broker_id =" + BrokerId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_Broker", "broker_name", "AND broker_name = '" + BrokerName + "' AND NOT broker_id =" + BrokerId));
            }
        }
    }
}
