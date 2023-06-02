﻿using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class InspectionRecieve
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public Inspection_RecieveProperty Save(Inspection_RecieveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();
                Request.AddParams("@inspection_id", pClsProperty.inspection_id, DbType.Int32);
                Request.AddParams("@inspection_master_id", pClsProperty.inspection_master_id, DbType.Int32);
                Request.AddParams("@inspection_no", pClsProperty.inspection_no, DbType.String);
                Request.AddParams("@inspection_date", pClsProperty.inspection_date, DbType.Date);
                Request.AddParams("@party_id", pClsProperty.Party_Id, DbType.Int32);
                Request.AddParams("@broker_id", pClsProperty.Broker_Id, DbType.Int32);
                Request.AddParams("@delivery_type_id", pClsProperty.delivery_type_id, DbType.Int32);
                Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
                Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
                Request.AddParams("@sub_sieve_id", pClsProperty.sub_sieve_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.rec_pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.rec_carat, DbType.Decimal);
                Request.AddParams("@rejection_pcs", pClsProperty.rej_pcs, DbType.Int32);
                Request.AddParams("@rejection_carat", pClsProperty.rej_carat, DbType.Decimal);
                Request.AddParams("@loss_pcs", pClsProperty.loss_pcs, DbType.Int32);  // Add By Praful On 04022020
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@loss_rate", pClsProperty.loss_rate, DbType.Decimal);
                Request.AddParams("@loss_amount", pClsProperty.loss_amount, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rec_rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.rec_amount, DbType.Decimal);
                Request.AddParams("@current_rate", pClsProperty.current_rate, DbType.Decimal);
                Request.AddParams("@current_amount", pClsProperty.current_amount, DbType.Decimal);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.Special_Remark, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.Client_Remark, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.Payment_Remark, DbType.String);
                Request.AddParams("@type", 2, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@term_days", pClsProperty.term_days, DbType.Int32);
                Request.AddParams("@discount_per", pClsProperty.discount_per, DbType.Decimal);
                Request.AddParams("@discount_amt", pClsProperty.discount_amount, DbType.Decimal);
                Request.AddParams("@net_amt", pClsProperty.net_amount, DbType.Decimal);
                Request.AddParams("@rejection_per", pClsProperty.rej_per, DbType.Decimal);
                Request.AddParams("@due_date", pClsProperty.due_date, DbType.Date);
                Request.AddParams("@final_term_days", pClsProperty.final_days, DbType.Int32);
                Request.AddParams("@final_due_date", pClsProperty.final_due_date, DbType.Date);
                Request.AddParams("@ex_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@currency_type", pClsProperty.rate_type, DbType.String);
                Request.AddParams("@purchase_rate", pClsProperty.purchase_rate, DbType.Decimal);
                Request.AddParams("@purchase_amount", pClsProperty.purchase_amount, DbType.Decimal);
                Request.AddParams("@demand_no", pClsProperty.demand_no, DbType.String);
                Request.AddParams("@demand_id", pClsProperty.demand_id, DbType.Int64);

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
                Request.AddParams("@order_carat", pClsProperty.Order_Carat, DbType.Decimal);
                Request.AddParams("@is_selection", pClsProperty.IS_Selection, DbType.Int32);
                Request.AddParams("@party_problem", pClsProperty.Party_Problem, DbType.Int32);
                Request.AddParams("@flag_check", pClsProperty.check_flag, DbType.Int32);
                Request.AddParams("@offer_rate", pClsProperty.offer_rate, DbType.Decimal);

                Request.CommandText = BLL.TPV.SProc.TRN_InspectionRecieve_Save;
                Request.CommandType = CommandType.StoredProcedure;
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Purchase_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }

        public DataTable GetTotalInspectionCarat(int p_numCompany_id, int p_numBranch_id, int p_numLocation_id, int p_numDepartment_id, string p_inspection_no)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_InspectionIss_GetCarat;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Company_id", p_numCompany_id, DbType.Int32);
            Request.AddParams("@Branch_id", p_numBranch_id, DbType.Int32);
            Request.AddParams("@Location_id", p_numLocation_id, DbType.Int32);
            Request.AddParams("@Department_id", p_numDepartment_id, DbType.Int32);
            Request.AddParams("@inspection_no", p_inspection_no, DbType.String);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }

        public DataTable GetOSCarat(int p_numCompany_id, int p_numBranch_id, int p_numLocation_id, int p_numDepartment_id, string p_inspection_no)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Inspection_IssRec_Outstanding;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Company_id", p_numCompany_id, DbType.Int32);
            Request.AddParams("@Branch_id", p_numBranch_id, DbType.Int32);
            Request.AddParams("@Location_id", p_numLocation_id, DbType.Int32);
            Request.AddParams("@Department_id", p_numDepartment_id, DbType.Int32);
            Request.AddParams("@inspection_no", p_inspection_no, DbType.String);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);            
            return DTab;
        }

        public int Delete(Inspection_RecieveProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();
                int IntRes = 0;

                Request.AddParams("@inspection_no", pClsProperty.inspection_no, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_Inspection_Receive_Delete;
                Request.CommandType = CommandType.StoredProcedure;
                
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