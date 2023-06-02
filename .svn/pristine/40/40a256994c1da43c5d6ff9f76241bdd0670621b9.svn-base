using BLL.PropertyClasses.Master.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.Store
{
    public class StoreManagerMaster
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

        public int SaveStoreManager(StoreManagerMasterProperty pClsProperty)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@manager_name", pClsProperty.manager_name, DbType.String);
                Request.AddParams("@mobile_no", pClsProperty.mobile_no, DbType.String);
                Request.AddParams("@address", pClsProperty.address, DbType.String);
                Request.AddParams("@salary", pClsProperty.salary, DbType.Decimal);
                Request.AddParams("@city_id", pClsProperty.city_id, DbType.Int64);
                Request.AddParams("@state_id", pClsProperty.state_id, DbType.Int64);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Store_MST_Manager_Master_Save;
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

        public string ISExists(string ManagerName, Int64 ManagerId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "STORE_MST_Manager", "manager_name", "AND manager_name = '" + ManagerName + "' AND NOT manager_id =" + ManagerId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "STORE_MST_Manager", "manager_name", "AND manager_name = '" + ManagerName + "' AND NOT manager_id =" + ManagerId));
            }
        }

        public DataTable GetData()
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Store_MST_Manager_Master_GetData;
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
