using BLL;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Transaction.MFG;
using DERP.Class;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmMfgManagerUpdateUtilty : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        //BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MfgDepartmentTransferConfirm objDepartmentCnf;
        DataTable m_dtbDetails;
        DataTable m_dtbDetailsGetData;
        int m_numForm_id;
        int IntRes;
        int IntRes1;
        int m_numSelectedCount;
        DataTable m_dtbParam;
        #endregion

        #region Constructor
        public FrmMfgManagerUpdateUtilty()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objDepartmentCnf = new MfgDepartmentTransferConfirm();
            m_dtbDetails = new DataTable();
            m_dtbParam = new DataTable();
            m_dtbDetailsGetData = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;
            IntRes1 = 0;
            m_numSelectedCount = 0;
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
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            if (GlobalDec.gEmployeeProperty.user_name == "TANMAY" || GlobalDec.gEmployeeProperty.user_name == "ASMITA")
            {
                Global.Message("Don't have permission...Please Contact to Administrator...");
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
            objBOFormEvents.ObjToDispose.Add(objDepartmentCnf);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion   

        #region Events     
        private void FrmDepartmentTransferConfirm_Load(object sender, EventArgs e)
        {
            dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpConfirmDate.EditValue = DateTime.Now;

            Global.LOOKUPManagerName(lueManager);

            m_dtbParam = Global.GetRoughCutAll();

            lueCutNo.Properties.DataSource = m_dtbParam;
            lueCutNo.Properties.ValueMember = "rough_cut_id";
            lueCutNo.Properties.DisplayMember = "rough_cut_no";
            //Global.LOOKUPRoughSieveWages(lueWagesSieve);
            //GetData();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
                if (Val.ToString(dtpConfirmDate.Text) == "" && chkCnfDate.Checked == true)
                {
                    Global.Message("Confirm Date not be blank");
                    dtpConfirmDate.Focus();
                    return;
                }
                if (Val.ToString(lueManager.Text) == "" && chkCnfDate.Checked == false)
                {
                    Global.Message("Please Select Manager");
                    lueManager.Focus();
                    return;
                }
                DateTime endDate = Convert.ToDateTime(DateTime.Today);
                endDate = endDate.AddDays(15);

                if (Convert.ToDateTime(dtpConfirmDate.Text) >= endDate)
                {
                    Global.Message(" Confirm Date Not Be Update Permission After 15 Days Confirm this Lot ID...Please Contact to Administrator");
                    dtpConfirmDate.Focus();
                    return;
                }
                btnConfirm.Enabled = false;
                m_dtbDetails.AcceptChanges();
                if (m_dtbDetails.Rows.Count > 0 && m_dtbDetailsGetData.Rows.Count > 0)
                {
                    Global.Message("Process 1 Request at a time");
                    lueManager.Focus();
                    return;
                }
                if (m_dtbDetails.Rows.Count == 0 && m_dtbDetailsGetData.Rows.Count > 0)
                {
                    m_dtbDetails = m_dtbDetailsGetData.Copy();
                }
                if (m_dtbDetails.Rows.Count > 0)
                {

                    DialogResult result = MessageBox.Show("Do you want to Update Manager/Confirm data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        btnConfirm.Enabled = true;
                        return;
                    }

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_DeptConfirm.RunWorkerAsync();
                }
                else
                {
                    General.ShowErrors("Atleast 1 Lot must be select in grid.");
                    btnConfirm.Enabled = true;
                    return;
                }
                btnConfirm.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtLotID_Validated(object sender, EventArgs e)
        {
            if (txtLotID.Text.Length == 0 && Val.ToInt64(txtLotID.Text) == 0)
            {
                return;
            }

            m_dtbDetails.AcceptChanges();
            if (m_dtbDetails != null)
            {
                if (m_dtbDetails.Rows.Count > 0)
                {
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt64(txtLotID.Text));
                        if (DtConfirm.Rows.Count == 0)
                        {
                            Global.Message("Please Confirm Lot First!!!");
                            return;
                        }
                    }
                }
            }

            if (txtLotID.Text.Length == 0)
            {
                return;
            }

            if (m_dtbDetails.Rows.Count > 0)
            {
                DataTable DTabTemp = new DataTable();

                DataRow[] dr = m_dtbDetails.Select("lot_id = " + Val.ToInt64(txtLotID.Text));

                if (dr.Length > 0)
                {
                    Global.Message("Lot ID Already Added in a grid");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }

                //if (m_dtbDetails.Select("lot_id=" + Val.ToInt64(txtLotID.Text)).Length > 0)
                //{
                //    Global.Message("Lot ID Already Added in a grid");
                //    txtLotID.Text = "";
                //    txtLotID.Focus();
                //    return;
                //}
                DataTable DTab_ValidateLotID = objDepartmentCnf.GetData(Val.ToInt64(txtLotID.Text));

                if (DTab_ValidateLotID.Rows.Count > 0)
                {

                }
                else
                {
                    Global.Message("Lot ID Not in Your Department");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }
                //DTabTemp = objDepartmentCnf.GetData(Val.ToInt64(txtLotID.Text));

                if (DTab_ValidateLotID.Columns.Contains("SEL") == false)
                {
                    if (DTab_ValidateLotID.Columns.Contains("SEL") == false)
                    {
                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        DTab_ValidateLotID.Columns.Add(Col);
                    }
                }
                DTab_ValidateLotID.Columns["SEL"].SetOrdinal(0);

                foreach (DataRow DRow in DTab_ValidateLotID.Rows)
                {
                    if (Val.ToString(DRow["lot_id"]) == txtLotID.Text)
                    {
                        DRow["SEL"] = true;
                    }
                }

                if (DTab_ValidateLotID.Rows.Count > 0)
                {
                    txtLotID.Text = "";
                    txtLotID.Focus();
                }
                m_dtbDetails.Merge(DTab_ValidateLotID);
            }
            else
            {
                //DataTable DTab_ValidateLotID = objDepartmentCnf.GetData(Val.ToInt64(txtLotID.Text));
                m_dtbDetails = objDepartmentCnf.GetData(Val.ToInt64(txtLotID.Text));

                if (m_dtbDetails.Rows.Count > 0)
                {
                }
                else
                {
                    Global.Message("Lot ID Not Issue in Janged");
                    txtLotID.Text = "";
                    txtLotID.Focus();
                    return;
                }

                //m_dtbDetails = objDepartmentCnf.GetData(Val.ToInt64(txtLotID.Text));

                if (m_dtbDetails.Columns.Contains("SEL") == false)
                {
                    if (m_dtbDetails.Columns.Contains("SEL") == false)
                    {
                        DataColumn Col = new DataColumn();
                        Col.ColumnName = "SEL";
                        Col.DataType = typeof(bool);
                        Col.DefaultValue = false;
                        m_dtbDetails.Columns.Add(Col);
                    }
                }
                m_dtbDetails.Columns["SEL"].SetOrdinal(0);

                foreach (DataRow DRow in m_dtbDetails.Rows)
                {
                    if (Val.ToString(DRow["lot_id"]) == txtLotID.Text)
                    {
                        DRow["SEL"] = true;
                    }
                }

                if (m_dtbDetails.Rows.Count > 0)
                {
                    if (Val.ToInt64(txtLotID.Text) != 0)
                    {
                        DataTable DtConfirm = Global.CheckConfirmLot(Val.ToInt64(txtLotID.Text));
                        if (DtConfirm.Rows.Count == 0)
                        {
                            Global.Message("Please Confirm Lot First!!!");
                            return;
                        }
                    }
                    txtLotID.Text = "";
                    txtLotID.Focus();
                }
            }

            grdManagerUpdateUtility.DataSource = m_dtbDetails;
            grdManagerUpdateUtility.RefreshDataSource();
            dgvManagerUpdateUtility.BestFitColumns();

            GetSummary();
        }
        private void dgvDepartmentTransferConfirm_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                Int64 Janged_No = Val.ToInt64(dgvManagerUpdateUtility.GetRowCellDisplayText(e.RowHandle, dgvManagerUpdateUtility.Columns["janged_no"]));

                if (Janged_No != 0)
                {
                    e.Appearance.BackColor = Color.Gray;
                }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
        }
        private void repChkSel_CheckedChanged(object sender, EventArgs e)
        {
            GetSummary();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLotID.Text = "";
            lueManager.EditValue = null;
            //lueWagesSieve.EditValue = null;
            //lueWagesType.EditValue = null;

            dtpConfirmDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpConfirmDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpConfirmDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpConfirmDate.Properties.CharacterCasing = CharacterCasing.Upper;
            dtpConfirmDate.EditValue = DateTime.Now;
            m_dtbDetails = new DataTable();
            m_dtbDetailsGetData = new DataTable();
            txtPassword.Text = "";
            grdManagerUpdateUtility.DataSource = null;
        }
        public void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if(col)
                //if (filterFlag == 0)
                //{
                for (int i = 0; i < dgvManagerUpdateUtility.RowCount; i++)
                {
                    //if (dgvDepartmentTransferConfirm.IsFilterRow(i))
                    dgvManagerUpdateUtility.SetRowCellValue(i, "SEL", chkAll.Checked);
                }
                //}
                GetSummary();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void repChkSel_QueryValueByCheckState(object sender, DevExpress.XtraEditors.Controls.QueryValueByCheckStateEventArgs e)
        {
            GetSummary();
        }
        private void repChkSel_MouseUp(object sender, MouseEventArgs e)
        {
            GetSummary();
        }
        private void backgroundWorker_BTConfirm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                //Conn = new BeginTranConnection(true, false);
                MFGDepartmentTransferProperty DepartmentTransferProperty = new MFGDepartmentTransferProperty();

                try
                {
                    IntRes = 0;
                    IntRes1 = 0;

                    //List<ListError> lstError = new List<ListError>();

                    int IntCounter = 0;
                    int Count = 0;

                    if (m_dtbDetails.Select("SEL = true").Length > 0)
                    {
                        m_dtbDetails = m_dtbDetails.Select("SEL = true").CopyToDataTable();
                        int TotalCount = m_dtbDetails.Rows.Count;

                        foreach (DataRow DRow in m_dtbDetails.Rows)
                        {
                            DepartmentTransferProperty.transfer_id = Val.ToInt(DRow["transfer_id"]);
                            DepartmentTransferProperty.janged_no = Val.ToInt64(DRow["janged_no"]);
                            DepartmentTransferProperty.lot_id = Val.ToInt64(DRow["lot_id"]);
                            DepartmentTransferProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                            DepartmentTransferProperty.receive_date = Val.DBDate(dtpConfirmDate.Text);
                            DepartmentTransferProperty.is_confirm = Val.ToBoolean(chkCnfDate.Checked);
                            IntRes = objDepartmentCnf.ManagerCnfDate_Update(DepartmentTransferProperty);

                            Count++;
                            IntCounter++;
                            IntRes++;
                            SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                        }
                    }

                    //if (m_dtbDetails.Rows.Count > 0)
                    //{
                    //    int TotalCount = m_dtbDetails.Rows.Count;

                    //    foreach (DataRow DRow in m_dtbDetails.Rows)
                    //    {
                    //        DepartmentTransferProperty.transfer_id = Val.ToInt(DRow["transfer_id"]);
                    //        DepartmentTransferProperty.janged_no = Val.ToInt64(DRow["janged_no"]);
                    //        DepartmentTransferProperty.lot_id = Val.ToInt64(DRow["lot_id"]);
                    //        DepartmentTransferProperty.manager_id = Val.ToInt64(lueManager.EditValue);
                    //        DepartmentTransferProperty.receive_date = Val.DBDate(dtpConfirmDate.Text);
                    //        DepartmentTransferProperty.is_confirm = Val.ToBoolean(chkCnfDate.Checked);
                    //        IntRes = objDepartmentCnf.ManagerCnfDate_Update(DepartmentTransferProperty);

                    //        Count++;
                    //        IntCounter++;
                    //        IntRes++;
                    //        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    //    }
                    //}
                    if (IntRes == -1)
                    {
                        Global.Confirm("Error In Update Manager/Confirm Date");
                    }
                    else
                    {
                        //Count++;
                        //IntCounter++;
                        //IntRes1++;
                        //SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    //Conn.Inter1.Commit();
                }
                catch (Exception ex)
                {
                    IntRes = -1;
                    IntRes1 = -1;
                    //Conn.Inter1.Rollback();
                    //Conn = null;
                    General.ShowErrors(ex.ToString());
                    return;
                }
                finally
                {
                    DepartmentTransferProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                IntRes1 = -1;
                //Conn.Inter1.Rollback();
                //Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_BTConfirm_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes1 > 0 || IntRes > 0)
                {
                    Global.Confirm("Update Manager/Confirm Date Successfully");
                    m_dtbDetails = null;
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In  Update Manager/Confirm Date");
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region GridEvents

        private void dgvDepartmentTransferConfirm_ColumnFilterChanged(object sender, EventArgs e)
        {
            if (dgvManagerUpdateUtility.ActiveFilterString != "")
            {
                //filterFlag = 1;
                //chkAll.Checked = false;
                //filterFlag = 0;
            }
            else
            {
                //filterFlag = 1;
                //chkAll.Checked = false;
                //filterFlag = 0;
            }
        }
        #endregion

        #endregion

        #region Functions


        private void CalculateTotal()
        {
            try
            {
                m_numSelectedCount = 0;
                dgvManagerUpdateUtility.UpdateCurrentRow();
                m_dtbDetails.AcceptChanges();
                foreach (DataRow drw in m_dtbDetails.Rows)
                {
                    if (Val.ToBoolean(drw["SEL"]) == true)
                    {
                        m_numSelectedCount += 1;
                    }
                }
                dgvManagerUpdateUtility.UpdateTotalSummary();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
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
                            dgvManagerUpdateUtility.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvManagerUpdateUtility.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvManagerUpdateUtility.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvManagerUpdateUtility.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvManagerUpdateUtility.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvManagerUpdateUtility.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvManagerUpdateUtility.ExportToCsv(Filepath);
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

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                if (Val.ToString(txtPassword.Text) == "123")
                {
                    lueCutNo.Visible = true;
                    btnShow.Visible = true;
                    lblCutNo.Visible = true;

                }
                else
                {
                    lueCutNo.Visible = false;
                    btnShow.Visible = false;
                    lblCutNo.Visible = false;
                }
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            //if (chkCnfDate.Checked == true)
            //{
            if (lueCutNo.Text != "")
            {
                m_dtbDetailsGetData.AcceptChanges();

                if (m_dtbDetailsGetData.Rows.Count > 0)
                {
                    DataTable DTab_ValidateLotID = objDepartmentCnf.Confirm_GetData(Val.ToInt32(lueCutNo.EditValue));
                    m_dtbDetailsGetData.Merge(DTab_ValidateLotID);
                }
                else
                {
                    m_dtbDetailsGetData = objDepartmentCnf.Confirm_GetData(Val.ToInt32(lueCutNo.EditValue));
                }
            }
            else
            {
                Global.Message("Cut No Must be Select");
                lueCutNo.Focus();
                return;
            }
            //}
            //else
            //{
            //    Global.Message("Please Checked Confirm Date");
            //    chkCnfDate.Focus();
            //    return;
            //}

            if (m_dtbDetailsGetData.Columns.Contains("SEL") == false)
            {
                if (m_dtbDetailsGetData.Columns.Contains("SEL") == false)
                {
                    DataColumn Col = new DataColumn();
                    Col.ColumnName = "SEL";
                    Col.DataType = typeof(bool);
                    Col.DefaultValue = false;
                    m_dtbDetailsGetData.Columns.Add(Col);
                }
            }
            m_dtbDetailsGetData.Columns["SEL"].SetOrdinal(0);

            grdManagerUpdateUtility.DataSource = m_dtbDetailsGetData;
            grdManagerUpdateUtility.RefreshDataSource();
            dgvManagerUpdateUtility.BestFitColumns();
            GetSummary();
            txtSelLot.EditValue = "";
            txtSelPcs.EditValue = "";
            txtSelCarat.EditValue = "";
        }
        private void GetSummary()
        {
            try
            {
                double IntSelPcs = 0; double IntSelLot = 0; double DouSelCarat = 0;
                System.Data.DataTable DtTransfer = (System.Data.DataTable)grdManagerUpdateUtility.DataSource;
                dgvManagerUpdateUtility.PostEditor();
                Global.DtTransfer.AcceptChanges();

                if (DtTransfer != null)
                {
                    if (DtTransfer.Rows.Count > 0)
                    {
                        foreach (DataRow DRow in DtTransfer.Rows)
                        {
                            if (Val.ToString(DRow["SEL"]) == "True")
                            {
                                IntSelLot = IntSelLot + 1;
                                IntSelPcs = IntSelPcs + Val.Val(DRow["pcs"]);
                                DouSelCarat = DouSelCarat + Val.Val(DRow["carat"]);
                            }
                        }
                    }
                }
                txtSelLot.Text = IntSelLot.ToString();
                txtSelPcs.Text = IntSelPcs.ToString();
                txtSelCarat.Text = DouSelCarat.ToString();
            }
            catch
            {
            }
        }

        private void txtLotID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
