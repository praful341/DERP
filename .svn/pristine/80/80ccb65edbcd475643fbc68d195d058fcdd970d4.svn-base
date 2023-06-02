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
    public partial class FrmMFGProcessReceive : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGProcessReceive objProcessReceive;
        MfgRoughSieve objRoughSieve;
        MfgRoughClarityMaster objClarity;
        public New_Report_DetailProperty ObjReportDetailProperty;
        Control _NextEnteredControl = new Control();
        private List<Control> _tabControls = new List<Control>();

        DataTable DtControlSettings;
        DataTable dtTemp;
        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_DtProcess;
        DataTable m_dtbType;
        DataTable m_dtOutstanding;
        DataTable m_dtbProcess;
        DataTable m_dtbSubProcess;
        DataTable m_dtbKapan;
        DataTable m_DTab = new DataTable();
        //DataTable DTab_KapanWiseData = new DataTable();

        int m_Kapan_id = 0;
        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Receive_IntRes;
        int m_manager_id;
        int m_emp_id;
        decimal m_OsCarat;
        int m_issue_id;
        decimal m_rate;

        decimal m_balcarat;

        bool m_blnflag;

        #endregion

        #region Constructor
        public FrmMFGProcessReceive()
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
            m_blnflag = new bool();
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
            //AddKeyPressListener(this);
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
        private void FrmMFGProcessReceive_Load(object sender, EventArgs e)
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

                //DataTable dtbSieve = new DataTable();
                //dtbSieve = objRoughSieve.GetData();
                //lueSieve.Properties.DataSource = dtbSieve;
                //lueSieve.Properties.ValueMember = "rough_sieve_id";
                //lueSieve.Properties.DisplayMember = "sieve_name";

                Global.LOOKUPRoughSieve(lueSieve);

                Global.LOOKUPProcess(lueProcess);
                Global.LOOKUPSubProcess(lueSubProcess);

                // Add By Praful On 29072021

                //DTab_KapanWiseData = Global.GetRoughStockWise(Val.ToInt(0), Val.ToInt32(0));

                // End By Praful On 29072021

                m_dtbProcess = (((DataTable)lueProcess.Properties.DataSource).Copy());
                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                string strFilter_Process = string.Empty;

                strFilter_Process = "process_id in(2,3)";

                m_dtbProcess.DefaultView.RowFilter = strFilter_Process;
                m_dtbProcess.DefaultView.ToTable();

                lueProcess.Properties.DataSource = m_dtbProcess;
                lueProcess.Properties.ValueMember = "process_id";
                lueProcess.Properties.DisplayMember = "process_name";
                lueProcess.EditValue = System.DBNull.Value;

                m_dtbParam = Global.GetRoughCutAll();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
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

                btnSave.Enabled = false;
                DataTable dtTemp = new DataTable();
                dtTemp = (DataTable)grdProcessReceive.DataSource;
                List<ListError> lstError = new List<ListError>();
                if (dtTemp == null)
                {
                    Global.Message("Atleast 1 record must be enter in grid");
                    btnSave.Enabled = true;
                    return;
                }
                if (!ValidateDetails())
                {
                    //blnReturn = false;
                    return;
                }

                DialogResult result = MessageBox.Show("Do you want to save receive data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_ProcessReceive.RunWorkerAsync();

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
                else if (view.FocusedColumn.FieldName.Contains("rate"))
                {
                    double rate = 0.000;
                    if (!double.TryParse(e.Value as string, out rate))
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
                            txtBalanceCarat.Text = Val.ToString(m_OsCarat);
                            m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                        }
                        else
                        {
                            txtBalanceCarat.Text = "0";
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
                        txtBalanceCarat.Text = Val.ToString(m_dtOutstanding.Rows[0]["carat"]);
                        m_OsCarat = Val.ToDecimal(txtBalanceCarat.Text);
                        m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["carat"]);
                    }
                    else
                    {
                        txtBalanceCarat.Text = "0";
                    }

                }
                else
                {
                    Global.Message("Lot not issue in this process.");
                }
            }
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

                txtBalanceCarat.Text = "0";
                txtIssProcess.Text = "";
                txtLotId.Text = "0";
                grdProcessReceive.DataSource = null;
                dgvProcessReceive.Columns.Clear();
                m_blnflag = false;
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
                if (!ValidateDetails())
                {
                    return;
                }
                dtTemp = new DataTable();
                dgvProcessReceive.Columns.Clear();
                //grdProcessReceive.DataSource = dtTemp;
                pivot pt = new pivot(objProcessReceive.GetData(Val.ToInt64(txtLotId.Text), Val.ToString(lueType.Text), Val.ToString(lueClarity.EditValue), Val.ToString(lueSieve.EditValue)));
                dtTemp = pt.PivotDataSuperPlus(new string[] { "clarity_id", "clarity" }, new string[] { "carat", "rate", "amount" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });
                //m_dtbDetail = objProcessReceive.GetData(); 
                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                DataColumn Rate = new System.Data.DataColumn("Rate", typeof(System.Decimal));
                DataColumn Amount = new System.Data.DataColumn("Amount", typeof(System.Decimal));
                Total.DefaultValue = "0.000";
                Rate.DefaultValue = "0.000";
                Amount.DefaultValue = "0.000";
                dtTemp.Columns.Add(Total);
                dtTemp.Columns.Add(Rate);
                dtTemp.Columns.Add(Amount);


                //  Add By Praful On 19062019 Total Summary

                decimal rate = 0;
                decimal carat = 0;
                decimal total = 0;
                decimal totRate = 0;
                decimal totAmount = 0;
                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            carat = Val.ToDecimal(dtTemp.Rows[i][j]);
                            total += carat;
                        }
                        else if (dtTemp.Columns[j].ToString().Contains("rate"))
                        {
                            rate = Val.ToDecimal(dtTemp.Rows[i][j]);
                        }
                        else if (dtTemp.Columns[j].ToString().Contains("amount"))
                        {
                            dtTemp.Rows[i][j] = Math.Round((carat * rate), 0).ToString();
                            totAmount += (carat * rate);
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtTemp.Rows[i][j] = Math.Round(total, 3).ToString();

                        }
                        else if (dtTemp.Columns[j].ColumnName.Contains("Rate"))
                        {
                            if (totAmount != 0 && total != 0)
                            {
                                totRate = totAmount / total;
                            }
                            else
                            {
                                totRate = 0;
                            }
                            dtTemp.Rows[i][j] = Math.Round(totRate, 2).ToString();

                        }
                        else if (dtTemp.Columns[j].ColumnName.Contains("Amount"))
                        {
                            decimal amount = Math.Round((total * totRate), 0);
                            dtTemp.Rows[i][j] = amount.ToString();
                            break;
                        }
                    }
                    totAmount = 0;
                    total = 0;
                    totRate = 0;
                }

                // End

                grdProcessReceive.DataSource = dtTemp;

                dgvProcessReceive.Columns["clarity_id"].Visible = false;
                dgvProcessReceive.Columns["clarity"].OptionsColumn.ReadOnly = true;
                dgvProcessReceive.Columns["clarity"].OptionsColumn.AllowFocus = false;
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
            //                    m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]), "CHIPIYO FINAL");
            //                    //txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
            //                    if (m_DtProcess.Rows.Count > 0)
            //                    {
            //                        txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
            //                    }

            //                    //prdId = Val.ToInt(dr[0]["prediction_id"]);
            //                    //if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
            //                    //{
            //                    //    GetOsCarat(Val.ToInt64(txtLotId.Text));
            //                    //}
            //                    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
            //                    if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
            //                    {
            //                        GetOsCarat(Val.ToInt64(txtLotId.Text));

            //                        DataTable DTab_Process = objProcessRecieve.MFG_ProcessName_GetData(Val.ToInt64(txtLotId.Text));

            //                        lueProcess.Properties.DataSource = DTab_Process;
            //                        lueProcess.Properties.DisplayMember = "process_name";
            //                        lueProcess.Properties.ValueMember = "process_id";

            //                        //dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt(m_DtProcess.Rows[0]["process_id"]), Val.ToInt(m_DtProcess.Rows[0]["sub_process_id"]), 1, "R");
            //                        //if (dtIssOS.Rows.Count > 0)
            //                        //{
            //                        //    txtBalanceCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
            //                        //    m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
            //                        //}
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
        private void txtLotId_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtLotId.EditValue != System.DBNull.Value)
            //    {
            //        if (m_dtCut.Rows.Count > 0)
            //        {
            //            DataRow[] dr = m_dtCut.Select("lot_id ='" + Val.ToString(txtLotId.Text) + "'");
            //            if (dr.Length > 0)
            //            {
            //                lueCutNo.EditValue = Val.ToInt64(dr[0]["rough_cut_id"]);
            //                m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]));
            //                txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
            //            }
            //            else
            //            {
            //                Global.Message("Cut No Data Not found");
            //                lueCutNo.EditValue = null;
            //                txtIssProcess.Text = string.Empty;
            //                return;
            //            }
            //            //lueCutNo.Tag = Val.ToString(dr[0]["rough_cut_no"]);
            //        }
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
                if (m_dtbParam.Rows.Count > 0)
                {
                    if (txtLotId.EditValue != System.DBNull.Value)
                    {
                        //DataRow[] dr = m_dtCut.Select("lot_id ='" + Val.ToString(txtLotId.Text) + "'");
                        //if (dr.Length > 0)
                        //{

                        if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
                        {

                            m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotId.Text), "CHIPIYO FINAL");
                            if (m_DtProcess.Rows.Count > 0)
                            {
                                m_blnflag = true;
                                txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
                                ////lueCutNo.Text = Val.ToString(m_DtProcess.Rows[0]["rough_cut_no"]);
                                //GetOsCarat(Val.ToInt64(txtLotId.Text));
                                GetOsCarat(Val.ToInt64(txtLotId.Text));
                                DataTable dtIssOS = new DataTable();
                                MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
                                dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt(m_DtProcess.Rows[0]["process_id"]), Val.ToInt(m_DtProcess.Rows[0]["sub_process_id"]), 1, "R");

                                if (dtIssOS.Rows.Count > 0)
                                {
                                    txtBalanceCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
                                    m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                                }
                                else if (Val.ToDecimal(m_balcarat) > 0 && dtIssOS.Rows.Count == 0)
                                {
                                    txtBalanceCarat.Text = Val.ToString(m_balcarat);
                                }
                            }
                            else
                            {
                                Global.Message("Lot Not Issue");
                                lueCutNo.EditValue = null;
                                txtIssProcess.Text = string.Empty;
                                txtLotId.Text = "0";
                                m_blnflag = false;
                                return;
                            }
                            //}
                            //m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]));
                            //txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
                            //m_blnflag = false;
                        }
                        else
                        {
                            Global.Message("Cut No Data Not found");
                            lueCutNo.EditValue = null;
                            txtIssProcess.Text = string.Empty;
                            txtLotId.Text = "0";
                            m_blnflag = true;
                            return;
                        }
                        //m_blnflag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueKapan_EditValueChanged(object sender, EventArgs e)
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
                    currcol = col[0] + "_" + col[1] + "_" + col[2];
                }
                decimal rate = 0;
                decimal carat = 0;
                decimal total = 0;
                decimal totRate = 0;
                decimal totAmount = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
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
        private void backgroundWorker_ProcessReceive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGProcessReceive MFGProcessReceive = new MFGProcessReceive();
                MFGProcessReceiveProperty objMFGProcessReceiveProperty = new MFGProcessReceiveProperty();

                Conn = new BeginTranConnection(true, false);


                ArrayList MyArrayList = new ArrayList();
                List<ListError> lstError = new List<ListError>();
                IntRes = 0;
                Receive_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                Int64 Lot_SrNo = 0;
                try
                {
                    DataTable dtIssueDet = MFGProcessReceive.ProcessIssue_GetData(Val.ToInt64(txtLotId.Text), "CHIPIYO FINAL");
                    objMFGProcessReceiveProperty.manager_id = Val.ToInt(dtIssueDet.Rows[0]["manager_id"]);
                    objMFGProcessReceiveProperty.employee_id = Val.ToInt(dtIssueDet.Rows[0]["employee_id"]);
                    //objMFGProcessReceiveProperty.process_id = Val.ToInt(dtIssueDet.Rows[0]["process_id"]);
                    objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                    //objMFGProcessReceiveProperty.sub_process_id = Val.ToInt(dtIssueDet.Rows[0]["sub_process_id"]);
                    objMFGProcessReceiveProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                    objMFGProcessReceiveProperty.Issue_id = Val.ToInt(dtIssueDet.Rows[0]["issue_id"]);

                    if (Val.ToInt(dtIssueDet.Rows[0]["issue_id"]) == 0)
                    {
                        Global.Message("Issue data not found in this Cut No: " + Val.ToString(lueCutNo.Text));
                        return;
                    }

                    //m_DTab = (DataTable)grdProcessReceive.DataSource;

                    DataTable dtbDetail = m_DTab.Copy();
                    //decimal Carat = 0;

                    //for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                    //{
                    //    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    //    {
                    //        if (dtTemp.Columns[j].ToString().Contains("carat"))
                    //        {
                    //            if (Val.ToDecimal(dtTemp.Rows[i][j]) != 0)
                    //            {
                    //                Carat = Carat + Val.ToDecimal(dtTemp.Rows[i][j]);
                    //            }
                    //        }
                    //    }
                    //}

                    decimal Total = m_DTab.AsEnumerable().Sum(x => Val.ToDecimal(x[m_DTab.Columns["Total"]]));

                    if (Total > (Val.ToDecimal(txtBalanceCarat.Text)))
                    {
                        lstError.Add(new ListError(36, "Transfer Carat not Equal to Balance Carat."));
                        return;
                    }

                    //DataTable DTab_Carat = objProcessReceive.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), 0, 0, 1, "R");

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
                                    objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "carat"]);
                                    objMFGProcessReceiveProperty.rate = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "rate"]);
                                    objMFGProcessReceiveProperty.amount = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "amount"]);
                                    objMFGProcessReceiveProperty.union_id = IntRes;
                                    objMFGProcessReceiveProperty.receive_union_id = Receive_IntRes;
                                    objMFGProcessReceiveProperty.form_id = m_numForm_id;
                                    objMFGProcessReceiveProperty.kapan_id = Val.ToInt(m_Kapan_id);
                                    if (objMFGProcessReceiveProperty.carat == 0)
                                        continue;

                                    objMFGProcessReceiveProperty.carat_plus = Val.ToDecimal(0);
                                    objMFGProcessReceiveProperty.loss_carat = Val.ToDecimal(0);
                                    objMFGProcessReceiveProperty.loss_count = Val.ToInt(1);
                                    objMFGProcessReceiveProperty.history_union_id = NewHistory_Union_Id;
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
        private void backgroundWorker_ProcessReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Process Receive Save Succesfully");
                    btnClear_Click(null, null);
                    lueType.EditValue = "BOTH";
                }
                else
                {
                    Global.Confirm("Error In Process Receive");
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

                decimal rate = 0;
                decimal carat = 0;
                decimal amount = 0;
                decimal totrate = 0;
                decimal totcarat = 0;
                decimal totamount = 0;
                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
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
                            carat = 0;
                            amount = 0;
                        }

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
                            totamount = 0;
                            totcarat = 0;
                        }

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
                if (lueCutNo.Text == "")
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
                if (txtIssProcess.Text != "CHIPIYO FINAL")
                {
                    lstError.Add(new ListError(5, "Lot not issue in CHIPIYO FINAL Process"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtLotId.Focus();
                    }
                }

                m_DTab = (DataTable)grdProcessReceive.DataSource;

                if (m_DTab != null)
                {
                    if (m_DTab.Rows.Count > 0)
                    {
                        decimal Total = m_DTab.AsEnumerable().Sum(x => Val.ToDecimal(x[m_DTab.Columns["Total"]]));

                        if (Total > (Val.ToDecimal(txtBalanceCarat.Text)))
                        {
                            lstError.Add(new ListError(36, "Transfer Carat not Equal to Balance Carat."));
                            if (!blnFocus)
                            {
                                btnSave.Enabled = true;
                                blnFocus = true;
                            }
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
                    //m_blnflag = true;
                    m_Kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
                }
                else
                {
                    BLL.General.ShowErrors("Cut No not Found");
                    //txtPcs.Text = "0";
                    txtBalanceCarat.Text = "0";
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

        private void btnSearchData_Click(object sender, EventArgs e)
        {
            FrmMFGSearchProcess FrmSearchProcess = new FrmMFGSearchProcess();
            FrmSearchProcess.FrmMFGProcessReceive = this;
            //FrmSearchProcess.DTab = DtPending;
            FrmSearchProcess.ShowForm(this);
        }

        private void lueCutNo_Validated(object sender, EventArgs e)
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
            //                    m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt(dr[0]["lot_id"]), "CHIPIYO FINAL");
            //                    if (m_DtProcess.Rows.Count > 0)
            //                    {
            //                        txtIssProcess.Text = Val.ToString(m_DtProcess.Rows[0]["process"]);
            //                    }
            //                    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();
            //                    if (txtLotId.Text != string.Empty || Val.ToInt64(txtLotId.Text) != 0)
            //                    {
            //                        GetOsCarat(Val.ToInt64(txtLotId.Text));

            //                        DataTable DTab_Process = objProcessRecieve.MFG_ProcessName_GetData(Val.ToInt64(txtLotId.Text));

            //                        lueProcess.Properties.DataSource = DTab_Process;
            //                        lueProcess.Properties.DisplayMember = "process_name";
            //                        lueProcess.Properties.ValueMember = "process_id";

            //                        //dtIssOS = objProcessRecieve.Carat_OutStanding_GetData(Val.ToInt64(txtLotId.Text), Val.ToInt(m_DtProcess.Rows[0]["process_id"]), Val.ToInt(m_DtProcess.Rows[0]["sub_process_id"]), 1, "R");
            //                        //if (dtIssOS.Rows.Count > 0)
            //                        //{
            //                        //    txtBalanceCarat.Text = Val.ToString(Val.ToDecimal(dtIssOS.Rows[0]["carat"]));
            //                        //    m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
            //                        //}
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



            try
            {

                if (!m_blnflag)
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
                                    DataRow[] drProcess = dtIssOS.Select("process_name ='" + Val.ToString("CHIPIYO FINAL") + "'");
                                    if (drProcess.Length > 0)
                                    {
                                        // lblOsPcs.Text = Val.ToString(Val.ToDecimal(drProcess[0]["pcs"]));
                                        txtBalanceCarat.Text = Val.ToString(Val.ToDecimal(drProcess[0]["carat"]));
                                        //m_process = Val.ToString(drProcess[0]["process_name"]);
                                        lueProcess.Text = Val.ToString(drProcess[0]["process_name"]);
                                        lueSubProcess.Text = Val.ToString(drProcess[0]["sub_process_name"]);
                                        // m_balPcs = Val.ToInt(drProcess[0]["pcs"]);
                                        m_balcarat = Val.ToDecimal(drProcess[0]["carat"]);
                                        lueProcess.Enabled = false;
                                        lueSubProcess.Enabled = false;
                                        m_DtProcess = objProcessReceive.GetIssueProcess(Val.ToInt64(txtLotId.Text), "CHIPIYO FINAL");
                                        if (m_DtProcess.Rows.Count > 0)
                                        {
                                            m_rate = Val.ToDecimal(m_DtProcess.Rows[0]["rate"]);
                                        }
                                    }
                                    else
                                    {
                                        Global.Message("Lot not issue in Chipiyo Final Process");
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
                    m_blnflag = false;
                }
                else
                {
                    m_blnflag = false;
                }

            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }

        private void txtLotId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
