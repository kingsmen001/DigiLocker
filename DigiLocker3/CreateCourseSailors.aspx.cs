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
    public partial class CreateCourse : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand com = new SqlCommand("select *from SAILOR_COURSE_TYPE", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseType.DataBind();
            con.Close();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if(txtCourseName.Text == "")
            {
                
                Response.Write("<script language='javascript'>alert('Enter Course Name');</script>");
            }
            else if (!FileUpload1.HasFile)
            {
                Response.Write("<script language='javascript'>alert('Select Course Detail File');</script>");
            }
            else
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension);
                ConfirmButton.Visible = true;
                ConfirmButton.EnableViewState = true;
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
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //Response.Write("Hello World submit " + GridView1.Rows.Count);
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

        protected void ConfirmButton_Click(object sender, EventArgs e)
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
                SqlCommand cmd = new SqlCommand("insert into SAILOR_COURSE_TYPE (TYPE_NAME, SENIORITY) values ('" + courseTypeName + "', " + seniority + ")", con);
                cmd.ExecuteNonQuery();
                courseTypeName = courseTypeName.Replace(" ", "_");
                string table_name = courseTypeName + "_ENTRY_TYPE";
                cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( TYPE_NAME VARCHAR(50) PRIMARY KEY, TERMS_NO int NOT NULL, TERM_LABEL varchar(50) NOT NULL  );", con);
                cmd.ExecuteNonQuery();
                string termLabel = "";
                string typename = "";
                int i = 0;
                foreach (GridViewRow g1 in GridView1.Rows)
                {

                    i++;
                    termLabel = g1.Cells[2].Text;
                    termLabel = termLabel.Replace(",", "_");
                    table_name = courseTypeName + "_ENTRY_TYPE";
                    query = "insert into " + table_name + " (TYPE_NAME, TERMS_NO, TERM_LABEL) values ( '" + g1.Cells[0].Text + "' , " + g1.Cells[1].Text + " , '" + termLabel + "' )";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    typename = g1.Cells[0].Text;
                    typename = typename.Replace(" ", "_");
                    table_name = courseTypeName + "_" + typename + "_SUBJECTS";
                    cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( ID int IDENTITY PRIMARY KEY, SUBJECT_NAME VARCHAR(50) UNIQUE, MAX_MARKS int, TERM varchar(10)  );", con);
                    cmd.ExecuteNonQuery();
                   // Response.Write(query + "       ");
                    

                }
                if (CheckBox1.Checked)
                {
                    Response.Redirect("SeniorityDetails.aspx?coursename=" + courseTypeName);
                }
                else
                {
                    Response.Redirect("AddSubjectsSailors.aspx?coursename=" + courseTypeName);
                }
            }
            catch(SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    string script = "alert(\" Course Name Already Exists \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    reset();
                }
                else throw;
            }
            con.Close();
            

        }

        protected void txtCourseName_TextChanged(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand com = new SqlCommand("select Count(TYPE_NAME) from Sailor_Course_type where TYPE_NAME = '" + txtCourseName.Text + "'", con); // table name 
            int count = (int)com.ExecuteScalar();
            if(count ==1)
            {
                string script = "alert(\" Course Name Already Exists \");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                txtCourseName.Text = "";
            }
            con.Close();
        }
    }
}