using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Transaction;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using DERP.Report;
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
    public partial class FrmMFGAssortShading : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        DataTable m_dtbSubProcess = new DataTable();
        MFGAssortFirst objAssortFirst;
        MFGAssortShading objAssortShading;
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
        DataTable m_dtbKapan;
        DataTable m_dtOutstanding;

        Int64 m_numForm_id;
        Int64 IntRes;
        Int64 Receive_IntRes;

        int m_manager_id;
        int m_emp_id;
        int m_IsLot;

        decimal m_OsCarat;
        decimal m_balcarat;

        #endregion

        #region Constructor
        public FrmMFGAssortShading()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objAssortFirst = new MFGAssortFirst();
            objAssortShading = new MFGAssortShading();
            objRoughSieve = new MfgRoughSieve();
            objClarity = new MfgRoughClarityMaster();
            ObjReportDetailProperty = new New_Report_DetailProperty();
            objProcessReceive = new MFGProcessReceive();
            DtControlSettings = new DataTable();
            dtTemp = new DataTable();
            m_dtbParam = new DataTable();
            m_dtCut = new DataTable();
            m_DtProcess = new DataTable();
            m_dtOutstanding = new DataTable();
            m_dtbKapan = new DataTable();
            m_numForm_id = 0;
            m_balcarat = 0;
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
            AddKeyPressListener(this);
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
                List<ListError> lstError = new List<ListError>();
                Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
                rtnCtrls = Global.CheckCompulsoryControls(Val.ToInt(ObjPer.form_id), this);
                if (rtnCtrls.Count > 0)
                {
                    foreach (KeyValuePair<Control, string> entry in rtnCtrls)
                    {
                        if (entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.DateEdit || entry.Key is DevExpress.XtraEditors.TextEdit)
                        {
                            lstError.Add(new ListError(13, entry.Value));
                        }
                    }
                    rtnCtrls.First().Key.Focus();
                    BLL.General.ShowErrors(lstError);
                    Cursor.Current = Cursors.Arrow;
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
                DialogResult result = MessageBox.Show("Do you want to save Assort Shading Receive data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result != DialogResult.Yes)
                {
                    btnSave.Enabled = true;
                    return;
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                backgroundWorker_AssortFirstReceive.RunWorkerAsync();

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
                //lueProcess.EditValue = System.DBNull.Value;
                //lueSubProcess.EditValue = System.DBNull.Value;

                //for (int i = 0; i < luePurity.Properties.Items.Count; i++)
                //    luePurity.Properties.Items[i].CheckState = CheckState.Unchecked;

                //for (int i = 0; i < lueSieve.Properties.Items.Count; i++)
                //    lueSieve.Properties.Items[i].CheckState = CheckState.Unchecked;

                txtLotId.Text = "0";
                lblOsCarat.Text = "0";
                lblOsPcs.Text = "0";
                grdAssortShading.DataSource = null;
                dgvAssortShading.Columns.Clear();
                panelControl1.Enabled = true;
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
                dgvAssortShading.Columns.Clear();
                pivot pt = new pivot(objAssortShading.AssortShadingGetData(Val.ToString(luePurity.EditValue), Val.ToString(lueSieve.EditValue)));
                dtTemp = pt.PivotDataSuperPlusAssortment(new string[] { "purity_id", "purity", "group" }, new string[] { "carat", "per(%)" }, new AggregateFunction[] { AggregateFunction.Sum, AggregateFunction.Sum }, new string[] { "sieve" });

                DataColumn Total = new System.Data.DataColumn("Total", typeof(System.Decimal));
                DataColumn Percentage = new System.Data.DataColumn("Total%", typeof(System.Decimal));
                Total.DefaultValue = "0.000";
                Percentage.DefaultValue = "0.000";
                dtTemp.Columns.Add(Total);
                dtTemp.Columns.Add(Percentage);

                grdAssortShading.DataSource = dtTemp;

                dgvAssortShading.Columns["purity_id"].Visible = false;
                dgvAssortShading.Columns["purity"].OptionsColumn.ReadOnly = true;
                dgvAssortShading.Columns["purity"].OptionsColumn.AllowFocus = false;
                dgvAssortShading.Columns["Total"].OptionsColumn.ReadOnly = true;
                dgvAssortShading.Columns["Total"].OptionsColumn.AllowFocus = false;
                dgvAssortShading.Columns["Total%"].OptionsColumn.ReadOnly = true;
                dgvAssortShading.Columns["Total%"].OptionsColumn.AllowFocus = false;
                dgvAssortShading.Columns["purity"].Fixed = FixedStyle.Left;
                dgvAssortShading.Columns["purity"].Caption = "#";
                dgvAssortShading.ClearGrouping();
                dgvAssortShading.Columns["group"].GroupIndex = 0;
                dgvAssortShading.OptionsView.ShowGroupedColumns = false;
                dgvAssortShading.ExpandAllGroups();
                dgvAssortShading.OptionsPrint.PrintFooter = false;
                for (int i = 0; i <= dtTemp.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtTemp.Columns.Count - 1; j++)
                    {
                        if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                        {
                            dgvAssortShading.Columns[j].OptionsColumn.AllowEdit = false;

                            string[] col = dtTemp.Columns[j].ToString().Split('_');
                            string currcol = "";
                            currcol = "%";
                            dgvAssortShading.Columns[j].Caption = currcol;
                            dgvAssortShading.Columns[dtTemp.Columns[j].ToString()].Width = 50;
                        }
                        if (dtTemp.Columns[j].ToString().Contains("carat"))
                        {
                            string[] col = dtTemp.Columns[j].ToString().Split('_');
                            string currcol = "";
                            currcol = col[1];
                            dgvAssortShading.Columns[j].Caption = currcol;
                            dgvAssortShading.Columns[dtTemp.Columns[j].ToString()].Width = 50;
                        }
                        if (dtTemp.Columns[j].ToString().Contains("Total%"))
                        {
                            dgvAssortShading.Columns[j].Caption = "%";
                            dgvAssortShading.Columns[dtTemp.Columns[j].ToString()].Width = 50;
                        }
                        if (dtTemp.Columns[j].ToString() == "Total")
                        {
                            dgvAssortShading.Columns[dtTemp.Columns[j].ToString()].Width = 50;
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
                            GridColumn column1 = dgvAssortShading.Columns[carat];
                            dgvAssortShading.Columns[carat].SummaryItem.DisplayFormat = "{0:n3}";
                            column1.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }

                        if (dtTemp.Columns[j].ToString().Contains("per(%)"))
                        {
                            string Per = dtTemp.Columns[j].ToString();
                            GridColumn column2 = dgvAssortShading.Columns[Per];
                            dgvAssortShading.Columns[Per].SummaryItem.DisplayFormat = " {0:n2}";
                            column2.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total"))
                        {
                            string total = dtTemp.Columns[j].ToString();
                            GridColumn column3 = dgvAssortShading.Columns[total];
                            dgvAssortShading.Columns[total].SummaryItem.DisplayFormat = "{0:n3}";
                            column3.SummaryItem.SummaryType = SummaryItemType.Sum;
                        }
                        if (dtTemp.Columns[j].ColumnName.Contains("Total%"))
                        {
                            string totalper = dtTemp.Columns[j].ToString();
                            GridColumn column4 = dgvAssortShading.Columns[totalper];
                            dgvAssortShading.Columns[totalper].SummaryItem.DisplayFormat = "{0:n2}";
                            column4.SummaryItem.SummaryType = SummaryItemType.Custom;
                        }

                    }
                    break;
                }
                dgvAssortShading.OptionsView.ShowFooter = true;
                panelControl1.Enabled = false;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
        }

        DataTable dtIssOS = new DataTable();

        private void lueCutNo_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    MFGProcessReceive objProcessRecieve = new MFGProcessReceive();

            //    m_dtOutstanding = Global.GetStockCutwise(Val.ToInt(lueKapan.EditValue), Val.ToInt(lueCutNo.EditValue));

            //    if (m_dtOutstanding.Rows.Count > 0)
            //    {
            //        m_kapan_id = Val.ToInt(m_dtOutstanding.Rows[0]["kapan_id"]);
            //        lblOsPcs.Text = Val.ToString(Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_pcs"]));
            //        lblOsCarat.Text = Val.ToString(Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]));
            //        m_balcarat = Val.ToDecimal(m_dtOutstanding.Rows[0]["balance_carat"]);
            //        txtLotId.Text = Val.ToString(m_dtOutstanding.Rows[0]["lot_id"]);
            //    }
            //    else
            //    {
            //        lblOsPcs.Text = Val.ToString("0");
            //        lblOsCarat.Text = Val.ToString("0.00");
            //        m_balcarat = 0;
            //        return;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    BLL.General.ShowErrors(ex);
            //    return;
            //}
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
                    (grdAssortShading.FocusedView as ColumnView).FocusedRowHandle++;
                    e.Handled = true;
                }
            }
            catch { }
        }

        private void lueClarity_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmClarityMaster frmClarity = new FrmClarityMaster();
                frmClarity.ShowDialog();
                //Global.LOOKUPPurity(luePurity);
                ClarityMaster objPurity = new ClarityMaster();
                DataTable dtbClarity = new DataTable();
                dtbClarity = objPurity.GetData();

                DataTable dtb = dtbClarity.Select("purity_group in('C1','C2')").CopyToDataTable();

                luePurity.Properties.DataSource = dtb;
                luePurity.Properties.ValueMember = "purity_id";
                luePurity.Properties.DisplayMember = "purity_name";
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
                DataTable dtAmount = new DataTable();
                dtAmount = (DataTable)grdAssortShading.DataSource;
                string[] col = e.Column.FieldName.Split('_');
                string currcol = "";
                if (col.Length > 3 && e.Column.FieldName.Contains("_") && col != null)
                {
                    currcol = col[0] + "_" + col[1];
                }
                decimal carat = 0;
                decimal total = 0;
                decimal perTotal = 0;
                string colname = "";
                decimal Percent = 0;
                for (int i = 0; i <= dtAmount.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                    {
                        if (e.RowHandle != i)
                            continue;
                        if (dtAmount.Columns[j].ToString().Contains("carat") && dtAmount.Columns[j].ColumnName.Contains(currcol))
                        {
                            perTotal = 0;
                            carat = Val.ToDecimal(dtAmount.Rows[i][j]);
                            total += carat;
                            perTotal = carat;
                            colname = currcol;
                        }
                        if (dtAmount.Columns[j].ToString().Contains("per(%)") && dtAmount.Columns[j].ColumnName.Contains(colname))
                        {
                            Percent = (perTotal * 100) / Val.ToDecimal(lblOsCarat.Text);
                            dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();

                        }

                        if (dtAmount.Columns[j].ColumnName.Contains("Total"))
                        {
                            dtAmount.Rows[i][j] = Math.Round(total, 3).ToString();
                            perTotal = carat;
                        }
                        if (dtAmount.Columns[j].ColumnName.Contains("Total%"))
                        {
                            Percent = (total * 100) / Val.ToDecimal(lblOsCarat.Text);
                            dtAmount.Rows[i][j] = Math.Round(Percent, 2).ToString();
                            break;
                        }

                    }
                    total = 0;
                }
                dgvAssortShading.BestFitColumns();
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

        private void FrmMFGAssortShading_Load(object sender, EventArgs e)
        {
            try
            {
                m_dtbKapan = Global.GetKapanAll_Assort();

                lueKapan.Properties.DataSource = m_dtbKapan;
                lueKapan.Properties.ValueMember = "kapan_id";
                lueKapan.Properties.DisplayMember = "kapan_no";

                m_dtCut = Global.GetRoughCutAll();
                lueCutNo.Properties.DataSource = m_dtCut;
                lueCutNo.Properties.ValueMember = "rough_cut_id";
                lueCutNo.Properties.DisplayMember = "rough_cut_no";

                dtpReceiveDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpReceiveDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpReceiveDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpReceiveDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpReceiveDate.EditValue = DateTime.Now;

                Global.LOOKUPSieve(lueSieve);
                lueSieve.SetEditValue("13,16");

                Global.LOOKUPProcess(lueProcess);
                lueProcess.Text = "ASSORTMENT";
                Global.LOOKUPSubProcess(lueSubProcess);
                Global.LOOKUPRoughShade(luePurity);

                m_dtbSubProcess = (((DataTable)lueSubProcess.Properties.DataSource).Copy());

                lueProcess_EditValueChanged(null, null);

                lueSubProcess.Text = "SHADING";

                m_dtbParam = Global.GetRoughCutAll();

                ClarityMaster objPurity = new ClarityMaster();
                DataTable dtbClarity = new DataTable();
                DataTable dtbdetails = new DataTable();
                dtbClarity = objPurity.GetData(1);

                DataTable dtb = dtbClarity.Select("purity_group in('C1','C2')").CopyToDataTable();

                luePurity.Properties.DataSource = dtb;
                luePurity.Properties.ValueMember = "purity_id";
                luePurity.Properties.DisplayMember = "purity_name";

                string Purity = string.Empty;
                foreach (DataRow DR in dtb.Rows)
                {
                    Purity = Purity + "," + DR["purity_id"];
                    luePurity.SetEditValue(Purity);
                }

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
                            m_balcarat = Val.ToDecimal(dtIssOS.Rows[0]["carat"]);
                        }
                        else
                        {
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
        private void backgroundWorker_AssortFirstReceive_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                MFGProcessReceive MFGProcessReceive = new MFGProcessReceive();
                MFGAssortFirst MFGAssortFirst = new MFGAssortFirst();
                MFGAssortShading MFGAssortReceive = new MFGAssortShading();
                MFGProcessReceiveProperty objMFGProcessReceiveProperty = new MFGProcessReceiveProperty();

                Conn = new BeginTranConnection(true, false);

                DataTable m_DTab = new DataTable();
                ArrayList MyArrayList = new ArrayList();

                IntRes = 0;
                Receive_IntRes = 0;
                Int64 NewHistory_Union_Id = 0;
                int NewIssueId = 0;
                Int64 Lot_SrNo = 0;
                try
                {
                    objMFGProcessReceiveProperty.manager_id = Val.ToInt(0);
                    objMFGProcessReceiveProperty.employee_id = Val.ToInt(0);
                    objMFGProcessReceiveProperty.process_id = Val.ToInt(lueProcess.EditValue);
                    objMFGProcessReceiveProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                    objMFGProcessReceiveProperty.Issue_id = Val.ToInt(NewIssueId);
                    objMFGProcessReceiveProperty.kapan_id = Val.ToInt(lueKapan.EditValue);
                    objMFGProcessReceiveProperty.lot_id = Val.ToInt64(txtLotId.Text);
                    objMFGProcessReceiveProperty.rough_cut_id = Val.ToInt(lueCutNo.EditValue);
                    DataTable DTCheckEntry = new DataTable();
                    DTCheckEntry = MFGAssortFirst.CheckEntry(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                    m_DTab = (DataTable)grdAssortShading.DataSource;

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
                                    objMFGProcessReceiveProperty.purity_id = Val.ToInt(Drw["purity_id"]);
                                    objMFGProcessReceiveProperty.rough_sieve_id = Val.ToInt(Val.ToString(dtbDetail.Columns[i]).Split('_')[0]);
                                    objMFGProcessReceiveProperty.carat = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "carat"]);
                                    objMFGProcessReceiveProperty.percentage = Val.ToDecimal(Drw[Val.ToString(objMFGProcessReceiveProperty.rough_sieve_id) + "_" + "per(%)"]);
                                    objMFGProcessReceiveProperty.rate = Val.ToDecimal(0);
                                    objMFGProcessReceiveProperty.amount = Val.ToDecimal(0);
                                    //if (Val.ToDecimal(m_rate) != 0)
                                    //{
                                    //    objMFGProcessReceiveProperty.amount = Val.ToDecimal(m_rate * objMFGProcessReceiveProperty.carat);
                                    //}
                                    objMFGProcessReceiveProperty.union_id = IntRes;
                                    objMFGProcessReceiveProperty.receive_union_id = Receive_IntRes;
                                    objMFGProcessReceiveProperty.form_id = m_numForm_id;
                                    objMFGProcessReceiveProperty.history_union_id = NewHistory_Union_Id;
                                    objMFGProcessReceiveProperty.lot_srno = Lot_SrNo;

                                    if (objMFGProcessReceiveProperty.carat == 0)
                                        continue;
                                    objMFGProcessReceiveProperty = MFGAssortReceive.Save(objMFGProcessReceiveProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                                    IntRes = objMFGProcessReceiveProperty.union_id;

                                    Receive_IntRes = objMFGProcessReceiveProperty.receive_union_id;
                                    NewHistory_Union_Id = Val.ToInt64(objMFGProcessReceiveProperty.history_union_id);
                                    NewIssueId = Val.ToInt(objMFGProcessReceiveProperty.Issue_id);
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
        private void backgroundWorker_AssortFirstReceive_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    if (Global.Confirm("Assort Shading Data Save Succesfully.... " + "\n Are You Sure To Print ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            btn_Print_Click(null, null);
                            btnClear_Click(null, null);
                        }
                        catch (Exception ex)
                        {
                            Global.Message(ex.ToString());
                            return;
                        }
                    }
                }
                else
                {
                    Global.Confirm("Error In Assort Shading Receive");
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
                dtAmount = (DataTable)grdAssortShading.DataSource;
                decimal percentage = 0;
                decimal totPer = 0;
                decimal carat = 0;
                decimal totcarat = 0;
                string column = "";
                for (int j = 0; j <= dtAmount.Columns.Count - 1; j++)
                {
                    if (dtAmount.Columns[j].ToString().Contains("per(%)"))
                    {
                        column = dtAmount.Columns[j].ToString();

                    }

                    if (dtAmount.Columns[j].ToString().Contains("carat"))
                    {
                        carat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (dtAmount.Columns[j].ColumnName.ToString() == "Total")
                    {
                        totcarat = dtAmount.AsEnumerable().Sum(x => Val.ToDecimal(x[dtAmount.Columns[j]]));
                    }
                    if (carat > 0 && Val.ToDecimal(lblOsCarat.Text) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == column)
                        {
                            percentage = Math.Round(carat * 100 / Val.ToDecimal(lblOsCarat.Text), 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = percentage;
                            column = "";
                            carat = 0;
                        }
                    }


                    if (totcarat > 0 && Val.ToDecimal(lblOsCarat.Text) > 0)
                    {
                        if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName == "Total%")
                        {
                            totPer = Math.Round(totcarat * 100 / Val.ToDecimal(lblOsCarat.Text), 3);
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = totPer;
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
                            dgvAssortShading.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvAssortShading.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvAssortShading.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvAssortShading.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvAssortShading.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvAssortShading.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvAssortShading.ExportToCsv(Filepath);
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

        private void btn_Print_Click(object sender, EventArgs e)
        {
            DataTable DTab_IssueJanged = objAssortFirst.Print_Shadding(Val.ToInt32(lueKapan.EditValue), Val.ToInt32(lueCutNo.EditValue), Val.ToInt32(lueProcess.EditValue), Val.ToInt32(lueSubProcess.EditValue));
            DTab_IssueJanged.TableName = "Shadding";
            //if (DTab_IssueJanged.Rows.Count > 0)
            //{
            //    foreach (DataRow DR in DTab_IssueJanged.Rows)
            //    {
            //        DR["org_carat"] = Val.ToDecimal(lblOsCarat.Text);
            //    }
            //}
            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(DTab_IssueJanged);
            //foreach (DataTable DTab in DSetSemi1.Tables)
            //    FrmReportViewer.DS.Tables.Add(DTab.Copy());
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm("CrtPolishGrading_Shadding", 120, FrmReportViewer.ReportFolder.ACCOUNT);

            DTab_IssueJanged = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }
    }
}
