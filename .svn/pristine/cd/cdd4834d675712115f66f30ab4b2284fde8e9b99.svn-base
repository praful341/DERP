using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class AvakJavakPartyMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(AvakJavakParty_MasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;

                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_Party_Save;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);
                Request.AddParams("@party_name", pClsProperty.party_name, DbType.String);
                Request.AddParams("@date", pClsProperty.date, DbType.Date);
                Request.AddParams("@address", pClsProperty.address, DbType.String);
                Request.AddParams("@phone_1", pClsProperty.phone_1, DbType.String);
                Request.AddParams("@phone_2", pClsProperty.phone_2, DbType.String);
                Request.AddParams("@opening_balance", pClsProperty.opening_balance, DbType.Decimal);
                Request.AddParams("@party_group_id", pClsProperty.party_group_id, DbType.Int64);
                Request.AddParams("@division", pClsProperty.division, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@active", pClsProperty.active, DbType.Int32);

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                }
                else
                {
                    IntRes = Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                }

                //Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetData_AvakJavakParty(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_Party_GetData;
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
        public DataTable GetData_AvakJavakPartyGroup()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_PartyGroup_GetData;
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
        public int Delete(AvakJavakParty_MasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                int IntRes = 0;

                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_Party_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                }
                else
                {
                    IntRes = Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);
                }

                // IntRes = Conn.Inter2.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request, pEnum);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetData_AvakJavakPartyGroup_Party(AvakJavakParty_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@party_group_name", pClsProperty.party_group_name, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Avak_Javak_PartyGroup_PartyGetData;
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
        //public int FindNewSrNo()
        //{
        //    int IntSrnoA = 0;
        //    int IntSrnoB = 0;

        //    IntSrnoA = Ope.FindNewID(DBConnections.ConnectionString, DBConnections.ProviderName, "MFG_TRN_Rough_Payment", "MAX(sr_no)", "");
        //    IntSrnoB = Ope.FindNewID(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, "MFG_TRN_Rough_Payment", "MAX(sr_no)", "");

        //    if (IntSrnoA > IntSrnoB)
        //    {
        //        return IntSrnoA;
        //    }
        //    else
        //    {
        //        return IntSrnoB;
        //    }
        //}
    }
}
