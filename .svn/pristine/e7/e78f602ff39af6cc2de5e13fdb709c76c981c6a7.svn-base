using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;

namespace DREP.Transaction
{
    public partial class FrmMixSplitSubSieveWise : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MixSplit objMixSplit;
        BranchTransfer objBranch;
        CompanyMemoIssueReceipt objCompanyMamoIssueReceipt;
        MixSplitProperty objMixSplitProperty = new MixSplitProperty();
        AssortMaster objAssort = new AssortMaster();
        SieveMaster objSieve = new SieveMaster();

        DataTable m_dtbMixDetails;
        DataTable m_dtbSplitDetails;
        DataTable DTab_Mix;
        DataTable DTab_Split;
        DataTable m_dtbCompanyMemoIssueDetail;

        DataTable m_dtbAssorts = new DataTable();
        DataTable m_dtbSieve = new DataTable();

        int m_numForm_id = 0;
        int IntRes = 0;
        string Party_Memo_No = string.Empty;
        string Company_Memo_No = string.Empty;

        decimal m_numTotalCarats = 0;
        decimal m_numTotalAmount = 0;

        double m_Balance_Carat = 0;
        double m_Rejection_Carat = 0;

        double m_TransferCarat = 0;
        double m_TransferRate = 0;
        double m_TransferAmount = 0;

        #endregion

        #region Constructor
        public FrmMixSplitSubSieveWise()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objMixSplit = new MixSplit();
            objBranch = new BranchTransfer();
            objCompanyMamoIssueReceipt = new CompanyMemoIssueReceipt();

            m_dtbMixDetails = new DataTable();
            m_dtbSplitDetails = new DataTable();
            DTab_Mix = new DataTable();
            DTab_Split = new DataTable();
            m_dtbCompanyMemoIssueDetail = new DataTable();

            m_numForm_id = 0;
            IntRes = 0;

            Party_Memo_No = string.Empty;
            Company_Memo_No = string.Empty;

            m_numTotalCarats = 0;
            m_numTotalAmount = 0;

