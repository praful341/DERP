using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DERP.Master;
using DERP.Master.MFG;
using System;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DREP.Transaction
{
    public partial class FRMMFGTargetConfirm : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        MFGTargetConfirm objMFGTargetConfirm;
        CompanyMemoIssueReceipt objCompanyMamoIssueReceipt;

        DataTable m_dtbCompanyMemoIssueDetail;

        int m_numForm_id;
        int IntRes;

        string Party_Memo_No;
        string Company_Memo_No;

        #endregion

        #region Constructor
        public FRMMFGTargetConfirm()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            objMFGTargetConfirm = new MFGTargetConfirm();
            objCompanyMamoIssueReceipt = new CompanyMemoIssueReceipt();

            m_dtbCompanyMemoIssueDetail = new DataTable();

            m_numForm_id = 0;
            IntRes = 0;

            Party_Memo_No = string.Empty;
            Company_Memo_No = string.Empty;
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
            objBOFormEvents.ObjToDispose.Add(objMFGTargetConfirm);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);

        }

        #endregion

        #region Events
        private void FRMMFGTargetConfirm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!LoadDefaults())
                {
                    btnClear.Enabled = false;
                    btnSave.Enabled = false;
                }
                else
                {
                    ClearDetails();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            string Str = "";
            if (Val.DBDate(BLL.GlobalDec.gStrServerDate) != Val.DBDate(dtpEntryDate.Text))
            {
                Str = GlobalDec.CheckLockIsOpenOrNot(Val.DBDate(dtpEntryDate.Text), Val.DBTime(DateTime.Now.ToShortTimeString())); //Val.GetFullTime12());
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
                    dtpEntryDate.Enabled = true;
                    dtpEntryDate.Visible = true;
                }
            }

            btnSave.Enabled = false;

            DialogResult result = MessageBox.Show("Do you want to Save Target data?", "Confirmation", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
            {
                btnSave.Enabled = true;
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            backgroundWorker_TargetConfirm.RunWorkerAsync();

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void BtnExitNew_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Btn_Show_Click(object sender, EventArgs e)
        {
            try
            {
                MFGTargetConfirmProperty objMFGTargetConfirmProperty = new MFGTargetConfirmProperty();
                objMFGTargetConfirmProperty.entry_date = Val.DBDate(dtpEntryDate.Text);
                objMFGTargetConfirmProperty.department_id = Val.ToInt(lueDepartment.EditValue);
                objMFGTargetConfirmProperty.sub_process_id = Val.ToInt(lueSubProcess.EditValue);
                if (RBtnType.SelectedIndex == 0)
                {
                    objMFGTargetConfirmProperty.confirm_type = Val.ToString(RBtnType.EditValue);
                    dgvTargetConfirm.Columns["is_done"].OptionsColumn.AllowEdit = true;
                    dgvTargetConfirm.Columns["done_remarks"].OptionsColumn.AllowEdit = true;
                    dgvTargetConfirm.Columns["is_confirm"].OptionsColumn.AllowEdit = false;
                    dgvTargetConfirm.Columns["confirm_remarks"].OptionsColumn.AllowEdit = false;
                    dgvTargetConfirm.Columns["is_done"].Visible = true;
                    dgvTargetConfirm.Columns["done_remarks"].Visible = true;
                    dgvTargetConfirm.Columns["is_confirm"].Visible = false;
                    dgvTargetConfirm.Columns["confirm_remarks"].Visible = false;
                    dgvTargetConfirm.Columns["rough_cut_no"].VisibleIndex = 1;
                    dgvTargetConfirm.Columns["is_done"].VisibleIndex = 2;
                    dgvTargetConfirm.Columns["done_remarks"].VisibleIndex = 3;
                }
                else
                {
                    objMFGTargetConfirmProperty.confirm_type = Val.ToString(RBtnType.EditValue);
                    dgvTargetConfirm.Columns["is_done"].OptionsColumn.AllowEdit = false;
                    dgvTargetConfirm.Columns["done_remarks"].OptionsColumn.AllowEdit = false;
                    dgvTargetConfirm.Columns["is_done"].Visible = false;
                    dgvTargetConfirm.Columns["done_remarks"].Visible = false;
                    dgvTargetConfirm.Columns["is_confirm"].OptionsColumn.AllowEdit = true;
                    dgvTargetConfirm.Columns["confirm_remarks"].OptionsColumn.AllowEdit = true;
                    dgvTargetConfirm.Columns["is_confirm"].Visible = true;
                    dgvTargetConfirm.Columns["confirm_remarks"].Visible = true;
                    dgvTargetConfirm.Columns["rough_cut_no"].VisibleIndex = 1;
                    dgvTargetConfirm.Columns["is_confirm"].VisibleIndex = 2;
                    dgvTargetConfirm.Columns["confirm_remarks"].VisibleIndex = 3;
                }

                DataTable DTab_Confirm = objMFGTargetConfirm.GetTargetConfirm(objMFGTargetConfirmProperty);

                if (DTab_Confirm.Rows.Count > 0)
                {
                    grdTargetConfirm.DataSource = DTab_Confirm;
                    dgvTargetConfirm.BestFitColumns();
                }
                else
                {
                    Global.Message("Target Confirm Data Not Found");
                    grdTargetConfirm.DataSource = null;
                    return;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void RBtnType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBtnType.SelectedIndex == 0)
                {
                    grdTargetConfirm.DataSource = null;
                }
                else
                {
                    grdTargetConfirm.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void lueDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmDepartmentMaster frmDepartment = new FrmDepartmentMaster();
                frmDepartment.ShowDialog();
                Global.LOOKUPDepartment(lueDepartment);
            }
        }

        private void lueSubProcess_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmMfgSubProcessMaster frmSubProcess = new FrmMfgSubProcessMaster();
                frmSubProcess.ShowDialog();
                Global.LOOKUPSubProcess(lueSubProcess);
            }
        }
        private void backgroundWorker_TargetConfirm_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.Default;
                Conn = new BeginTranConnection(true, false);
                MFGTargetConfirmProperty objMFGTargetConfirmProperty = new MFGTargetConfirmProperty();
                MFGTargetConfirm objMFGTargetConfirm = new MFGTargetConfirm();
                try
                {
                    if (RBtnType.SelectedIndex == 0)
                    {
                        IntRes = 0;

                        for (int i = 0; i < dgvTargetConfirm.RowCount; i++)
                        {
                            DataRow DRow = dgvTargetConfirm.GetDataRow(i);
                            if (Val.ToBooleanToInt(DRow["is_done"]) == 0)
                            {
                                continue;
                            }
                            objMFGTargetConfirmProperty = new MFGTargetConfirmProperty();
                            objMFGTargetConfirmProperty.is_done = Val.ToBoolean(DRow["is_done"]);
                            objMFGTargetConfirmProperty.done_remarks = Val.ToString(DRow["done_remarks"]);
                            objMFGTargetConfirmProperty.emp_performance_id = Val.ToInt(DRow["emp_performance_id"]);
                            objMFGTargetConfirmProperty.confirm_type = Val.ToString(RBtnType.EditValue);
                            objMFGTargetConfirmProperty.done_date = Val.DBDate(dtpEntryDate.Text);

                            IntRes = objMFGTargetConfirm.Target_Confirm_Save(objMFGTargetConfirmProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                        }
                        Conn.Inter1.Commit();
                    }
                    else
                    {
                        IntRes = 0;

                        for (int i = 0; i < dgvTargetConfirm.RowCount; i++)
                        {
                            DataRow DRow = dgvTargetConfirm.GetDataRow(i);
                            if (Val.ToBooleanToInt(DRow["is_confirm"]) == 0)
                            {
                                continue;
                            }
                            objMFGTargetConfirmProperty = new MFGTargetConfirmProperty();
                            objMFGTargetConfirmProperty.is_confirm = Val.ToBoolean(DRow["is_confirm"]);
                            objMFGTargetConfirmProperty.confirm_remarks = Val.ToString(DRow["confirm_remarks"]);
                            objMFGTargetConfirmProperty.emp_performance_id = Val.ToInt(DRow["emp_performance_id"]);
                            objMFGTargetConfirmProperty.confirm_type = Val.ToString(RBtnType.EditValue);
                            DateTime Start = Convert.ToDateTime(dtpEntryDate.Text);
                            DateTime End = Convert.ToDateTime(DRow["performance_date"]);
                            TimeSpan difference = Start - End;
                            int NrOfDays = difference.Days + 1;
                            objMFGTargetConfirmProperty.confirm_difference_days = Val.ToInt(NrOfDays);
                            objMFGTargetConfirmProperty.done_date = Val.DBDate(dtpEntryDate.Text);

                            IntRes = objMFGTargetConfirm.Target_Confirm_Save(objMFGTargetConfirmProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
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
                finally
                {
                    objMFGTargetConfirmProperty = null;
                }
            }
            catch (Exception ex)
            {
                IntRes = -1;
                Conn.Inter1.Rollback();
                Conn = null;
                General.ShowErrors(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_TargetConfirm_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IntRes > 0)
                {
                    Global.Confirm("Target Confirm Entry Data Save Successfully");
                    ClearDetails();
                }
                else
                {
                    Global.Confirm("Error In Purchase Invoice");
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #endregion

        #region Functions
        private bool LoadDefaults()
        {
            bool blnReturn = true;
            try
            {
                Global.LOOKUPDepartment(lueDepartment);
                Global.LOOKUPSubProcess(lueSubProcess);
                RBtnType.SelectedIndex = 0;

                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;

                dgvTargetConfirm.Columns["is_confirm"].OptionsColumn.AllowEdit = true;
                dgvTargetConfirm.Columns["confirm_remarks"].OptionsColumn.AllowEdit = true;
                dgvTargetConfirm.Columns["is_confirm"].Visible = true;
                dgvTargetConfirm.Columns["confirm_remarks"].Visible = true;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            finally
            {
            }
            return blnReturn;
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                lueDepartment.EditValue = null;
                lueSubProcess.EditValue = null;
                RBtnType.SelectedIndex = 0;
                grdTargetConfirm.DataSource = null;
                dtpEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpEntryDate.EditValue = DateTime.Now;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                blnReturn = false;
            }
            return blnReturn;
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
                            dgvTargetConfirm.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvTargetConfirm.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvTargetConfirm.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvTargetConfirm.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvTargetConfirm.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvTargetConfirm.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvTargetConfirm.ExportToCsv(Filepath);
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
    }
}