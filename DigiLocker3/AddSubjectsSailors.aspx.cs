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
    public partial class AddSubjectsSailors : System.Web.UI.Page
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

            

            string name = ddlCourseType.Items[0].Value + "_COURSE_TYPE";
            com = new SqlCommand("select * from " + name, con); // table name 
            da = new SqlDataAdapter(com);
            ds = new DataSet();
            da.Fill(ds);  // fill dataset
            lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            lbEntryType.DataBind();

            com = new SqlCommand("select *from MEAT_COURSE_TYPE", con);
            SqlDataReader dr = com.ExecuteReader();
            List<string> typeList = new List<string>();
            SqlCommand cmd;
            string table_name;
            while (dr.Read())
            {
                name = dr.GetValue(0).ToString();
                name = name.Replace(" ", "_");
                table_name = "MEAT_" + name + "_SUBJECT";
                typeList.Add(table_name);
                
                            
            }

            dr.Close();
            foreach(string table_nam in typeList)
                {
                //cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_nam + "') CREATE TABLE " + table_nam + "(Subject_Name varchar(10), Max_Marks int);", con);
                //cmd = new SqlCommand("Alter Table " + table_nam +" Add Subject_Code varchar(10)", con);
                //cmd.ExecuteNonQuery();
                //cmd = new SqlCommand("Alter Table " + table_nam + " Add Term varchar(5)", con);
                //cmd.ExecuteNonQuery();
            }
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
            List<string> entry_list = new List<string>();
            foreach (ListItem item in lbEntryType.Items)
            {
                entry_list.Add(item.Text);
            }
            foreach (string type in entry_list)
            {
                string entry_type = type;
                entry_type = entry_type.Replace(" ", "_");
                string table_name = course_type + "_" + entry_type + "_SUBJECT";
                int j = 100;
                foreach (GridViewRow g1 in GridView1.Rows)
                {

                    j++;
                    SqlCommand cmd = new SqlCommand("insert into " + table_name + "(Subject_Name, Max_Marks, Subject_Code, Term) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + j + "','" + ddlTerm.SelectedValue + "')", con);
                    cmd.ExecuteNonQuery();


                }
            }
            entry_list.Clear();
            con.Close();

            //string script = "alert(\" " + i + " Trainees Added to " + course_type + entry_type + " \");";
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

       

        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();


            

            string name = ddlCourseType.SelectedValue + "_COURSE_TYPE";
            SqlCommand com = new SqlCommand("select * from " + name, con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            lbEntryType.DataBind();


            con.Close();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            String name = ddlCourseType.SelectedValue + "_COURSE_TYPE";
            SqlCommand com = new SqlCommand("select * from" + name, con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            lbEntryType.DataBind();

        }
    }
}