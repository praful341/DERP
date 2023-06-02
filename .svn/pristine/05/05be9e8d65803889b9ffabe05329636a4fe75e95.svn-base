using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGRoughRateUpdate : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbSubProcess = new DataTable();
        MfgRoughSieve objRoughSieve;
        MFGRoughRateUpdate objRoughRate;
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
        //DataTable DTab_KapanWiseData = new DataTable();
        int Search = 0;
        Int64 m_numForm_id;
        Int64 IntRes;
        int m_IsLot;
        decimal m_numSummMakableRate = 0;

        #endregion

        #region Constructor
        public FrmMFGRoughRateUpdate()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objRoughSieve = new MfgRoughSieve();
            objClarity = new MfgRoughClarityMaster();
            ObjReportDetailProperty = new New_Report_DetailProperty();
            objRoughRate = new MFGRoughRateUpdate();
            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            m_numForm_id = 0;
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
            //if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            //{
            //    Global.Message("Select First User Setting...Please Contact to Administrator...");
            //    return;
            //}

            //ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            //AddKeyPressListener(this);
            //this.KeyPreview = true;

            //TabControlsToList(this.Controls);
            //_tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();
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
                btnSave.Enabled = false;
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
                    btnSave.Enabled = true;
                    return;
                }
                DialogResult result = MessageBox.Show("Do you want to save Process data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                panelProgress.Visible = true;
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
            ClearDetails();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                lblCarat.Text = Val.ToString(0);
                lblRate.Text = Val.ToString(0);
                lblAmount.Text = Val.ToString(0);

                grdProcessReceive.DataSource = null;
                dgvProcessReceive.Columns.Clear();
                grdProcessIssue.DataSource = null;
                grdRejectionRate.DataSource = null;
                //RBtnType.SelectedIndex = 0;
                btnSave.Enabled = true;
                RBtnType.Enabled = true;
                lueType.EditValue = "BOTH";
                grdProcessIssue.Visible = true;
                grdProcessReceive.Visible = false;
                grdRejectionRate.Visible = false;

                RBtnType_EditValueChanged(null, null);
                //for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                //    ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
                //for (int i = 0; i < ListCutNo.Properties.Items.Count; i++)
                //    ListCutNo.Properties.Items[i].CheckState = CheckState.Unchecked;

                grdMakableSawable.DataSource = null;
                grdRejection.DataSource = null;

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

                RBtnType.Enabled = false;
                dtTemp = new DataTable();
                dgvProcessReceive.Columns.Clear();
                pivot pt;
                if (RBtnType.EditValue.ToString() == "ISSUE")
                {
                    grdProcessReceive.Visible = false;
                    grdRejectionRate.Visible = false;
                    grdProcessIssue.Visible = true;
                    DataTable DTab_Issue = objRoughRate.RoughRateProcessIssue_GetData(Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueProcess.EditValue));

                    grdProcessIssue.DataSource = DTab_Issue;
                    grvProcessIssue.BestFitColumns();
                }
                else if (RBtnType.EditValue.ToString() == "RECEIVE")
                {
                    if (lueProcess.Text == "SOYEBLE")
                    {
                        grdRejectionRate.Visible = false;
                        grdProcessIssue.Visible = false;
                        grdProcessReceive.Visible = true;

                        DataTable dtb = objRoughRate.RoughRate_GetData(Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToString(lueType.Text));

                        DataColumn flag = new System.Data.DataColumn("flag", typeof(System.Int32));

                        flag.DefaultValue = 0;
                        dtb.Columns.Add(flag);
                        DataColumnCollection columns = dtb.Columns;
                        grdProcessReceive.DataSource = dtb;
                        dgvProcessReceive.BestFitColumns();
                        for (int i = 0; i <= dtb.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtb.Columns.Count - 1; j++)
                            {
                                if (dtb.Columns[j].ToString().Contains("rate"))
                                {
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowEdit = true;
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowFocus = true;
                                }
                                else
                                {
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowEdit = false;
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowFocus = false;
                                }
                            }
                        }
                        dgvProcessReceive.OptionsView.ShowFooter = true;
                        dgvProcessReceive.BestFitColumns();
                    }
                    else if (lueProcess.Text == "AKHU BHAR FINAL" || lueProcess.Text == "SHINE ISSUE" || lueProcess.Text == "LS ASSORT")
                    {
                        grdRejectionRate.Visible = true;
                        grdProcessIssue.Visible = false;
                        grdProcessReceive.Visible = false;

                        DataTable dtb = objRoughRate.RoughRate_GetData(Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToString(lueType.Text));

                        DataColumn flag = new System.Data.DataColumn("flag", typeof(System.Int32));

                        flag.DefaultValue = 0;
                        dtb.Columns.Add(flag);
                        DataColumnCollection columns = dtb.Columns;
                        grdRejectionRate.DataSource = dtb;
                        dgvRejectionRate.BestFitColumns();
                        dgvRejectionRate.OptionsView.ShowFooter = true;
                        dgvRejectionRate.BestFitColumns();
                    }
                    else if (lueProcess.Text == "CHARNI")
                    {
                        grdProcessIssue.Visible = false;
                        grdRejectionRate.Visible = false;
                        grdProcessReceive.Visible = true;

                        pt = new pivot(objRoughRate.RoughRate_GetData(Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToString(lueType.Text)));


                        dtTemp = pt.PivotDataSuperPlus(new string[] { "sieve", "normcrt" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum, }, new string[] { "sieve" });

                        DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                        DataColumn Rate = new System.Data.DataColumn("TRate", typeof(System.Decimal));
                        DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                        DataColumn flag = new System.Data.DataColumn("flag", typeof(System.Int32));

                        Total.DefaultValue = "0.000";
                        Rate.DefaultValue = "0";
                        Amount.DefaultValue = "0";

                        flag.DefaultValue = 0;
                        dtTemp.Columns.Add(Total);
                        dtTemp.Columns.Add(Rate);
                        dtTemp.Columns.Add(Amount);
                        dtTemp.Columns.Add(flag);
                        DataColumnCollection columns = dtTemp.Columns;

                        grdProcessReceive.DataSource = dtTemp;

                        dgvProcessReceive.Columns["Total"].OptionsColumn.ReadOnly = true;
                        dgvProcessReceive.Columns["Total"].OptionsColumn.AllowFocus = false;
                        dgvProcessReceive.Columns["Amount"].OptionsColumn.ReadOnly = true;
                        dgvProcessReceive.Columns["Amount"].OptionsColumn.AllowFocus = false;
                        dgvProcessReceive.Columns["flag"].OptionsColumn.ReadOnly = true;
                        for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                            {
                                if (dtTemp.Columns[j].ToString().Contains("amount"))
                                {
                                    dgvProcessReceive.Columns[j].Visible = false;
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowEdit = false;
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowFocus = false;
                                }
                            }
                        }
                        decimal Tcarat = 0;
                        decimal Trate = 0;
                        decimal Tamount = 0;
                        for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                            {
                                if (dtTemp.Columns[j].ToString().Contains("carat"))
                                {
                                    Tcarat += Val.ToDecimal(dtTemp.Rows[i][j]);
                                }

                                if (dtTemp.Columns[j].ToString().Contains("amount"))
                                {
                                    Tamount += Val.ToDecimal(dtTemp.Rows[i][j]);
                                }
                                if (dtTemp.Columns[j].ToString().Contains("Total"))
                                {
                                    dtTemp.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                                }
                                if (dtTemp.Columns[j].ToString().Contains("TRate"))
                                {
                                    if (Tcarat != 0 && Tamount != 0)
                                    {
                                        Trate = Tamount / Tcarat;
                                    }
                                    else
                                    {
                                        Trate = 0;
                                    }
                                    dtTemp.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                                }
                                if (dtTemp.Columns[j].ToString().Contains("Amount"))
                                {
                                    dtTemp.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                                    Tcarat = 0;
                                    Trate = 0;
                                    Tamount = 0;
                                    break;
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
                                if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                                {
                                    string total = dtTemp.Columns[j].ToString();
                                    GridColumn column4 = dgvProcessReceive.Columns[total];
                                    dgvProcessReceive.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                                    column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                                }
                                if (dtTemp.Columns[j].ColumnName.Contains("TRate"))
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
                        dgvProcessReceive.OptionsView.ShowFooter = true;
                        dgvProcessReceive.BestFitColumns();
                    }
                    else
                    {
                        grdProcessIssue.Visible = false;
                        grdRejectionRate.Visible = false;
                        grdProcessReceive.Visible = true;
                        pt = new pivot(objRoughRate.RoughRate_GetData(Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToString(lueType.Text)));


                        dtTemp = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity", "normcrt" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum, }, new string[] { "sieve" });


                        DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                        DataColumn Rate = new System.Data.DataColumn("TRate", typeof(System.Decimal));
                        DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                        DataColumn flag = new System.Data.DataColumn("flag", typeof(System.Int32));

                        Total.DefaultValue = "0.000";
                        Rate.DefaultValue = "0";
                        Amount.DefaultValue = "0";

                        flag.DefaultValue = 0;
                        dtTemp.Columns.Add(Total);
                        dtTemp.Columns.Add(Rate);
                        dtTemp.Columns.Add(Amount);
                        dtTemp.Columns.Add(flag);
                        DataColumnCollection columns = dtTemp.Columns;

                        if (dtTemp_old != null)
                        {
                            if (dtTemp_old.Columns.Count > 0)
                            {
                                for (int i = 0; i < dtTemp_old.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dtTemp_old.Columns.Count; j++)
                                    {
                                        for (int k = 0; k < dtTemp.Rows.Count; k++)
                                        {
                                            if (dtTemp.Rows[k]["clarity"].ToString().Trim() == dtTemp_old.Rows[i]["clarity"].ToString().Trim())
                                            {
                                                if (columns.Contains(dtTemp_old.Columns[j].ColumnName.ToString().Trim()))
                                                {
                                                    dtTemp.Rows[k][dtTemp_old.Columns[j].ColumnName.ToString().Trim()] = dtTemp_old.Rows[i][dtTemp_old.Columns[j].ColumnName.ToString().Trim()];
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }

                        grdProcessReceive.DataSource = dtTemp;

                        dgvProcessReceive.Columns["clarity_id"].Visible = false;
                        dgvProcessReceive.Columns["normcrt"].Visible = false;
                        dgvProcessReceive.Columns["clarity"].OptionsColumn.ReadOnly = true;
                        dgvProcessReceive.Columns["clarity"].OptionsColumn.AllowFocus = false;
                        dgvProcessReceive.Columns["Total"].OptionsColumn.ReadOnly = true;
                        dgvProcessReceive.Columns["Total"].OptionsColumn.AllowFocus = false;
                        dgvProcessReceive.Columns["Amount"].OptionsColumn.ReadOnly = true;
                        dgvProcessReceive.Columns["Amount"].OptionsColumn.AllowFocus = false;
                        dgvProcessReceive.Columns["flag"].OptionsColumn.ReadOnly = true;
                        dgvProcessReceive.Columns["clarity"].Fixed = FixedStyle.Left;
                        for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                            {
                                if (dtTemp.Columns[j].ToString().Contains("amount"))
                                {
                                    dgvProcessReceive.Columns[j].Visible = false;
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowEdit = false;
                                    dgvProcessReceive.Columns[j].OptionsColumn.AllowFocus = false;
                                }
                            }
                        }
                        decimal Tcarat = 0;
                        decimal Trate = 0;
                        decimal Tamount = 0;
                        for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                            {
                                if (dtTemp.Columns[j].ToString().Contains("carat"))
                                {
                                    Tcarat += Val.ToDecimal(dtTemp.Rows[i][j]);
                                }

                                if (dtTemp.Columns[j].ToString().Contains("amount"))
                                {
                                    Tamount += Val.ToDecimal(dtTemp.Rows[i][j]);
                                }
                                if (dtTemp.Columns[j].ToString().Contains("Total"))
                                {
                                    dtTemp.Rows[i][j] = Val.ToString(Math.Round(Tcarat, 3));
                                }
                                if (dtTemp.Columns[j].ToString().Contains("TRate"))
                                {
                                    if (Tcarat != 0 && Tamount != 0)
                                    {
                                        Trate = Tamount / Tcarat;
                                    }
                                    else
                                    {
                                        Trate = 0;
                                    }
                                    dtTemp.Rows[i][j] = Val.ToString(Math.Round(Trate, 2));
                                }
                                if (dtTemp.Columns[j].ToString().Contains("Amount"))
                                {
                                    dtTemp.Rows[i][j] = Val.ToString(Math.Round(Tamount, 2));
                                    Tcarat = 0;
                                    Trate = 0;
                                    Tamount = 0;
                                    break;
                                }
                            }
                            //break;
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
                                if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                                {
                                    string total = dtTemp.Columns[j].ToString();
                                    GridColumn column4 = dgvProcessReceive.Columns[total];
                                    dgvProcessReceive.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                                    column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                                }
                                if (dtTemp.Columns[j].ColumnName.Contains("TRate"))
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
                        dgvProcessReceive.OptionsView.ShowFooter = true;
                        dgvProcessReceive.BestFitColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
        private void txtLotId_Validated(object sender, EventArgs e)
        {
            try
            {
                m_IsLot = 1;
                lueKapan.EditValue = null;
                lueCutNo.EditValue = null;
                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
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
                    //if (m_dtbParam.Rows.Count == 0)
                    //{
                    //    m_dtbParam = DTab_KapanWiseData;
                    //}
                }
                lueCutNo.Properties.DataSource = m_dtbParam;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";
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

                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                DtpEntryDateAll.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DtpEntryDateAll.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DtpEntryDateAll.Properties.Mask.UseMaskAsDisplayFormat = true;
                DtpEntryDateAll.Properties.CharacterCasing = CharacterCasing.Upper;
                DtpEntryDateAll.EditValue = DateTime.Now;

                Global.LOOKUPProcess(lueProcess);

                ListKapan.Properties.DataSource = m_dtbKapan;
                ListKapan.Properties.DisplayMember = "kapan_no";
                ListKapan.Properties.ValueMember = "kapan_id";

                ListCutNo.Properties.DataSource = m_dtCut;
                ListCutNo.Properties.ValueMember = "rough_cut_id";
                ListCutNo.Properties.DisplayMember = "rough_cut_no";

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "BOTH";

                m_dtbParam = Global.GetRoughCutAll();

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                RBtnType_EditValueChanged(null, null);

                grdProcessIssue.Visible = true;
                grdProcessReceive.Visible = false;
                grdRejectionRate.Visible = false;
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
                    DataTable dtIss = new DataTable();
                    dtIss = objRoughRate.ChipyoIssue_GetData(Val.ToInt(lueCutNo.EditValue), Val.ToInt(lueKapan.EditValue), Val.ToInt(lueProcess.EditValue));
                    if (dtIss.Rows.Count > 0)
                    {
                        lblCarat.Text = Val.ToString(dtIss.Rows[0]["carat"]);
                        lblRate.Text = Val.ToString(dtIss.Rows[0]["rate"]);
                        lblAmount.Text = Val.ToString(dtIss.Rows[0]["amount"]);
                    }
                    else
                    {
                        lblCarat.Text = Val.ToString(0);
                        lblRate.Text = Val.ToString(0);
                        lblAmount.Text = Val.ToString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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
                DataTable m_DTab_Makable = new DataTable();
                DataTable m_DTab_Rejection = new DataTable();

                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                try
                {
                    if (RBtnType.EditValue.ToString() == "ISSUE")
                    {
                        m_DTab = (DataTable)grdProcessIssue.DataSource;
                        DataTable dtbDetail = new DataTable();
                        DataRow[] dr2 = m_DTab.Select("flag=1");

                        if (dr2.Count() > 0)
                        {
                            dtbDetail = dr2.CopyToDataTable();
                        }

                        if (dtbDetail.Rows.Count > 0)
                        {
                            foreach (DataRow Drw in dtbDetail.Rows)
                            {
                                objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt64(Drw["rough_cut_id"]);
                                objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                objMFGProcessReceiveProperty.Issue_id = Val.ToInt64(Drw["issue_id"]);
                                objMFGProcessReceiveProperty.prediction_id = Val.ToInt64(Drw["prediction_id"]);
                                objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw["carat"]);
                                objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["rate"]);
                                objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                if (Val.ToDecimal(objMFGProcessReceiveProperty.rate) != 0)
                                {
                                    objMFGProcessReceiveProperty.amount = Val.ToDecimal(objMFGProcessReceiveProperty.rate * objMFGProcessReceiveProperty.carat);
                                }
                                objMFGProcessReceiveProperty.type = Val.ToString(RBtnType.EditValue);

                                IntRes = MFGProcessReceive.Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                            }
                        }
                        Conn.Inter1.Commit();
                    }
                    else if (RBtnType.EditValue.ToString() == "RECEIVE")
                    {
                        if (Val.ToString(lueProcess.Text) == "AKHU BHAR FINAL" || Val.ToString(lueProcess.Text) == "SHINE ISSUE" || Val.ToString(lueProcess.Text) == "LS ASSORT")
                        {
                            m_DTab = (DataTable)grdRejectionRate.DataSource;
                        }
                        else
                        {
                            m_DTab = (DataTable)grdProcessReceive.DataSource;
                        }

                        DataTable dtbDetail = new DataTable();
                        DataRow[] dr2 = m_DTab.Select("flag=1");
                        if (dr2.Count() > 0)
                        {
                            dtbDetail = dr2.CopyToDataTable();
                        }
                        int count = dtbDetail.Rows.Count;
                        if (Val.ToString(lueProcess.Text) == "SOYEBLE")
                        {
                            if (dtbDetail.Rows.Count > 0)
                            {
                                foreach (DataRow Drw in dtbDetail.Rows)
                                {
                                    objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                    objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGProcessReceiveProperty.purity_id = Val.ToInt(Drw["purity_id"]);
                                    objMFGProcessReceiveProperty.prediction_id = Val.ToInt(Drw["prediction_id"]);
                                    //objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "carat"]);
                                    objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["rate"]);
                                    objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                    //objMFGProcessReceiveProperty.total_rate = Val.ToDecimal(NormRate);
                                    objMFGProcessReceiveProperty.count = Val.ToInt(count);
                                    if (Val.ToDecimal(objMFGProcessReceiveProperty.rate) != 0)
                                    {
                                        objMFGProcessReceiveProperty.amount = Val.ToDecimal(objMFGProcessReceiveProperty.rate * objMFGProcessReceiveProperty.carat);
                                    }
                                    objMFGProcessReceiveProperty.type = Val.ToString(RBtnType.EditValue);

                                    IntRes = MFGProcessReceive.Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    count--;
                                }
                            }
                        }
                        else if (Val.ToString(lueProcess.Text) == "AKHU BHAR FINAL" || Val.ToString(lueProcess.Text) == "SHINE ISSUE" || Val.ToString(lueProcess.Text) == "LS ASSORT")
                        {
                            if (dtbDetail.Rows.Count > 0)
                            {
                                foreach (DataRow Drw in dtbDetail.Rows)
                                {
                                    objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                    objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                    objMFGProcessReceiveProperty.rough_clarity_id = Val.ToInt(Drw["rough_clarity_id"]);
                                    objMFGProcessReceiveProperty.rough_quality_id = Val.ToInt(Drw["quality_id"]);
                                    // objMFGProcessReceiveProperty.rough_quality_id = Val.ToInt(Drw["quality_id"]);
                                    //objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    //objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "carat"]);
                                    objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw["carat"]);
                                    objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["rate"]);
                                    objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                    objMFGProcessReceiveProperty.total_rate = Val.ToDecimal(Drw["rate"]);
                                    if (Val.ToDecimal(objMFGProcessReceiveProperty.rate) != 0)
                                    {
                                        objMFGProcessReceiveProperty.amount = Val.ToDecimal(objMFGProcessReceiveProperty.rate * objMFGProcessReceiveProperty.carat);
                                    }
                                    objMFGProcessReceiveProperty.type = Val.ToString(RBtnType.EditValue);

                                    IntRes = MFGProcessReceive.Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                }
                            }
                        }
                        else if (Val.ToString(lueProcess.Text) == "CHARNI")
                        {
                            if (dtbDetail.Rows.Count > 0)
                            {
                                for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                                {
                                    if (Val.ToString(dtbDetail.Columns[i]) != "TRate" && Val.ToString(dtbDetail.Columns[i]) != "Amount" && Val.ToString(dtbDetail.Columns[i]) != "flag" && Val.ToString(dtbDetail.Columns[i]) != "Total")
                                    {
                                        string strNew = Val.ToString(dtbDetail.Columns[i]);
                                        string str = strNew.Substring(strNew.LastIndexOf("_") + 1);

                                        dtbDetail.Columns[Val.ToString(dtbDetail.Columns[i])].ColumnName = strNew.Split('_')[0] + "_" + str;
                                    }
                                }
                            }
                            else
                            {
                                return;
                            }
                            object sumTotal;
                            sumTotal = m_DTab.Compute("Sum(Amount)", "");
                            decimal rej_amount = 0;
                            decimal NormCarat = 0;
                            decimal NewAmount = 0;
                            decimal NormRate = 0;
                            if (Val.ToInt(RBtnType.SelectedIndex) == 1)
                            {
                                rej_amount = Val.ToDecimal(sumTotal);
                                NormCarat = Val.ToDecimal(dtbDetail.Rows[0]["normcrt"]);
                                NewAmount = Val.ToDecimal(lblAmount.Text) - rej_amount;
                                NormRate = Val.ToDecimal(NewAmount) / NormCarat;
                            }
                            else
                            {
                                rej_amount = Val.ToDecimal(0);
                                NormCarat = Val.ToDecimal(0);
                                NewAmount = Val.ToDecimal(lblAmount.Text) - rej_amount;
                                NormRate = Val.ToDecimal(0);
                            }

                            if (dtbDetail.Rows.Count > 0)
                            {
                                for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                                {
                                    if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_carat")
                                    {
                                        foreach (DataRow Drw in dtbDetail.Rows)
                                        {
                                            objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                            objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                            //objMFGProcessReceiveProperty.rough_clarity_id = Val.ToInt(Drw["clarity_id"]);
                                            objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                            objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["TRate"]);
                                            objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                            objMFGProcessReceiveProperty.total_rate = Val.ToDecimal(NormRate);
                                            //objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw["carat"]);
                                            objMFGProcessReceiveProperty.count = Val.ToInt(count);
                                            if (Val.ToDecimal(objMFGProcessReceiveProperty.rate) != 0)
                                            {
                                                objMFGProcessReceiveProperty.amount = Val.ToDecimal(Drw["Amount"]); //Val.ToDecimal(objMFGProcessReceiveProperty.rate * objMFGProcessReceiveProperty.carat);
                                            }
                                            objMFGProcessReceiveProperty.type = Val.ToString(RBtnType.EditValue);

                                            IntRes = MFGProcessReceive.Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                            count--;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //DataTable dtbDetail = m_DTab.Select("flag = 1").CopyToDataTable();
                            if (dtbDetail.Rows.Count > 0)
                            {
                                for (int i = dtbDetail.Columns.Count - 1; i >= 3; i--)
                                {
                                    if (Val.ToString(dtbDetail.Columns[i]) != "TRate" && Val.ToString(dtbDetail.Columns[i]) != "Amount" && Val.ToString(dtbDetail.Columns[i]) != "flag" && Val.ToString(dtbDetail.Columns[i]) != "Total")
                                    {
                                        string strNew = Val.ToString(dtbDetail.Columns[i]);
                                        string str = strNew.Substring(strNew.LastIndexOf("_") + 1);

                                        dtbDetail.Columns[Val.ToString(dtbDetail.Columns[i])].ColumnName = strNew.Split('_')[0] + "_" + str;
                                    }
                                }
                            }
                            else
                            {
                                return;
                            }
                            object sumTotal;
                            sumTotal = m_DTab.Compute("Sum(Amount)", "");
                            decimal rej_amount = 0;
                            decimal NormCarat = 0;
                            decimal NewAmount = 0;
                            decimal NormRate = 0;
                            if (Val.ToInt(RBtnType.SelectedIndex) == 1)
                            {
                                rej_amount = Val.ToDecimal(sumTotal);
                                NormCarat = Val.ToDecimal(dtbDetail.Rows[0]["normcrt"]);
                                NewAmount = Val.ToDecimal(lblAmount.Text) - rej_amount;
                                NormRate = Val.ToDecimal(NewAmount) / NormCarat;
                            }
                            else
                            {
                                rej_amount = Val.ToDecimal(0);
                                NormCarat = Val.ToDecimal(0);
                                NewAmount = Val.ToDecimal(lblAmount.Text) - rej_amount;
                                NormRate = Val.ToDecimal(0);
                            }

                            if (dtbDetail.Rows.Count > 0)
                            {
                                for (int i = dtbDetail.Columns.Count - 1; i >= 2; i--)
                                {
                                    if (Val.ToString(dtbDetail.Columns[i]) == Val.ToString(dtbDetail.Columns[i]).Split('_')[0] + "_carat")
                                    {
                                        foreach (DataRow Drw in dtbDetail.Rows)
                                        {
                                            objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                                            objMFGProcessReceiveProperty.receive_date = Val.DBDate(dtpReceiveDate.Text);
                                            objMFGProcessReceiveProperty.rough_clarity_id = Val.ToInt(Drw["clarity_id"]);
                                            objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                            objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["TRate"]);
                                            objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                            objMFGProcessReceiveProperty.total_rate = Val.ToDecimal(NormRate);
                                            objMFGProcessReceiveProperty.count = Val.ToInt(count);
                                            if (Val.ToDecimal(objMFGProcessReceiveProperty.rate) != 0)
                                            {
                                                objMFGProcessReceiveProperty.amount = Val.ToDecimal(objMFGProcessReceiveProperty.rate * objMFGProcessReceiveProperty.carat);
                                            }
                                            objMFGProcessReceiveProperty.type = Val.ToString(RBtnType.EditValue);

                                            IntRes = MFGProcessReceive.Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                            count--;
                                        }
                                    }
                                }
                            }
                        }
                        Conn.Inter1.Commit();
                    }
                    else if (RBtnType.EditValue.ToString() == "ALL")
                    {
                        m_DTab_Makable = (DataTable)grdMakableSawable.DataSource;

                        foreach (DataRow Drw in m_DTab_Makable.Rows)
                        {
                            objMFGProcessReceiveProperty.rough_cut_no = Val.Trim(ListCutNo.Properties.GetCheckedItems());
                            objMFGProcessReceiveProperty.kapan_no = Val.Trim(ListKapan.Properties.GetCheckedItems());
                            objMFGProcessReceiveProperty.department_id = Val.ToInt64(Drw["department_id"]);
                            objMFGProcessReceiveProperty.transfer_date = Val.DBDate(Drw["transfer_date"].ToString());
                            objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["rate"]);
                            objMFGProcessReceiveProperty.type = Val.ToString("MAKABLE-SAWABLE");

                            IntRes = MFGProcessReceive.Rough_Makable_Rate_Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                        m_DTab_Rejection = (DataTable)grdRejection.DataSource;

                        foreach (DataRow Drw in m_DTab_Rejection.Rows)
                        {
                            objMFGProcessReceiveProperty.rough_cut_no = Val.Trim(ListCutNo.Properties.GetCheckedItems());
                            objMFGProcessReceiveProperty.kapan_no = Val.Trim(ListKapan.Properties.GetCheckedItems());
                            objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt32(Drw["sieve_id"]);
                            objMFGProcessReceiveProperty.rough_clarity_id = Val.ToInt32(Drw["clarity_id"]);
                            objMFGProcessReceiveProperty.purity_id = Val.ToInt32(Drw["purity_id"]);
                            objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["rate"]);
                            objMFGProcessReceiveProperty.type = Val.ToString("REJECTION");

                            IntRes = MFGProcessReceive.Rough_Makable_Rate_Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                        Conn.Inter1.Commit();
                    }
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
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Rate Updated Succesfully.");
                    btnClear_Click(null, null);
                }
                else
                {
                    Global.Confirm("Rate Not Updated");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region GridEvents
        private void dgvProcessReceive_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdProcessReceive.DataSource;

                decimal carat = 0;
                decimal totcarat = 0;
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("carat"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }
        private void grvProcessIssue_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
        }
        private void grvProcessIssue_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(grvProcessIssue.FocusedRowHandle);
        }
        private void grvProcessIssue_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(grvProcessIssue.FocusedRowHandle);
        }
        private void grvProcessIssue_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Column.Caption == "Rate")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["flag"], 1);
            }
            else
            {
                return;
            }
        }
        private void dgvProcessReceive_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdProcessReceive.DataSource;
                string[] col = e.Column.FieldName.Split('_');
                string currcol = "";

                if (col.Length > 3 && e.Column.FieldName.Contains("_") && col != null)
                {
                    currcol = col[0] + "_" + col[1];
                }
                decimal carat = 0;
                decimal total = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (e.RowHandle != i)
                            continue;
                        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                        {
                            total = Val.ToDecimal(dtAmount.Rows[i][j]);
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("TRate"))
                        {
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("Amount"))
                        {
                            dtAmount.Rows[i][j] = Val.ToDecimal(total * carat).ToString();
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("flag"))
                        {
                            dtAmount.Rows[i][j] = Val.ToInt32(1).ToString();
                            break;
                        }
                    }
                    total = 0;
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

        #endregion

        #endregion

        #region Functions
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (RBtnType.EditValue.ToString() == "ISSUE" || RBtnType.EditValue.ToString() == "RECEIVE")
                {
                    if (lueProcess.ItemIndex < 0)
                    {
                        lstError.Add(new ListError(13, "Process"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            lueProcess.Focus();
                        }
                    }

                    if (Search == 0 && Val.ToString(lueProcess.Text) != "SOYEBLE")
                    {
                        DataTable DTab_Data = (DataTable)grdProcessReceive.DataSource;

                        if (DTab_Data != null)
                        {
                            if (DTab_Data.Rows.Count > 0)
                            {
                                decimal Total = DTab_Data.AsEnumerable().Sum(x => Val.ToDecimal(x[DTab_Data.Columns["Total"]]));
                            }
                        }
                    }
                    Search = 0;
                }
                else if (RBtnType.EditValue.ToString() == "ALL")
                {
                    if (ListKapan.Text == string.Empty)
                    {
                        lstError.Add(new ListError(13, "Kapan"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            ListKapan.Focus();
                        }
                    }
                    if (ListCutNo.Text == string.Empty)
                    {
                        lstError.Add(new ListError(13, "Cut No"));
                        if (!blnFocus)
                        {
                            blnFocus = true;
                            ListCutNo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
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
        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                grvProcessIssue.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDouble(grvProcessIssue.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(grvProcessIssue.GetRowCellValue(rowindex, "carat")), 0));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }
        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
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

        private void dgvRejectionRate_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (e.Column.Caption == "Rate")
            {
                view.SetRowCellValue(e.RowHandle, view.Columns["flag"], 1);
            }
            else
            {
                return;
            }
        }
        private void CalculateGridRejAmount(int rowindex)
        {
            try
            {
                dgvRejectionRate.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDouble(dgvRejectionRate.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvRejectionRate.GetRowCellValue(rowindex, "carat")), 0));
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        private void dgvRejectionRate_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            CalculateGridRejAmount(e.PrevFocusedRowHandle);
        }

        private void dgvRejectionRate_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            CalculateGridRejAmount(dgvRejectionRate.FocusedRowHandle);
        }

        private void dgvRejectionRate_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridRejAmount(dgvRejectionRate.FocusedRowHandle);
        }

        private void RBtnType_EditValueChanged(object sender, EventArgs e)
        {
            if (RBtnType.Text == "ISSUE" || RBtnType.Text == "RECEIVE")
            {
                PanelSearchAll.Visible = false;
                PanelMkbSoyable.Visible = false;
                PanelRejection.Visible = false;
                grdRejectionRate.Visible = true;
                grdProcessIssue.Visible = true;
                grdProcessReceive.Visible = true;
                for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                    ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
                for (int i = 0; i < ListCutNo.Properties.Items.Count; i++)
                    ListCutNo.Properties.Items[i].CheckState = CheckState.Unchecked;
            }
            else if (RBtnType.Text == "ALL")
            {
                PanelSearchAll.Visible = true;
                PanelMkbSoyable.Visible = true;
                PanelRejection.Visible = true;
                grdProcessIssue.Visible = false;
                grdProcessReceive.Visible = false;
            }
        }

        private void ListKapan_EditValueChanged(object sender, EventArgs e)
        {
            DataTable DTabCut = Global.GetReportKapanWise(Val.ToString(ListKapan.EditValue));
            DTabCut.DefaultView.Sort = "rough_cut_id";
            DTabCut = DTabCut.DefaultView.ToTable();

            ListCutNo.Properties.DataSource = DTabCut;
            ListCutNo.Properties.DisplayMember = "rough_cut_no";
            ListCutNo.Properties.ValueMember = "rough_cut_id";
        }

        private void BtnSearchAll_Click(object sender, EventArgs e)
        {
            if (!ValidateDetails())
            {
                return;
            }

            RBtnType.Enabled = false;
            if (RBtnType.EditValue.ToString() == "ALL")
            {
                DataSet DTab_Issue = objRoughRate.RoughRateAll_GetData(Val.DBDate(DtpEntryDateAll.Text), Val.Trim(ListKapan.Properties.GetCheckedItems()), Val.Trim(ListCutNo.Properties.GetCheckedItems()));

                grdMakableSawable.DataSource = DTab_Issue.Tables[0];
                dgvMakableSawable.BestFitColumns();

                grdRejection.DataSource = DTab_Issue.Tables[1];
                dgvRejection.BestFitColumns();
            }
        }

        private void RepMakableRate_EditValueChanged(object sender, EventArgs e)
        {
            TextEdit textEditor = (TextEdit)sender;

            int rowindex = dgvMakableSawable.FocusedRowHandle;
            int RowNumber = dgvMakableSawable.FocusedRowHandle;
            decimal Current_Rate = Val.ToDecimal(textEditor.EditValue);
            decimal Grid_Carat = Val.ToDecimal(dgvMakableSawable.GetRowCellValue(rowindex, "carat"));
            decimal Grid_Amount = Val.ToDecimal(Current_Rate * Grid_Carat);
            dgvMakableSawable.SetRowCellValue(rowindex, "amount", Math.Round(Val.ToDecimal(Grid_Amount), 3));
        }

        private void dgvMakableSawable_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (Val.ToDecimal(clmMakableAmount.SummaryItem.SummaryValue) > 0 && Val.ToDecimal(clmMakableCarat.SummaryItem.SummaryValue) > 0)
                {
                    m_numSummMakableRate = Math.Round((Val.ToDecimal(clmMakableAmount.SummaryItem.SummaryValue) / Val.ToDecimal(clmMakableCarat.SummaryItem.SummaryValue)), 3, MidpointRounding.AwayFromZero);

                }
                else
                {
                    m_numSummMakableRate = 0;
                }
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "rate")
                {
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = m_numSummMakableRate;
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        private void CalculateGridAmount_Makable(int rowindex)
        {
            try
            {
                dgvMakableSawable.SetRowCellValue(rowindex, "amount", Math.Round((Val.ToDouble(dgvMakableSawable.GetRowCellValue(rowindex, "rate")) * Val.ToDouble(dgvMakableSawable.GetRowCellValue(rowindex, "carat"))), 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvMakableSawable_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount_Makable(dgvMakableSawable.FocusedRowHandle);
        }

        private void dgvMakableSawable_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount_Makable(e.PrevFocusedRowHandle);
        }
    }
}
