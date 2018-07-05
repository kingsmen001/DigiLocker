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
            
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
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
            SqlCommand cmd = new SqlCommand("insert into SAILOR_COURSE_TYPE (TYPE_NAME) values ('" + courseTypeName + "')", con);
            cmd.ExecuteNonQuery();
            courseTypeName = courseTypeName.Replace(" ","_");
            string table_name = courseTypeName + "_ENTRY_TYPE";
            cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( TYPE_NAME VARCHAR(50), TERMS_NO int, TERM_LABEL varchar(50)  );", con);
            cmd.ExecuteNonQuery();  
            string termLabel = "";
            string typename = "";
            int i = 0;
            foreach (GridViewRow g1 in GridView1.Rows)
            {

                    i++;
                termLabel = g1.Cells[2].Text;
                termLabel = termLabel.Replace(",", "_");
                string query = "insert into " + table_name + " (TYPE_NAME, TERMS_NO, TERM_LABEL) values ( '" + g1.Cells[0].Text + "' , " + g1.Cells[1].Text + " , '" + termLabel + "' )";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                typename = g1.Cells[0].Text;
                typename = typename.Replace(" ", "_");
                table_name = courseTypeName + "_" + typename + "_SUBJECTS";
                cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "( SUBJECT_NAME VARCHAR(50), MAX_MARKS int, TERM varchar(10)  );", con);
                cmd.ExecuteNonQuery();
                Response.Write(query+ "       ");

                }
            con.Close();

            }
            
           

            //string script = "alert(\" " + i + " Trainees Added to " + course_type + entry_type + " \");";
            //ScriptManager.RegisterStartupScript(this, GetType(),
            //                      "ServerControlScript", script, true);
        

    }
}