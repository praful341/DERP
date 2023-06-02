using BLL.PropertyClasses.Master.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.Store
{
    public class StorePartyMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Property Settings

        private DataTable _DTab = new DataTable("Item_Master");

        public DataTable DTab
        {
            get { return _DTab; }
            set { _DTab = value; }
        }

        #endregion

        #region Other Function

        public int SaveStoreManager(StorePartyMasterProperty pClsProperty)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);
                Request.AddParams("@party_name", pClsProperty.party_name, DbType.String);
                Request.AddParams("@mobile_no", pClsProperty.mobile_no, DbType.String);
                Request.AddParams("@address", pClsProperty.address, DbType.String);
                Request.AddParams("@contect_person", pClsProperty.contect_person, DbType.String);


                Request.AddParams("@cst_no", pClsProperty.cst_no, DbType.String);
                Request.AddParams("@gst_no", pClsProperty.gst_no, DbType.String);
                Request.AddParams("@pan_no", pClsProperty.pan_no, DbType.String);
                Request.AddParams("@party_group_id", pClsProperty.party_group_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Store_MST_Party_Master_Save;
                Request.CommandType = CommandType.StoredProcedure;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
                else
                {
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
                }
            }
            catch
            {
                IntRes = 0;
            }
            return IntRes;
        }

        public string ISExists(string PartyName, Int64 PartyID)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "STORE_MST_Party", "party_name", "AND party_name = '" + PartyName + "' AND NOT party_id =" + PartyID));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "STORE_MST_Party", "party_name", "AND party_name = '" + PartyName + "' AND NOT party_id =" + PartyID));
            }
        }

        public DataTable GetData()
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Store_MST_Party_Master_GetData;
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

        public DataTable Party_Group_WiseGetData(string Party_Group)
        {
            Request Request = new Request();

            Request.AddParams("@party_group", Party_Group, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Store_MST_Party_GroupWise_GetData;
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

        #endregion
    }
}
