using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction.MFG
{
    public partial class FrmMFGRateUpdateProcessIssue : DevExpress.XtraEditors.XtraForm
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

        #endregion

        #region Constructor
        public FrmMFGRateUpdateProcessIssue()
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
            AddGotFocusListener(this);
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

                lblCarat.Text = Val.ToString(0);
                lblRate.Text = Val.ToString(0);
                lblAmount.Text = Val.ToString(0);

                grdRateUpdateProcessIssue.DataSource = null;
                dgvRateUpdateProcessIssue.Columns.Clear();
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
                DataTable dtTemp_old = (DataTable)grdRateUpdateProcessIssue.DataSource;

                dtTemp = new DataTable();
                dgvRateUpdateProcessIssue.Columns.Clear();
                pivot pt = new pivot(objRoughRate.RoughRateProcessIssue_GetData(Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueProcess.EditValue)));
                dtTemp = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Average, AggregateFunction.Sum, }, new string[] { "sieve" });

                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
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

                grdRateUpdateProcessIssue.DataSource = dtTemp;

                dgvRateUpdateProcessIssue.Columns["clarity_id"].Visible = false;
                dgvRateUpdateProcessIssue.Columns["clarity"].OptionsColumn.ReadOnly = true;
                dgvRateUpdateProcessIssue.Columns["clarity"].OptionsColumn.AllowFocus = false;
                dgvRateUpdateProcessIssue.Columns["Total"].OptionsColumn.ReadOnly = true;
                dgvRateUpdateProcessIssue.Columns["Total"].OptionsColumn.AllowFocus = false;
                dgvRateUpdateProcessIssue.Columns["Amount"].OptionsColumn.ReadOnly = true;
                dgvRateUpdateProcessIssue.Columns["Amount"].OptionsColumn.AllowFocus = false;
                dgvRateUpdateProcessIssue.Columns["flag"].OptionsColumn.ReadOnly = true;
                dgvRateUpdateProcessIssue.Columns["clarity"].Fixed = FixedStyle.Left;
                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dgvRateUpdateProcessIssue.Columns[j].Visible = false;
                            dgvRateUpdateProcessIssue.Columns[j].OptionsColumn.AllowEdit = false;
                            dgvRateUpdateProcessIssue.Columns[j].OptionsColumn.AllowFocus = false;
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
                        if (dtTemp.Columns[j].ToString().Contains("Rate"))
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
                            GridColumn column1 = dgvRateUpdateProcessIssue.Columns[carat];
                            dgvRateUpdateProcessIssue.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            string rate = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvRateUpdateProcessIssue.Columns[rate];
                            dgvRateUpdateProcessIssue.Columns[rate].SummaryItem.DisplayFormat = " {0:n3}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            string amount = dtTemp.Columns[j].ToString();
                            GridColumn column3 = dgvRateUpdateProcessIssue.Columns[amount];
                            dgvRateUpdateProcessIssue.Columns[amount].SummaryItem.DisplayFormat = "{0:n3}";
                            column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            string total = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvRateUpdateProcessIssue.Columns[total];
                            dgvRateUpdateProcessIssue.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Rate"))
                        {
                            string totrate = dtTemp.Columns[j].ToString();
                            GridColumn column5 = dgvRateUpdateProcessIssue.Columns[totrate];
                            dgvRateUpdateProcessIssue.Columns[totrate].SummaryItem.DisplayFormat = "{0:n3}";
                            column5.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Amount"))
                        {
                            string totamount = dtTemp.Columns[j].ToString();
                            GridColumn column6 = dgvRateUpdateProcessIssue.Columns[totamount];
                            dgvRateUpdateProcessIssue.Columns[totamount].SummaryItem.DisplayFormat = "{0:n3}";
                            column6.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                    }
                    break;
                }
                dgvRateUpdateProcessIssue.OptionsView.ShowFooter = true;
                dgvRateUpdateProcessIssue.BestFitColumns();
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
                            MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
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
        private void grdProcessReceive_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    (grdRateUpdateProcessIssue.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }

        private void dgvRateUpdateProcessIssue_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdRateUpdateProcessIssue.DataSource;
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
                        if (dtAmount.Columns[j].ColumnName.Contains("Rate"))
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
        private void dgvRateUpdateProcessIssue_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
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

        private void FrmMFGRateUpdateProcessIssue_Load(object sender, EventArgs e)
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

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                m_dtbParam = Global.GetRoughCutAll();

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021
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
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void backgroundWorker_RateUpdateProcessIssue_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGProcessReceive MFGProcessReceive = new MFGProcessReceive();
                MFGProcessReceiveProperty objMFGProcessReceiveProperty = new MFGProcessReceiveProperty();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                try
                {
                    m_DTab = (DataTable)grdRateUpdateProcessIssue.DataSource;
                    DataTable dtbDetail = m_DTab.Select("flag = 1").CopyToDataTable();

                    if (dtbDetail.Rows.Count > 0)
                    {
                        for (int i = dtbDetail.Columns.Count - 1; i >= 3; i--)
                        {
                            if (i < 12)
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
                    int count = dtbDetail.Rows.Count;
                    object sumTotal;
                    sumTotal = m_DTab.Compute("Sum(Amount)", "");
                    decimal rej_amount = Val.ToDecimal(sumTotal);
                    decimal NewAmount = Val.ToDecimal(lblAmount.Text) - rej_amount;
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
                                    objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw["Rate"]);
                                    objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                                    objMFGProcessReceiveProperty.count = Val.ToInt(count);
                                    if (Val.ToDecimal(objMFGProcessReceiveProperty.rate) != 0)
                                    {
                                        objMFGProcessReceiveProperty.amount = Val.ToDecimal(objMFGProcessReceiveProperty.rate * objMFGProcessReceiveProperty.carat);
                                    }
                                    IntRes = MFGProcessReceive.Update(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    count--;
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
        private void backgroundWorker_RateUpdateProcessIssue_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes >= 0)
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
                dtAmount = (DataTable)grdRateUpdateProcessIssue.DataSource;

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

                if (lueProcess.ItemIndex < 0)
                {
                    lstError.Add(new ListError(13, "Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        lueProcess.Focus();
                    }
                }

                if (Search == 0)
                {
                    DataTable DTab_Data = (DataTable)grdRateUpdateProcessIssue.DataSource;
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
                            dgvRateUpdateProcessIssue.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvRateUpdateProcessIssue.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvRateUpdateProcessIssue.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvRateUpdateProcessIssue.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvRateUpdateProcessIssue.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvRateUpdateProcessIssue.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvRateUpdateProcessIssue.ExportToCsv(Filepath);
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
    }
}
