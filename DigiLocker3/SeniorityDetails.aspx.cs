﻿using System;
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
    public partial class SeniorityDetails : System.Web.UI.Page
    {
        string coursename = "";
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            coursename = Request.QueryString["coursename"];
            if (!this.IsPostBack)
            {
                con.Open();
                string table_name = coursename.Replace(" ", "_") + "_ENTRY_TYPE";
                List<string> termLabel = new List<string>();
                //string term_Label = "";
                SqlCommand com = new SqlCommand("select TERM_LABEL from " + table_name, con); // table name 
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
            }
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
            //int i = 0;
            //string course_type = ddlCourseType.SelectedValue;
            //course_type = course_type.Replace(" ", "_");
            //string table_name = course_type + "_SENIORITY";
            //SqlCommand cmd;
            //cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(lower_lmt decimal(2,2), upper_lmt decimal(2,2), seniority decimal(2,2));", con);
            //cmd.ExecuteNonQuery();
            //string query;

            //foreach (GridViewRow g1 in GridView1.Rows)
            //{

            //    cmd = new SqlCommand("insert into " + table_name + "(lower_lmt, upper_lmt, seniority) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
            //    cmd = new SqlCommand(query, con);
            //    //string Marks = (g1.FindControl("txtMarks") as TextBox).Text;
            //    //Response.Write(Marks+"      ");
            //    cmd.ExecuteNonQuery();

            //    i++;
            //}
            con.Close();

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

    }
}