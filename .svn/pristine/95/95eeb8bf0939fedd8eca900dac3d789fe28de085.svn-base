using BLL.PropertyClasses.Master;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Utility
{
    public class UserAuthentication
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        User_MasterProperty objEmp = new User_MasterProperty();

        #region Property Settings

        private DataTable _DTab = new DataTable(BLL.TPV.Table.User_Authentication);
        public DataTable DTab
        {
            get { return _DTab; }
            set { _DTab = value; }
        }
        public DataTable Get_MENU_FormDetail(int Role_Id)
        {
            Request Request = new Request();
            DataTable _DTable = new DataTable();
            Request.AddParams("@Role_Id", Role_Id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.UserAuth_Menu_Form_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, _DTable, Request);
            //}
            //else
            //{
            //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, _DTable, Request);
            //}
            _DTab = _DTable;
            return _DTable;
        }
        public DataTable Get_MENU_FormGetDetail(int Role_Id)
        {
            Request Request = new Request();
            DataTable _DTable = new DataTable();
            Request.AddParams("@Role_Id", Role_Id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.UserAuth_Menu_Form_GetDetailData;
            Request.CommandType = CommandType.StoredProcedure;

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, _DTable, Request);
            //}
            //else
            //{
            //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, _DTable, Request);
            //}
            _DTab = _DTable;
            return _DTable;
        }

        #endregion

        #region Other Function
        public int SaveThemes(string Theme_Name)
        {
            Request Request = new Request();
            Request.AddParams("@User_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@Theme_Name", Theme_Name, DbType.String);
            Request.AddParams("@Active", 1, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Theme_Save;
            Request.CommandType = CommandType.StoredProcedure;
            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            //}
            //else
            //{
            //    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            //}
        }
        public DataTable Get_Theme_Master()
        {
            Request Request = new Request();
            DataTable DTable = new DataTable();
            Request.AddParams("@User_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Theme_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTable, Request);
            //}
            //else
            //{
            //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTable, Request);
            //}
            return DTable;
        }
        public DataTable GetData_Single_User_General_Preferences_Settings(int user_id)
        {
            DataTable DtPreView = new DataTable();
            Request Request = new Request();
            Request.CommandType = CommandType.StoredProcedure;
            Request.CommandText = BLL.TPV.SProc.MST_User_Preference_GetData;
            Request.AddParams("@user_id", user_id, DbType.Int32);

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DtPreView, Request);
            //}
            //else
            //{
            //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DtPreView, Request);
            //}
            return DtPreView;
        }
        public int Save_Login_History()
        {
            Request Request = new Request();
            Request.AddParams("@history_type", "Desktop Login", DbType.String);
            Request.AddParams("@login_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@login_time", Val.ToString(GlobalDec.gStr_SystemTime), DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Confir_Login_History_Save;
            Request.CommandType = CommandType.StoredProcedure;
            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            //}
            //else
            //{
            //    return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            //}
        }
        public DataTable GetStartMenuData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@role_id", GlobalDec.gEmployeeProperty.role_id);

            Request.CommandText = BLL.TPV.SProc.Config_Menu_Master_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            //}
            //else
            //{
            //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            //}
            return DTab;
        }
        #endregion
    }
}
