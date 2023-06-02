using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class DemandNoting
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public Demand_NotingProperty Save(Demand_NotingProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@demand_id", pClsProperty.demand_id, DbType.Int32);
                Request.AddParams("@demand_master_id", pClsProperty.demand_master_id, DbType.Int32);
                Request.AddParams("@demand_no", pClsProperty.demand_No, DbType.String);
                Request.AddParams("@demand_srno", pClsProperty.demand_srno, DbType.String);
                Request.AddParams("@demand_date", pClsProperty.demand_date, DbType.Date);
                Request.AddParams("@demand_name", pClsProperty.demand_name, DbType.String);
                Request.AddParams("@rough_shade_id", pClsProperty.rough_shade_id, DbType.String);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@party_id", pClsProperty.Party_Id, DbType.Int32);
                Request.AddParams("@broker_id", pClsProperty.Broker_Id, DbType.Int32);
                Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
                Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@sub_sieve_id", pClsProperty.sub_sieve_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@current_rate", pClsProperty.current_rate, DbType.Decimal);
                Request.AddParams("@current_amount", pClsProperty.current_amount, DbType.Decimal);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@demand_rate", pClsProperty.demand_rate, DbType.Decimal);
                Request.AddParams("@demand_amount", pClsProperty.demand_amount, DbType.Decimal);
                Request.AddParams("@quality", pClsProperty.quality, DbType.Int32);
                Request.AddParams("@term_days", pClsProperty.term_days, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.Remark, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.Special_Remark, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.Client_Remark, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.Payment_Remark, DbType.String);
                Request.AddParams("@status", pClsProperty.Status, DbType.String);
                Request.AddParams("@demand_deal_final", pClsProperty.demand_deal_final, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@is_Purity", pClsProperty.IS_Purity, DbType.Int32);
                Request.AddParams("@is_color", pClsProperty.IS_Color, DbType.Int32);
                Request.AddParams("@is_price", pClsProperty.IS_Price, DbType.Int32);
                Request.AddParams("@is_cut", pClsProperty.IS_Cut, DbType.Int32);
                Request.AddParams("@is_size", pClsProperty.IS_Size, DbType.Int32);
                Request.AddParams("@is_netting", pClsProperty.IS_NotOnHand, DbType.Int32);
                Request.AddParams("@is_sold", pClsProperty.IS_Sold, DbType.Int32);
                Request.AddParams("@is_offer", pClsProperty.IS_Offer, DbType.Int32);
                Request.AddParams("@is_packetpending", pClsProperty.IS_PacketPending, DbType.Int32);
                Request.AddParams("@is_no_mal", pClsProperty.IS_No_Mal, DbType.Int32);
                Request.AddParams("@is_pending_demand", pClsProperty.IS_Pending_Demand, DbType.Int32);

                Request.AddParams("@is_qty", pClsProperty.IS_QTY, DbType.Int32);
                Request.AddParams("@is_service", pClsProperty.IS_Service, DbType.Int32);

                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@cutting", pClsProperty.cutting, DbType.String);
                Request.AddParams("@demand_time", pClsProperty.Demand_Time, DbType.Int32);
                Request.AddParams("@packet_time", pClsProperty.Packet_Time, DbType.Int32);
                Request.AddParams("@packet_date", pClsProperty.Packet_Date, DbType.Date);
                Request.AddParams("@receive_time", pClsProperty.Rec_Time, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.Rec_Date, DbType.Date);
                Request.AddParams("@party_choice", pClsProperty.Party_Choice, DbType.String);
                Request.AddParams("@suggest_assort_id", pClsProperty.suggest_assort_id, DbType.String);
                Request.AddParams("@suggest_assort_name", pClsProperty.suggest_assort_name, DbType.String);
                Request.AddParams("@final_rate", pClsProperty.Final_Rate, DbType.Decimal);
                Request.AddParams("@discount_per", pClsProperty.Discount_Per, DbType.Decimal);
                Request.AddParams("@ok_rate", pClsProperty.OK_Rate, DbType.Decimal);

                Request.AddParams("@order_carat", pClsProperty.Order_Carat, DbType.Decimal);
                Request.AddParams("@is_selection", pClsProperty.IS_Selection, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.TRN_Demand_Master_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbMasterId = new DataTable();
                    if (Conn != null)
                        Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbMasterId, Request, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);               

                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.demand_srno = Val.ToInt32(p_dtbMasterId.Rows[0][0]);
                        pClsProperty.demand_No = Val.ToInt32(p_dtbMasterId.Rows[0][1]);
                        pClsProperty.demand_id = Val.ToInt32(p_dtbMasterId.Rows[0][2]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, Int32 Party_id, Int32 Broker_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Demand_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@party_id", Party_id, DbType.Int32);
            Request.AddParams("@broker_id", Broker_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);          
            return DTab;
        }
        public DataTable GetDataDetails(Int64 p_numID)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_Demand_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@demand_srno", p_numID, DbType.Int64);
                
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);                
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetRateWiseAssort(decimal fromRate, decimal toRate, int color_id)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_RateWiseAssort_GetData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@Active", 1, DbType.Int32);
                Request.AddParams("@from_rate", fromRate, DbType.Decimal);
                Request.AddParams("@to_rate", toRate, DbType.Decimal);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@color_id", color_id, DbType.Int32);
                Request.AddParams("@ratetype_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@sieve_id", 0, DbType.Int32);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);               
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public Int64 FindMaxMemoMasterID()
        {
            Int64 DemandMasterID = 0;            
                DemandMasterID = Ope.FindNewIDInt64(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "TRN_Demand_Noting", "MAX(demand_master_id)", "");
           
            return DemandMasterID;
        }
        public int Delete(Demand_NotingProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@demand_no", pClsProperty.demand_No, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_Demand_Master_Delete;
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
    }
}
