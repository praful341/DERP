﻿using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgJangedSieve
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgJanged_SieveProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@janged_sieve_id", pClsProperty.janged_sieve_id, DbType.Int32);
            Request.AddParams("@sieve_name", pClsProperty.sieve_name, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_JangedSieve_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_JangedSieve_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
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
        public string ISExists(string SieveName, Int64 JSieveId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Janged_Sieve", "sieve_name", "AND sieve_name = '" + SieveName + "' AND NOT janged_sieve_id =" + JSieveId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MFG_MST_Janged_Sieve", "sieve_name", "AND sieve_name = '" + SieveName + "' AND NOT janged_sieve_id =" + JSieveId));
            }
        }
    }
}
