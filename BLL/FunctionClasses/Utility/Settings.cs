using BLL.PropertyClasses.Utility;
using DLL;
using System.Data;
namespace BLL.FunctionClasses.Utility
{
    public class Settings
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Operation

        public Request AddSettingsParams(Settings_Property pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("VERSION_", pClsProperty.version, DbType.String);
            Request.AddParams("EXE_COPY_PATH_", pClsProperty.exe_copy_path, DbType.String);
            Request.AddParams("SENDER_EMAIL_", pClsProperty.sender_email, DbType.String);
            Request.AddParams("SENDER_PASSWORD_", pClsProperty.sender_password, DbType.String);
            Request.AddParams("SMTPSERVER_", pClsProperty.smtpserver, DbType.String);
            Request.AddParams("SMTPPORT_", pClsProperty.smtpport, DbType.Int32);
            Request.AddParams("IS_ENABLE_SSL_", pClsProperty.is_enable_ssl, DbType.Int32);
            Request.AddParams("ALLOW_EMAIL_SEND_", pClsProperty.allow_email_send, DbType.Int32);
            Request.AddParams("GUEST_HOSTNAME_", pClsProperty.guest_hostname, DbType.String);
            Request.AddParams("GUEST_PORT_", pClsProperty.guest_port, DbType.String);
            Request.AddParams("GUEST_SERVICENAME_", pClsProperty.guest_servicename, DbType.String);
            Request.AddParams("GUEST_USERNAME_", pClsProperty.guest_username, DbType.String);
            Request.AddParams("GUEST_PASSWORD_", pClsProperty.guest_password, DbType.String);
            Request.AddParams("WEB_HOSTNAME_", pClsProperty.WEB_HOSTNAME, DbType.Int32);
            Request.AddParams("WEB_DATABASE_", pClsProperty.WEB_DATABASE, DbType.Int32);
            Request.AddParams("WEB_USERNAME_", pClsProperty.WEB_USERNAME, DbType.Int32);
            Request.AddParams("WEB_PASSWORD_", pClsProperty.WEB_PASSWORD, DbType.Int32);
            Request.AddParams("ALLOW_TIME_DIFF_", pClsProperty.ALLOW_TIME_DIFF, DbType.Int32);

            Request.CommandText = "";// BLL.TPV.SProc.Settings_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Request;
        }

        public Settings_Property GetDataByPK()
        {
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.MST_Settings_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            DataRow DRow = Ope.GetDataRow(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            if (DRow == null)
            {
                return null;
            }
            Settings_Property Property = new Settings_Property();
            //Property.version = Val.ToString(DRow["version"]);
            //Property.exe_copy_path = Val.ToString(DRow["exe_copy_path"]);

            //Property.smtpserver = Val.ToString(DRow["smtpserver"]);
            //Property.smtpport = Val.ToInt(DRow["smtpport"]);
            //Property.sender_email = Val.ToString(DRow["sender_email"]);
            //Property.email_from_for_user = Val.ToString(DRow["email_from_for_user"]);
            //if (Val.ToString(DRow["sender_password"]) != "")
            //{
            //    Property.sender_password = BLL.GlobalDec.Decrypt(Val.ToString(DRow["sender_password"]), true);
            //}
            //else
            //{
            //    Property.sender_password = "";
            //}
            //Property.is_enable_ssl = Val.ToBooleanToInt(DRow["is_enable_ssl"]);
            //Property.allow_email_send = Val.ToInt(DRow["allow_email_send"]);

            Property.guest_hostname = BLL.GlobalDec.Decrypt(Val.ToString(DRow["guest_h_temp1"]), true);
            Property.guest_servicename = BLL.GlobalDec.Decrypt(Val.ToString(DRow["guest_s_temp2"]), true);
            Property.guest_username = BLL.GlobalDec.Decrypt(Val.ToString(DRow["guest_u_temp3"]), true);
            Property.guest_password = BLL.GlobalDec.Decrypt(Val.ToString(DRow["guest_p_temp4"]), true);

            return Property;
        }

        #endregion
    }
}
