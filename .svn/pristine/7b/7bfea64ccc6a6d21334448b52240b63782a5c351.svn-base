using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{
    public partial class FrmMappingMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        MappingMaster ObjMapping;

        DataTable DtSieve;
        DataTable DtAssort;
        DataTable DtShape;
        DataTable DtColor;
        DataTable DtClarity;
        DataTable DtCut;
        DataTable DtColorGroup;
        DataTable DTabFile;
        DataTable DTab_Data;
        DataTable DShape;
        DataTable DColor;
        DataTable DClarity;
        DataTable DCut;
        DataTable DColorGroup;
        DataTable DAssort;
        DataTable DSieve;
        DataTable dtMapping;
        DataTable dtAddMapping;
        DataTable m_dtbMapDetails;
        int m_numForm_id;
        int IntRes;

        bool m_blnsave;
        #endregion

        #region Constructor
        public FrmMappingMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            ObjMapping = new MappingMaster();

            DtSieve = new SieveMaster().GetData();
            DtAssort = new AssortMaster().GetData();
            DtShape = new ShapeMaster().GetData();
            DtColor = new ColorMaster().GetData();
            DtClarity = new ClarityMaster().GetData();
            DtCut = new CutMaster().GetData();
            DtColorGroup = new ColorGroupMaster().GetData();
            DTabFile = new DataTable();
            DTab_Data = new DataTable();
            DShape = new DataTable();
            DColor = new DataTable();
            DClarity = new DataTable();
            DCut = new DataTable();
            DColorGroup = new DataTable();
            DAssort = new DataTable();
            DSieve = new DataTable();
            dtMapping = new DataTable();
            dtAddMapping = new DataTable();
            m_dtbMapDetails = new DataTable();
            m_numForm_id = 0;
            IntRes = 0;

            m_blnsave = new bool();
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
            Val.frmGenSet(this);
            AttachFormEvents();
            btnSave.Enabled = false;
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            //objBOFormEvents.ObjToDispose.Add(objCountry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMappingMaster_Load(object sender, EventArgs e)
        {
            RepLueShape.DataSource = DtShape;
            RepLueShape.ValueMember = "shape_id";
            RepLueShape.DisplayMember = "shape_name";

            RepLuePurity.DataSource = DtClarity;
            RepLuePurity.ValueMember = "purity_id";
            RepLuePurity.DisplayMember = "purity_name";

            RepLueCut.DataSource = DtCut;
            RepLueCut.ValueMember = "cut_id";
            RepLueCut.DisplayMember = "cut_name";

            RepLueColor.DataSource = DtColor;
            RepLueColor.ValueMember = "color_id";
            RepLueColor.DisplayMember = "color_name";

            RepLueAssort.DataSource = DtAssort;
            RepLueAssort.ValueMember = "assort_id";
            RepLueAssort.DisplayMember = "assort_name";

            RepLueSieve.DataSource = DtSieve;
            RepLueSieve.ValueMember = "sieve_id";
            RepLueSieve.DisplayMember = "sieve_name";

            RepLueColorGroup.DataSource = DtColorGroup;
            RepLueColorGroup.ValueMember = "color_group_id";
            RepLueColorGroup.DisplayMember = "color_group_name";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Mapping_MasterProperty objMappingProperty = new Mapping_MasterProperty();
                ObjPer.SetFormPer();
                if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
                {
                    Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                    return;
                }

                m_blnsave = true;
                if (!ValidateDetails())
                {
                    m_blnsave = false;
                    btnSave.Enabled = true;
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                //DataTable dt = new DataTable();
                //dt = ObjPrcImp.GetData(Val.ToString(dtpDate.Text), Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                //if (dt.Rows.Count == 0)
                //{
                int rowNo = 1;

                if (grdMappingImport.DataSource != null)
                {

                    DTab_Data = (DataTable)grdMappingImport.DataSource;
                    DataView view = new DataView(DTab_Data);
                    DataTable distinctValues = view.ToTable(true, "shape_id", "color_id", "assort_id", "color_group_id");

                    if (dtMapping.Rows.Count == 0)
                    {
                        if (DTab_Data.Rows.Count == distinctValues.Rows.Count)
                        {
                            foreach (DataRow DRow in DTab_Data.Rows)
                            {
                                //if (DRow["shape"] != null)
                                //{
                                //    if (Val.ToString(DRow["shape"]) != "")
                                //    {
                                //        if (DtShape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").Length > 0)
                                //        {
                                //            DShape = DtShape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").CopyToDataTable();
                                //        }
                                //        else
                                //        {
                                //            Global.Message("Shape Not found in Master" + Val.ToString(DRow["shape"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            return;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Global.Message("Shape Name Blank at Row No :" + rowNo);
                                //        this.Cursor = Cursors.Default;
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    Global.Message("Shape Name are not found :" + Val.ToString(DRow["shape"]));
                                //    this.Cursor = Cursors.Default;
                                //    return;
                                //}

                                //if (DRow["assort"] != null)
                                //{
                                //    if (Val.ToString(DRow["assort"]) != "")
                                //    {
                                //        if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                                //        {
                                //            DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                                //        }
                                //        else
                                //        {
                                //            Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            return;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Global.Message("Assort Name Blank at Row No :" + rowNo);
                                //        this.Cursor = Cursors.Default;
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    Global.Message("Assort Name are not found :" + Val.ToString(DRow["assort"]));
                                //    this.Cursor = Cursors.Default;
                                //    return;
                                //}
                                //if (DRow["sieve"] != null)
                                //{
                                //    if (Val.ToString(DRow["sieve"]) != "")
                                //    {
                                //        if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                                //        {
                                //            DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                                //        }
                                //        else
                                //        {
                                //            Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            return;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Global.Message("Sieve Name Blank at Row No :" + rowNo);
                                //        this.Cursor = Cursors.Default;
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    Global.Message("Sieve Name are not found :" + Val.ToString(DRow["sieve"]));
                                //    this.Cursor = Cursors.Default;
                                //    return;
                                //}
                                //if (DRow["color_group"] != null)
                                //{
                                //    if (Val.ToString(DRow["color_group"]) != "")
                                //    {
                                //        if (DtColorGroup.Select("color_group_name ='" + Val.ToString(DRow["color_group"]) + "'").Length > 0)
                                //        {
                                //            DColorGroup = DtColorGroup.Select("color_group_name ='" + Val.ToString(DRow["color_group"]) + "'").CopyToDataTable();
                                //        }
                                //        else
                                //        {
                                //            Global.Message("Color Group Not found in Master" + Val.ToString(DRow["color_group"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //            return;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Global.Message("Color Group Name Blank at Row No :" + rowNo);
                                //        this.Cursor = Cursors.Default;
                                //        return;
                                //    }
                                //}
                                //else
                                //{
                                //    Global.Message("Color Group Name are not found :" + Val.ToString(DRow["color_group"]));
                                //    this.Cursor = Cursors.Default;
                                //    return;
                                //}
                                objMappingProperty.shape_id = Val.ToInt(DRow["shape_id"]);
                                objMappingProperty.color_id = Val.ToInt(DRow["color_id"]);
                                objMappingProperty.color_group_id = Val.ToInt(DRow["color_group_id"]);
                                objMappingProperty.assort_id = Val.ToInt(DRow["assort_id"]);
                                objMappingProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);
                                //if (DColor.Rows.Count > 0)
                                //{
                                //    objMappingProperty.color_id = Val.ToInt(DColor.Rows[0]["color_id"]);
                                //}
                                //else
                                //{
                                //    objMappingProperty.color_id = Val.ToInt(0);
                                //}
                                //if (DClarity.Rows.Count > 0)
                                //{
                                //    objMappingProperty.purity_id = Val.ToInt(DClarity.Rows[0]["purity_id"]);
                                //}
                                //else
                                //{
                                //    objMappingProperty.purity_id = Val.ToInt(0);
                                //}
                                //if (DCut.Rows.Count > 0)
                                //{
                                //    objMappingProperty.cut_id = Val.ToInt(DCut.Rows[0]["cut_id"]);
                                //}
                                //else
                                //{
                                //    objMappingProperty.cut_id = Val.ToInt(0);
                                //}


                                //if (DSieve.Rows.Count > 0)
                                //{
                                //    objMappingProperty.sieve_id = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                                //}
                                //else
                                //{
                                //    objMappingProperty.sieve_id = Val.ToInt(0);
                                //}

                                int checkRes = ObjMapping.GetData(objMappingProperty);
                                if (checkRes > 0)
                                {
                                    Global.Message("Mapping Already Exist Please check at Row No :" + rowNo);
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                rowNo++;
                            }
                        }
                        //else
                        //{
                        //    Global.Message("Duplicate Data Exist Please Check");
                        //    this.Cursor = Cursors.Default;
                        //    return;
                        //}
                    }
                    else
                    {
                        //if (dtMapping.Rows.Count != distinctValues.Rows.Count)
                        //{
                        //    Global.Message("Duplicate Data Exist Please Check");
                        //    this.Cursor = Cursors.Default;
                        //    return;
                        //}
                    }
                    objMappingProperty = null;
                    DialogResult result = MessageBox.Show("Do you want to save data?", "Confirmation", MessageBoxButtons.YesNoCancel);
                    if (result != DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                    panelProgress.Visible = true;
                    backgroundWorker_Mapping.RunWorkerAsync();

                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    Global.Message("Please Import Mapping File First");
                    this.Cursor = Cursors.Default;
                    return;
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                dtMapping = ObjMapping.GetMappingData();
                grdMappingImport.DataSource = dtMapping;
                dgvMappingImport.BestFitColumns();
                //grdMappingImport.RefreshDataSource();
                btnImport.Enabled = false;
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            m_blnsave = true;
            dtMapping.Rows.Clear();
            OpenFileDialog OpenDialog = new OpenFileDialog();
            if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            txtFileName.Text = OpenDialog.FileName;
            OpenDialog.Dispose();
            OpenDialog = null;

            if (File.Exists(txtFileName.Text) == false)
            {
                Global.Message("File Is Not Exists To The Path");
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            grdMappingImport.DataSource = null;

            if (txtFileName.Text.Length != 0)
            {
                using (var pck = new ExcelPackage(new FileInfo(txtFileName.Text)))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                    DTabFile = WorksheetToDataTable(ws, true);
                }
            }
            m_dtbMapDetails = new DataTable();
            m_dtbMapDetails.Columns.Add("mapping_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("shape_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("shape", typeof(string));
            m_dtbMapDetails.Columns.Add("color_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("color", typeof(string));
            m_dtbMapDetails.Columns.Add("purity_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("purity", typeof(string));
            m_dtbMapDetails.Columns.Add("cut_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("cut", typeof(string));
            m_dtbMapDetails.Columns.Add("sieve_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("sieve", typeof(string));
            m_dtbMapDetails.Columns.Add("assort_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("assort", typeof(string));
            m_dtbMapDetails.Columns.Add("color_group_id", typeof(int)).DefaultValue = 0;
            m_dtbMapDetails.Columns.Add("color_group", typeof(string));

            if (DTabFile.Rows.Count > 0)
            {
                foreach (DataRow DRow in DTabFile.Rows)
                {
                    if (DRow["shape"] != null)
                    {
                        if (Val.ToString(DRow["shape"]) != "")
                        {
                            if (DtShape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").Length > 0)
                            {
                                DShape = DtShape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Shape Not found in Master" + Val.ToString(DRow["shape"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }

                    }
                    if (DRow["cut"] != null)
                    {
                        if (Val.ToString(DRow["cut"]) != "")
                        {
                            if (DtCut.Select("cut_name ='" + Val.ToString(DRow["cut"]) + "'").Length > 0)
                            {
                                DCut = DtCut.Select("cut_name ='" + Val.ToString(DRow["cut"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Cut Not found in Master" + Val.ToString(DRow["cut"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    if (DRow["purity"] != null)
                    {
                        if (Val.ToString(DRow["purity"]) != "")
                        {
                            if (DtClarity.Select("purity_name ='" + Val.ToString(DRow["purity"]) + "'").Length > 0)
                            {
                                DClarity = DtClarity.Select("purity_name ='" + Val.ToString(DRow["purity"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Purity Not found in Master" + Val.ToString(DRow["purity"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    if (DRow["color"] != null)
                    {
                        if (Val.ToString(DRow["color"]) != "")
                        {
                            if (DtColor.Select("color_name = '" + Val.ToString(DRow["color"]) + "' ").Length > 0)
                            {
                                DColor = DtColor.Select("color_name ='" + Val.ToString(DRow["color"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Color Not found in Master" + Val.ToString(DRow["color"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    if (DRow["assort"] != null)
                    {
                        if (Val.ToString(DRow["assort"]) != "")
                        {
                            if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                            {
                                DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Assort Not found in Master" + Val.ToString(DRow["assort"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    if (DRow["sieve"] != null)
                    {
                        if (Val.ToString(DRow["sieve"]) != "")
                        {
                            if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                            {
                                DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Sieve Not found in Master" + Val.ToString(DRow["sieve"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }

                    if (DRow["color group"] != null)
                    {
                        if (Val.ToString(DRow["color group"]) != "")
                        {
                            if (DtColorGroup.Select("color_group_name ='" + Val.ToString(DRow["color group"]) + "'").Length > 0)
                            {
                                DColorGroup = DtColorGroup.Select("color_group_name ='" + Val.ToString(DRow["color group"]) + "'").CopyToDataTable();
                            }
                            else
                            {
                                Global.Message("Color Group Not found in Master" + Val.ToString(DRow["color group"]), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                    DataRow drwNew = m_dtbMapDetails.NewRow();
                    if (DShape.Rows.Count > 0)
                    {
                        drwNew["shape_id"] = Val.ToInt(DShape.Rows[0]["shape_id"]);
                        drwNew["shape"] = Val.ToString(DShape.Rows[0]["shape_name"]);
                    }
                    if (DColor.Rows.Count > 0)
                    {
                        drwNew["color_id"] = Val.ToInt(DColor.Rows[0]["color_id"]);
                        drwNew["color"] = Val.ToString(DColor.Rows[0]["color_name"]);
                    }
                    if (DClarity.Rows.Count > 0)
                    {
                        drwNew["purity_id"] = Val.ToInt(DClarity.Rows[0]["purity_id"]);
                        drwNew["purity"] = Val.ToString(DClarity.Rows[0]["purity_name"]);
                    }
                    if (DCut.Rows.Count > 0)
                    {
                        drwNew["cut_id"] = Val.ToInt(DCut.Rows[0]["cut_id"]);
                        drwNew["cut"] = Val.ToString(DColorGroup.Rows[0]["cut_name"]);
                    }
                    if (DAssort.Rows.Count > 0)
                    {
                        drwNew["assort_id"] = Val.ToInt(DAssort.Rows[0]["assort_id"]);
                        drwNew["assort"] = Val.ToString(DAssort.Rows[0]["assort_name"]);
                    }
                    if (DSieve.Rows.Count > 0)
                    {
                        drwNew["sieve_id"] = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                        drwNew["sieve"] = Val.ToString(DSieve.Rows[0]["sieve_name"]);
                    }
                    if (DColorGroup.Rows.Count > 0)
                    {
                        drwNew["color_group_id"] = Val.ToInt(DColorGroup.Rows[0]["color_group_id"]);
                        drwNew["color_group"] = Val.ToString(DColorGroup.Rows[0]["color_group_name"]);
                    }
                    m_dtbMapDetails.Rows.Add(drwNew);

                    dgvMappingImport.MoveLast();
                }

                grdMappingImport.DataSource = m_dtbMapDetails;
                btnSave.Enabled = true;
            }
            this.Cursor = Cursors.Default;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtFileName.Text = "";
                btnImport.Enabled = true;
                dtMapping.Rows.Clear();
                DTabFile.Rows.Clear();
                grdMappingImport.DataSource = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblFormatSample_Click(object sender, EventArgs e)
        {
            Global.CopyFormat(System.Windows.Forms.Application.StartupPath + @"\FORMAT\MappingFormat.xlsx", "MappingFormat.xlsx", "xlsx");
        }
        private void backgroundWorker_Mapping_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                IntRes = 0;

                int IntCounter = 0;
                int Count = 0;
                int TotalCount = DTab_Data.Rows.Count;

                Mapping_MasterProperty objMappingProperty = new Mapping_MasterProperty();


                //foreach (DataRow DRow in DTab_Data.Rows)
                //{
                //    if (DRow["shape"] != null)
                //    {
                //        if (Val.ToString(DRow["shape"]) != "")
                //        {
                //            if (DtShape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").Length > 0)
                //            {
                //                DShape = DtShape.Select("shape_name ='" + Val.ToString(DRow["shape"]) + "'").CopyToDataTable();
                //            }
                //        }
                //    }
                //    if (DRow["color"] != null)
                //    {
                //        if (Val.ToString(DRow["color"]) != "")
                //        {
                //            if (DtColor.Select("color_name ='" + Val.ToString(DRow["color"]) + "'").Length > 0)
                //            {
                //                DColor = DtColor.Select("color_name ='" + Val.ToString(DRow["color"]) + "'").CopyToDataTable();
                //            }
                //        }
                //    }
                //    //if (DRow["clarity"] != null)
                //    //{
                //    //    if (Val.ToString(DRow["clarity"]) != "")
                //    //    {
                //    //        if (DtClarity.Select("purity_name ='" + Val.ToString(DRow["clarity"]) + "'").Length > 0)
                //    //        {
                //    //            DClarity = DtClarity.Select("purity_name ='" + Val.ToString(DRow["clarity"]) + "'").CopyToDataTable();
                //    //        }
                //    //    }
                //    //}
                //    //if (DRow["cut"] != null)
                //    //{
                //    //    if (Val.ToString(DRow["cut"]) != "")
                //    //    {
                //    //        if (DtCut.Select("cut_name ='" + Val.ToString(DRow["cut"]) + "'").Length > 0)
                //    //        {
                //    //            DCut = DtCut.Select("cut_name ='" + Val.ToString(DRow["cut"]) + "'").CopyToDataTable();
                //    //        }
                //    //    }
                //    //}
                //    if (DRow["color_group"] != null)
                //    {
                //        if (Val.ToString(DRow["color_group"]) != "")
                //        {
                //            if (DtColorGroup.Select("color_group_name ='" + Val.ToString(DRow["color_group"]) + "'").Length > 0)
                //            {
                //                DColorGroup = DtColorGroup.Select("color_group_name ='" + Val.ToString(DRow["color_group"]) + "'").CopyToDataTable();
                //            }
                //        }
                //    }
                //    if (DRow["assort"] != null)
                //    {
                //        if (Val.ToString(DRow["assort"]) != "")
                //        {
                //            if (DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").Length > 0)
                //            {
                //                DAssort = DtAssort.Select("assort_name ='" + Val.ToString(DRow["assort"]) + "'").CopyToDataTable();
                //            }
                //        }
                //    }

                //if (DRow["sieve"] != null)
                //{
                //    if (Val.ToString(DRow["sieve"]) != "")
                //    {
                //        if (DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").Length > 0)
                //        {
                //            DSieve = DtSieve.Select("sieve_name ='" + Val.ToString(DRow["sieve"]) + "'").CopyToDataTable();
                //        }
                //    }
                //}
                foreach (DataRow DRow in DTab_Data.Rows)
                {
                    if (Val.ToInt(DRow["mapping_id"]) == 0)
                    {
                        objMappingProperty.shape_id = Val.ToInt(DRow["shape_id"]);
                        objMappingProperty.color_id = Val.ToInt(DRow["color_id"]);
                        objMappingProperty.cut_id = Val.ToInt(DRow["cut_id"]);
                        //if (DColor.Rows.Count > 0)
                        //{
                        //    objMappingProperty.color_id = Val.ToInt(DColor.Rows[0]["color_id"]);
                        //}
                        //else
                        //{
                        //    objMappingProperty.color_id = Val.ToInt(0);
                        //}
                        //if (DClarity.Rows.Count > 0)
                        //{
                        //    objMappingProperty.purity_id = Val.ToInt(DClarity.Rows[0]["purity_id"]);
                        //}
                        //else
                        //{
                        //    objMappingProperty.purity_id = Val.ToInt(0);
                        //}
                        //if (DCut.Rows.Count > 0)
                        //{
                        //    objMappingProperty.cut_id = Val.ToInt(DCut.Rows[0]["cut_id"]);
                        //}
                        //else
                        //{
                        //    objMappingProperty.cut_id = Val.ToInt(0);
                        //}

                        objMappingProperty.color_group_id = Val.ToInt(DRow["color_group_id"]);
                        objMappingProperty.assort_id = Val.ToInt(DRow["assort_id"]);
                        objMappingProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);
                        //if (DSieve.Rows.Count > 0)
                        //{
                        //    objMappingProperty.sieve_id = Val.ToInt(DSieve.Rows[0]["sieve_id"]);
                        //}
                        //else
                        //{
                        //    objMappingProperty.sieve_id = Val.ToInt(0);
                        //}

                        //i++;
                        IntRes = ObjMapping.Save(objMappingProperty);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                    else if (Val.ToInt(DRow["is_edit"]) == 1)
                    {
                        objMappingProperty.mapping_id = Val.ToInt(DRow["mapping_id"]);
                        objMappingProperty.shape_id = Val.ToInt(DRow["shape_id"]);
                        objMappingProperty.color_id = Val.ToInt(DRow["color_id"]);
                        objMappingProperty.cut_id = Val.ToInt(DRow["cut_id"]);
                        objMappingProperty.color_group_id = Val.ToInt(DRow["color_group_id"]);
                        objMappingProperty.assort_id = Val.ToInt(DRow["assort_id"]);
                        objMappingProperty.sieve_id = Val.ToInt(DRow["sieve_id"]);

                        IntRes = ObjMapping.Save(objMappingProperty);

                        Count++;
                        IntCounter++;
                        IntRes++;
                        SetControlPropertyValue(lblProgressCount, "Text", Count.ToString() + "" + "/" + "" + TotalCount.ToString() + " Completed....");
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                if (ex.InnerException != null)
                {
                    Global.Message(ex.InnerException.ToString());
                }
            }
        }
        private void backgroundWorker_Mapping_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                panelProgress.Visible = false;
                if (IntRes > 0)
                {
                    Global.Confirm("Mapping Import Data Save Successfully");
                    btnClear_Click(null, null);
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Global.Confirm("Error In Mapping Import");
                    this.Cursor = Cursors.Default;

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                Global.Message(ex.InnerException.ToString());
            }
        }

        #region Grid Events
        private void dgvMappingImport_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Column == clmShape || e.Column == clmColor || e.Column == ClmAssort || e.Column == clmColorGroup)
                {
                    dgvMappingImport.SetRowCellValue(e.RowHandle, "is_edit", 1);
                    dgvMappingImport.UpdateCurrentRow();
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
            //bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();

            try
            {
                if (m_blnsave)
                {
                    //if (lueCurrency.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Currency"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueCurrency.Focus();
                    //    }
                    //}

                    //if (lueRateType.ItemIndex < 0)
                    //{
                    //    lstError.Add(new ListError(13, "Rate Type"));
                    //    if (!blnFocus)
                    //    {
                    //        blnFocus = true;
                    //        lueRateType.Focus();
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public System.Data.DataTable WorksheetToDataTable(ExcelWorksheet ws, bool hasHeader = true)
        {
            System.Data.DataTable dt = new System.Data.DataTable(ws.Name);
            int totalCols = ws.Dimension.End.Column;
            int totalRows = ws.Dimension.End.Row;
            int startRow = hasHeader ? 2 : 1;
            ExcelRange wsRow;
            DataRow dr;
            foreach (var firstRowCell in ws.Cells[1, 1, 1, totalCols])
            {
                dt.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }

            for (int rowNum = startRow; rowNum <= totalRows; rowNum++)
            {
                wsRow = ws.Cells[rowNum, 1, rowNum, totalCols];
                dr = dt.NewRow();
                foreach (var cell in wsRow)
                {
                    dr[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(dr);
            }

            return dt;
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
                            dgvMappingImport.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvMappingImport.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvMappingImport.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvMappingImport.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvMappingImport.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvMappingImport.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvMappingImport.ExportToCsv(Filepath);
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
