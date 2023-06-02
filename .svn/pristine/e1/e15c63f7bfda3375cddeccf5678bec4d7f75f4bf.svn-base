using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGHistoryView
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public DataTable GetHistoryData(int KapanId, int CutId, Int64 LotId, int Active)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rough_cut_id", CutId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@active", Active, DbType.Int32);


            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int DeleteHistory(Int64 LotId, int HistoryId)
        {
            //DataTable DTab = new DataTable();            
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Delete_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            //Request.AddParams("@rough_cut_id", CutId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);



            return Ope.ExecuteNonQuery_History(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable MultiUpdateCheck(Int64 LotId, int HistoryId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Check_Multi_Entry;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable CheckFromLot(Int64 LotId, int HistoryId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_SplitLotCheck;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int UpdateHistory(Int64 LotId, int HistoryId, int DeptId, int MGrId)
        {
            //DataTable DTab = new DataTable();            
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@department_id", DeptId, DbType.Int32);
            Request.AddParams("@manager_id", MGrId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@update_type", "DEPT", DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdateManagerAndEmp_History(Int64 Lot_SrNo, Int64 Process_ID, Int64 Manager_ID, Int64 Employee_ID, Int64 Lot_ID, Int64 History_Type_ID, Int64 Sieve_ID, Int64 Rough_Clarity_ID, Int64 Quality_ID, bool Manager, bool Employee)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_Manager_History;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_srno", Lot_SrNo, DbType.Int64);
            Request.AddParams("@process_id", Process_ID, DbType.Int64);
            Request.AddParams("@lot_id", Lot_ID, DbType.Int64);
            Request.AddParams("@manager_id", Manager_ID, DbType.Int64);
            Request.AddParams("@employee_id", Employee_ID, DbType.Int64);
            Request.AddParams("@history_type_id", History_Type_ID, DbType.Int64);
            Request.AddParams("@sieve_id", Sieve_ID, DbType.Int64);
            Request.AddParams("@rough_clarity_id", Rough_Clarity_ID, DbType.Int64);
            Request.AddParams("@quality_id", Quality_ID, DbType.Int64);
            Request.AddParams("@checked_manager", Manager, DbType.Int32);
            Request.AddParams("@checked_employee", Employee, DbType.Int32);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdateParty_History(Int64 Lot_SrNo, Int64 Janged_ID, Int64 Party_ID, Int64 Lot_ID, Int64 Previous_Party_ID)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_Party_History;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_srno", Lot_SrNo, DbType.Int64);
            Request.AddParams("@janged_id", Janged_ID, DbType.Int64);
            Request.AddParams("@party_id", Party_ID, DbType.Int64);
            Request.AddParams("@lot_id", Lot_ID, DbType.Int64);
            Request.AddParams("@previous_party_id", Previous_Party_ID, DbType.Int64);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int DeleteMixSplit_LotWise_History(Int64 Lot_SrNo, Int64 Balance_Pcs, decimal Balance_Carat, Int64 Lot_ID)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Delete_MixSplitLotWise_History;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_srno", Lot_SrNo, DbType.Int64);
            Request.AddParams("@balance_pcs", Balance_Pcs, DbType.Int64);
            Request.AddParams("@balance_carat", Balance_Carat, DbType.Decimal);
            Request.AddParams("@lot_id", Lot_ID, DbType.Int64);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdatePcsCaratHistory(Int64 LotId, int HistoryId, int RecPcs, decimal RecCarat, int RRPcs, decimal RRCarat, int RejPcs, decimal RejCarat, int ResawPcs, decimal ResawCarat, int BreakPcs, decimal BreakCarat, int LostPcs, decimal LostCarat, decimal LossCarat, decimal PlusCarat, int flag, int count, decimal diffloss = 0, decimal diffRec = 0)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@return_pcs", RecPcs, DbType.Int32);
            Request.AddParams("@return_carat", RecCarat, DbType.Decimal);
            Request.AddParams("@rr_pcs", RRPcs, DbType.Int32);
            Request.AddParams("@rr_carat", RRCarat, DbType.Decimal);
            Request.AddParams("@rejection_pcs", RejPcs, DbType.Int32);
            Request.AddParams("@rejection_carat", RejCarat, DbType.Decimal);
            Request.AddParams("@resoing_pcs", ResawPcs, DbType.Int32);
            Request.AddParams("@resoing_carat", ResawCarat, DbType.Decimal);
            Request.AddParams("@breakage_pcs", BreakPcs, DbType.Int32);
            Request.AddParams("@breakage_carat", BreakCarat, DbType.Decimal);
            Request.AddParams("@lost_pcs", LostPcs, DbType.Int32);
            Request.AddParams("@lost_carat", LostCarat, DbType.Decimal);
            Request.AddParams("@loss_carat", LossCarat, DbType.Decimal);
            Request.AddParams("@carat_plus", PlusCarat, DbType.Decimal);
            Request.AddParams("@old_return_pcs", RecPcs, DbType.Int32);
            Request.AddParams("@old_return_carat", RecCarat, DbType.Decimal);
            Request.AddParams("@old_rr_pcs", RRPcs, DbType.Int32);
            Request.AddParams("@old_rr_carat", RRCarat, DbType.Decimal);
            Request.AddParams("@old_rejection_pcs", RejPcs, DbType.Int32);
            Request.AddParams("@old_rejection_carat", RejCarat, DbType.Decimal);
            Request.AddParams("@old_resoing_pcs", ResawPcs, DbType.Int32);
            Request.AddParams("@old_resoing_carat", ResawCarat, DbType.Decimal);
            Request.AddParams("@old_breakage_pcs", BreakPcs, DbType.Int32);
            Request.AddParams("@old_breakage_carat", BreakCarat, DbType.Decimal);
            Request.AddParams("@old_lost_pcs", LostPcs, DbType.Int32);
            Request.AddParams("@old_lost_carat", LostCarat, DbType.Decimal);
            Request.AddParams("@old_loss_carat", LossCarat, DbType.Decimal);
            Request.AddParams("@old_carat_plus", PlusCarat, DbType.Decimal);
            Request.AddParams("@manager_id", 0, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@update_type", "PCSCARAT", DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Request.AddParams("@count", count, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdateStockCaratHistory(Int64 LotId, decimal RecCarat, decimal LossCarat, decimal PlusCarat)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_StockCarat_History;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@return_carat", RecCarat, DbType.Decimal);
            Request.AddParams("@loss_carat", LossCarat, DbType.Decimal);
            Request.AddParams("@carat_plus", PlusCarat, DbType.Decimal);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            //Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            //Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            //Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            //Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdateAllPcsHistory(Int64 LotId, int HistoryId, int RecPcs, decimal RecCarat, int RRPcs, decimal RRCarat, int RejPcs, decimal RejCarat, int ResawPcs, decimal ResawCarat, int BreakPcs, decimal BreakCarat, int LostPcs, decimal LostCarat, decimal LossCarat, decimal PlusCarat, int flag, int count)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@return_pcs", RecPcs, DbType.Int32);
            Request.AddParams("@return_carat", RecCarat, DbType.Decimal);
            Request.AddParams("@rr_pcs", RRPcs, DbType.Int32);
            Request.AddParams("@rr_carat", RRCarat, DbType.Decimal);
            Request.AddParams("@rejection_pcs", RejPcs, DbType.Int32);
            Request.AddParams("@rejection_carat", RejCarat, DbType.Decimal);
            Request.AddParams("@resoing_pcs", ResawPcs, DbType.Int32);
            Request.AddParams("@resoing_carat", ResawCarat, DbType.Decimal);
            Request.AddParams("@breakage_pcs", BreakPcs, DbType.Int32);
            Request.AddParams("@breakage_carat", BreakCarat, DbType.Decimal);
            Request.AddParams("@lost_pcs", LostPcs, DbType.Int32);
            Request.AddParams("@lost_carat", LostCarat, DbType.Decimal);
            Request.AddParams("@loss_carat", LossCarat, DbType.Decimal);
            Request.AddParams("@carat_plus", PlusCarat, DbType.Decimal);
            Request.AddParams("@old_return_pcs", 0, DbType.Int32);
            Request.AddParams("@old_return_carat", 0, DbType.Decimal);
            Request.AddParams("@old_rr_pcs", 0, DbType.Int32);
            Request.AddParams("@old_rr_carat", 0, DbType.Decimal);
            Request.AddParams("@old_rejection_pcs", 0, DbType.Int32);
            Request.AddParams("@old_rejection_carat", 0, DbType.Decimal);
            Request.AddParams("@old_resoing_pcs", 0, DbType.Int32);
            Request.AddParams("@old_resoing_carat", 0, DbType.Decimal);
            Request.AddParams("@old_breakage_pcs", 0, DbType.Int32);
            Request.AddParams("@old_breakage_carat", 0, DbType.Decimal);
            Request.AddParams("@old_lost_pcs", 0, DbType.Int32);
            Request.AddParams("@old_lost_carat", 0, DbType.Decimal);
            Request.AddParams("@old_loss_carat", 0, DbType.Decimal);
            Request.AddParams("@old_carat_plus", 0, DbType.Decimal);
            Request.AddParams("@manager_id", 0, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@update_type", "ALLPCS", DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Request.AddParams("@count", count, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdateMultiPcsCaratHistory(Int64 LotId, int HistoryId, int RecPcs, decimal RecCarat, int RRPcs, decimal RRCarat, int RejPcs, decimal RejCarat, int ResawPcs, decimal ResawCarat, int BreakPcs, decimal BreakCarat, int LostPcs, decimal LostCarat, decimal LossCarat, decimal PlusCarat, int oldRecPcs, decimal oldRecCarat, int oldRRPcs, decimal oldRRCarat, int oldRejPcs, decimal oldRejCarat, int oldResawPcs, decimal oldResawCarat, int oldBreakPcs, decimal oldBreakCarat, int oldLostPcs, decimal oldLostCarat, decimal oldLossCarat, decimal oldPlusCarat, int flag, int count)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_MultiUpdate_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@return_pcs", RecPcs, DbType.Int32);
            Request.AddParams("@return_carat", RecCarat, DbType.Decimal);
            Request.AddParams("@rr_pcs", RRPcs, DbType.Int32);
            Request.AddParams("@rr_carat", RRCarat, DbType.Decimal);
            Request.AddParams("@rejection_pcs", RejPcs, DbType.Int32);
            Request.AddParams("@rejection_carat", RejCarat, DbType.Decimal);
            Request.AddParams("@resoing_pcs", ResawPcs, DbType.Int32);
            Request.AddParams("@resoing_carat", ResawCarat, DbType.Decimal);
            Request.AddParams("@breakage_pcs", BreakPcs, DbType.Int32);
            Request.AddParams("@breakage_carat", BreakCarat, DbType.Decimal);
            Request.AddParams("@lost_pcs", LostPcs, DbType.Int32);
            Request.AddParams("@lost_carat", LostCarat, DbType.Decimal);
            Request.AddParams("@loss_carat", LossCarat, DbType.Decimal);
            Request.AddParams("@carat_plus", PlusCarat, DbType.Decimal);
            Request.AddParams("@old_return_pcs", oldRecPcs, DbType.Int32);
            Request.AddParams("@old_return_carat", oldRecCarat, DbType.Decimal);
            Request.AddParams("@old_rr_pcs", oldRRPcs, DbType.Int32);
            Request.AddParams("@old_rr_carat", oldRRCarat, DbType.Decimal);
            Request.AddParams("@old_rejection_pcs", oldRejPcs, DbType.Int32);
            Request.AddParams("@old_rejection_carat", oldRejCarat, DbType.Decimal);
            Request.AddParams("@old_resoing_pcs", oldResawPcs, DbType.Int32);
            Request.AddParams("@old_resoing_carat", oldResawCarat, DbType.Decimal);
            Request.AddParams("@old_breakage_pcs", oldBreakPcs, DbType.Int32);
            Request.AddParams("@old_breakage_carat", oldBreakCarat, DbType.Decimal);
            Request.AddParams("@old_lost_pcs", oldLostPcs, DbType.Int32);
            Request.AddParams("@old_lost_carat", oldLostCarat, DbType.Decimal);
            Request.AddParams("@old_loss_carat", oldLossCarat, DbType.Decimal);
            Request.AddParams("@old_carat_plus", oldPlusCarat, DbType.Decimal);
            Request.AddParams("@manager_id", 0, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@update_type", "PCSCARAT", DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Request.AddParams("@count", count, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdateAssortFinal(Int64 LotId, int HistoryId, int RecPcs, decimal RecCarat)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_Assort_Final;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@return_pcs", RecPcs, DbType.Int32);
            Request.AddParams("@return_carat", RecCarat, DbType.Decimal);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int UpdatePrediction(Int64 LotId, int HistoryId, int CharniId = 0, int ShadeId = 0, int QualityId = 0)
        {
            //DataTable DTab = new DataTable();            
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_History_View;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@charni_id", CharniId, DbType.Int32);
            Request.AddParams("@shade_id", ShadeId, DbType.Int32);
            Request.AddParams("@quality_id", QualityId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@history_id", HistoryId, DbType.Int32);
            Request.AddParams("@update_type", "PRD", DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetLotStockDetail(int CutId, Int64 LotId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Stock_Detail;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rough_cut_id", CutId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
