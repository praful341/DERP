using BLL.PropertyClasses.Account;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Account
{
    public class AvakJavakCashTransfer
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(AvakJavakCashTransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;

                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_Cash_Transfer_Save;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@cash_transfer_id", pClsProperty.cash_transfer_id, DbType.Int64);
                Request.AddParams("@cash_transfer_date", pClsProperty.cash_transfer_date, DbType.Date);
                Request.AddParams("@cash_transfer_time", GlobalDec.gStr_SystemTime, DbType.Date);
                Request.AddParams("@from_party_id", pClsProperty.from_party_id, DbType.Int64);
                Request.AddParams("@to_party_id", pClsProperty.to_party_id, DbType.Int64);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                //IntRes = Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                }
                else
                {
                    IntRes = Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                }

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetData_AvakJavakCashTransfer(AvakJavakCashTransferProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_CashTransfer_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.String);
            Request.AddParams("@party_id", pClsProperty.from_party_id, DbType.Int64);

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

        public int Delete(AvakJavakCashTransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                int IntRes = 0;

                Request.AddParams("@cash_transfer_id", pClsProperty.cash_transfer_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_CashTransfer_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                }
                else
                {
                    IntRes = Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                }

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
