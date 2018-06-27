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
    public partial class UploadMarks : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //reset();
                Response.Write("Refreshed");
            }
            //reset();
            Response.Write("Refreshed");

            con.Open();

            SqlCommand com = new SqlCommand("select *from SAILOR_COURSE_TYPE", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseType.DataBind();

            string name = ddlCourseType.Items[0].Value;
            com = new SqlCommand("select Course_No from Sailor_Courses where Course_Name ='" + name + "'", con); // table name 
            da = new SqlDataAdapter(com);
            ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
            ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseNo.DataBind();

            name = ddlCourseType.Items[0].Value + "_COURSE_TYPE";
            com = new SqlCommand("select * from " + name, con); // table name 
            da = new SqlDataAdapter(com);
            ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlEntryType.DataBind();


            con.Close();
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

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            con.Open();
            int i = 0;
            string course_type = ddlCourseType.SelectedValue;
            string course_no = ddlCourseNo.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            entry_type = entry_type.Replace(" ", "_");
            string table_name = course_type + "_" + course_no + "_" + entry_type;
            SqlCommand cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(Personal_No varchar(10), Name varchar(50), Rank varchar(20));", con);
            cmd.ExecuteNonQuery();

            foreach (GridViewRow g1 in GridView1.Rows)
            {

                cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                cmd.ExecuteNonQuery();
                i++;
            }
            con.Close();

            string script = "alert(\" " + i + " Trainees Added to " + course_type + course_no + " " + entry_type + " \");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
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

        protected void updatecourselist()
        {

            con.Open();
            //string course = this.DropDownList1.SelectedIndex; GetItemText(this.DropDownList1.SelectedItem);
            int index = ddlCourseType.SelectedIndex;
            string selectQuery;
            SqlCommand cmd;
            if (index == 0)
                selectQuery = "select Course_No from Officer_Courses";
            else
                selectQuery = "select Course_No from Sailor_Courses";
            SqlCommand selectCommand = new SqlCommand(selectQuery, con);
            selectCommand.CommandType = CommandType.Text;
            SqlDataReader selectData;
            selectData = selectCommand.ExecuteReader();
            if (selectData.HasRows)
            {
                ddlCourseNo.DataSource = selectData;
                ddlCourseNo.DataValueField = "Course_No";
                ddlCourseNo.DataTextField = "Course_No";
                ddlCourseNo.DataBind();
            }
            else
            {
                //MessageBox.Show("No is found");
                //CloseConnection = new Connection();
                //CloseConnection.closeConnection(); // close the connection 
            }

            con.Close();
        }

        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();


            string name = ddlCourseType.SelectedValue;
            SqlCommand com = new SqlCommand("select Course_No from Sailor_Courses where Course_Name ='" + name + "'", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
            ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseNo.DataBind();

            name = ddlCourseType.SelectedValue + "_COURSE_TYPE";
            com = new SqlCommand("select * from " + name, con); // table name 
            da = new SqlDataAdapter(com);
            ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlEntryType.DataBind();


            con.Close();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            String name = ddlCourseType.SelectedValue + "_COURSE_TYPE";
            SqlCommand com = new SqlCommand("select * from" + name, con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlEntryType.DataBind();

        }
    }
}