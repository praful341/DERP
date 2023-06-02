using BLL;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgKapanCutMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.BeginTranConnection Conn;
        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MFGKapanCreate objKapanCut;
        DataTable m_dtbType = new DataTable();
        int m_numForm_id;
        #endregion

        #region Constructor
        public FrmMfgKapanCutMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objKapanCut = new MFGKapanCreate();

            m_numForm_id = 0;
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

            if (GlobalDec.gEmployeeProperty.role_name == "SURAT ASSORT")
            {
                if (GlobalDec.gEmployeeProperty.user_name != "MAYANK" && GlobalDec.gEmployeeProperty.user_name != "RIKITA" && GlobalDec.gEmployeeProperty.user_name != "DEVANGI" && GlobalDec.gEmployeeProperty.user_name != "VIRAJ")
                {
                    Global.Message("Don't have permission...Please Contact to Administrator...");
                    return;
                }
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
            objBOFormEvents.ObjToDispose.Add(objKapanCut);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgArticleMaster_Load(object sender, EventArgs e)
        {
            try
            {
                //GetData();
                Global.LOOKUPRoughCutType(lueRoughCuttype);
                m_dtbType = new DataTable();
                m_dtbType.Columns.Add("type");
                m_dtbType.Rows.Add("NORMAL");
                m_dtbType.Rows.Add("REJECTION");

                lueType.Properties.DataSource = m_dtbType;
                lueType.Properties.ValueMember = "type";
                lueType.Properties.DisplayMember = "type";
                lueType.EditValue = "NORMAL";
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ObjPer.FormName = this.Name.ToUpper();
            ObjPer.SetFormPer();
            if (ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            if (lblMode.Text == "Edit Mode")
            {
                if (ObjPer.AllowUpdate == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }
            }
            btnSave.Enabled = false;

            if (SaveDetails())
            {
                //GetData();
                btnClear_Click(sender, e);
            }

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                dtpKapanCutDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpKapanCutDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpKapanCutDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpKapanCutDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpKapanCutDate.EditValue = DateTime.Now;
                lueRoughCuttype.EditValue = null;
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtKapanNo.Text = string.Empty;
                txtCutNo.Text = string.Empty;
                lueType.EditValue = "NORMAL";
                txtKapanNo.Focus();
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents        
        private void dgvMfgArticleMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvKapanCutMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["kapan_id"]);
                        txtKapanNo.Text = Val.ToString(Drow["kapan_no"]);
                        txtCutNo.Text = Val.ToString(Drow["rough_cut_no"]);
                        txtCutNo.Tag = Val.ToInt(Drow["rough_cut_id"]);
                        txtKapanNo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            Int64 NewLotid;
            Int64 NewKapanid = 0;
            Int64 NewHistory_Union_Id = 0;
            Int64 Dept_union_Id = 0;
            Int64 Lot_SrNo = 0;
            MFGKapanCreate objMFGKapanCreate = new MFGKapanCreate();
            MFGKapanCreateProperty KapanCutMasterProperty = new MFGKapanCreateProperty();
            MFGCutCreateProperty objMFGCutCreateProperty = new MFGCutCreateProperty();
            MFGCutCreate objMFGCutCreate = new MFGCutCreate();
            Conn = new BeginTranConnection(true, false);
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                KapanCutMasterProperty.kapan_id = Val.ToInt32(lblMode.Tag);
                KapanCutMasterProperty.kapan_date = Val.DBDate(dtpKapanCutDate.Text);
                KapanCutMasterProperty.type = Val.ToString(lueType.Text);
                KapanCutMasterProperty.kapan_no = txtKapanNo.Text.ToUpper();
                KapanCutMasterProperty.company_id = Val.ToInt32(GlobalDec.gEmployeeProperty.company_id);
                KapanCutMasterProperty.branch_id = Val.ToInt32(GlobalDec.gEmployeeProperty.branch_id);
                KapanCutMasterProperty.location_id = Val.ToInt32(GlobalDec.gEmployeeProperty.location_id);
                KapanCutMasterProperty.department_id = Val.ToInt32(GlobalDec.gEmployeeProperty.department_id);
                KapanCutMasterProperty.form_id = m_numForm_id;

                KapanCutMasterProperty = objMFGKapanCreate.Save_MfgStock_New(KapanCutMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                NewLotid = Val.ToInt64(KapanCutMasterProperty.lot_id);

                KapanCutMasterProperty.history_union_id = NewHistory_Union_Id;
                KapanCutMasterProperty.lot_srno = Lot_SrNo;

                KapanCutMasterProperty = objMFGKapanCreate.KapanSave_New(KapanCutMasterProperty, DLL.GlobalDec.EnumTran.Continue, Conn);
                NewKapanid = Val.ToInt64(KapanCutMasterProperty.kapan_id);
                NewHistory_Union_Id = Val.ToInt64(KapanCutMasterProperty.history_union_id);
                Lot_SrNo = Val.ToInt64(KapanCutMasterProperty.lot_srno);


                //KapanCutMasterProperty.rough_cut_id = Val.ToInt32(txtCutNo.Tag);
                //KapanCutMasterProperty.rough_cut_no = txtCutNo.Text.ToUpper();

                objMFGCutCreateProperty.rough_cut_id = Val.ToInt(txtCutNo.Tag);
                objMFGCutCreateProperty.rough_cut_date = Val.DBDate(dtpKapanCutDate.Text);
                objMFGCutCreateProperty.rough_cut_no = Val.ToString(txtCutNo.Text);
                objMFGCutCreateProperty.kapan_id = Val.ToInt(NewKapanid);
                objMFGCutCreateProperty.rough_cuttype_id = Val.ToInt(lueRoughCuttype.EditValue);
                objMFGCutCreateProperty.company_id = Val.ToInt32(GlobalDec.gEmployeeProperty.company_id);
                objMFGCutCreateProperty.branch_id = Val.ToInt32(GlobalDec.gEmployeeProperty.branch_id);
                objMFGCutCreateProperty.location_id = Val.ToInt32(GlobalDec.gEmployeeProperty.location_id);
                objMFGCutCreateProperty.department_id = Val.ToInt32(GlobalDec.gEmployeeProperty.department_id);
                objMFGCutCreateProperty.form_id = m_numForm_id;
                objMFGCutCreateProperty.history_union_id = NewHistory_Union_Id;
                objMFGCutCreateProperty.lot_srno = Lot_SrNo;

                objMFGCutCreateProperty = objMFGCutCreate.CutSave_New(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                Int64 NewCutid = Val.ToInt64(objMFGCutCreateProperty.rough_cut_id);
                objMFGCutCreateProperty.rough_cut_id = NewCutid;
                objMFGCutCreateProperty.rough_lot_id = Val.ToInt(NewLotid);
                NewHistory_Union_Id = Val.ToInt64(objMFGCutCreateProperty.history_union_id);
                objMFGCutCreateProperty.department_union_id = Dept_union_Id;

                objMFGCutCreateProperty = objMFGCutCreate.Save_RoughCutStock(objMFGCutCreateProperty, DLL.GlobalDec.EnumTran.Continue, Conn);

                NewLotid = Val.ToInt64(objMFGCutCreateProperty.lot_id);

                Conn.Inter1.Commit();

                //NewLotid = Val.ToInt64(objMFGCutCreateProperty.lot_id);
                //Dept_union_Id = Val.ToInt64(objMFGCutCreateProperty.department_union_id);
                //int IntRes = objKapanCut.Save(KapanCutMasterProperty);
                if (NewLotid <= 0)
                {
                    Global.Confirm("Error In Kapan Cut Save.");
                    txtKapanNo.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Kapan Cut Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Kapan Cut Update Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                KapanCutMasterProperty = null;
                objMFGCutCreateProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtKapanNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Kapan No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtKapanNo.Focus();
                    }
                }
                if (txtCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Cut No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutNo.Focus();
                    }
                }
                if (!objKapanCut.ISExists(Val.ToString(txtCutNo.Text), Val.ToString(txtKapanNo.Text), Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(5, "Kapan No and Cut No Already Exists in Master"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutNo.Focus();
                    }

                }
                if (txtCutNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtCutNo.Focus();
                    }
                }
                DateTime endDate = Convert.ToDateTime(DateTime.Today);
                endDate = endDate.AddDays(3);

                if (Convert.ToDateTime(dtpKapanCutDate.Text) >= endDate)
                {
                    lstError.Add(new ListError(5, " Kapan Cut Date Not Be Permission After 3 Days...Please Contact to Administrator"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        dtpKapanCutDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));

        }
        public void GetData()
        {
            try
            {
                //DataTable DTab = objKapanCut.GetData();
                //grdKapanCutMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }


        #endregion

        #region Export Grid
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
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
                            dgvKapanCutMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvKapanCutMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvKapanCutMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvKapanCutMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvKapanCutMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvKapanCutMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvKapanCutMaster.ExportToCsv(Filepath);
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
