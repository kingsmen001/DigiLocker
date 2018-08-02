using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DigiLocker3
{
    public partial class CreateCourseOfficers : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        public enum MessageType { Success, Error, Info, Warning };

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

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
                    opnAddCourse.Visible = false;
                    opnAddTrainees.Visible = false;
                    opnCreateCourse.Visible = false;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = false;

                    opnAddCourseOfficer.Visible = false;
                    opnAddTraineesOfficer.Visible = false;
                    opnCreateCourseOfficer.Visible = false;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = true;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = false;
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
                Response.Redirect("Home.aspx", false);
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
            if (!this.IsPostBack)
            {
                con.Open();

                SqlCommand com = new SqlCommand("select *from OFFICER_COURSE_TYPE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();
                con.Close();

                SetInitialRow();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            int flag = 0;
            Regex regex2 = new Regex("^\\w+(,\\w+)*$");
            Regex regex1 = new Regex("^[\\w][\\w ]*$");
            Regex regex3 = new Regex("^\\d+$");
            foreach (GridViewRow r in excelgrd.Rows)
            {

                string entryname = (r.FindControl("txtMaxMarks") as TextBox).Text;
                string term = (r.FindControl("txtMinMarks") as TextBox).Text;
                string duration = (r.FindControl("txtSeniority") as TextBox).Text;
                if (regex1.IsMatch(entryname) & regex2.IsMatch(term) & regex3.IsMatch(duration))
                {
                    flag = 1;
                }
            }
            if (!regex1.IsMatch(txtCourseName.Text))
            {

                Response.Write("<script language='javascript'>alert('Enter Course Name');</script>");
            }
            else if (flag == 0)
            {
                Response.Write("<script language='javascript'>alert('There should be atleast one entry type');</script>");
            }
            else
            {
                con.Open();
                string courseTypeName = txtCourseName.Text.ToUpper();
                string query = "";
                int seniority = 0;
                if (CheckBox1.Checked)
                {
                    seniority = 1;
                }
                try
                {
                    SqlCommand cmd = new SqlCommand("insert into OFFICER_COURSE_TYPE (TYPE_NAME, SENIORITY) values ('" + courseTypeName + "', " + seniority + ")", con);
                    cmd.ExecuteNonQuery();
                    courseTypeName = courseTypeName.Replace(" ", "_");
                    string table_name = courseTypeName + "_ENTRY_TYPE";
                    cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( TYPE_NAME VARCHAR(50) PRIMARY KEY, Duration varchar(3), TERM_LABEL varchar(50) NOT NULL);", con);
                    cmd.ExecuteNonQuery();
                    string termLabel = "";
                    string typename = "";
                    int i = 0;
                    foreach (GridViewRow r in excelgrd.Rows)
                    {

                        string entryname = (r.FindControl("txtMaxMarks") as TextBox).Text;
                        string term = (r.FindControl("txtMinMarks") as TextBox).Text;
                        string duration = (r.FindControl("txtSeniority") as TextBox).Text;
                        if (!regex1.IsMatch(entryname) || !regex2.IsMatch(term) || !regex3.IsMatch(duration))
                        {

                        }
                        else {
                            term = (r.FindControl("txtMinMarks") as TextBox).Text.Replace(",", "_");
                            table_name = courseTypeName + "_ENTRY_TYPE";
                            query = "insert into " + table_name + " (TYPE_NAME, TERM_LABEL, Duration) values ( '" + entryname + "' , '" + term + "' , '" + duration + "' )";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            typename = entryname;
                            typename = typename.Replace(" ", "_");
                            table_name = courseTypeName + "_" + typename + "_SUBJECTS";
                            cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( ID int IDENTITY PRIMARY KEY, SUBJECT_NAME VARCHAR(50) UNIQUE, MAX_MARKS int, THEORY int, IA int, PRACTICAL int, TERM varchar(10) );", con);
                            cmd.ExecuteNonQuery();
                        }
                    }


                    if (CheckBox1.Checked)
                    {
                        Response.Redirect("SeniorityDetailsOfficers.aspx?coursename=" + courseTypeName.Replace("_", " "));
                    }
                    else
                    {
                        Response.Redirect("AddSubjectsOfficers.aspx?coursename=" + courseTypeName.Replace("_", " "));
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        string script = "alert(\" Course Name Already Exists \");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                        reset();
                    }
                    else
                        Response.Write(ex.ToString());
                }
                con.Close();
                //string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                //string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                //string FilePath = Server.MapPath(FolderPath + FileName);
                //FileUpload1.SaveAs(FilePath);
                //Import_To_Grid(FilePath, Extension);
                //ConfirmButton.Visible = true;
                //ConfirmButton.EnableViewState = true;
            }
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
            //GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            //Response.Write("Hello World submit " + GridView1.Rows.Count);
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            reset();
        }

        protected void reset()
        {
            //GridView1.DataSource = null;
            //GridView1.DataBind();
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            //con.Open();
            //string courseTypeName = txtCourseName.Text.ToUpper();
            //string query = "";
            //int seniority = 0;
            //if (CheckBox1.Checked)
            //{
            //    seniority = 1;
            //}
            //try
            //{
            //    SqlCommand cmd = new SqlCommand("insert into SAILOR_COURSE_TYPE (TYPE_NAME, SENIORITY) values ('" + courseTypeName + "', " + seniority + ")", con);
            //    cmd.ExecuteNonQuery();
            //    courseTypeName = courseTypeName.Replace(" ", "_");
            //    string table_name = courseTypeName + "_ENTRY_TYPE";
            //    cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( TYPE_NAME VARCHAR(50) PRIMARY KEY, TERMS_NO int NOT NULL, TERM_LABEL varchar(50) NOT NULL  );", con);
            //    cmd.ExecuteNonQuery();
            //    string termLabel = "";
            //    string typename = "";
            //    int i = 0;
            //    foreach (GridViewRow g1 in GridView1.Rows)
            //    {

            //        i++;
            //        termLabel = g1.Cells[2].Text;
            //        termLabel = termLabel.Replace(",", "_");
            //        table_name = courseTypeName + "_ENTRY_TYPE";
            //        query = "insert into " + table_name + " (TYPE_NAME, TERMS_NO, TERM_LABEL) values ( '" + g1.Cells[0].Text + "' , " + g1.Cells[1].Text + " , '" + termLabel + "' )";
            //        cmd = new SqlCommand(query, con);
            //        cmd.ExecuteNonQuery();
            //        typename = g1.Cells[0].Text;
            //        typename = typename.Replace(" ", "_");
            //        table_name = courseTypeName + "_" + typename + "_SUBJECTS";
            //        cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( ID int IDENTITY PRIMARY KEY, SUBJECT_NAME VARCHAR(50) UNIQUE, MAX_MARKS int, TERM varchar(10)  );", con);
            //        cmd.ExecuteNonQuery();
            //       // Response.Write(query + "       ");


            //    }
            //    if (CheckBox1.Checked)
            //    {
            //        Response.Redirect("SeniorityDetails.aspx?coursename=" + courseTypeName);
            //    }
            //    else
            //    {
            //        Response.Redirect("AddSubjectsSailors.aspx?coursename=" + courseTypeName);
            //    }
            //}
            //catch(SqlException ex)
            //{
            //    if (ex.Number == 2627 || ex.Number == 2601)
            //    {
            //        string script = "alert(\" Course Name Already Exists \");";
            //        ScriptManager.RegisterStartupScript(this, GetType(),
            //                              "ServerControlScript", script, true);
            //        reset();
            //    }
            //    else throw;
            //}
            //con.Close();


        }

        protected void txtCourseName_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex("^[\\w][\\w ]*$");
            if (regex.IsMatch(txtCourseName.Text))
            {

                con.Open();

                SqlCommand com = new SqlCommand("select Count(TYPE_NAME) from Officer_Course_type where TYPE_NAME = '" + txtCourseName.Text + "'", con); // table name 
                int count = (int)com.ExecuteScalar();
                if (count == 1)
                {
                    string script = "alert(\" Course Name Already Exists \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    lblMessage.Text = "I am called";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                    txtCourseName.Text = "";
                }
                con.Close();
            }
            else
            {
                string script = "alert(\" Only AlphaNumeric Characters are Allowed. Name Cannot start with Space \");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                txtCourseName.Text = "";
            }
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
                        dtCurrentTable.Rows[i - 1]["Entry Name"] = tx1.Text;
                        dtCurrentTable.Rows[i - 1]["Term Label"] = tx2.Text;
                        dtCurrentTable.Rows[i - 1]["Duration(in Weeks)"] = tx3.Text;
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
                        tx1.Text = dt.Rows[i]["Entry Name"].ToString();
                        tx2.Text = dt.Rows[i]["Term Label"].ToString();
                        tx3.Text = dt.Rows[i]["Duration(in Weeks)"].ToString();
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



            dt.Columns.Add(new DataColumn("Entry Name", typeof(string)));

            dt.Columns.Add(new DataColumn("Term Label", typeof(string)));

            dt.Columns.Add(new DataColumn("Duration(in Weeks)", typeof(string)));

            dr = dt.NewRow();



            dr["Entry Name"] = string.Empty;

            dr["Term Label"] = string.Empty;

            dr["Duration(in Weeks)"] = string.Empty;

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
                //Response.Write("<script language='javascript'>alert('This field Cannot be left blank');</script>");
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx", false);
        }
    }
}