using BLL.FunctionClasses.Transaction.MFG;
using BLL.PropertyClasses.Report;
using DERP.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmAssortReportView : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();

        DataTable m_dtbParam;
        DataTable m_dtCut;
        DataTable m_dtbKapan;
        //DataTable DTab_KapanWiseData;

        #endregion

        #region Constructor
        public FrmAssortReportView()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();
            //DTab_KapanWiseData = new DataTable();

            ReportParams_Property = new ReportParams_Property();
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
            objBOFormEvents.ObjToDispose.Add("");
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private bool ClearDetails()
        {
            bool blnReturn = true;
            try
            {
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                lueKapan.EditValue = System.DBNull.Value;
                lueCutNo.EditValue = System.DBNull.Value;

                txtJangedNo.Text = "0";

                RBtnType.SelectedIndex = 0;
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
        private void FrmMFGProcessReceiveWithSplit_Load(object sender, EventArgs e)
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

                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = DateTime.Now;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

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
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearDetails();
        }
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            ReportParams_Property = new ReportParams_Property();
            ReportParams_Property.From_Date = Val.DBDate(dtpFromDate.Text);
            ReportParams_Property.To_Date = Val.DBDate(dtpToDate.Text);
            ReportParams_Property.kapan_id = Val.ToString(lueKapan.EditValue);
            ReportParams_Property.cut_id = Val.ToString(lueCutNo.EditValue);
            ReportParams_Property.janged_no = Val.ToInt64(txtJangedNo.Text);
            ReportParams_Property.type = Val.ToString(RBtnType.Text);

            MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
            DataTable DTab_IssueJanged = objMFGJangedIssue.GetDataDetails(Val.ToInt64(txtJangedNo.Text));

        }

        #endregion
    }
}