            m_Balance_Carat = 0;
            m_Rejection_Carat = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }

            Val.frmGenSet(this);
            AttachFormEvents();
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(objMixSplit);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            var result1 = DateTime.Compare(Convert.ToDateTime(dtpEntryDate.Text), DateTime.Today);
            if (result1 > 0)
            {
                Global.Message("Date Not Be Greater Than Today Date");
                dtpEntryDate.Focus();
                return;

            }
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            btnSave.Enabled = false;
            double mix_Detail_Carat = Val.ToDouble(dgvMixDetails.Columns["trf_carat"].SummaryText);
            double Split_Detail_Carat = Math.Round(Val.ToDouble(dgvSplitDetails.Columns["trf_carat"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_loss"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText), 3);
            double Without_Carat_plus = Math.Round(Val.ToDouble(dgvSplitDetails.Columns["trf_carat"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_loss"].SummaryText), 3);
            if (mix_Detail_Carat != Split_Detail_Carat)
            {

                if ((mix_Detail_Carat + Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText)) != Without_Carat_plus)
                {
                    Global.Message("Carat doesn't tally. Please verify!");
                    IntRes = 0;
                    btnSave.Enabled = true;
                    return;
                }
                if (mix_Detail_Carat >= (Without_Carat_plus + 0.01) && mix_Detail_Carat >= (Without_Carat_plus + 0.05))
                {
                    Global.Message("Carat doesn't tally. Please verify!");
                    IntRes = 0;
                    btnSave.Enabled = true;
                    return;
                }
                if (Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText) > 0.05)
                {
                    Global.Message("Carat doesn't tally. Please verify!");
                    IntRes = 0;
                    btnSave.Enabled = true;
                    return;
                }
                if (Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText) < 0.001)
                {
                    Global.Message("Carat doesn't tally. Please verify!");
                    IntRes = 0;
                    btnSave.Enabled = true;
                    return;
                }
            }

            DialogResult result = MessageBox.Show("Do you want to Mix and Split data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            //if (SaveDetails())
            //{
            //    ClearDetails();
            //}
            backgroundWorker_MixSplit.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker_MixSplit_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            IntRes = 0;
            string p_numFromStockRate = string.Empty;
            string p_numToStockRate = string.Empty;

            #region "Memo"
            if (RBtnType.SelectedIndex == 2)
            {
                Int64 NewMixSplitid = 0;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                double mix_Detail_Carat = Val.ToDouble(dgvMixDetails.Columns["rej_carat"].SummaryText);
                double Split_Detail_Carat = Math.Round(Val.ToDouble(dgvSplitDetails.Columns["trf_carat"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_loss"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText), 3);

                try
                {
                    DTab_Mix = (DataTable)grdMixDetail.DataSource;

                    if (DTab_Mix.Select("rej_carat > 0").Length > 0)
                    {
                        DTab_Mix = DTab_Mix.Select("rej_carat > 0").CopyToDataTable();

                        for (int i = 0; i < DTab_Mix.Rows.Count; i++)
                        {
                            objMixSplitProperty.mixsplit_id = Val.ToInt64(0);
                            objMixSplitProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                            objMixSplitProperty.mixsplit_date = Val.DBDate(dtpEntryDate.Text);
                            objMixSplitProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                            objMixSplitProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                            objMixSplitProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                            objMixSplitProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                            objMixSplitProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                            objMixSplitProperty.from_assort_id = Val.ToInt(DTab_Mix.Rows[i]["assort_id"]);
                            objMixSplitProperty.from_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]);
                            //objMixSplitProperty.from_sub_sieve_id = Val.ToInt(387);
                            objMixSplitProperty.from_sub_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sub_sieve_id"]);
                            objMixSplitProperty.from_pcs = Val.ToInt(DTab_Mix.Rows[i]["rej_pcs"]);
                            objMixSplitProperty.from_carat = Val.ToDecimal(DTab_Mix.Rows[i]["rej_carat"]);

                            p_numFromStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Mix.Rows[i]["assort_id"]), Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]));
                            objMixSplitProperty.from_rate = Val.ToDecimal(p_numFromStockRate);

                            if (Val.ToDecimal(p_numFromStockRate) == 0)
                            {
                                Global.Message("Rate not found please check the Assort no.");
                                return;
                            }
                            objMixSplitProperty.from_amount = Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["rej_carat"]) * Val.ToDecimal(p_numFromStockRate), 3);
                            objMixSplitProperty.to_assort_id = Val.ToInt(DTab_Mix.Rows[i]["assort_id"]); //Val.ToInt(416);
                            objMixSplitProperty.to_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]);
                            //objMixSplitProperty.to_sub_sieve_id = Val.ToInt(387);
                            objMixSplitProperty.to_sub_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sub_sieve_id"]);
                            objMixSplitProperty.to_pcs = Val.ToInt(0);
                            objMixSplitProperty.to_carat = Val.ToDecimal(0);
                            objMixSplitProperty.to_rate = Val.ToDecimal(0);
                            objMixSplitProperty.to_amount = Val.ToDecimal(0);
                            objMixSplitProperty.mixsplit_type_id = Val.ToInt(1);
                            objMixSplitProperty.form_id = m_numForm_id;
                            objMixSplitProperty.transaction_type_id = Val.ToInt(2);
                            objMixSplitProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                            objMixSplitProperty.rate_type_id = Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id);
                            objMixSplitProperty.user_id = Val.ToInt(GlobalDec.gEmployeeProperty.user_id);
                            objMixSplitProperty.entry_date = Val.DBDate(GlobalDec.gStr_SystemDate);
                            objMixSplitProperty.entry_time = Val.ToString(GlobalDec.gStr_SystemTime);
                            objMixSplitProperty.ip_address = Val.ToString(GlobalDec.gStrComputerIP);
                            objMixSplitProperty.form_id = m_numForm_id;
                            objMixSplitProperty.trn_type = Val.ToString("Mix");
                            objMixSplitProperty.company_memo_no = Val.ToString(Company_Memo_No);
                            objMixSplitProperty.party_memo_no = Val.ToString(Party_Memo_No);
                            if (Val.ToString(RBtnType.EditValue) == "B")
                            {
                                objMixSplitProperty.type = Val.ToString("Balance");
                            }
                            else if (Val.ToString(RBtnType.EditValue) == "R")
                            {
                                objMixSplitProperty.type = Val.ToString("Rejection");
                            }
                            else
                            {
                                objMixSplitProperty.type = Val.ToString("Memo");
                            }

                            objMixSplitProperty = objMixSplit.SieveWise_Save(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                            NewMixSplitid = Val.ToInt64(objMixSplitProperty.mixsplit_srno);
                        }
                    }
                    else
                    {
                        Global.Message("No Any Mixing Data Found in Grid");
                        IntRes = 0;
                        return;
                    }

                    DTab_Split = (DataTable)grdSplitDetail.DataSource;
                    if (DTab_Split.Select("trf_carat > 0").Length > 0)
                    {
                        DTab_Split = DTab_Split.Select("trf_carat > 0").CopyToDataTable();

                        for (int i = 0; i < DTab_Split.Rows.Count; i++)
                        {
                            objMixSplitProperty = new MixSplitProperty();

                            objMixSplitProperty.mixsplit_id = Val.ToInt64(0);
                            objMixSplitProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                            objMixSplitProperty.mixsplit_date = Val.DBDate(dtpEntryDate.Text);
                            objMixSplitProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                            objMixSplitProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                            objMixSplitProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                            objMixSplitProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                            objMixSplitProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                            objMixSplitProperty.from_assort_id = Val.ToInt(416);
                            objMixSplitProperty.from_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sieve_id"]);
                            //objMixSplitProperty.from_sub_sieve_id = Val.ToInt(387);
                            objMixSplitProperty.from_sub_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sub_sieve_id"]);
                            objMixSplitProperty.to_assort_id = Val.ToInt(DTab_Split.Rows[i]["assort_id"]);
                            objMixSplitProperty.to_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sieve_id"]);
                            //objMixSplitProperty.to_sub_sieve_id = Val.ToInt(387);
                            objMixSplitProperty.to_sub_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sub_sieve_id"]);
                            objMixSplitProperty.from_pcs = Val.ToInt(0);
                            objMixSplitProperty.from_carat = Val.ToDecimal(0);
                            p_numFromStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Split.Rows[i]["assort_id"]), Val.ToInt(DTab_Split.Rows[i]["sieve_id"]));
                            objMixSplitProperty.from_rate = Val.ToDecimal(0);
                            objMixSplitProperty.from_amount = Val.ToDecimal(0);

                            objMixSplitProperty.to_pcs = Val.ToInt(DTab_Split.Rows[i]["trf_pcs"]);
                            objMixSplitProperty.to_carat = Val.ToDecimal(DTab_Split.Rows[i]["trf_carat"]);
                            objMixSplitProperty.to_rate = Val.ToDecimal(p_numFromStockRate);
                            objMixSplitProperty.to_amount = Math.Round(Val.ToDecimal(DTab_Split.Rows[i]["trf_carat"]) * Val.ToDecimal(p_numFromStockRate), 3);
                            objMixSplitProperty.mixsplit_type_id = Val.ToInt(2);
                            objMixSplitProperty.form_id = m_numForm_id;
                            objMixSplitProperty.transaction_type_id = Val.ToInt(2);
                            objMixSplitProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                            objMixSplitProperty.rate_type_id = Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id);
                            objMixSplitProperty.user_id = Val.ToInt(GlobalDec.gEmployeeProperty.user_id);
                            objMixSplitProperty.entry_date = Val.DBDate(GlobalDec.gStr_SystemDate);
                            objMixSplitProperty.entry_time = Val.ToString(GlobalDec.gStr_SystemTime);
                            objMixSplitProperty.ip_address = Val.ToString(GlobalDec.gStrComputerIP);
                            objMixSplitProperty.form_id = m_numForm_id;
                            objMixSplitProperty.trn_type = Val.ToString("Split");
                            objMixSplitProperty.loss_carat = Val.ToDecimal(DTab_Split.Rows[i]["weight_loss"]);
                            objMixSplitProperty.carat_plus = Val.ToDecimal(DTab_Split.Rows[i]["weight_plus"]);
                            objMixSplitProperty.company_memo_no = Val.ToString(Company_Memo_No);
                            objMixSplitProperty.party_memo_no = Val.ToString(Party_Memo_No);

                            if (Val.ToString(RBtnType.EditValue) == "B")
                            {
                                objMixSplitProperty.type = Val.ToString("Balance");
                            }
                            else if (Val.ToString(RBtnType.EditValue) == "R")
                            {
                                objMixSplitProperty.type = Val.ToString("Rejection");
                            }
                            else
                            {
                                objMixSplitProperty.type = Val.ToString("Memo");
                            }

                            IntRes = objMixSplit.SieveWise_Save_New(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                    }
                    else
                    {
                        Global.Message("No Any Split Data Found in Grid");
                        IntRes = 0;
                        return;
                    }
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    objMixSplitProperty = null;
                }
            }
            #endregion "Memo"           

            #region "Balance"
            else
            {
                Int64 NewMixSplitid = 0;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Conn = new BeginTranConnection(true, false);
                }
                else
                {
                    Conn = new BeginTranConnection(false, true);
                }
                double mix_Detail_Carat = Val.ToDouble(dgvMixDetails.Columns["trf_carat"].SummaryText);
                double Split_Detail_Carat = Math.Round(Val.ToDouble(dgvSplitDetails.Columns["trf_carat"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_loss"].SummaryText) + Val.ToDouble(dgvSplitDetails.Columns["weight_plus"].SummaryText), 3);

                try
                {
                    DTab_Mix = (DataTable)grdMixDetail.DataSource;

                    m_TransferCarat = 0;
                    m_TransferRate = 0;
                    m_TransferAmount = 0;

                    if (DTab_Mix.Select("trf_carat > 0").Length > 0)
                    {
                        DTab_Split = (DataTable)grdSplitDetail.DataSource;
                        DTab_Mix = DTab_Mix.Select("trf_carat > 0").CopyToDataTable();
                        DTab_Split = DTab_Split.Select("trf_carat > 0").CopyToDataTable();

                        #region"Mix = 1 to Many"
                        if (DTab_Mix.Rows.Count == 1)
                        {
                            int count = 0;
                            for (int i = 0; i < DTab_Mix.Rows.Count; i++)
                            {
                                for (int j = 0; j < DTab_Split.Rows.Count; j++)
                                {
                                    objMixSplitProperty = new MixSplitProperty();

                                    objMixSplitProperty.mixsplit_id = Val.ToInt64(0);
                                    objMixSplitProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                                    objMixSplitProperty.mixsplit_date = Val.DBDate(dtpEntryDate.Text);
                                    objMixSplitProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                    objMixSplitProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                                    objMixSplitProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                                    objMixSplitProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                                    objMixSplitProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);

                                    objMixSplitProperty.from_assort_id = Val.ToInt(DTab_Mix.Rows[i]["assort_id"]);
                                    objMixSplitProperty.from_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]);
                                    //objMixSplitProperty.from_sub_sieve_id = Val.ToInt(387);
                                    objMixSplitProperty.from_sub_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sub_sieve_id"]);
                                    //objMixSplitProperty.slip_no = Val.ToString(txtSlipNo.Text);

                                    if (j == 0)
                                    {
                                        objMixSplitProperty.from_pcs = Val.ToInt(DTab_Mix.Rows[i]["trf_pcs"]);
                                        objMixSplitProperty.from_carat = Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]);
                                        objMixSplitProperty.from_rate = Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]);
                                        objMixSplitProperty.from_amount = Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]) * Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]), 3);
                                    }

                                    objMixSplitProperty.to_assort_id = Val.ToInt(DTab_Split.Rows[j]["assort_id"]); //Val.ToInt(416);
                                    objMixSplitProperty.to_sieve_id = Val.ToInt(DTab_Split.Rows[j]["sieve_id"]);
                                    objMixSplitProperty.to_sub_sieve_id = Val.ToInt(DTab_Split.Rows[j]["sub_sieve_id"]);
                                    //objMixSplitProperty.to_sub_sieve_id = Val.ToInt(387);

                                    objMixSplitProperty.to_pcs = Val.ToInt(DTab_Split.Rows[j]["trf_pcs"]);
                                    objMixSplitProperty.to_carat = Val.ToDecimal(DTab_Split.Rows[j]["trf_carat"]);
                                    p_numToStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Split.Rows[j]["assort_id"]), Val.ToInt(DTab_Split.Rows[j]["sieve_id"]));


                                    if (Val.ToDecimal(p_numToStockRate) == 0)
                                    {
                                        Global.Message("Rate not found in Master . Assort = " + DTab_Split.Rows[j]["assort_name"] + " and  Sieve = " + DTab_Split.Rows[j]["sieve_name"]);
                                        objMixSplitProperty = null;
                                        return;
                                    }

                                    objMixSplitProperty.to_rate = Val.ToDecimal(p_numToStockRate);
                                    objMixSplitProperty.to_amount = Math.Round(Val.ToDecimal(DTab_Split.Rows[j]["trf_carat"]) * Val.ToDecimal(p_numToStockRate), 3);

                                    objMixSplitProperty.mixsplit_type_id = Val.ToInt(2);
                                    objMixSplitProperty.form_id = m_numForm_id;
                                    objMixSplitProperty.transaction_type_id = Val.ToInt(2);
                                    objMixSplitProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                                    objMixSplitProperty.rate_type_id = Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id);
                                    objMixSplitProperty.user_id = Val.ToInt(GlobalDec.gEmployeeProperty.user_id);
                                    objMixSplitProperty.entry_date = Val.DBDate(GlobalDec.gStr_SystemDate);
                                    objMixSplitProperty.entry_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                    objMixSplitProperty.ip_address = Val.ToString(GlobalDec.gStrComputerIP);
                                    objMixSplitProperty.form_id = m_numForm_id;
                                    objMixSplitProperty.trn_type = Val.ToString("Mix_Split");
                                    m_TransferAmount += Val.Val(Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]) * Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]), 3));
                                    m_TransferCarat += Val.Val(DTab_Mix.Rows[i]["trf_carat"]);
                                    if (Val.ToString(RBtnType.EditValue) == "B")
                                    {
                                        objMixSplitProperty.type = Val.ToString("Balance");
                                    }
                                    else
                                    {
                                        objMixSplitProperty.type = Val.ToString("Rejection");
                                    }
                                    objMixSplitProperty.count = count;
                                    if (count == 0)
                                    {
                                        objMixSplitProperty = objMixSplit.SieveWise_Save_Mix(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                        count = 1;
                                    }
                                    else
                                    {
                                        objMixSplitProperty.count = count;
                                        objMixSplitProperty = objMixSplit.SieveWise_Save_Split(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    }
                                    //objMixSplitProperty = objMixSplit.Save(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    //int IntRes = objMixSplit.Save(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                    NewMixSplitid = Val.ToInt64(objMixSplitProperty.mixsplit_srno);
                                }
                            }
                        }
                        #endregion " Mix = 1 to Many "

                        #region "Split = 1 to Many"
                        else if (DTab_Split.Rows.Count == 1)
                        {
                            int count = 0;
                            for (int j = 0; j < DTab_Split.Rows.Count; j++)
                            {
                                decimal To_Amount = Val.ToDecimal(DTab_Mix.Compute("sum(trf_amount)", ""));

                                for (int i = 0; i < DTab_Mix.Rows.Count; i++)
                                {
                                    objMixSplitProperty = new MixSplitProperty();

                                    objMixSplitProperty.mixsplit_id = Val.ToInt64(0);
                                    objMixSplitProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                                    objMixSplitProperty.mixsplit_date = Val.DBDate(dtpEntryDate.Text);
                                    objMixSplitProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                    objMixSplitProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                                    objMixSplitProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                                    objMixSplitProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                                    objMixSplitProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                                    objMixSplitProperty.from_assort_id = Val.ToInt(DTab_Mix.Rows[i]["assort_id"]);
                                    objMixSplitProperty.from_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]);
                                    objMixSplitProperty.from_sub_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sub_sieve_id"]);
                                    //objMixSplitProperty.from_sub_sieve_id = Val.ToInt(387);
                                    objMixSplitProperty.from_pcs = Val.ToInt(DTab_Mix.Rows[i]["trf_pcs"]);
                                    objMixSplitProperty.from_carat = Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]);
                                    //p_numFromStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Mix.Rows[i]["assort_id"]), Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]));
                                    objMixSplitProperty.from_rate = Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]);
                                    objMixSplitProperty.from_amount = Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]) * Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]), 3);

                                    objMixSplitProperty.to_assort_id = Val.ToInt(DTab_Split.Rows[j]["assort_id"]); //Val.ToInt(416);
                                    objMixSplitProperty.to_sieve_id = Val.ToInt(DTab_Split.Rows[j]["sieve_id"]);
                                    objMixSplitProperty.to_sub_sieve_id = Val.ToInt(DTab_Split.Rows[j]["sub_sieve_id"]);
                                    //objMixSplitProperty.to_sub_sieve_id = Val.ToInt(387);
                                    //objMixSplitProperty.slip_no = Val.ToString(txtSlipNo.Text);

                                    if (i == 0)
                                    {

                                        objMixSplitProperty.to_pcs = Val.ToInt(DTab_Split.Rows[j]["trf_pcs"]);
                                        objMixSplitProperty.to_carat = Val.ToDecimal(DTab_Split.Rows[j]["trf_carat"]);

                                        p_numToStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Split.Rows[j]["assort_id"]), Val.ToInt(DTab_Split.Rows[j]["sieve_id"]));

                                        if (Val.ToDecimal(p_numToStockRate) == 0)
                                        {
                                            Global.Message("Rate not found in Master . Assort = " + DTab_Split.Rows[j]["assort_name"] + " and  Sieve = " + DTab_Split.Rows[j]["sieve_name"]);
                                            objMixSplitProperty = null;
                                            return;
                                        }

                                        objMixSplitProperty.to_rate = Math.Round(Val.ToDecimal(p_numToStockRate), 3);
                                        objMixSplitProperty.to_amount = Math.Round(Val.ToDecimal(p_numToStockRate) * Val.ToDecimal(DTab_Split.Rows[j]["trf_carat"]), 3);
                                    }

                                    objMixSplitProperty.mixsplit_type_id = Val.ToInt(1);
                                    objMixSplitProperty.form_id = m_numForm_id;
                                    objMixSplitProperty.transaction_type_id = Val.ToInt(2);
                                    objMixSplitProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                                    objMixSplitProperty.rate_type_id = Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id);
                                    objMixSplitProperty.user_id = Val.ToInt(GlobalDec.gEmployeeProperty.user_id);
                                    objMixSplitProperty.entry_date = Val.DBDate(GlobalDec.gStr_SystemDate);
                                    objMixSplitProperty.entry_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                    objMixSplitProperty.ip_address = Val.ToString(GlobalDec.gStrComputerIP);
                                    objMixSplitProperty.form_id = m_numForm_id;
                                    objMixSplitProperty.trn_type = Val.ToString("Mix_Split");
                                    m_TransferAmount += Val.Val(Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]) * Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]), 3));
                                    m_TransferCarat += Val.Val(DTab_Mix.Rows[i]["trf_carat"]);
                                    if (Val.ToString(RBtnType.EditValue) == "B")
                                    {
                                        objMixSplitProperty.type = Val.ToString("Balance");
                                    }
                                    else
                                    {
                                        objMixSplitProperty.type = Val.ToString("Rejection");
                                    }

                                    //objMixSplitProperty = objMixSplit.Save(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                    if (count == 0)
                                    {
                                        objMixSplitProperty.count = count;
                                        objMixSplitProperty = objMixSplit.SieveWise_Save_Split(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                        count = 1;
                                    }
                                    else
                                    {
                                        objMixSplitProperty.count = count;
                                        objMixSplitProperty = objMixSplit.SieveWise_Save_Mix(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    }

                                    NewMixSplitid = Val.ToInt64(objMixSplitProperty.mixsplit_srno);
                                }
                            }
                        }
                        #endregion "Split = 1 to Many"

                        #region "Many - to - Many"
                        else
                        {
                            for (int i = 0; i < DTab_Mix.Rows.Count; i++)
                            {
                                objMixSplitProperty = new MixSplitProperty();

                                objMixSplitProperty.mixsplit_id = Val.ToInt64(0);
                                objMixSplitProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                                objMixSplitProperty.mixsplit_date = Val.DBDate(dtpEntryDate.Text);
                                objMixSplitProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                objMixSplitProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                                objMixSplitProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                                objMixSplitProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                                objMixSplitProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                                objMixSplitProperty.from_assort_id = Val.ToInt(DTab_Mix.Rows[i]["assort_id"]);
                                objMixSplitProperty.from_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]);
                                objMixSplitProperty.from_sub_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sub_sieve_id"]);
                                //objMixSplitProperty.from_sub_sieve_id = Val.ToInt(387);
                                objMixSplitProperty.from_pcs = Val.ToInt(DTab_Mix.Rows[i]["trf_pcs"]);
                                objMixSplitProperty.from_carat = Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]);
                                //p_numFromStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Mix.Rows[i]["assort_id"]), Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]));
                                objMixSplitProperty.from_rate = Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]);
                                objMixSplitProperty.from_amount = Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]) * Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]), 3);
                                //objMixSplitProperty.to_assort_id = Val.ToInt(416);
                                objMixSplitProperty.to_assort_id = Val.ToInt(DTab_Mix.Rows[i]["assort_id"]);
                                objMixSplitProperty.to_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sieve_id"]);
                                objMixSplitProperty.to_sub_sieve_id = Val.ToInt(DTab_Mix.Rows[i]["sub_sieve_id"]);
                                //objMixSplitProperty.to_sub_sieve_id = Val.ToInt(387);
                                objMixSplitProperty.to_pcs = Val.ToInt(0);
                                objMixSplitProperty.to_carat = Val.ToDecimal(0);
                                objMixSplitProperty.to_rate = Val.ToDecimal(0);
                                objMixSplitProperty.to_amount = Val.ToDecimal(0);
                                objMixSplitProperty.mixsplit_type_id = Val.ToInt(1);
                                objMixSplitProperty.form_id = m_numForm_id;
                                objMixSplitProperty.transaction_type_id = Val.ToInt(2);
                                objMixSplitProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                                objMixSplitProperty.rate_type_id = Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id);
                                objMixSplitProperty.user_id = Val.ToInt(GlobalDec.gEmployeeProperty.user_id);
                                objMixSplitProperty.entry_date = Val.DBDate(GlobalDec.gStr_SystemDate);
                                objMixSplitProperty.entry_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                objMixSplitProperty.ip_address = Val.ToString(GlobalDec.gStrComputerIP);
                                objMixSplitProperty.form_id = m_numForm_id;
                                objMixSplitProperty.trn_type = Val.ToString("Mix");
                                //objMixSplitProperty.slip_no = Val.ToString(txtSlipNo.Text);
                                m_TransferAmount += Val.Val(Math.Round(Val.ToDecimal(DTab_Mix.Rows[i]["trf_carat"]) * Val.ToDecimal(DTab_Mix.Rows[i]["trf_rate"]), 3));
                                m_TransferCarat += Val.Val(DTab_Mix.Rows[i]["trf_carat"]);
                                if (Val.ToString(RBtnType.EditValue) == "B")
                                {
                                    objMixSplitProperty.type = Val.ToString("Balance");
                                }
                                else
                                {
                                    objMixSplitProperty.type = Val.ToString("Rejection");
                                }

                                objMixSplitProperty = objMixSplit.SieveWise_Save(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                                NewMixSplitid = Val.ToInt64(objMixSplitProperty.mixsplit_srno);
                            }

                            DTab_Split = (DataTable)grdSplitDetail.DataSource;
                            if (DTab_Split.Select("trf_carat > 0").Length > 0)
                            {
                                DTab_Split = DTab_Split.Select("trf_carat > 0").CopyToDataTable();
                                m_TransferRate = (m_TransferAmount / m_TransferCarat);

                                for (int i = 0; i < DTab_Split.Rows.Count; i++)
                                {
                                    objMixSplitProperty = new MixSplitProperty();

                                    objMixSplitProperty.mixsplit_id = Val.ToInt64(0);
                                    objMixSplitProperty.mixsplit_srno = Val.ToInt64(NewMixSplitid);
                                    objMixSplitProperty.mixsplit_date = Val.DBDate(dtpEntryDate.Text);
                                    objMixSplitProperty.mixsplit_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                    objMixSplitProperty.company_id = Val.ToInt(GlobalDec.gEmployeeProperty.company_id);
                                    objMixSplitProperty.branch_id = Val.ToInt(GlobalDec.gEmployeeProperty.branch_id);
                                    objMixSplitProperty.location_id = Val.ToInt(GlobalDec.gEmployeeProperty.location_id);
                                    objMixSplitProperty.department_id = Val.ToInt(GlobalDec.gEmployeeProperty.department_id);
                                    objMixSplitProperty.from_assort_id = Val.ToInt(416);
                                    objMixSplitProperty.from_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sieve_id"]);
                                    objMixSplitProperty.from_sub_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sub_sieve_id"]);
                                    //objMixSplitProperty.from_sub_sieve_id = Val.ToInt(387);
                                    objMixSplitProperty.to_assort_id = Val.ToInt(DTab_Split.Rows[i]["assort_id"]);
                                    objMixSplitProperty.to_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sieve_id"]);
                                    objMixSplitProperty.to_sub_sieve_id = Val.ToInt(DTab_Split.Rows[i]["sub_sieve_id"]);
                                    //objMixSplitProperty.to_sub_sieve_id = Val.ToInt(387);
                                    objMixSplitProperty.from_pcs = Val.ToInt(0);
                                    objMixSplitProperty.from_carat = Val.ToDecimal(0);
                                    //p_numFromStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Split.Rows[i]["assort_id"]), Val.ToInt(DTab_Split.Rows[i]["sieve_id"]));
                                    objMixSplitProperty.from_rate = Val.ToDecimal(0);
                                    objMixSplitProperty.from_amount = Val.ToDecimal(0);
                                    //objMixSplitProperty.slip_no = Val.ToString(txtSlipNo.Text);
                                    objMixSplitProperty.to_pcs = Val.ToInt(DTab_Split.Rows[i]["trf_pcs"]);
                                    objMixSplitProperty.to_carat = Val.ToDecimal(DTab_Split.Rows[i]["trf_carat"]);
                                    p_numToStockRate = objBranch.GetLetestPrice(Val.ToInt(DTab_Split.Rows[i]["assort_id"]), Val.ToInt(DTab_Split.Rows[i]["sieve_id"]));

                                    if (Val.ToDecimal(p_numToStockRate) == 0)
                                    {
                                        Global.Message("Rate not found in Master . Assort = " + DTab_Split.Rows[i]["assort_name"] + " and  Sieve = " + DTab_Split.Rows[i]["sieve_name"]);
                                        objMixSplitProperty = null;
                                        return;
                                    }

                                    objMixSplitProperty.to_rate = Val.ToDecimal(p_numToStockRate);
                                    objMixSplitProperty.to_amount = Math.Round(Val.ToDecimal(DTab_Split.Rows[i]["trf_carat"]) * Val.ToDecimal(Val.Val(p_numToStockRate)), 3);
                                    objMixSplitProperty.mixsplit_type_id = Val.ToInt(2);
                                    objMixSplitProperty.form_id = m_numForm_id;
                                    objMixSplitProperty.transaction_type_id = Val.ToInt(2);
                                    objMixSplitProperty.currency_id = Val.ToInt(GlobalDec.gEmployeeProperty.currency_id);
                                    objMixSplitProperty.rate_type_id = Val.ToInt(GlobalDec.gEmployeeProperty.rate_type_id);
                                    objMixSplitProperty.user_id = Val.ToInt(GlobalDec.gEmployeeProperty.user_id);
                                    objMixSplitProperty.entry_date = Val.DBDate(GlobalDec.gStr_SystemDate);
                                    objMixSplitProperty.entry_time = Val.ToString(GlobalDec.gStr_SystemTime);
                                    objMixSplitProperty.ip_address = Val.ToString(GlobalDec.gStrComputerIP);
                                    objMixSplitProperty.form_id = m_numForm_id;
                                    objMixSplitProperty.trn_type = Val.ToString("Split");
                                    objMixSplitProperty.loss_carat = Val.ToDecimal(DTab_Split.Rows[i]["weight_loss"]);
                                    objMixSplitProperty.carat_plus = Val.ToDecimal(DTab_Split.Rows[i]["weight_plus"]);

                                    if (Val.ToString(RBtnType.EditValue) == "B")
                                    {
                                        objMixSplitProperty.type = Val.ToString("Balance");
                                    }
                                    else
                                    {
                                        objMixSplitProperty.type = Val.ToString("Rejection");
                                    }

                                    IntRes = objMixSplit.SieveWise_Save_New(objMixSplitProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }

                            else
                            {
                                Global.Message("No Any Split Data Found in Grid");
                                IntRes = 0;
                                return;
                            }
                        }
                        #endregion "Many - to Many"
                    }
                    else
                    {
                        Global.Message("No Any Mixing Data Found in Grid");
                        IntRes = 0;
                        return;
                    }

                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        Conn.Inter2.Commit();
                    }
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        Conn.Inter1.Rollback();
                    }
                    else
                    {
                        Conn.Inter2.Rollback();
                    }
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    //objMixSplitProperty = null;
                }
                //}
                //else
                //{
                //    Global.Message("Carat doesn't tally. Please verify!");
                //    IntRes = 0;
                //    return;
                //}
            }
            #endregion "Balance"
        }
        private void backgroundWorker_MixSplit_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0 || objMixSplitProperty != null)
                {
                    Global.Message("Data Save Successfully");
                    ClearDetails();
                }
                else if (IntRes == -1 || objMixSplitProperty == null)
                {
                    Global.Confirm("Error In Mix Split");
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region "Grid Events" 
        private void dgvBranchDetails_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                m_numTotalCarats = Math.Round(Val.ToDecimal(clmCarats.SummaryItem.SummaryValue), 3, MidpointRounding.AwayFromZero);

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        m_numTotalAmount = 0;
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        m_numTotalAmount += (Val.ToDecimal(e.GetValue("carat")) * Val.ToDecimal(e.GetValue("rate")));
                    else if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                    {
                        if (m_numTotalAmount > 0 && m_numTotalCarats > 0)
                            e.TotalValue = Math.Round((m_numTotalAmount / m_numTotalCarats), 3, MidpointRounding.AwayFromZero);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvMixDetails_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                CalculateGridAmount(dgvMixDetails.FocusedRowHandle);
                GridView view = sender as GridView;

                if (Val.ToString(RBtnType.EditValue) == "B")
                {
                    if (view.FocusedColumn.FieldName == "trf_carat")
                    {
                        string brd = e.Value as string;
                        if (Val.ToDouble(brd) > Val.ToDouble(m_Balance_Carat))
                        {
                            e.Valid = false;
                            e.ErrorText = "Transfer Carat not more then Balance Carat.";
                        }
                    }
                }
                else
                {
                    if (view.FocusedColumn.FieldName == "trf_carat")
                    {
                        string brd = e.Value as string;
                        if (Val.ToDouble(brd) > Val.ToDouble(m_Rejection_Carat))
                        {
                            e.Valid = false;
                            e.ErrorText = "Transfer Carat not more then Rejection Carat.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void dgvSplitDetails_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                CalculateGridSplitAmount(dgvSplitDetails.FocusedRowHandle);
                GridView view = sender as GridView;

                if (Val.ToString(RBtnType.EditValue) == "B")
                {
                    if (view.FocusedColumn.FieldName == "weight_loss")
                    {
                        string brd = e.Value as string;
                        if (Val.ToDouble(brd) > Val.ToDouble(m_Balance_Carat))
                        {
                            e.Valid = false;
                            e.ErrorText = "weight loss not more then trf carat.";
                        }
                    }
                }
                else
                {
                    if (view.FocusedColumn.FieldName == "weight_loss")
                    {
                        string brd = e.Value as string;
                        if (Val.ToDouble(brd) > Val.ToDouble(m_Balance_Carat))
                        {
                            e.Valid = false;
                            e.ErrorText = "weight loss not more then trf carat.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void dgvMixDetails_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(dgvMixDetails.FocusedRowHandle);
        }

        private void dgvMixDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
        }

        private void dgvSplitDetails_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            decimal TrfCarat = 0;
            decimal WeightLoss = 0;
            decimal WeightPlus = 0;
            decimal Total = 0;
            if (e.Column.Caption == "Trf Carat")
            {
                TrfCarat = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "trf_carat"));
                WeightLoss = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "weight_loss"));
                WeightPlus = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "weight_plus"));
                Total = Val.ToDecimal(TrfCarat + WeightPlus) - Val.ToDecimal(WeightLoss);
                view.SetRowCellValue(e.RowHandle, view.Columns["total"], Total);
                //break;
            }
            if (e.Column.Caption == "Weight Loss")
            {
                TrfCarat = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "trf_carat"));
                WeightLoss = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "weight_loss"));
                WeightPlus = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "weight_plus"));
                Total = Val.ToDecimal(TrfCarat - Val.ToDecimal(WeightLoss) + Val.ToDecimal(WeightPlus));
                view.SetRowCellValue(e.RowHandle, view.Columns["total"], Total);
                //break;
            }
            if (e.Column.Caption == "Weight Plus")
            {
                TrfCarat = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "trf_carat"));
                WeightLoss = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "weight_loss"));
                WeightPlus = Val.ToDecimal(view.GetRowCellValue(e.RowHandle, "weight_plus"));
                Total = Val.ToDecimal(TrfCarat + WeightPlus) - Val.ToDecimal(WeightLoss);
                view.SetRowCellValue(e.RowHandle, view.Columns["total"], Total);
                //break;
            }

        }

        #endregion

        #endregion

        #region Functions   

        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                RBtnType.SelectedIndex = 1;

                m_dtbAssorts = objAssort.GetData(1);
                lueAssortName.Properties.DataSource = m_dtbAssorts;
                lueAssortName.Properties.ValueMember = "assort_id";
                lueAssortName.Properties.DisplayMember = "assort_name";

                m_dtbSieve = objSieve.GetData(1);
                lueSieveName.Properties.DataSource = m_dtbSieve;
                lueSieveName.Properties.ValueMember = "sieve_id";
                lueSieveName.Properties.DisplayMember = "sieve_name";

                if (!GenerateSaleInvoiceDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;
                lueAssortName.EditValue = DBNull.Value;
                lueSieveName.EditValue = DBNull.Value;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private bool GenerateSaleInvoiceDetails()
        {
            bool blnReturn = true;
            //try
            //{
            //    if (Val.ToString(RBtnType.EditValue) == "R")
            //    {
            //        m_dtbMixDetails.Rows.Clear();
            //        m_dtbMixDetails = objMixSplit.GetMixStock(1);
            //        dgvMixDetails.Columns["balance_pcs"].Visible = false;
            //        dgvMixDetails.Columns["balance_carat"].Visible = false;
            //        dgvMixDetails.Columns["rej_pcs"].Visible = true;
            //        dgvMixDetails.Columns["rej_carat"].Visible = true;
            //        dgvMixDetails.Columns["trf_pcs"].Visible = true;
            //        dgvMixDetails.Columns["trf_carat"].Visible = true;
            //        dgvMixDetails.Columns["assort_name"].VisibleIndex = 1;
            //        dgvMixDetails.Columns["sieve_name"].VisibleIndex = 2;
            //        dgvMixDetails.Columns["sub_sieve_name"].VisibleIndex = 3;
            //        dgvMixDetails.Columns["rej_pcs"].VisibleIndex = 4;
            //        dgvMixDetails.Columns["rej_carat"].VisibleIndex = 5;
            //        dgvMixDetails.Columns["trf_pcs"].VisibleIndex = 6;
            //        dgvMixDetails.Columns["trf_carat"].VisibleIndex = 7;

            //        if (m_dtbMixDetails.Select("rej_carat > 0").Length > 0)
            //        {
            //            grdMixDetail.DataSource = m_dtbMixDetails.Select("rej_carat > 0").CopyToDataTable();
            //            grdMixDetail.Refresh();
            //            dgvMixDetails.BestFitColumns();
            //        }
            //        //grdMixDetail.DataSource = m_dtbMixDetails;
            //        //grdMixDetail.Refresh();
            //        //dgvMixDetails.BestFitColumns();

            //        m_dtbSplitDetails.Rows.Clear();
            //        m_dtbSplitDetails = objMixSplit.GetMixStock_All(1);
            //        dgvSplitDetails.Columns["balance_pcs"].Visible = false;
            //        dgvSplitDetails.Columns["balance_carat"].Visible = false;
            //        dgvSplitDetails.Columns["trf_pcs"].Visible = true;
            //        dgvSplitDetails.Columns["trf_carat"].Visible = true;
            //        dgvSplitDetails.Columns["assort_name"].VisibleIndex = 1;
            //        dgvSplitDetails.Columns["sieve_name"].VisibleIndex = 2;
            //        dgvSplitDetails.Columns["sub_sieve_name"].VisibleIndex = 3;
            //        dgvSplitDetails.Columns["trf_pcs"].VisibleIndex = 4;
            //        dgvSplitDetails.Columns["trf_carat"].VisibleIndex = 5;
            //        //dgvSplitDetails.Columns["total"].VisibleIndex = 6;
            //        grdSplitDetail.DataSource = m_dtbSplitDetails;
            //        grdSplitDetail.Refresh();
            //        dgvSplitDetails.BestFitColumns();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    blnReturn = false;
            //}
            return blnReturn;
        }
        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                if (Val.ToString(RBtnType.EditValue) == "B")
                {
                    m_Balance_Carat = Math.Round(Val.ToDouble(dgvMixDetails.GetRowCellValue(rowindex, "balance_carat")), 3);
                    double Trf_Carat = Math.Round(Val.ToDouble(dgvMixDetails.GetRowCellValue(rowindex, "trf_carat")) * Val.ToDouble(dgvMixDetails.GetRowCellValue(rowindex, "trf_rate")), 3);
                    if (Trf_Carat != 0)
                    {
                        dgvMixDetails.SetRowCellValue(rowindex, "trf_amount", Math.Round(Val.ToDouble(dgvMixDetails.GetRowCellValue(rowindex, "trf_carat")) * Val.ToDouble(dgvMixDetails.GetRowCellValue(rowindex, "trf_rate")), 0));
                    }
                }
                else
                {
                    m_Rejection_Carat = Math.Round(Val.ToDouble(dgvMixDetails.GetRowCellValue(rowindex, "rej_carat")), 3);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        private void CalculateGridSplitAmount(int rowindex)
        {
            try
            {
                if (Val.ToString(RBtnType.EditValue) == "B")
                {
                    m_Balance_Carat = Math.Round(Val.ToDouble(dgvSplitDetails.GetRowCellValue(rowindex, "trf_carat")), 3);
                }
                else
                {
                    m_Rejection_Carat = Math.Round(Val.ToDouble(dgvSplitDetails.GetRowCellValue(rowindex, "rej_carat")), 3);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        #endregion

        private void FrmMixSplitSubSieveWise_Load(object sender, EventArgs e)
        {
            try
            {
                ClearDetails();


            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(RBtnType.EditValue) == "B")
                {
                    grdMixDetail.DataSource = null;
                    grdMixDetail.Refresh();
                    dgvMixDetails.BestFitColumns();
                    grdSplitDetail.DataSource = null;
                    grdSplitDetail.Refresh();
                    dgvSplitDetails.BestFitColumns();
                    m_dtbMixDetails.Rows.Clear();
                    MixSplitProperty objMixSplitProperty = new MixSplitProperty();
                    objMixSplitProperty.sieve_id = Val.ToInt32(lueSieveName.EditValue);
                    objMixSplitProperty.assort_id = Val.ToInt32(lueAssortName.EditValue);
                    m_dtbMixDetails = objMixSplit.GetMixStock(1, objMixSplitProperty);
                    if (m_dtbMixDetails.Rows.Count > 0)
                    {
                        txtCarat.Text = Val.ToDecimal(m_dtbMixDetails.Rows[0]["balance_carat"]).ToString();
                        txtRate.Text = Val.ToDecimal(m_dtbMixDetails.Rows[0]["trf_rate"]).ToString();
                    }
                    dgvMixDetails.Columns["balance_pcs"].Visible = false;
                    dgvMixDetails.Columns["balance_carat"].Visible = false;
                    dgvMixDetails.Columns["trf_pcs"].Visible = false;
                    dgvMixDetails.Columns["trf_carat"].Visible = true;
                    dgvMixDetails.Columns["trf_rate"].Visible = true;
                    dgvMixDetails.Columns["trf_amount"].Visible = true;
                    dgvMixDetails.Columns["rej_pcs"].Visible = false;
                    dgvMixDetails.Columns["rej_carat"].Visible = false;
                    dgvMixDetails.Columns["assort_name"].VisibleIndex = 1;
                    dgvMixDetails.Columns["sieve_name"].VisibleIndex = 2;
                    //dgvMixDetails.Columns["balance_pcs"].VisibleIndex = 3;
                    //dgvMixDetails.Columns["balance_carat"].VisibleIndex = 3;
                    //dgvMixDetails.Columns["trf_pcs"].VisibleIndex = 5;
                    dgvMixDetails.Columns["trf_carat"].VisibleIndex = 3;
                    dgvMixDetails.Columns["trf_rate"].VisibleIndex = 4;
                    dgvMixDetails.Columns["trf_amount"].VisibleIndex = 5;
                    dgvMixDetails.OptionsBehavior.Editable = true;

                    if (m_dtbMixDetails.Select("balance_carat > 0").Length > 0)
                    {
                        DataTable p_dtbDetail = m_dtbMixDetails.Select("balance_carat > 0").CopyToDataTable();
                        p_dtbDetail.DefaultView.Sort = "sieve_sequence_no,assort_sequence_no";

                        grdMixDetail.DataSource = p_dtbDetail;
                        m_dtbMixDetails.AcceptChanges();
                        grdMixDetail.Refresh();
                        dgvMixDetails.BestFitColumns();
                    }
                    else
                    {
                        Global.Message("Rejection Data Not Found");
                        grdMixDetail.DataSource = null;
                        grdMixDetail.Refresh();
                        dgvMixDetails.BestFitColumns();
                        grdSplitDetail.DataSource = null;
                        grdSplitDetail.Refresh();
                        dgvSplitDetails.BestFitColumns();
                        return;
                    }

                    m_dtbSplitDetails.Rows.Clear();
                    m_dtbSplitDetails = objMixSplit.GetMixStock_All(1, objMixSplitProperty);
                    dgvSplitDetails.Columns["trf_pcs"].Visible = false;
                    dgvSplitDetails.Columns["trf_carat"].Visible = true;
                    dgvSplitDetails.Columns["assort_name"].VisibleIndex = 1;
                    dgvSplitDetails.Columns["sieve_name"].VisibleIndex = 2;
                    //dgvSplitDetails.Columns["trf_pcs"].VisibleIndex = 3;
                    dgvSplitDetails.Columns["trf_carat"].VisibleIndex = 3;

                    grdSplitDetail.DataSource = m_dtbSplitDetails;
                    grdSplitDetail.Refresh();
                    dgvSplitDetails.BestFitColumns();
                }
                else if (Val.ToString(RBtnType.EditValue) == "R")
                {
                    grdMixDetail.DataSource = null;
                    grdMixDetail.Refresh();
                    dgvMixDetails.BestFitColumns();
                    grdSplitDetail.DataSource = null;
                    grdSplitDetail.Refresh();
                    dgvSplitDetails.BestFitColumns();
                    m_dtbMixDetails.Rows.Clear();
                    MixSplitProperty objMixSplitProperty = new MixSplitProperty();
                    objMixSplitProperty.sieve_id = Val.ToInt32(lueSieveName.EditValue);
                    objMixSplitProperty.assort_id = Val.ToInt32(lueAssortName.EditValue);
                    m_dtbMixDetails = objMixSplit.GetMixStock(1, objMixSplitProperty);

                    if (m_dtbMixDetails.Rows.Count > 0)
                    {
                        txtCarat.Text = Val.ToDecimal(m_dtbMixDetails.Rows[0]["rej_carat"]).ToString();
                        txtRate.Text = Val.ToDecimal(0).ToString();
                    }

                    dgvMixDetails.Columns["balance_pcs"].Visible = false;
                    dgvMixDetails.Columns["balance_carat"].Visible = false;
                    dgvMixDetails.Columns["rej_pcs"].Visible = false;
                    dgvMixDetails.Columns["rej_carat"].Visible = false;
                    dgvMixDetails.Columns["trf_pcs"].Visible = false;
                    dgvMixDetails.Columns["trf_carat"].Visible = true;
                    dgvMixDetails.Columns["trf_rate"].Visible = false;
                    dgvMixDetails.Columns["trf_amount"].Visible = false;
                    dgvMixDetails.Columns["assort_name"].VisibleIndex = 1;
                    dgvMixDetails.Columns["sieve_name"].VisibleIndex = 2;
                    //dgvMixDetails.Columns["rej_pcs"].VisibleIndex = 3;
                    //dgvMixDetails.Columns["rej_carat"].VisibleIndex = 3;
                    //dgvMixDetails.Columns["trf_pcs"].VisibleIndex = 5;
                    dgvMixDetails.Columns["trf_carat"].VisibleIndex = 3;
                    dgvMixDetails.OptionsBehavior.Editable = true;

                    if (m_dtbMixDetails.Select("rej_carat > 0").Length > 0)
                    {
                        grdMixDetail.DataSource = m_dtbMixDetails.Select("rej_carat > 0").CopyToDataTable();
                        grdMixDetail.Refresh();
                        dgvMixDetails.BestFitColumns();
                    }
                    else
                    {
                        Global.Message("Rejection Data Not Found");
                        grdMixDetail.DataSource = null;
                        grdMixDetail.Refresh();
                        dgvMixDetails.BestFitColumns();
                        grdSplitDetail.DataSource = null;
                        grdSplitDetail.Refresh();
                        dgvSplitDetails.BestFitColumns();
                        return;
                    }

                    m_dtbSplitDetails.Rows.Clear();
                    m_dtbSplitDetails = objMixSplit.GetMixStock_All(1, objMixSplitProperty);
                    dgvSplitDetails.Columns["trf_pcs"].Visible = false;
                    dgvSplitDetails.Columns["trf_carat"].Visible = true;
                    dgvSplitDetails.Columns["assort_name"].VisibleIndex = 1;
                    dgvSplitDetails.Columns["sieve_name"].VisibleIndex = 2;
                    // dgvSplitDetails.Columns["trf_pcs"].VisibleIndex = 3;
                    dgvSplitDetails.Columns["trf_carat"].VisibleIndex = 3;

                    grdSplitDetail.DataSource = m_dtbSplitDetails;
                    grdSplitDetail.Refresh();
                    dgvSplitDetails.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
    }
}