using BLL.PropertyClasses.Utility;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Utility
{
    public class Login
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Other Function
        public int CheckLogin(string UserName, string Password)
        {
            DataRow Drow;
            Request Request = new Request();
            Request.AddParams("@UserName", UserName, DbType.String);
            Request.AddParams("@Password", Password, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Check_Login;
            Request.CommandType = CommandType.StoredProcedure;
            Drow = Ope.GetDataRow(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            if (Drow == null)
            {
                return -1;
            }
            else
            {
                GlobalDec.gEmployeeProperty.company_id = Val.ToInt32(Drow["company_id"]);
                GlobalDec.gEmployeeProperty.branch_id = Val.ToInt32(Drow["branch_id"]);
                GlobalDec.gEmployeeProperty.location_id = Val.ToInt32(Drow["location_id"]);
                GlobalDec.gEmployeeProperty.department_id = Val.ToInt32(Drow["department_id"]);
                GlobalDec.gEmployeeProperty.company_name = Val.ToString(Drow["company_name"]);
                GlobalDec.gEmployeeProperty.branch_name = Val.ToString(Drow["branch_name"]);
                GlobalDec.gEmployeeProperty.location_name = Val.ToString(Drow["location_name"]);
                GlobalDec.gEmployeeProperty.department_name = Val.ToString(Drow["department_name"]);
                GlobalDec.gEmployeeProperty.user_id = Val.ToInt32(Drow["user_id"]);
                GlobalDec.gEmployeeProperty.user_name = Val.ToString(Drow["user_name"]);
                GlobalDec.gEmployeeProperty.user_type = Val.ToString(Drow["user_type"]);
                GlobalDec.gEmployeeProperty.password = GlobalDec.Decrypt(Val.ToString(Drow["password"]), true);
                GlobalDec.gEmployeeProperty.employee_id = Val.ToInt32(Drow["employee_id"]);
                GlobalDec.gEmployeeProperty.party_id = Val.ToInt32(Drow["party_id"]);
                GlobalDec.gEmployeeProperty.theme = Val.ToString(Drow["Theme"]);
                GlobalDec.gEmployeeProperty.role_id = Val.ToInt32(Drow["role_id"]);
                GlobalDec.gEmployeeProperty.role_name = Val.ToString(Drow["role_name"]);
                GlobalDec.gEmployeeProperty.role_type = Val.ToString(Drow["role_type"]);
                GlobalDec.gEmployeeProperty.mobile_no = Val.ToString(Drow["mobile_no"]);
                GlobalDec.gEmployeeProperty.state_id = Val.ToInt(Drow["state_id"]);
                GlobalDec.gEmployeeProperty.cgst_per = Val.ToDecimal(Drow["cgst_per"]);
                GlobalDec.gEmployeeProperty.sgst_per = Val.ToDecimal(Drow["sgst_per"]);
                GlobalDec.gEmployeeProperty.igst_per = Val.ToDecimal(Drow["igst_per"]);
                GlobalDec.gEmployeeProperty.Allow_Developer = 0;
                GlobalDec.gEmployeeProperty.DB_Allow_Developer = 1;

                DataTable p_DtbUserPreference = new UserAuthentication().GetData_Single_User_General_Preferences_Settings(Val.ToInt(GlobalDec.gEmployeeProperty.user_id));

                int IntRes = new UserAuthentication().Save_Login_History();

                if (p_DtbUserPreference.Rows.Count > 0)
                {
                    DataRow DRow = p_DtbUserPreference.Rows[0];
                    GlobalDec.gEmployeeProperty.currency_id = Val.ToInt32(DRow["currency_id"]);
                    GlobalDec.gEmployeeProperty.secondary_currency_id = Val.ToInt32(DRow["secondary_currency_id"]);
                    GlobalDec.gEmployeeProperty.rate_type_id = Val.ToInt32(DRow["rate_type_id"]);
                    GlobalDec.gEmployeeProperty.sale_rate_type_id = Val.ToInt32(DRow["sale_rate_type_id"]);
                    GlobalDec.gEmployeeProperty.delivery_type_id = Val.ToInt32(DRow["delivery_type_id"]);
                }
                else
                {
                    GlobalDec.gEmployeeProperty.currency_id = 0;
                    GlobalDec.gEmployeeProperty.secondary_currency_id = 0;
                    GlobalDec.gEmployeeProperty.rate_type_id = 0;
                    GlobalDec.gEmployeeProperty.sale_rate_type_id = 0;
                }
                return 1;
            }
        }

        public UserAuthenticationProperty ChangePassword(UserAuthenticationProperty pClsProperty, string Old_Password, string New_Password, Int64 Emp_ID)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@employee_id", Emp_ID, DbType.Int64);
                Request.AddParams("@new_password", New_Password, DbType.String);
                Request.AddParams("@old_password", Old_Password, DbType.String);
                Request.CommandText = BLL.TPV.SProc.Change_Password;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbChangePassword = new DataTable();

                //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                //{
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbChangePassword, Request);
                //}
                //else
                //{
                //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, p_dtbChangePassword, Request);
                //}
                if (p_dtbChangePassword != null)
                {
                    if (p_dtbChangePassword.Rows.Count > 0)
                    {
                        pClsProperty.Message = Val.ToString(p_dtbChangePassword.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int LockDelete(Int32 pLock_ID, Int64 pStrKapn, string Lot_ID)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Lock_Delete;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@lock_type_id", pLock_ID, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@lot_id", Lot_ID, DbType.String);

            int IntRes = 0;

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            //}
            //else
            //{
            //    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            //}
            return IntRes;
        }
    }
    #endregion
}


