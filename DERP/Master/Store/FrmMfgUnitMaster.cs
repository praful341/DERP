using BLL;
using BLL.FunctionClasses.Master.Store;
using BLL.PropertyClasses.Master.Store;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.Store
{
    public partial class FrmMfgUnitMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        MfgUnitMaster objUnit;
        #endregion

        #region Constructor
        public FrmMfgUnitMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objUnit = new MfgUnitMaster();
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
            objBOFormEvents.ObjToDispose.Add(objUnit);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgGroupMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
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
                txtUnit.Text = string.Empty;
                txtSequenceNo.Text = string.Empty;
                txtRemark.Text = string.Empty;
                chkActive.Checked = true;
                GetData();

                //DataTable DTab_Assort = objUnit.GetData_Assort();
                //if (DTab_Assort.Rows.Count > 0)
                //{

                //    int count = 0;
                //    foreach (DataRow dr in DTab_Assort.Rows)
                //    {
                //        MfgUnit_MasterProperty UnitMasterProperty = new MfgUnit_MasterProperty();

                //        UnitMasterProperty.assortment_date = Val.DBDate(dr["assortment_date"].ToString());
                //        UnitMasterProperty.lot_id = Val.ToInt64(dr["lot_id"]);
                //        UnitMasterProperty.lot_srno = Val.ToInt64(dr["lot_srno"]);
                //        UnitMasterProperty.kapan_id = Val.ToInt64(dr["kapan_id"]);
                //        UnitMasterProperty.rough_cut_id = Val.ToInt64(dr["rough_cut_id"]);
                //        UnitMasterProperty.process_id = Val.ToInt64(dr["process_id"]);
                //        UnitMasterProperty.sub_process_id = Val.ToInt64(dr["sub_process_id"]);
                //        UnitMasterProperty.assort_id = Val.ToInt64(dr["assort_id"]);
                //        UnitMasterProperty.sieve_id = Val.ToInt64(dr["sieve_id"]);
                //        UnitMasterProperty.purity_id = Val.ToInt64(dr["purity_id"]);
                //        UnitMasterProperty.color_id = Val.ToInt64(dr["color_id"]);
                //        UnitMasterProperty.temp_quality_name = Val.ToString(dr["temp_quality_name"]);
                //        UnitMasterProperty.temp_sieve_name = Val.ToString(dr["temp_sieve_name"]);
                //        UnitMasterProperty.pcs = Val.ToInt32(dr["pcs"]);
                //        UnitMasterProperty.carat = Val.ToDecimal(dr["carat"]);
                //        UnitMasterProperty.percentage = Val.ToDecimal(dr["percentage"]);
                //        UnitMasterProperty.rate = Val.ToDecimal(dr["rate"]);
                //        UnitMasterProperty.amount = Val.ToDecimal(dr["amount"]);
                //        UnitMasterProperty.assort_total_carat = Val.ToDecimal(dr["assort_total_carat"]);
                //        UnitMasterProperty.company_id = Val.ToInt64(dr["company_id"]);
                //        UnitMasterProperty.branch_id = Val.ToInt64(dr["branch_id"]);
                //        UnitMasterProperty.location_id = Val.ToInt64(dr["location_id"]);
                //        UnitMasterProperty.department_id = Val.ToInt64(dr["department_id"]);
                //        UnitMasterProperty.form_id = Val.ToInt64(dr["form_id"]);
                //        UnitMasterProperty.user_id = Val.ToInt64(dr["user_id"]);
                //        UnitMasterProperty.entry_date = Val.DBDate(dr["entry_date"].ToString());
                //        UnitMasterProperty.entry_time = Val.ToString(dr["entry_time"]);
                //        UnitMasterProperty.ip_address = Val.ToString(dr["ip_address"]);

                //        int IntRes = objUnit.Save_Assort(UnitMasterProperty);

                //        count = count + 1;
                //    }
                //}

                //Global.Message("Data Insert Successfully");

                txtUnit.Focus();
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
        private void dgvUnitMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvUnitMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt64(Drow["unit_id"]);
                        txtUnit.Text = Val.ToString(Drow["unit_name"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtUnit.Focus();
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
            MfgUnit_MasterProperty UnitMasterProperty = new MfgUnit_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                UnitMasterProperty.unit_id = Val.ToInt32(lblMode.Tag);
                UnitMasterProperty.unit_name = txtUnit.Text.ToUpper();
                UnitMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);
                UnitMasterProperty.remarks = txtRemark.Text.ToUpper();
                UnitMasterProperty.active = Val.ToBoolean(chkActive.Checked);

                int IntRes = objUnit.Save(UnitMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Unit Details");
                    txtUnit.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Unit Details Data Successfully");
                    }
                    else
                    {
                        Global.Confirm("Unit Details  Update Successfully");
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
                UnitMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtUnit.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Unit"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtUnit.Focus();
                    }
                }


                if (!objUnit.ISExists(txtUnit.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Unit"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtUnit.Focus();
                    }

                }
                if (txtSequenceNo.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
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
                DataTable DTab = objUnit.GetData();
                grdUnitMaster.DataSource = DTab;
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
                            dgvUnitMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvUnitMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvUnitMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvUnitMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvUnitMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvUnitMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvUnitMaster.ExportToCsv(Filepath);
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
