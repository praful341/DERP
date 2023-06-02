using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class Single_Setting
    {
        Validation Val = new Validation();
        InterfaceLayer Ope = new InterfaceLayer();

        #region Other Function      

        public int SaveSingleSettings(Single_SettingProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@role_id", pClsProperty.role_id, DbType.Int32);
                Request.AddParams("@column_name", pClsProperty.column_name, DbType.String);
                Request.AddParams("@caption", pClsProperty.caption, DbType.String);
                Request.AddParams("@field_no", pClsProperty.field_no, DbType.Int32);
                Request.AddParams("@is_visible", pClsProperty.is_visible, DbType.Int32);
                Request.AddParams("@is_compulsory", pClsProperty.is_compulsory, DbType.Int32);
                Request.AddParams("@tab_index", pClsProperty.tab_index, DbType.Int32);
                Request.AddParams("@is_editable", pClsProperty.is_editable, DbType.Int32);
                Request.AddParams("@is_newrow", pClsProperty.is_newrow, DbType.Int32);
                Request.AddParams("@is_control", pClsProperty.is_control, DbType.Int32);
                Request.AddParams("@column_type", pClsProperty.column_type, DbType.String);
                Request.AddParams("@gridname", pClsProperty.gridname, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Config_Single_Setting_Save; // "SINGLE_SETTINGS_SAVE";
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

        public DataTable GetData(Single_SettingProperty pClsProperty)
        {
            DataTable DtPreView = new DataTable();
            Request Request = new Request();
            Request.CommandType = CommandType.StoredProcedure;
            Request.CommandText = BLL.TPV.SProc.Config_Single_Setting_GetData;
            Request.AddParams("@role_id", pClsProperty.role_id, DbType.Int32);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DtPreView, Request);
            return DtPreView;
        }

        public int DeleteSingleSettings(Single_SettingProperty pClsProperty)
        {
            DataTable DtPreView = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandType = CommandType.StoredProcedure;
            Request.CommandText = BLL.TPV.SProc.Config_Single_Setting_Delete;
            Request.AddParams("@role_id", pClsProperty.role_id, DbType.Int32);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
            Request.AddParams("@field_no", pClsProperty.field_no, DbType.Int32);

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }

        public int CopySingleSettings(Int32 Role_Id, Int32 Copy_Role_Id, int FormId, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@role_id", Role_Id, DbType.Int32);
                Request.AddParams("@copy_role_id", Copy_Role_Id, DbType.Int32);
                Request.AddParams("@form_id", FormId, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.Config_Single_Setting_CopySave;
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
        #endregion
    }
}

