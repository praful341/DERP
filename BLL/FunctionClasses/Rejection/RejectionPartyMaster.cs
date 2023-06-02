using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class RejectionPartyMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public DataTable Save(RejectionParty_MasterProperty pClsProperty)
        {
            DataTable p_dtbPartyId = new DataTable();
            try
            {
                Request Request = new Request();

                Request.AddParams("@rejection_party_id", pClsProperty.rejection_party_id, DbType.Int64);
                Request.AddParams("@rejection_party_name", pClsProperty.rejection_party_name, DbType.String);
                Request.AddParams("@rejection_party_shortname", pClsProperty.rejection_party_shortname, DbType.String);
                Request.AddParams("@broker_name", pClsProperty.broker_name, DbType.String);
                Request.AddParams("@category_id", pClsProperty.category_id, DbType.Int64);
                Request.AddParams("@init_name", pClsProperty.init_name, DbType.String);
                Request.AddParams("@first_name", pClsProperty.first_name, DbType.String);
                Request.AddParams("@last_name", pClsProperty.last_name, DbType.String);
                Request.AddParams("@party_type", pClsProperty.party_type, DbType.String);
                Request.AddParams("@business_type", pClsProperty.business_type, DbType.String);
                Request.AddParams("@city_id", pClsProperty.city_id, DbType.Int64);
                Request.AddParams("@state_id", pClsProperty.state_id, DbType.Int32);
                Request.AddParams("@country_id", pClsProperty.country_id, DbType.Int32);
                Request.AddParams("@active", pClsProperty.active, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@pincode", pClsProperty.pincode, DbType.String);
                Request.AddParams("@address", pClsProperty.address, DbType.String);
                Request.AddParams("@phone1", pClsProperty.phone1, DbType.String);
                Request.AddParams("@phone1city", pClsProperty.phone1city, DbType.Int64);
                Request.AddParams("@phone1country", pClsProperty.phone1country, DbType.Int32);
                Request.AddParams("@mobile1", pClsProperty.mobile1, DbType.String);
                Request.AddParams("@mobile1country", pClsProperty.mobile1country, DbType.Int64);
                Request.AddParams("@fax", pClsProperty.fax, DbType.String);
                Request.AddParams("@website", pClsProperty.website, DbType.String);
                Request.AddParams("@primary_email", pClsProperty.primary_email, DbType.String);
                Request.AddParams("@secondary_email", pClsProperty.secondary_email, DbType.String);
                Request.AddParams("@discount", pClsProperty.discount, DbType.String);
                Request.AddParams("@aadhar_no", pClsProperty.aadhar_no, DbType.String);
                Request.AddParams("@pancard_no", pClsProperty.pancard_no, DbType.String);
                Request.AddParams("@registration_source", pClsProperty.registration_source, DbType.String);
                Request.AddParams("@tds_circle", pClsProperty.tds_circle, DbType.String);
                Request.AddParams("@vat_no", pClsProperty.vat_no, DbType.String);
                Request.AddParams("@vat_date", pClsProperty.vat_date, DbType.String);
                Request.AddParams("@gst_no", pClsProperty.gst_no, DbType.String);
                Request.AddParams("@gst_date", pClsProperty.gst_date, DbType.String);
                Request.AddParams("@cst_no", pClsProperty.cst_no, DbType.String);
                Request.AddParams("@cst_date", pClsProperty.cst_date, DbType.String);
                Request.AddParams("@tan_no", pClsProperty.tan_no, DbType.String);
                Request.AddParams("@tan_date", pClsProperty.tan_date, DbType.String);
                Request.AddParams("@service_tax_no", pClsProperty.service_tax_no, DbType.String);
                Request.AddParams("@service_tax_date", pClsProperty.service_tax_date, DbType.String);
                Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.String);
                Request.AddParams("@qbc", pClsProperty.qbc, DbType.String);
                Request.AddParams("@factory", pClsProperty.factory, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@IDProof_ID", pClsProperty.IDProof_ID, DbType.Int32);
                Request.AddParams("@IDProof_No", pClsProperty.IDProof_No, DbType.Int32);
                Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
                Request.AddParams("@is_autoconfirm", pClsProperty.is_autoconfirm, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MST_Rejection_Party_Save;
                Request.CommandType = CommandType.StoredProcedure;

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbPartyId, Request);
                return p_dtbPartyId;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            return p_dtbPartyId;
        }
        public int Update(RejectionParty_MasterProperty pClsProperty)
        {
            Request RequestDetails = new Request();
            RequestDetails.AddParams("@rejection_party_id", pClsProperty.rejection_party_id, DbType.Int64);
            RequestDetails.AddParams("@aadhar_path", pClsProperty.aadhar_path, DbType.String);
            RequestDetails.AddParams("@pancard_path", pClsProperty.pan_path, DbType.String);
            RequestDetails.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            RequestDetails.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            RequestDetails.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            RequestDetails.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            RequestDetails.CommandText = BLL.TPV.SProc.MST_Rejection_Party_Update;
            RequestDetails.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, RequestDetails);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Rejection_Party_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string RejectionParty, Int64 RejectionPartyId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Rejection_Party", "rejection_party_name", "AND rejection_party_name = '" + RejectionParty + "' AND NOT rejection_party_id =" + RejectionPartyId));
        }
        public Int64 FindPartyID(string RejectionPartyName)
        {
            Int64 IntRejectionPartyID = 0;

            IntRejectionPartyID = Ope.FindSrNo(DBConnections.ConnectionString, DBConnections.ProviderName, "MST_Rejection_Party", "rejection_party_id", " AND rejection_party_name = '" + RejectionPartyName + "'");
            return IntRejectionPartyID;
        }
        public string ISExists_Broker(string RejectionBroker, Int64 RejectionPartyId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Rejection_Party", "broker_name", "AND broker_name = '" + RejectionBroker + "' AND NOT rejection_party_id =" + RejectionPartyId));
        }
        public DataTable Broker_GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MST_Rejection_PartyBroker_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Contact_Person_Distinct_GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            
            Request.CommandText = BLL.TPV.SProc.MST_Rejection_Contact_Person_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
