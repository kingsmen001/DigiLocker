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
            if (!this.IsPostBack)
            {
                con.Open();
                        
                SqlCommand com = new SqlCommand("select * from SAILOR_COURSE_TYPE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();



                string name = ddlCourseType.Items[0].Value + "_ENTRY_TYPE";
                com = new SqlCommand("select * from " + name, con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                lbEntryType.DataBind();

                com = new SqlCommand("select *from MEAT_ENTRY_TYPE", con);
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
                foreach (string table_nam in typeList)
                {
                    //cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_nam + "') CREATE TABLE " + table_nam + "(Subject_Name varchar(10), Max_Marks int);", con);
                    //cmd = new SqlCommand("Alter Table " + table_nam +" Add Subject_Code varchar(10)", con);
                    //cmd.ExecuteNonQuery();
                    //cmd = new SqlCommand("Alter Table " + table_nam + " Add Term varchar(5)", con);
                    //cmd.ExecuteNonQuery();
                }
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
            string course_type = ddlCourseType.SelectedValue;
            course_type = course_type.Replace(" ","_");
            List<string> entry_list = new List<string>();
            

            for (int i = 0; i < lbEntryType.Items.Count; i++)
            {
                if (lbEntryType.Items[i].Selected)
                {
                    string entry_type = lbEntryType.Items[i].Text;
                    string table_name = course_type + "_" + entry_type + "_" + "SUBJECTS";
                    foreach (GridViewRow g1 in GridView1.Rows)
                    {

                        SqlCommand cmd = new SqlCommand("insert into " + table_name + "(Subject_Name, Max_Marks, Term) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + ddlTerm.SelectedValue + "')", con);
                        cmd.ExecuteNonQuery();


                    }
                    //Response.Write(selectedItem + "   ");
                    //insert command
                }
            }

            
            con.Close();

            //string script = "alert(\" " + i + " Trainees Added to " + course_type + entry_type + " \");";
            //ScriptManager.RegisterStartupScript(this, GetType(),
            //                      "ServerControlScript", script, true);
        }

        protected void lblEntryTypeIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(lbEntryType.SelectedItem.ToString());
           // Response.Write(lbEntryType.SelectedValue);
           // string script = "alert(\" " + " Trainees Added to " + lbEntryType.SelectedItem.ToString() + " \");";
           //ScriptManager.RegisterStartupScript(this, GetType(),
           //                       "ServerControlScript", script, true);
            con.Open();



            string term_Label = "______________";
            string table_name = ddlCourseType.SelectedValue.Replace(" ","_") + "_ENTRY_TYPE";
            for (int i = 0; i < lbEntryType.Items.Count; i++)
            {
                if (lbEntryType.Items[i].Selected)
                {
                    string new_term_Label = " ";
                    string entry_name = lbEntryType.Items[i].Text;
                    SqlCommand com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry_name + "'", con); // table name 
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                           new_term_Label = dr[0].ToString();
                        }
                    }
                    if((new_term_Label.Split('_').Length - 1)<((term_Label.Split('_').Length - 1)))
                    {
                        term_Label = new_term_Label;
                    }
                    //Response.Write(selectedItem + "   ");
                    //insert command
                }
            }
            //string entry_name = lbEntryType.SelectedValue;
            /*SqlCommand com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry_name +"'"   , con); // table name 
            using (SqlDataReader dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    term_Label = dr[0].ToString();
                }
            }*/
            ddlTerm.DataSource = term_Label.Split('_');
            ddlTerm.DataBind();
            //DataSet ds = new DataSet();
            //da.Fill(ds);  // fill dataset
            //lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            //lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            //lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            //lbEntryType.DataBind();


            con.Close();
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