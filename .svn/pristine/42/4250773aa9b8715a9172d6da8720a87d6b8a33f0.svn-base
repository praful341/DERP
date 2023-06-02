using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DREP.Master.MFG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGProcessChipiyoRecieve : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbSubProcess = new DataTable();
        MFGProcessReceive objProcessReceive;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;

        public New_Report_DetailProperty ObjReportDetailProperty;
        private List<Control> _tabControls = new List<Control>();
        Control _NextEnteredControl = new Control();

        DataTable DtControlSettings;
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbType;
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;
        // DataTable DTab_KapanWiseData = new DataTable();
        int Search = 0;

        int m_kapan_id;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Receive_IntRes;
        int Loss_Count = 0;
        int m_manager_id;
        int m_emp_id;
        int m_IsLot;
        string m_process = "";

        decimal m_OsCarat;
        decimal m_rate;
        decimal m_balcarat;
        int m_balPcs;
        int m_issue_id;

        #endregion

        #region Constructor
        public FrmMFGProcessChipiyoRecieve()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objProcessReceive = new MFGProcessReceive();
            objRoughSieve = new MfgRoughSieve();
            objClarity = new MfgRoughClarityMaster();
            ObjReportDetailProperty = new New_Report_DetailProperty();

            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            m_numForm_id = 0;
            m_balcarat = 0;
            m_balPcs = 0;
            m_IsLot = 0;
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
            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            // AddKeyPressListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                string Str = "";
                if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpReceiveDate.Text))
                {
                    Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpReceiveDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
                    if (Str != "YES")
                    {
                        if (Str != "")
                        {
                            Global.Message(Str);
                            return;
                        }
                        else
                        {
                            Global.Message("You Are Not Suppose to Make Entry On Different Date");
                            return;
                        }
                    }
                    else
                    {
                        dtpReceiveDate.Enabled = true;
                        dtpReceiveDate.Visible = true;
                    }
                }

                //btnSave.Enabled = false;

                if (!ValidateDetails())
                {
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save Process Chipyo Receive data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_ProcessChipyoReceive.RunWorkerAsync();

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            //grdProcessReceive.DataSource = null;
            //btnSearch_Click(null, null);
            ClearDetails();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;
                lueProcess.EditValue = System.DBNull.Value;
                lueSubProcess.EditValue = System.DBNull.Value;

                for (int i = 0; i < lueClarity.Properties.Items.Count; i++)
                    lueClarity.Properties.Items[i].CheckState = CheckState.Unchecked;

                for (int i = 0; i < lueSieve.Properties.Items.Count; i++)
                    lueSieve.Properties.Items[i].CheckState = CheckState.Unchecked;

                txtWeightLoss.Text = "0";
                txtWeightPlus.Text = "0";
                //txtBalanceCarat.Text = "0";
                //txtIssProcess.Text = "";
                txtLotId.Text = "0";
                grdProcessReceive.DataSource = null;
                dgvProcessReceive.Columns.Clear();
                m_kapan_id = 0;
                lueKapan.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search = 1;
                if (!ValidateDetails())
                {
                    return;
                }
                DataTable dtTemp_old = (DataTable)grdProcessReceive.DataSource;

                //dtTemp = new DataTable();
                dgvProcessReceive.Columns.Clear();
                //grdProcessReceive.DataSource = dtTemp;
                pivot pt = new pivot(objProcessReceive.Process_Chipyo_Rec_GetData(Val.ToString(lueType.Text), Val.ToString(lueClarity.EditValue), Val.ToString(lueSieve.EditValue)));
                DataTable dtbDetails = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity" }, new string[] { "pcs", "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });
                //m_dtbDetail = objProcessReceive.GetData(); 
                DataColumn Total_Pcs = new System.Data.DataColumn("T_Pcs", typeof(System.Int32));
                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                Total_Pcs.DefaultValue = "0";
                Total.DefaultValue = "0.000";
                Rate.DefaultValue = "0.000";
                Amount.DefaultValue = "0.000";
                dtbDetails.Columns.Add(Total_Pcs);
                dtbDetails.Columns.Add(Total);
                dtbDetails.Columns.Add(Rate);
                dtbDetails.Columns.Add(Amount);
                //DataColumnCollection columns = dtTemp.Columns;

                //if (dtTemp_old != null)
                //{
                //    if (dtTemp_old.Columns.Count > 0)
                //    {
                //        for (int i = 0; i < dtTemp_old.Rows.Count; i++)
                //        {
                //            for (int j = 0; j < dtTemp_old.Columns.Count; j++)
                //            {
                //                for (int k = 0; k < dtTemp.Rows.Count; k++)
                //                {
                //                    if (dtTemp.Rows[k]["clarity"].ToString().Trim() == dtTemp_old.Rows[i]["clarity"].ToString().Trim())
                //                    {
                //                        if (columns.Contains(dtTemp_old.Columns[j].ColumnName.ToString().Trim()))
                //                        {
                //                            dtTemp.Rows[k][dtTemp_old.Columns[j].ColumnName.ToString().Trim()] = dtTemp_old.Rows[i][dtTemp_old.Columns[j].ColumnName.ToString().Trim()];
                //                        }
                //                    }
                //                }

                //            }
                //        }

                //    }
                //}

                //  Add By Praful On 19062019 Total Summary
                Int32 pcs = 0;
                decimal rate = 0;
                decimal carat = 0;
                Int32 total_pcs = 0;
                decimal total = 0;
                decimal totRate = 0;
                decimal totAmount = 0;
                for (int i = 0; i <= dtbDetails.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtbDetails.Columns.Count - 1; j++)
                    {
                        if (dtbDetails.Columns[j].ToString().Contains("pcs"))
                        {
                            pcs = Val.ToInt32(dtbDetails.Rows[i][j]);
                            total_pcs += pcs;
                        }
                        if (dtbDetails.Columns[j].ToString().Contains("carat"))
                        {
                            carat = Val.ToDecimal(dtbDetails.Rows[i][j]);
                            total += carat;
                        }
                        else if (dtbDetails.Columns[j].ToString().Contains("rate"))
                        {
                            rate = Val.ToDecimal(dtbDetails.Rows[i][j]);
                        }
                        else if (dtbDetails.Columns[j].ToString().Contains("amount"))
                        {
                            dtbDetails.Rows[i][j] = Math.Round((carat * rate), 0).ToString();
                            totAmount += (carat * rate);
                        }
                        if (dtbDetails.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            dtbDetails.Rows[i][j] = Val.ToInt32(total_pcs);

                        }
                        if (dtbDetails.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtbDetails.Rows[i][j] = Math.Round(total, 3).ToString();
                        }
                        else if (dtbDetails.Columns[j].ColumnName.Contains("Rate"))
                        {
                            if (totAmount != 0 && total != 0)
                            {
                                totRate = totAmount / total;
                            }
                            else
                            {
                                totRate = 0;
                            }
                            dtbDetails.Rows[i][j] = Math.Round(totRate, 2).ToString();

                        }
                        else if (dtbDetails.Columns[j].ColumnName.Contains("Amount"))
                        {
                            decimal amount = Math.Round((total * totRate), 0);
                            dtbDetails.Rows[i][j] = amount.ToString();
                            break;
                        }
                    }
                    total_pcs = 0;
                    totAmount = 0;
                    total = 0;
                    totRate = 0;
                }

                DataColumnCollection columns = dtbDetails.Columns;
                if (dtTemp_old != null)
                {
                    if (dtTemp_old.Columns.Count > 0)
                    {
                        for (int i = 0; i < dtTemp_old.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtTemp_old.Columns.Count; j++)
                            {
                                for (int k = 0; k < dtbDetails.Rows.Count; k++)
                                {
                                    if (dtbDetails.Rows[k]["clarity"].ToString().Trim() == dtTemp_old.Rows[i]["clarity"].ToString().Trim())
                                    {
                                        if (columns.Contains(dtTemp_old.Columns[j].ColumnName.ToString().Trim()))
                                        {
                                            dtbDetails.Rows[k][dtTemp_old.Columns[j].ColumnName.ToString().Trim()] = dtTemp_old.Rows[i][dtTemp_old.Columns[j].ColumnName.ToString().Trim()];
                                        }
                                    }
                                }

                            }
                        }

                    }
                }
                dtTemp = dtbDetails.Copy();
                grdProcessReceive.DataSource = dtTemp;

                dgvProcessReceive.Columns["clarity_id"].Visible = false;
                dgvProcessReceive.Columns["clarity"].OptionsColumn.ReadOnly = true;
                dgvProcessReceive.Columns["clarity"].OptionsColumn.AllowFocus = false;
                dgvProcessReceive.Columns["T_Pcs"].OptionsColumn.ReadOnly = true;
                dgvProcessReceive.Columns["T_Pcs"].OptionsColumn.AllowFocus = false;
                dgvProcessReceive.Columns["Total"].OptionsColumn.ReadOnly = true;
                dgvProcessReceive.Columns["Total"].OptionsColumn.AllowFocus = false;
                dgvProcessReceive.Columns["Rate"].OptionsColumn.ReadOnly = true;
                dgvProcessReceive.Columns["Rate"].OptionsColumn.AllowFocus = false;
                dgvProcessReceive.Columns["Amount"].OptionsColumn.ReadOnly = true;
                dgvProcessReceive.Columns["Amount"].OptionsColumn.AllowFocus = false;
                dgvProcessReceive.Columns["clarity"].Fixed = FixedStyle.Left;
                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dgvProcessReceive.Columns[j].OptionsColumn.AllowEdit = false;
                        }
                    }
                }

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("pcs"))
                        {
                            string pcs1 = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvProcessReceive.Columns[pcs1];
                            dgvProcessReceive.Columns[pcs1].SummaryItem.DisplayFormat = "{0:n0}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string carat1 = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvProcessReceive.Columns[carat1];
                            dgvProcessReceive.Columns[carat1].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            string rate1 = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvProcessReceive.Columns[rate1];
                            dgvProcessReceive.Columns[rate1].SummaryItem.DisplayFormat = " {0:n3}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            string amount1 = dtTemp.Columns[j].ToString();
                            GridColumn column3 = dgvProcessReceive.Columns[amount1];
                            dgvProcessReceive.Columns[amount1].SummaryItem.DisplayFormat = "{0:n3}";
                            column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            string total_pcs1 = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvProcessReceive.Columns[total_pcs1];
                            dgvProcessReceive.Columns[total_pcs1].SummaryItem.DisplayFormat = "{0:n0}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            string total1 = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvProcessReceive.Columns[total1];
                            dgvProcessReceive.Columns[total1].SummaryItem.DisplayFormat = "{0:n3}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Rate"))
                        {
                            string totrate = dtTemp.Columns[j].ToString();
                            GridColumn column5 = dgvProcessReceive.Columns[totrate];
                            dgvProcessReceive.Columns[totrate].SummaryItem.DisplayFormat = "{0:n3}";
                            column5.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Amount"))
                        {
                            string totamount = dtTemp.Columns[j].ToString();
                            GridColumn column6 = dgvProcessReceive.Columns[totamount];
                            dgvProcessReceive.Columns[totamount].SummaryItem.DisplayFormat = "{0:n3}";
                            column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                    }
                    break;
                }

                //for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                //{
                //    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                //    {
                //        if (dtTemp.Columns[j].ToString().Contains("carat"))
                //        {
                //            string carat = dtTemp.Columns[j].ToString();
                //            GridColumn column1 = dgvProcessReceive.Columns[carat];
                //            dgvProcessReceive.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                //            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                //        }

                //        //if (dtTemp.Columns[j].ToString().Contains("rate"))
                //        //{
                //        //    string rate = dtTemp.Columns[j].ToString();
                //        //    GridColumn column2 = dgvProcessReceive.Columns[rate];
                //        //    dgvProcessReceive.Columns[rate].SummaryItem.DisplayFormat = " {0:n3}";
                //        //    column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                //        //}

                //        //if (dtTemp.Columns[j].ToString().Contains("amount"))
                //        //{
                //        //    string amount = dtTemp.Columns[j].ToString();
                //        //    GridColumn column3 = dgvProcessReceive.Columns[amount];
                //        //    dgvProcessReceive.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                //        //    column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                //        //}
                //        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                //        {
                //            string total = dtTemp.Columns[j].ToString();
                //            GridColumn column4 = dgvProcessReceive.Columns[total];
                //            dgvProcessReceive.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                //            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                //        }
                //        //if (dtTemp.Columns[j].ColumnName.Contains("Rate"))
                //        //{
                //        //    string totrate = dtTemp.Columns[j].ToString();
                //        //    GridColumn column5 = dgvProcessReceive.Columns[totrate];
                //        //    dgvProcessReceive.Columns[totrate].SummaryItem.DisplayFormat = "{0:n3}";
                //        //    column5.SummaryItem.SummaryType = SummaryItemType.Custom;
                //        //}
                //        //if (dtTemp.Columns[j].ColumnName.Contains("Amount"))
                //        //{
                //        //    string totamount = dtTemp.Columns[j].ToString();
                //        //    GridColumn column6 = dgvProcessReceive.Columns[totamount];
                //        //    dgvProcessReceive.Columns[totamount].SummaryItem.DisplayFormat = "{0:n3}";
                //        //    column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                //        //}
                //    }
                //    break;
                //}
                dgvProcessReceive.OptionsView.ShowFooter = true;
                //ShowSummary();
                dgvProcessReceive.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                //blnReturn = false;
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                Search = 1;
                if (!ValidateDetails())
                {
                    return;
                }
                DataTable dtList;
                pivot pt = new pivot(objProcessReceive.GetShowData(Val.ToString(lueType.Text)));
                dtList = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });
                //m_dtbDetail = objProcessReceive.GetData();

                grdProcessReceive.DataSource = dtList;

                dgvProcessReceive.Columns["clarity_id"].Visible = false;

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dgvProcessReceive.Columns[j].OptionsColumn.AllowEdit = false;
                        }
                    }
                }

                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string carat = dtTemp.Columns[j].ToString();
                            GridColumn column1 = dgvProcessReceive.Columns[carat];
                            dgvProcessReceive.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            string rate = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvProcessReceive.Columns[rate];
                            dgvProcessReceive.Columns[rate].SummaryItem.DisplayFormat = " {0:n3}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            string amount = dtTemp.Columns[j].ToString();
                            GridColumn column3 = dgvProcessReceive.Columns[amount];
                            dgvProcessReceive.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                            column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                    }
                    break;
                }

                dgvProcessReceive.OptionsView.ShowFooter = true;
                //ShowSummary();
                dgvProcessReceive.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                //blnReturn = false;
            }
        }
        DataTable dtIssOS = new DataTable();
        private void lueCutNo_Validated(object sender, EventArgs e)
        {
            try
            {

                if (m_IsLot == 0)
                {
                    if (lueCutNo.EditValue != null)
                    {
                        if (m_dtbParam.Rows.Count > 0)
                        {
                            DataRow[] dr = m_dtbParam.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");

                            txtLotId.Text = Val.ToString(dr[0]["lot_id"]);

                            MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                            if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
                            {
                                GetOsCarat(Val.ToInt64(txtLotId.Text));
                                dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt(0), Val.ToInt(0), 1, "R");
                                if (dtIssOS.Rows.Count > 0)
                                {
                                    DataRow[] drProcess = dtIssOS.Select("process_name ='" + Val.ToString("CHIPIYO") + "'");
                                    if (drProcess.Length > 0)
                                    {
                                        lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                        lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                        m_process = Val.ToString(drProcess[0]["process_name"]);
                                        lueProcess.Text = Val.ToString(drProcess[0]["process_name"]);
                                        lueSubProcess.Text = Val.ToString(drProcess[0]["sub_process_name"]);
                                        m_balPcs = Val.ToInt(drProcess[0]["pcs"]);
                                        m_balcarat = Val.ToDecimal(drProcess[0]["carat"]);
                                        lueProcess.Enabled = false;
                                        lueSubProcess.Enabled = false;
                                        //m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotId.Text), "CHIPIYO");
                                        //if (m_DtProcess.Rows.Count > 0)
                                        //{
                                        //    m_rate = Val.ToDecimal(m_DtProcess.Rows[0]["rate"]);
                                        //}
                                    }
                                    else
                                    {
                                        Global.Message("Lot not issue in Chipiyo Process");
                                        lueCutNo.Focus();
                                        return;
                                    }

                                }
                                else
                                {
                                    Global.Message("Lot Not Issue");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {


            //try
            //{
            //    if (!m_blnflag)
            //    {
            //        if (lueCutNo.EditValue != System.DBNull.Value)
            //        {
            //            if (m_dtCut.Rows.Count > 0)
            //            {
            //                if (Val.ToString(lueCutNo.Text) != string.Empty)
            //                {
            //                    DataRow[] dr = m_dtCut.Select("rough_cut_no ='" + Val.ToString(lueCutNo.Text) + "'");
            //                    txtLotId.Text = Val.ToString(dr[0]["lot_id"]);
            //                    m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]));
            //                    txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
            //                    //prdId = Val.ToInt(dr[0]["prediction_id"]);
            //                    if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
            //                    {
            //                        GetOsCarat(Val.ToInt64(txtLotId.Text));

            //                    }
            //                }
            //                else
            //                {
            //                    txtIssProcess.Text = string.Empty;
            //                }
            //            }
            //        }
            //        m_blnflag = false;
            //    }
            //    else
            //    {
            //        m_blnflag = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.ToString());
            //    return;
            //}
        }
        private void txtLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                m_IsLot = 1;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                if (Val.ToInt64(txtLotId.Text) != 0 && Val.ToInt64(lueKapan.EditValue) == 0 && Val.ToInt64(lueCutNo.EditValue) == 0)
                {
                    m_dtbParam = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt64(txtLotId.Text));
                    if (m_dtbParam.Rows.Count > 0)
                    {
                        lueKapan.EditValue = Val.ToInt64(m_dtbParam.Rows[0]["kapan_id"]);
                        int CutId = Val.ToInt(m_dtbParam.Rows[0]["rough_cut_id"]);
                        m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));

                        lueCutNo.Properties.DataSource = m_dtbParam;
                        lueCutNo.Properties.ValueMember = "rough_cut_id";
                        lueCutNo.Properties.DisplayMember = "rough_cut_no";
                        lueCutNo.EditValue = Val.ToInt64(CutId);
                        //m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotId.Text), "CHIPIYO");
                        //if (m_DtProcess.Rows.Count > 0)
                        //{
                        //    m_rate = Val.ToDecimal(m_DtProcess.Rows[0]["rate"]);
                        //}

                        GetOsCarat(Val.ToInt64(txtLotId.Text));
                        DataTable DTab_Process = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(0), Val.ToInt32(0), 0, "R");
                        if (DTab_Process.Rows.Count > 0)
                        {
                            lueProcess.Properties.DataSource = DTab_Process;
                            lueProcess.Properties.DisplayMember = "process_name";
                            lueProcess.Properties.ValueMember = "process_id";

                            DataRow[] drProcess = DTab_Process.Select("process_name ='" + Val.ToString("CHIPIYO") + "'");
                            if (drProcess.Length > 0)
                            {
                                lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                lblOsCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                m_process = Val.ToString(drProcess[0]["process_name"]);
                                lueProcess.Text = Val.ToString(drProcess[0]["process_name"]);
                                lueSubProcess.Text = Val.ToString(drProcess[0]["sub_process_name"]);
                                m_balcarat = Val.ToDecimal(DTab_Process.Rows[0]["carat"]);
                                lueProcess.Enabled = false;
                                lueSubProcess.Enabled = false;
                                m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotId.Text), "CHIPIYO");

                                if (m_DtProcess.Rows.Count > 0)
                                {
                                    m_rate = Val.ToDecimal(m_DtProcess.Rows[0]["rate"]);
                                }
                            }
                            else
                            {
                                Global.Message("Lot not issue in Chipiyo Process");
                                lueProcess.Enabled = true;
                                lueSubProcess.Enabled = true;
                                lueProcess.EditValue = null;
                                lueSubProcess.EditValue = null;
                                lblOsPcs.Text = Val.ToString("0");
                                lblOsCarat.Text = Val.ToString("0.00");
                                m_IsLot = 0;
                                return;
                            }
                        }
                    }
                    else
                    {
                        BLL.General.ShowErrors("Cut No not Found");
                        lueCutNo.EditValue = System.DBNull.Value;
                        m_IsLot = 0;
                    }
                }
                m_IsLot = 0;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
        {
            if (m_IsLot == 0)
            {
                m_dtbParam = new DataTable();
                if (lueKapan.Text.ToString() != "")
                {
                    m_dtbParam = Global.GetRoughKapanWise_Data(Val.ToInt(lueKapan.EditValue));
                }
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
            }
        }
        private void lueType_EditValueChanged(object sender, EventArgs e)
        {
            MfgRoughClarityMaster objClarity = new MfgRoughClarityMaster();
            DataTable dtbClarity = new DataTable();
            DataTable dtbdetails = new DataTable();
            dtbClarity = objClarity.GetData();

            if (lueType.EditValue.ToString() != "")
            {
                if (lueType.EditValue.ToString() != "BOTH")
                {
                    dtbClarity.DefaultView.RowFilter = "type = '" + lueType.EditValue + "'";
                    dtbClarity.DefaultView.ToTable();
                    dtbdetails = dtbClarity.DefaultView.ToTable();
                }
                else
                {
                    dtbdetails = dtbClarity.Copy();
                }

                lueClarity.Properties.DataSource = dtbdetails;
                lueClarity.Properties.ValueMember = "rough_clarity_id";
                lueClarity.Properties.DisplayMember = "rough_clarity_name";
            }
        }
        private void grdProcessReceive_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdProcessReceive.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }

        private void lueClarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughClarityMaster frmClarity = new FrmMfgRoughClarityMaster();
                frmClarity.ShowDialog();
                Global.LOOKUPRoughClarity(lueClarity);
            }
        }

        private void lueSieve_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgRoughSieve frmRoughSieve = new FrmMfgRoughSieve();
                frmRoughSieve.ShowDialog();
                Global.LOOKUPRoughSieve(lueSieve);
            }
        }

        private void dgvProcessReceive_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                //DataTable dtAmount = new DataTable();
                //dtAmount = (DataTable)grdProcessReceive.DataSource;
                //string[] col = e.Column.FieldName.Split('_');
                //string currcol = "";
                //if (col.Length > 3 && e.Column.FieldName.Contains("_") && col != null)
                //{
                //    currcol = col[0] + "_" + col[1];
                //}
                //decimal carat = 0;
                //decimal total = 0;
                //for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                //{
                //    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                //    {
                //        if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                //        {
                //            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                //            total += carat;
                //        }
                //        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                //        {
                //            dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();
                //            break;
                //        }
                //    }
                //    total = 0;
                //}
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdProcessReceive.DataSource;
                string[] col = e.Column.FieldName.Split('_');
                string currcol = "";
                if (col.Length > 3 && e.Column.FieldName.Contains("_") && col != null)
                {
                    currcol = col[0] + "_" + col[1] + "_" + col[2];
                }
                Int32 pcs = 0;
                decimal rate = 0;
                decimal carat = 0;
                decimal total_pcs = 0;
                decimal total = 0;
                decimal totRate = 0;
                decimal totAmount = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (dtAmount.Columns[j].ToString().Contains("pcs") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            pcs = Val.ToInt32(dtAmount.Rows[i][j]);
                            total_pcs += pcs;
                        }
                        if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                            total += carat;
                        }
                        else if (dtAmount.Columns[j].ToString().Contains("rate") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            rate = Val.ToDecimal(dtAmount.Rows[i][j]);
                        }
                        else if (dtAmount.Columns[j].ToString().Contains("amount") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            dtAmount.Rows[i][j] = Math.Round((carat * rate), 0).ToString();
                            totAmount += (carat * rate);
                            //break;
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("T_Pcs"))
                        {
                            dtAmount.Rows[i][j] = Val.ToInt32(total_pcs);

                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();
                        }
                        else if (dtAmount.Columns[j].ColumnName.Contains("Rate"))
                        {
                            if (totAmount != 0 && total != 0)
                            {
                                totRate = totAmount / total;
                            }
                            else
                            {
                                totRate = 0;
                            }
                            dtAmount.Rows[i][j] = Math.Round(totRate, 2).ToString();

                        }
                        else if (dtAmount.Columns[j].ColumnName.Contains("Amount"))
                        {
                            decimal amount = Math.Round((total * totRate), 0);
                            dtAmount.Rows[i][j] = amount.ToString();
                            break;
                        }

                    }
                    total_pcs = 0;
                    totAmount = 0;
                    total = 0;
                    totRate = 0;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void dgvProcessReceive_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                if (view.FocusedColumn.FieldName.Contains("carat"))
                {
                    double carat = 0.000;
                    if (!double.TryParse(e.Value as string, out carat))
                    {
                        e.Valid = false;
                        e.ErrorText = "Input string was not in a correct format.";
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void lueProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmProcessMaster frmProcess = new FrmProcessMaster();
                frmProcess.ShowDialog();
                Global.LOOKUPProcess(lueProcess);
            }
        }

        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueSubProcess);
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());
            }
        }

        private void FrmMFGProcessChipiyoRecieve_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbKapan = Global.GetKapanAll();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("BOTH");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "BOTH";

                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                Global.LOOKUPRoughSieve(lueSieve);

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                m_dtbParam = Global.GetRoughCutAll();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void lueProcess_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (lueProcess.EditValue != System.DBNull.Value)
                {
                    if (m_dtbSubProcess.Rows.Count > 0)
                    {
                        DataTable dtbdetail = m_dtbSubProcess;

                        string strFilter = string.Empty;

                        if (lueProcess.Text != "")
                            strFilter = "process_id = " + lueProcess.EditValue;


                        dtbdetail.DefaultView.RowFilter = strFilter;
                        dtbdetail.DefaultView.ToTable();

                        DataTable dtb = dtbdetail.DefaultView.ToTable();

                        lueSubProcess.Properties.DataSource = dtb;
                        lueSubProcess.Properties.ValueMember = "sub_process_id";
                        lueSubProcess.Properties.DisplayMember = "sub_process_name";
                        lueSubProcess.EditValue = System.DBNull.Value;
                    }
                }
                if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotId.Text) != 0)
                {
                    DataTable dtIss = new DataTable();
                    dtIss = objProcessReceive.GetIssueID(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                        m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                        m_dtOutstanding = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                        if (m_dtOutstanding.Rows.Count > 0)
                        {
                            m_OsCarat = Val.ToInt32(m_dtOutstanding.Rows[0]["carat"]);
                            //txtBalanceCarat.Text = Val.ToString(m_OsCarat);
                            m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                        }
                        else
                        {
                            //txtBalanceCarat.Text = "0";
                            m_balcarat = 0;
                        }

                    }
                    else
                    {
                        Global.Message("Lot not issue in this process.");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void lueSubProcess_EditValueChanged(object sender, EventArgs e)
        {
            if (lueProcess.EditValue != System.DBNull.Value && lueSubProcess.EditValue != System.DBNull.Value && Val.ToInt64(txtLotId.Text) != 0)
            {
                DataTable dtIss = new DataTable();
                dtIss = objProcessReceive.GetIssueID(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
                if (dtIss.Rows.Count > 0)
                {
                    m_manager_id = Val.ToInt(dtIss.Rows[0]["manager_id"]);
                    m_emp_id = Val.ToInt(dtIss.Rows[0]["employee_id"]);
                    m_issue_id = Val.ToInt(dtIss.Rows[0]["issue_id"]);
                    m_dtOutstanding = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue), 1, "R");
                    if (m_dtOutstanding.Rows.Count > 0)
                    {
                        //txtBalanceCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                        //m_OsCarat = Val.ToDecimal(txtBalanceCarat.Text);
                    }
                    else
                    {
                        //txtBalanceCarat.Text = "0";
                    }

                }
                else
                {
                    Global.Message("Lot not issue in this process.");
                }
            }
        }
        private void backgroundWorker_ProcessChipyoReceive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGProcessReceive MFGProcessReceive = new MFGProcessReceive();
                MFGProcessReceiveProperty objMFGProcessReceiveProperty = new MFGProcessReceiveProperty();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Receive_IntRes = 0;
                Loss_Count = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;
                try
                {

                    DataTable dtIssueDet = MFGProcessReceive.ProcessIssue_GetData(Val.ToInt64(txtLotId.Text), "CHIPIYO");
                    objMFGProcessReceiveProperty.manager_id = Val.ToInt(dtIssueDet.Rows[0]["manager_id"]);
                    objMFGProcessReceiveProperty.employee_id = Val.ToInt(dtIssueDet.Rows[0]["employee_id"]);
                    objMFGProcessReceiveProperty.process_id = Val.ToInt(dtIssueDet.Rows[0]["process_id"]);
                    objMFGProcessReceiveProperty.sub_process_id = Val.ToInt(dtIssueDet.Rows[0]["sub_process_id"]);
                    objMFGProcessReceiveProperty.Issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);
                    objMFGProcessReceiveProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    //objMFGProcessReceiveProperty.rate = Val.ToDecimal(m_rate);
                    if (Val.ToInt(dtIssueDet.Rows[0]["issue_id"]) == 0)
                    {
                        Global.Message("Issue data not found in this Cut No: " + Val.ToString(lueCutNo.Text));
                        return;
                    }

                    m_DTab = (DataTable)grdProcessReceive.DataSource;

                    DataTable dtbDetail = m_DTab.Copy();
                    decimal Carat = 0;

                    for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                        {
                            if (dtTemp.Columns[j].ToString().Contains("carat"))
                            {
                                if (Val.ToDecimal(dtTemp.Rows[i][j]) != 0)
                                {
                                    Carat = Carat + Val.ToDecimal(dtTemp.Rows[i][j]);
                                }
                            }
                        }
                    }

                    //DataTable DTab_Carat = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), 0, 0);

                    //if (DTab_Carat.Rows.Count > 0)
                    //{
                    //    if (Val.ToDecimal(DTab_Carat.Rows[0]["carat"]) < Carat)
                    //    {
                    //        Global.Message("Carat more then Outstanding Carat");
                    //        return;
                    //    }
                    //}

                    for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                    {
                        string strNew = Val.ToString(dtbDetail.Columns[i]);
                        string str = strNew.Substring(strNew.LastIndexOf("_") + 1);

                        dtbDetail.Columns[Val.ToString(dtbDetail.Columns[i])].ColumnName = strNew.Split('_')[0] + "_" + str;
                    }

                    if (dtbDetail.Rows.Count > 0)
                    {
                        for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                        {
                            if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_carat")
                            {
                                foreach (DataRow Drw in dtbDetail.Rows)
                                {
                                    objMFGProcessReceiveProperty.lot_id = Val.ToInt64(txtLotId.Text);
                                    objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                    objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGProcessReceiveProperty.rough_clarity_id = Val.ToInt(Drw["clarity_id"]);
                                    objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    objMFGProcessReceiveProperty.pcs = Val.ToInt32(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "pcs"]);
                                    objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "carat"]);
                                    objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "rate"]);
                                    objMFGProcessReceiveProperty.amount = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "amount"]);
                                    //objMFGProcessReceiveProperty.rate = Val.ToDecimal(m_rate);
                                    //if (Val.ToDecimal(m_rate) != 0)
                                    //{
                                    //    objMFGProcessReceiveProperty.amount = Val.ToDecimal(m_rate * objMFGProcessReceiveProperty.carat);
                                    //}
                                    objMFGProcessReceiveProperty.union_id = IntRes;
                                    objMFGProcessReceiveProperty.receive_union_id = Receive_IntRes;
                                    objMFGProcessReceiveProperty.form_id = m_numForm_id;
                                    objMFGProcessReceiveProperty.history_union_id = NewHistory_Union_Id;
                                    if (objMFGProcessReceiveProperty.carat == 0)
                                        continue;

                                    if (Loss_Count == 0)
                                    {
                                        if (txtWeightPlus.Text != "" || txtWeightLoss.Text != "")
                                        {
                                            objMFGProcessReceiveProperty.carat_plus = Val.ToDecimal(txtWeightPlus.Text);
                                            objMFGProcessReceiveProperty.loss_carat = Val.ToDecimal(txtWeightLoss.Text);
                                            objMFGProcessReceiveProperty.loss_count = Val.ToInt(Loss_Count);
                                            Loss_Count = Loss_Count + 1;
                                        }
                                        else
                                        {
                                            objMFGProcessReceiveProperty.carat_plus = Val.ToDecimal(0);
                                            objMFGProcessReceiveProperty.loss_carat = Val.ToDecimal(0);
                                            objMFGProcessReceiveProperty.loss_count = Val.ToInt(1);
                                            Loss_Count = Loss_Count + 1;
                                        }
                                    }
                                    else
                                    {
                                        objMFGProcessReceiveProperty.carat_plus = Val.ToDecimal(0);
                                        objMFGProcessReceiveProperty.loss_carat = Val.ToDecimal(0);
                                        objMFGProcessReceiveProperty.loss_count = Val.ToInt(Loss_Count);
                                    }

                                    objMFGProcessReceiveProperty.lot_srno = Lot_SrNo;

                                    //IntRes = MFGProcessReceive.Save(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    objMFGProcessReceiveProperty = MFGProcessReceive.Save(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    IntRes = objMFGProcessReceiveProperty.union_id;
                                    Receive_IntRes = objMFGProcessReceiveProperty.receive_union_id;
                                    NewHistory_Union_Id = Val.ToInt64(objMFGProcessReceiveProperty.history_union_id);
                                    Lot_SrNo = Val.ToInt64(objMFGProcessReceiveProperty.lot_srno);
                                }
                            }
                        }
                    }
                    Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    Conn.Inter1.Rollback();
                    Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_ProcessChipyoReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Process Chipyo Receive Data Save Succesfully");
                    //btnSearch_Click(null, null);
                    btnClear_Click(null, null);
                    lueType.EditValue = "BOTH";
                }
                else
                {
                    Global.Confirm("Error In Process Chipyo Receive");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }
        private void btnSearchData_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGProcessChipiyoRecieve = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }
        private void txtWeightLoss_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
        private void txtWeightPlus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as DevExpress.XtraEditors.TextEdit).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        #region GridEvents
        private void dgvProcessReceive_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdProcessReceive.DataSource;

                Int32 pcs = 0;
                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;
                Int32 totpcs = 0;
                decimal totrate = 0;
                decimal totcarat = 0;
                decimal totamount = 0;
                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("pcs"))
                    {
                        pcs = dtAmount.AsEnumerable().Sum(x => Val.ToInt32(x[dtAmount.Columns[j]]));
                        //carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                    }
                    if (dtAmount.Columns[j].ToString().Contains("carat"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                        //carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                    }
                    if (dtAmount.Columns[j].ToString().Contains("amount"))
                    {
                        amount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }
                    if (dtAmount.Columns[j].ToString().Contains("rate"))
                    {
                        //rate = carat * amount;
                        column = dtAmount.Columns[j].ToString();
                        amount = 0;
                    }
                    if (Val.ToDecimal(amount) > 0 && Val.ToDecimal(carat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            rate = amount / carat;
                            //((DevExpress.XtraGrid.GridSummaryItem)e.Item).;
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = rate;
                            column = "";
                            pcs = 0;
                            carat = 0;
                            amount = 0;
                        }
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("T_Pcs"))
                    {
                        totpcs = dtAmount.AsEnumerable().Sum(x => Val.ToInt32(x[dtAmount.Columns[j]]));
                        //carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                        //carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Amount"))
                    {
                        totamount = dtAmount.AsEnumerable().Sum(x => Math.Round(Val.ToDecimal(x[dtAmount.Columns[j]]), 0));
                    }

                    if (Val.ToDecimal(totamount) > 0 && Val.ToDecimal(totcarat) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "Rate")
                        {
                            totrate = totamount / totcarat;
                            //((DevExpress.XtraGrid.GridSummaryItem)e.Item).;
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = totrate;
                            //column = "";
                            totpcs = 0;
                            totamount = 0;
                            totcarat = 0;
                        }

                    }
                }
                //DataTable dtAmount = new DataTable();
                //dtAmount = (DataTable)grdProcessReceive.DataSource;

                //decimal carat = 0;
                //decimal totcarat = 0;
                //for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                //{
                //    if (dtAmount.Columns[j].ToString().Contains("carat"))
                //    {
                //        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                //    }
                //    if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                //    {
                //        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                //    }
                //}
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (lueCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueCutNo.Focus();
                    }
                }
                if (lueSieve.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(12, "Rough Sieve"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSieve.Focus();
                    }
                }
                if (lueClarity.EditValue.ToString() == string.Empty)
                {
                    lstError.Add(new ListError(12, "Clarity"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueClarity.Focus();
                    }
                }
                if (lueProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }
                if (lueSubProcess.Text == "")
                {
                    lstError.Add(new ListError(13, "Sub Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueSubProcess.Focus();
                    }
                }
                if (Val.ToString(m_process) != "CHIPIYO")
                {
                    lstError.Add(new ListError(5, "Lot not issue in CHIPIYO Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLotId.Focus();
                    }
                }

                if (Search == 0)
                {
                    // Add By Praful On 17072020
                    DataTable DTab_Data = (DataTable)grdProcessReceive.DataSource;

                    if (DTab_Data != null)
                    {
                        if (DTab_Data.Rows.Count > 0)
                        {
                            //if ((Val.ToDecimal(txtWeightLoss.Text) == 0 && Val.ToDecimal(txtWeightPlus.Text) == 0) || Val.ToDecimal(txtWeightPlus.Text) != 0)
                            //{
                            decimal Total = DTab_Data.AsEnumerable().Sum(x => Val.ToDecimal(x[DTab_Data.Columns["Total"]]));

                            if ((Total + Val.ToDecimal(txtWeightLoss.Text)) != (Val.ToDecimal(lblOsCarat.Text) + Val.ToDecimal(txtWeightPlus.Text)))
                            {
                                lstError.Add(new ListError(36, "Transfer Carat not Equal to Balance Carat."));
                                if (!blnFocus)
                                {
                                    blnFocus = true;
                                }
                            }
                            //else if ((Val.ToDecimal(txtBalanceCarat.Text)) < (Total - Val.ToDecimal(txtWeightLoss.Text)))
                            //{
                            //    lstError.Add(new ListError(36, "Transfer Carat not Equal to Balance Carat."));
                            //    if (!blnFocus)
                            //    {
                            //        blnFocus = true;
                            //    }
                            //}
                            //}
                            //else if (Val.ToDecimal(txtWeightLoss.Text) != 0)
                            //{
                            //    decimal sumData = DTab_Data.AsEnumerable().Sum(x => Val.ToDecimal(x[DTab_Data.Columns["Total"]]));
                            //    decimal Tot_Data = sumData + Val.ToDecimal(txtWeightLoss.Text);
                            //    if (Val.ToDecimal(txtBalanceCarat.Text) != Tot_Data)
                            //    {
                            //        lstError.Add(new ListError(36, "Transfer Carat not Equal to Balance Carat."));
                            //        if (!blnFocus)
                            //        {
                            //            blnFocus = true;
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                //End

                //if (txtIssProcess.Text != "CHIPIYO" && txtIssProcess.Text != "CHARNI")
                //{
                //    lstError.Add(new ListError(5, "Lot not issue in Chipiyo Process"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtLotId.Focus();
                //    }
                //}
                Search = 0;
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public void GetOsCarat(Int64 lotId)
        {
            try
            {
                if (lotId > 0)
                {
                    m_dtOutstanding = objProcessReceive.GetBalanceCarat(lotId);

                }


                if (m_dtOutstanding.Rows.Count > 0)
                {
                    //txtBalanceCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["balance_carat"]);
                    //m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
                    //lueCutNo.Text = Val.ToString(m_dtOutstanding.Rows[0]["rough_cut_no"]);
                    m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);

                    //m_blnflag = true;
                }
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    //txtPcs.Text = "0";
                    //txtBalanceCarat.Text = "0";
                    lueCutNo.EditValue = System.DBNull.Value;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }

        }
        private void AddGotFocusListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GotFocus += new EventHandler(Control_GotFocus);
                if (c.Controls.Count > 0)
                {
                    AddGotFocusListener(c);
                }
            }
        }
        private void AddKeyPressListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                if (c.Controls.Count > 0)
                {
                    AddKeyPressListener(c);
                }
            }
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                if ((Control)sender is CheckedComboBoxEdit)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
            }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    ((LookUpEdit)(Control)sender).ShowPopup();
                }
                else if ((Control)sender is CheckedComboBoxEdit)
                {
                    ((CheckedComboBoxEdit)(Control)sender).ShowPopup();
                }
            }
        }
        private void TabControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.TabStop)
                    _tabControls.Add(control);
                if (control.HasChildren)
                    TabControlsToList(control.Controls);
            }
        }
        private void ControlSettingDT(int FormCode, Form pForm)
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                            where Val.ToBooleanToInt(dr["is_control"]) == 1
                                            && dr["column_type"].ToString() != "LABEL"
                                            select dr).CopyToDataTable();
            DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            l.OptionsFocus.EnableAutoTabOrder = false;

            if (DtFilterColSetting.Rows.Count > 0)
            {
                DtControlSettings = DtFilterColSetting;
                foreach (Control item1 in pForm.Controls)
                {
                    ControllSettings(item1, DtFilterColSetting);
                }
            }
        }
        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

            //else
            {
                var VarControlSetting = (from DataRow dr in DTab.Rows
                                         where dr["column_name"].ToString() == item2.Name.ToString()
                                         select dr);

                if (VarControlSetting.Count() > 0)
                {
                    DataRow DRow = VarControlSetting.CopyToDataTable().Rows[0];
                    if (item2.Name.ToString() == Val.ToString(DRow["column_name"]))
                    {
                        if (!(item2 is TextEdit))
                        {
                            if (!(item2 is DateTimePicker))
                            {
                                if (!(item2 is DevExpress.XtraEditors.TextEdit))
                                {
                                    item2.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                                }
                            }
                        }
                        if (Val.ToInt(DRow["tab_index"]) >= 0)
                        {
                            if (item2.CanSelect)
                                item2.TabStop = true;
                        }
                        else
                            item2.TabStop = false;
                        if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                        {
                            item2.Visible = true;
                            item2.TabStop = true;
                        }
                        else
                        {
                            item2.Visible = false;
                            item2.TabStop = false;
                        }

                        item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                        if (item2.TabIndex == 1)
                        {
                            item2.Select();
                            item2.Focus();
                        }
                        if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                        {
                            item2.Enabled = true;
                        }
                        else
                        {
                            item2.Enabled = false;
                        }
                    }
                }
                else
                {
                    item2.TabStop = false;
                }
            }
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }
        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            dgvProcessReceive.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProcessReceive.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProcessReceive.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProcessReceive.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProcessReceive.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProcessReceive.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProcessReceive.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
