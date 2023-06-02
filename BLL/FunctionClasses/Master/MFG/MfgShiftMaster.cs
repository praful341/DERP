using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;


namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgShiftMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgShift_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@shift_id", pClsProperty.shift_id, DbType.Int32);
            Request.AddParams("@shift_name", pClsProperty.shift_name, DbType.String);
            Request.AddParams("@start_time", pClsProperty.start_time, DbType.String);
            Request.AddParams("@end_time", pClsProperty.end_time, DbType.String);
            Request.AddParams("@punch_start_time", pClsProperty.punch_start_time, DbType.String);
            Request.AddParams("@punch_end_time", pClsProperty.punch_end_time, DbType.String);
            Request.AddParams("@lunch_start_time", pClsProperty.lunch_start_time, DbType.String);
            Request.AddParams("@lunch_end_time", pClsProperty.lunch_end_time, DbType.String);
            Request.AddParams("@half_day_after", pClsProperty.half_day_after, DbType.String);
            Request.AddParams("@half_day_before", pClsProperty.half_day_before, DbType.String);
            Request.AddParams("@grace_time", pClsProperty.grace_time, DbType.String);
            Request.AddParams("@start_time_interval", pClsProperty.start_time_interval, DbType.String);
            Request.AddParams("@end_time_interval", pClsProperty.end_time_interval, DbType.String);
            Request.AddParams("@next_shift_id", pClsProperty.next_shift_id, DbType.Int32);
            Request.AddParams("@total_shift_hrs", pClsProperty.total_shift_hrs, DbType.String);
            Request.AddParams("@shift_type", pClsProperty.shift_type, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_Shift_Save;
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
            Request.CommandText = BLL.TPV.SProc.MFG_MST_Shift_GetData;
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
        public string ISExists(string ShiftName, Int64 ShiftId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Shift", "shift_name", "AND shift_name = '" + ShiftName + "' AND NOT shift_id =" + ShiftId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MFG_MST_Shift", "shift_name", "AND shift_name = '" + ShiftName + "' AND NOT shift_id =" + ShiftId));
            }
        }
    }
}
