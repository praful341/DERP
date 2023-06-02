using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP
{
    public partial class FrmFinancialYearMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        FinancialYearMaster objCompany;
        #endregion

        #region Constructor
        public FrmFinancialYearMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objCompany = new FinancialYearMaster();
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
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmCompanyYearMaster_Shown(object sender, EventArgs e)
        {
            try
            {
                DTPStartDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPStartDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPStartDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPStartDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DTPStartDate.EditValue = DateTime.Now;

                DTPEndDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPEndDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPEndDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPEndDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DTPEndDate.EditValue = DateTime.Now;

                GetData();
                btnClear_Click(btnClear, null);
                DTPStartDate.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
                btnSave.Enabled = false;
                if (SaveDetails())
                {
                    GetData();
                    btnClear_Click(sender, e);
                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtFinancialYear.Text = "";
                txtShortName.Text = "";
                DTPStartDate.Text = Val.DBDate(System.DateTime.Now.ToString());
                DTPEndDate.Text = Val.DBDate(System.DateTime.Now.ToString());
                ChkActive.Checked = true;
                DTPStartDate.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DTPStartDate_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DTPEndDate.EditValue = new DateTime(DTPStartDate.DateTime.Year + 1, DTPStartDate.DateTime.Month, DTPStartDate.DateTime.Day).AddDays(-1);
                txtFinancialYear.Text = DTPStartDate.DateTime.Year + "-" + DTPEndDate.DateTime.Year;
                txtShortName.Text = DTPStartDate.DateTime.Year + "-" + txtFinancialYear.Text.Substring(7, 2);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        #region GridEvents
        private void dgvCountryMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow DRow = dgvCompanyYearMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToString(DRow["fin_year_id"]);
                        txtFinancialYear.Text = Val.ToString(DRow["financial_year"]);
                        DTPStartDate.Text = Val.DBDate(DRow["start_date"].ToString());
                        DTPEndDate.Text = Val.DBDate(DRow["end_date"].ToString());
                        txtShortName.Text = Val.ToString(DRow["short_name"]);
                        ChkActive.Checked = Val.ToInt(DRow["active"]) == 1 ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        #endregion

        #endregion

        #region Functions       
        private bool SaveDetails()
        {
            bool blnReturn = true;
            Financial_Year_MasterProperty CompanyYearProperty = new Financial_Year_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                CompanyYearProperty.Fin_Year_ID = Val.ToInt64(lblMode.Tag);
                CompanyYearProperty.Financial_year = Val.ToString(txtFinancialYear.Text);
                CompanyYearProperty.Start_Date = Val.DBDate(DTPStartDate.Text);
                CompanyYearProperty.End_Date = Val.DBDate(DTPEndDate.Text);
                CompanyYearProperty.Short_Name = Val.ToString(txtShortName.Text);
                CompanyYearProperty.Active = Val.ToInt64(ChkActive.Checked == true ? 1 : 0);
                string StrStart_YearMonth = Global.GetFinancialYearNew(DTPStartDate.Text.ToString());
                string StrEnd_YearMonth = Global.GetFinancialYearNew(DTPEndDate.Text.ToString());
                CompanyYearProperty.Start_YearMonth = Val.ToInt64(StrStart_YearMonth.ToString());
                CompanyYearProperty.End_YearMonth = Val.ToInt64(StrEnd_YearMonth.ToString());

                int IntRes = objCompany.Save(CompanyYearProperty);


                if (IntRes == -1)
                {
                    Global.Message("Error In Save Year  Details");
                    DTPStartDate.Focus();
                }
                else
                {
                    Global.Message("Company Year Data save successfully");
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                CompanyYearProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtFinancialYear.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Financial Year"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtFinancialYear.Focus();
                    }
                }
                DateTime fromdate = Convert.ToDateTime(DTPStartDate.Text);
                DateTime todate = Convert.ToDateTime(DTPEndDate.Text);

                if (fromdate > todate)
                {
                    lstError.Add(new ListError(8, "From Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPStartDate.Focus();
                    }
                }
                if (!objCompany.ISExists(txtFinancialYear.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Financial Year"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtFinancialYear.Focus();
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
                DataTable DTab = objCompany.GetData();
                grdCompanyYearMaster.DataSource = DTab;
                dgvCompanyYearMaster.BestFitColumns();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
                            dgvCompanyYearMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvCompanyYearMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvCompanyYearMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvCompanyYearMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvCompanyYearMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvCompanyYearMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvCompanyYearMaster.ExportToCsv(Filepath);
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
