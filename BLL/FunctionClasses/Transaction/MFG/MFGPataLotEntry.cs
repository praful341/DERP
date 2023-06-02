using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGPataLotEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGPataLotEntryProperty Save(MFGPataLotEntryProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@mix_id", pClsProperty.mix_id, DbType.Int64);
                Request.AddParams("@mix_date", pClsProperty.date, DbType.Date);

                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);

                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);

                Request.AddParams("@k_process_id", pClsProperty.k_process_id, DbType.Int64);

                Request.AddParams("@total_pcs", pClsProperty.total_pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);

                Request.AddParams("@t_1_carat", pClsProperty.t_1_carat, DbType.Decimal);
                Request.AddParams("@t_1_per", pClsProperty.t_1_per, DbType.Decimal);

                Request.AddParams("@t_2_carat", pClsProperty.t_2_carat, DbType.Decimal);
                Request.AddParams("@t_2_per", pClsProperty.t_2_per, DbType.Decimal);

                Request.AddParams("@t_3_carat", pClsProperty.t_3_carat, DbType.Decimal);
                Request.AddParams("@t_3_per", pClsProperty.t_3_per, DbType.Decimal);

                Request.AddParams("@t_4_carat", pClsProperty.t_4_carat, DbType.Decimal);
                Request.AddParams("@t_4_per", pClsProperty.t_4_per, DbType.Decimal);

                Request.AddParams("@t_5_carat", pClsProperty.t_5_carat, DbType.Decimal);
                Request.AddParams("@t_5_per", pClsProperty.t_5_per, DbType.Decimal);

                Request.AddParams("@t_6_carat", pClsProperty.t_6_carat, DbType.Decimal);
                Request.AddParams("@t_6_per", pClsProperty.t_6_per, DbType.Decimal);

                Request.AddParams("@t_7_carat", pClsProperty.t_7_carat, DbType.Decimal);
                Request.AddParams("@t_7_per", pClsProperty.t_7_per, DbType.Decimal);

                Request.AddParams("@t_8_carat", pClsProperty.t_8_carat, DbType.Decimal);
                Request.AddParams("@t_8_per", pClsProperty.t_8_per, DbType.Decimal);

                Request.AddParams("@t_9_carat", pClsProperty.t_9_carat, DbType.Decimal);
                Request.AddParams("@t_9_per", pClsProperty.t_9_per, DbType.Decimal);

                Request.AddParams("@t_10_carat", pClsProperty.t_10_carat, DbType.Decimal);
                Request.AddParams("@t_10_per", pClsProperty.t_10_per, DbType.Decimal);

                Request.AddParams("@t_11_carat", pClsProperty.t_11_carat, DbType.Decimal);
                Request.AddParams("@t_11_per", pClsProperty.t_11_per, DbType.Decimal);

                Request.AddParams("@t_12_carat", pClsProperty.t_12_carat, DbType.Decimal);
                Request.AddParams("@t_12_per", pClsProperty.t_12_per, DbType.Decimal);

                Request.AddParams("@t_13_carat", pClsProperty.t_13_carat, DbType.Decimal);
                Request.AddParams("@t_13_per", pClsProperty.t_13_per, DbType.Decimal);

                Request.AddParams("@t_14_carat", pClsProperty.t_14_carat, DbType.Decimal);
                Request.AddParams("@t_14_per", pClsProperty.t_14_per, DbType.Decimal);

                Request.AddParams("@t_15_carat", pClsProperty.t_15_carat, DbType.Decimal);
                Request.AddParams("@t_15_per", pClsProperty.t_15_per, DbType.Decimal);

                Request.AddParams("@t_16_carat", pClsProperty.t_16_carat, DbType.Decimal);
                Request.AddParams("@t_16_per", pClsProperty.t_16_per, DbType.Decimal);

                Request.AddParams("@t_17_carat", pClsProperty.t_17_carat, DbType.Decimal);
                Request.AddParams("@t_17_per", pClsProperty.t_17_per, DbType.Decimal);

                Request.AddParams("@t_18_carat", pClsProperty.t_18_carat, DbType.Decimal);
                Request.AddParams("@t_18_per", pClsProperty.t_18_per, DbType.Decimal);

                Request.AddParams("@t_19_carat", pClsProperty.t_19_carat, DbType.Decimal);
                Request.AddParams("@t_19_per", pClsProperty.t_19_per, DbType.Decimal);

                Request.AddParams("@t_20_carat", pClsProperty.t_20_carat, DbType.Decimal);
                Request.AddParams("@t_20_per", pClsProperty.t_20_per, DbType.Decimal);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Patalot_Entry_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessRecId, Request);

                if (p_dtbProcessRecId != null)
                {
                    if (p_dtbProcessRecId.Rows.Count > 0)
                    {
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable MFGPataLotEntryGetData(MFGPataLotEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PataLotEntry_GetData;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
            Request.AddParams("@entry_date", pClsProperty.date, DbType.Date);
            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable MFGPataLotEntryGetDataList(MFGPataLotEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PataLotEntry_GetDataList;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Date);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetPendingStock(MFGPataLotEntryProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();
            Request.AddParams("@rough_cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@from_date", PClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", PClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_PataLotEntry_Stock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public int GetDeletePataLotEntry(MFGPataLotEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PataLot_Entry_Delete;

            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public DataTable MFGPataLotEntryPrintGetData(MFGPataLotEntryProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Patalot_Weight_RPT;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
            Request.AddParams("@lot_srno", pClsProperty.lot_srno_list, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
