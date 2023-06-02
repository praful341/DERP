using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgProfitCenterMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;

        MfgProfitCenterMaster objProfitCenter;

        DataTable m_profitCenter_type;
        #endregion

        #region Constructor
        public FrmMfgProfitCenterMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objProfitCenter = new MfgProfitCenterMaster();

            m_profitCenter_type = new DataTable();
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
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
            objBOFormEvents.ObjToDispose.Add(objProfitCenter);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgDepartmentTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                m_profitCenter_type = new DataTable();
                m_profitCenter_type.Columns.Add("profit_center_type");
                m_profitCenter_type.Rows.Add("PROFIT CENTER");
                m_profitCenter_type.Rows.Add("COST CENTER");
                m_profitCenter_type.Rows.Add("NONE");

                lueProfitCenterType.Properties.DataSource = m_profitCenter_type;
                lueProfitCenterType.Properties.ValueMember = "profit_center_type";
                lueProfitCenterType.Properties.DisplayMember = "profit_center_type";
                lueProfitCenterType.EditValue = "NONE";
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
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            btnSave.Enabled = false;

            if (SaveDetails())
            {
                GetData();
                btnClear_Click(sender, e);
            }

            btnSave.Enabled = true;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtProfitCenter.Text = string.Empty;
                lueProfitCenterType.EditValue = "None";
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                txtProfitCenter.Focus();
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
        private void dgvMfgDepartmentTypeMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvProfiCenterMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["profit_center_id"]);
                        txtProfitCenter.Text = Val.ToString(Drow["profit_center"]);
                        lueProfitCenterType.EditValue = Val.ToString(Drow["profit_center_type"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtProfitCenter.Focus();
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
            MfgProfitCenter_MasterProperty ProfitCenterMasterProperty = new MfgProfitCenter_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                ProfitCenterMasterProperty.profit_center_id = Val.ToInt32(lblMode.Tag);
                ProfitCenterMasterProperty.profit_center = txtProfitCenter.Text.ToUpper();
                ProfitCenterMasterProperty.profit_center_type = Val.ToString(lueProfitCenterType.EditValue);
                ProfitCenterMasterProperty.remarks = txtRemark.Text.ToUpper();
                ProfitCenterMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objProfitCenter.Save(ProfitCenterMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Profit Center Details");
                    txtProfitCenter.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Profit Center Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Profit Center Details  Update Successfully");
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
                ProfitCenterMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtProfitCenter.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Profit Center"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtProfitCenter.Focus();
                    }
                }


                if (!objProfitCenter.ISExists(txtProfitCenter.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Profit Center"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtProfitCenter.Focus();
                    }

                }
                //if (lueProfitCenterType.ItemIndex < 0)
                //{
                //    lstError.Add(new ListError(13, "Profit Center Type"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        lueProfitCenterType.Focus();
                //    }
                //}
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
                DataTable DTab = objProfitCenter.GetData();
                grdProfitCenterMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
                            dgvProfiCenterMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvProfiCenterMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvProfiCenterMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvProfiCenterMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvProfiCenterMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvProfiCenterMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvProfiCenterMaster.ExportToCsv(Filepath);
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
