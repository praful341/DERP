using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Report;
using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace DREP.Transaction
{
    public partial class FrmMFGStockIssuePendingReport : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;
        MfgCutWiseView objCutwise;
        MfgRoughClarityMaster objClarity;
        MfgQualityMaster objQuality;
        MfgRoughSieve objRoughSieve;
        ClarityMaster objPurity;
        MfgRoughClarityMaster objRoughClarity;
        FillCombo ObjFillCombo = new FillCombo();
        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        DataTable DTab = new DataTable();
        ReportParams ObjReportParams = new ReportParams();
        New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();

        #endregion

        #region Constructor
        public FrmMFGStockIssuePendingReport()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            objCutwise = new MfgCutWiseView();
            objClarity = new MfgRoughClarityMaster();
            objQuality = new MfgQualityMaster();
            objRoughSieve = new MfgRoughSieve();
            objPurity = new ClarityMaster();
            objRoughClarity = new MfgRoughClarityMaster();
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
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

        #region "Events" 
        private void ListKapan_EditValueChanged(object sender, EventArgs e)
        {
            DataTable DTabCut = Global.GetReportKapanWise(Val.ToString(ListKapan.EditValue));
            DTabCut.DefaultView.Sort = "rough_cut_id";
            DTabCut = DTabCut.DefaultView.ToTable();

            ListRoughCut.Properties.DataSource = DTabCut;
            ListRoughCut.Properties.DisplayMember = "rough_cut_no";
            ListRoughCut.Properties.ValueMember = "rough_cut_id";
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListCompany.Properties.Items.Count; i++)
                ListCompany.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListBranch.Properties.Items.Count; i++)
                ListBranch.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListLocation.Properties.Items.Count; i++)
                ListLocation.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListDepartment.Properties.Items.Count; i++)
                ListDepartment.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListKapan.Properties.Items.Count; i++)
                ListKapan.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListRoughCut.Properties.Items.Count; i++)
                ListRoughCut.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListQuality.Properties.Items.Count; i++)
                ListQuality.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListClarity.Properties.Items.Count; i++)
                ListClarity.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListPurity.Properties.Items.Count; i++)
                ListPurity.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListSieve.Properties.Items.Count; i++)
                ListSieve.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListProcess.Properties.Items.Count; i++)
                ListProcess.Properties.Items[i].CheckState = CheckState.Unchecked;
            for (int i = 0; i < ListSubProcess.Properties.Items.Count; i++)
                ListSubProcess.Properties.Items[i].CheckState = CheckState.Unchecked;

            DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPFromDate.EditValue = DateTime.Now;

            DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPToDate.EditValue = DateTime.Now;

            CmbType.SelectedItem = "";
            RBtnType.SelectedIndex = 0;

            ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());
            ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());
            ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());
            ListDepartment.SetEditValue(BLL.GlobalDec.gEmployeeProperty.department_id.ToString());
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMFGStockIssuePendingReport_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFillCombo.user_id = GlobalDec.gEmployeeProperty.user_id;

                DTPFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPFromDate.EditValue = DateTime.Now;

                DTPToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPToDate.EditValue = DateTime.Now;

                DataTable DTabCompany = ObjFillCombo.FillCmb(FillCombo.TABLE.Company_Master);
                DTabCompany.DefaultView.Sort = "Company_Name";
                DTabCompany = DTabCompany.DefaultView.ToTable();

                ListCompany.Properties.DataSource = DTabCompany;
                ListCompany.Properties.DisplayMember = "Company_Name";
                ListCompany.Properties.ValueMember = "company_id";

                ListCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.company_id.ToString());

                DataTable DTabBranch = ObjFillCombo.FillCmb(FillCombo.TABLE.Branch_Master);
                DTabBranch.DefaultView.Sort = "Branch_Name";
                DTabBranch = DTabBranch.DefaultView.ToTable();

                ListBranch.Properties.DataSource = DTabBranch;
                ListBranch.Properties.DisplayMember = "Branch_Name";
                ListBranch.Properties.ValueMember = "branch_id";

                DataTable DTabLocation = ObjFillCombo.FillCmb(FillCombo.TABLE.Location_Master);
                DTabLocation.DefaultView.Sort = "Location_Name";
                DTabLocation = DTabLocation.DefaultView.ToTable();

                ListLocation.Properties.DataSource = DTabLocation;
                ListLocation.Properties.DisplayMember = "Location_Name";
                ListLocation.Properties.ValueMember = "location_id";

                DataTable DTabDepartment = ObjFillCombo.FillCmb(FillCombo.TABLE.Department_Master);
                DTabDepartment.DefaultView.Sort = "Department_Name";
                DTabDepartment = DTabDepartment.DefaultView.ToTable();

                ListDepartment.Properties.DataSource = DTabDepartment;
                ListDepartment.Properties.DisplayMember = "Department_Name";
                ListDepartment.Properties.ValueMember = "department_id";

                ListBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.branch_id.ToString());
                ListDepartment.SetEditValue(BLL.GlobalDec.gEmployeeProperty.department_id);

                DataTable DTabKapan = ObjFillCombo.FillCmb(FillCombo.TABLE.Kapan_Master);
                DTabKapan.DefaultView.Sort = "kapan_id";
                DTabKapan = DTabKapan.DefaultView.ToTable();

                ListKapan.Properties.DataSource = DTabKapan;
                ListKapan.Properties.DisplayMember = "kapan_no";
                ListKapan.Properties.ValueMember = "kapan_id";

                DataTable DTabCut = ObjFillCombo.FillCmb(FillCombo.TABLE.Cut_Master);
                DTabCut.DefaultView.Sort = "rough_cut_id";
                DTabCut = DTabCut.DefaultView.ToTable();

                ListRoughCut.Properties.DataSource = DTabCut;
                ListRoughCut.Properties.DisplayMember = "rough_cut_no";
                ListRoughCut.Properties.ValueMember = "rough_cut_id";

                DataTable DTabProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Process_Master_New);
                DTabProcess.DefaultView.Sort = "process_name";
                DTabProcess = DTabProcess.DefaultView.ToTable();

                ListProcess.Properties.DataSource = DTabProcess;
                ListProcess.Properties.DisplayMember = "process_name";
                ListProcess.Properties.ValueMember = "process_id";

                DataTable DTabSubProcess = ObjFillCombo.FillCmb(FillCombo.TABLE.Sub_Process_Master);
                DTabSubProcess.DefaultView.Sort = "sub_process_name";
                DTabSubProcess = DTabSubProcess.DefaultView.ToTable();

                ListSubProcess.Properties.DataSource = DTabSubProcess;
                ListSubProcess.Properties.DisplayMember = "sub_process_name";
                ListSubProcess.Properties.ValueMember = "sub_process_id";

                ListLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.location_id.ToString());

                DataTable DTabQuality = ObjFillCombo.FillCmb(FillCombo.TABLE.Quality_Master);
                DTabQuality.DefaultView.Sort = "quality_name";
                DTabQuality = DTabQuality.DefaultView.ToTable();

                ListQuality.Properties.DataSource = DTabQuality;
                ListQuality.Properties.DisplayMember = "quality_name";
                ListQuality.Properties.ValueMember = "quality_id";

                DataTable DTabClarity = ObjFillCombo.FillCmb(FillCombo.TABLE.Rough_Clarity_Master);
                DTabClarity.DefaultView.Sort = "rough_clarity_name";
                DTabClarity = DTabClarity.DefaultView.ToTable();

                ListClarity.Properties.DataSource = DTabClarity;
                ListClarity.Properties.DisplayMember = "rough_clarity_name";
                ListClarity.Properties.ValueMember = "rough_clarity_id";

                DataTable DTabPurity = ObjFillCombo.FillCmb(FillCombo.TABLE.Purity_Master);
                DTabPurity.DefaultView.Sort = "purity_id";
                DTabPurity = DTabPurity.DefaultView.ToTable();

                ListPurity.Properties.DataSource = DTabPurity;
                ListPurity.Properties.DisplayMember = "purity_name";
                ListPurity.Properties.ValueMember = "purity_id";

                DataTable DTabSieve = ObjFillCombo.FillCmb(FillCombo.TABLE.Rough_Sieve_Master);
                DTabSieve.DefaultView.Sort = "sieve_name";
                DTabSieve = DTabSieve.DefaultView.ToTable();

                ListSieve.Properties.DataSource = DTabSieve;
                ListSieve.Properties.DisplayMember = "sieve_name";
                ListSieve.Properties.ValueMember = "rough_sieve_id";
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            var result = DateTime.Compare(Convert.ToDateTime(DTPFromDate.Text), Convert.ToDateTime(DTPToDate.Text.ToString()));
            if (result > 0)
            {
                Global.Message(" Transaction From Date Not Be Greater Than Transaction To Date");
                return;
            }

            PanelLoading.Visible = true;
            ReportParams_Property.From_Date = Val.DBDate(DTPFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(DTPToDate.Text);
            ReportParams_Property.company_id = Val.Trim(ListCompany.Properties.GetCheckedItems());
            ReportParams_Property.branch_id = Val.Trim(ListBranch.Properties.GetCheckedItems());
            ReportParams_Property.location_id = Val.Trim(ListLocation.Properties.GetCheckedItems());
            ReportParams_Property.department_id = Val.Trim(ListDepartment.Properties.GetCheckedItems());

            ReportParams_Property.process_id = Val.Trim(ListProcess.Properties.GetCheckedItems());
            ReportParams_Property.sub_process_id = Val.Trim(ListSubProcess.Properties.GetCheckedItems());
            ReportParams_Property.kapan_id = Val.Trim(ListKapan.Properties.GetCheckedItems());
            ReportParams_Property.cut_id = Val.Trim(ListRoughCut.Properties.GetCheckedItems());
            ReportParams_Property.quality_id = Val.Trim(ListQuality.Properties.GetCheckedItems());
            ReportParams_Property.rough_clarity_id = Val.Trim(ListClarity.Properties.GetCheckedItems());
            ReportParams_Property.rough_sieve_id = Val.Trim(ListSieve.Properties.GetCheckedItems());
            ReportParams_Property.purity_id = Val.Trim(ListPurity.Properties.GetCheckedItems());

            if (this.backgroundWorker_MFGStockIssuePending.IsBusy)
            {
            }
            else
            {
                backgroundWorker_MFGStockIssuePending.RunWorkerAsync();
            }
        }
        private void backgroundWorker_MFGStockIssuePending_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DTab = ObjReportParams.GetMFGLiveStock(ReportParams_Property, ObjReportDetailProperty.Procedure_Name);
        }

        private void backgroundWorker_MFGStockIssuePending_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }

        #endregion
    }
}