using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigiLocker3
{
    public partial class SeniorityDetailsOfficers : System.Web.UI.Page
    {
        string coursename = " ";
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["User_ID"] as string))
            {


                if (Session["Access_Level"].ToString().Equals("1"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = true;
                    opnUpdateMarks.Visible = true;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;
                    opnAddCourseOfficer.Visible = true;
                    opnAddTraineesOfficer.Visible = true;
                    opnCreateCourseOfficer.Visible = true;
                    opnUpdateMarksOfficer.Visible = true;
                    opnViewResultOfficer.Visible = true;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = true;
                }
                else if (Session["Access_Level"].ToString().Equals("2"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = true;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;

                    opnAddCourseOfficer.Visible = true;
                    opnAddTraineesOfficer.Visible = true;
                    opnCreateCourseOfficer.Visible = true;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = true;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = true;
                }
                else if (Session["Access_Level"].ToString().Equals("3"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = false;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = false;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = false;

                    opnAddCourseOfficer.Visible = true;
                    opnAddTraineesOfficer.Visible = true;
                    opnCreateCourseOfficer.Visible = false;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = false;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = false;
                }
                else
                {
                    opnAddCourse.Visible = false;
                    opnAddTrainees.Visible = false;
                    opnCreateCourse.Visible = false;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = false;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;

                    opnAddCourseOfficer.Visible = false;
                    opnAddTraineesOfficer.Visible = false;
                    opnCreateCourseOfficer.Visible = false;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = false;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = true;
                }

            }
            else
            {
                opnAddCourseOfficer.Visible = false;
                opnAddTraineesOfficer.Visible = false;
                opnCreateCourseOfficer.Visible = false;
                opnUpdateMarksOfficer.Visible = false;
                opnViewResultOfficer.Visible = false;
                opnViewTraineesOfficer.Visible = true;
                opnUploadMarksOfficer.Visible = false;

                opnAddCourse.Visible = false;
                opnAddTrainees.Visible = false;
                opnCreateCourse.Visible = false;
                opnUpdateMarks.Visible = false;
                opnViewResult.Visible = false;
                opnViewTrainees.Visible = true;
                opnUploadMarks.Visible = false;
            }
            coursename = Request.QueryString["coursename"].Replace(" ", "_");

            string txt = " ";
            if (!this.IsPostBack)
            {
                if (Request.QueryString["coursename"] != null)
                {
                    heading.InnerHtml = Request.QueryString["coursename"] + " (Add Seniority Details)";
                }
                ddlEntryType.SelectedIndex = 0;
                ddlTerm.SelectedIndex = 0;

                con.Open();

                SqlCommand com = new SqlCommand("select TYPE_NAME from " + coursename + "_ENTRY_TYPE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();

                if (Request.QueryString.Count == 0)
                {
                    ddlEntryType.SelectedValue = coursename;
                }

                string table_name = coursename + "_ENTRY_TYPE";
                List<string> termLabel = new List<string>();
                //string term_Label = "";
                com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + ddlEntryType.Items[0].Text + "'", con); // table name 
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        List<string> term_Label = dr[0].ToString().Split('_').ToList();
                        foreach (string lbl in term_Label)
                        {
                            termLabel.Add(lbl);
                        }
                    }
                }
                ddlTerm.DataSource = termLabel.Distinct().ToList();
                ddlTerm.DataBind();
                con.Close();
                checkData();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            con.Open();
            string table_name = coursename + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + ddlTerm.SelectedValue + "_SENIORITY";
            SqlCommand cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(upper_lmt nvarchar(10) unique, lower_lmt nvarchar(10) unique, seniority nvarchar(10) unique)", con);
            cmd.ExecuteNonQuery();

            foreach (GridViewRow r in excelgrd.Rows)
            {
                string maxMarks = (r.FindControl("txtMaxMarks") as TextBox).Text;
                string minMarks = (r.FindControl("txtMinMarks") as TextBox).Text;
                string seniority = (r.FindControl("txtSeniority") as TextBox).Text;
                if (string.IsNullOrWhiteSpace(maxMarks) || string.IsNullOrWhiteSpace(minMarks) || string.IsNullOrWhiteSpace(seniority))
                {

                }
                else {
                    try
                    {

                        string query = "insert into " + table_name + "(lower_lmt, upper_lmt, seniority) values(" + minMarks + ", " + maxMarks + ", " + seniority + ")";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();


                    }
                    catch (Exception e1)
                    {
                        string error = e1.ToString();
                        Response.Write(error);
                    }
                    finally
                    {


                    }
                }


            }
            Response.Write("<script language='javascript'>alert('Seniority Criteria Added Successfully');</script>");
            con.Close();
            checkData();

            //if (FileUpload1.HasFile)
            //{
            //    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //    string FilePath = Server.MapPath(FolderPath + FileName);
            //    FileUpload1.SaveAs(FilePath);
            //    Import_To_Grid(FilePath, Extension);
            //    ConfirmButton.Visible = true;
            //    ConfirmButton.EnableViewState = true;
            //}
        }

        private void Import_To_Grid(string FilePath, string Extension)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //Response.Write("Hello World submit " + GridView1.Rows.Count);
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            //con.Open();

            //SqlCommand cmd;
            //string table_name = "";
            //string query = "";
            //for (int i = 0; i < ddlTerm.Items.Count; i++)
            //{
            //    if (ddlTerm.Items[i].Selected)
            //    {
            //        table_name = ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + ddlTerm.Items[i].Text + "_SENIORITY";
            //        cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(upper_lmt nvarchar(10) unique, lower_lmt nvarchar(10) unique, seniority nvarchar(10) unique)", con);
            //        cmd.ExecuteNonQuery();
            //        foreach (GridViewRow g1 in GridView1.Rows)
            //        {
            //            query = "insert into " + table_name + "(lower_lmt, upper_lmt, seniority) values(" + System.Convert.ToDecimal(g1.Cells[1].Text) + ", " + System.Convert.ToDecimal(g1.Cells[0].Text) + ", " + System.Convert.ToDecimal(g1.Cells[2].Text) + ")";
            //            cmd = new SqlCommand(query, con);

            //        }
            //    }
            //}

            //con.Close();

            Response.Redirect("AddSubjectsOfficers.aspx?coursename=" + coursename.Replace("_", " "));

            //string script = "alert(\" " + i + " Trainees Added to " + course_type + course_no + " " + entry_type + " \");";
            //ScriptManager.RegisterStartupScript(this, GetType(),
            //                      "ServerControlScript", script, true);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            reset();
        }

        protected void reset()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlEntryTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string table_name = coursename + "_ENTRY_TYPE";
            List<string> termLabel = new List<string>();
            termLabel.Clear();
            //string term_Label = "";
            SqlCommand com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + ddlEntryType.SelectedValue + "'", con); // table name 
            using (SqlDataReader dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    termLabel = dr[0].ToString().Split('_').ToList();

                }
            }

            ddlTerm.DataSource = termLabel.Distinct().ToList();
            ddlTerm.DataBind();
            con.Close();
            checkData();
        }

        protected void ddlTermIndexChanged(object sender, EventArgs e)
        {
            checkData();
        }

        protected void checkData()
        {
            con.Open();
            string table_name = coursename + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + ddlTerm.SelectedValue + "_SENIORITY";
            SqlCommand cmd = new SqlCommand("If exists(select name from sysobjects where name = '" + table_name + "') Select upper_lmt as \"Max Marks(in %)\", lower_lmt as \"Min Marks(in %)\", seniority as \"Seniority(in Months)\" from  " + table_name, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                div1.Visible = false;
                div2.Visible = true;
                ConfirmButton.Visible = true;
                ConfirmButton.EnableViewState = true;

            }
            else
            {
                SetInitialRow();
                div2.Visible = false;
                div1.Visible = true;
                ConfirmButton.Visible = false;
                ConfirmButton.EnableViewState = false;
            }
            con.Close();
        }

        protected void addnewrow()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox tx1 = (TextBox)excelgrd.Rows[rowIndex].Cells[0].FindControl("txtMaxMarks");
                        TextBox tx2 = (TextBox)excelgrd.Rows[rowIndex].Cells[1].FindControl("txtMinMarks");
                        TextBox tx3 = (TextBox)excelgrd.Rows[rowIndex].Cells[2].FindControl("txtSeniority");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Max Marks(in %)"] = tx1.Text;
                        dtCurrentTable.Rows[i - 1]["Min Marks(in %)"] = tx2.Text;
                        dtCurrentTable.Rows[i - 1]["Seniority(in Months)"] = tx3.Text;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    excelgrd.DataSource = dtCurrentTable;
                    excelgrd.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox tx1 = (TextBox)excelgrd.Rows[rowIndex].Cells[0].FindControl("txtMaxMarks");
                        TextBox tx2 = (TextBox)excelgrd.Rows[rowIndex].Cells[1].FindControl("txtMinMarks");
                        TextBox tx3 = (TextBox)excelgrd.Rows[rowIndex].Cells[2].FindControl("txtSeniority");
                        tx1.Text = dt.Rows[i]["Max Marks(in %)"].ToString();
                        tx2.Text = dt.Rows[i]["Min Marks(in %)"].ToString();
                        tx3.Text = dt.Rows[i]["Seniority(in Months)"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            addnewrow();
        }

        protected void SetInitialRow()

        {

            DataTable dt = new DataTable();

            DataRow dr = null;



            dt.Columns.Add(new DataColumn("Max Marks(in %)", typeof(string)));

            dt.Columns.Add(new DataColumn("Min Marks(in %)", typeof(string)));

            dt.Columns.Add(new DataColumn("Seniority(in Months)", typeof(string)));

            dr = dt.NewRow();



            dr["Max Marks(in %)"] = string.Empty;

            dr["Min Marks(in %)"] = string.Empty;

            dr["Seniority(in Months)"] = string.Empty;

            dt.Rows.Add(dr);

            //dr = dt.NewRow();



            //Store the DataTable in ViewState

            ViewState["CurrentTable"] = dt;



            excelgrd.DataSource = dt;

            excelgrd.DataBind();

        }

        protected void OntextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            textBox.Focus();
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                Response.Write("<script language='javascript'>alert('This field Cannot be left blank');</script>");
            }
        }
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx", false);
        }

    }
}