
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
    public partial class UploadNominalRoll : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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

                string name = ddlCourseType.Items[0].Value;
                com = new SqlCommand("select Course_No from Sailor_Courses where Course_Name ='" + name + "'", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();

                name = ddlCourseType.Items[0].Value.Replace(" ","_") + "_ENTRY_TYPE";
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
            string table_name = "";
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            //int i = 0;
            try
            {
                int i = 0;
                string course_type = ddlCourseType.SelectedValue.Replace(" ", "_");
                course_type = course_type.Replace(" ", "_");
                string course_no = ddlCourseNo.SelectedValue;
                string entry_type = ddlEntryType.SelectedValue.Replace(" ", "_");
                entry_type = entry_type.Replace(" ", "_");
                table_name = course_type + "_" + course_no + "_" + entry_type;
                //Response.Write(course_type + "_" + entry_type + "_SUBJECTS");
                SqlCommand com = new SqlCommand("select Subject_Name, Term from " + course_type + "_" + entry_type + "_SUBJECTS", con,tran);
                SqlDataReader dr = com.ExecuteReader();
                List<string> column_List = new List<string>();

                string col_name;
                while (dr.Read())
                {
                    col_name = dr.GetValue(0).ToString().Replace(" ", "_");
                    //Response.Write(col_name + i);
                    i++;

                    column_List.Add(col_name);


                }

                dr.Close();
                string col_List = "";
                foreach (string col_nam in column_List)
                {
                    col_List = col_List + ", " + col_nam + " int DEFAULT 0";

                }
                //table_name = course_type +  + "_SENIORITY_CRITERIA";
                table_name = course_type + "_ENTRY_TYPE";
                com = new SqlCommand("select Term_Label from " + table_name + " where TYPE_NAME = '" + ddlEntryType.SelectedValue + "'", con,tran);
                dr = com.ExecuteReader();
                string term_label = "";
                string seniority = "";
                while (dr.Read())
                {
                    term_label = dr.GetValue(0).ToString();
                    //seniority = dr.GetValue(1).ToString();
                }
                dr.Close();
                table_name = "SAILOR_COURSE_TYPE";
                com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con,tran);
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    seniority = dr.GetValue(0).ToString();
                    //seniority = dr.GetValue(1).ToString();
                }
                dr.Close();

                if (seniority.Equals("1"))
                {
                    foreach (string term in term_label.Split('_'))
                    {
                        col_List = col_List + ", " + term + "_total int DEFAULT 0, " + term + "_percentage decimal(4,2) DEFAULT 0, " + term + "_seniority_gained decimal(4,2) DEFAULT 0, " + term + "_seniority_lost decimal(4,2) DEFAULT 0, " + term + "_seniority_total decimal(4,2) DEFAULT 0 ";
                    }
                }
                else
                {
                    foreach (string term in term_label.Split('_'))
                    {
                        col_List = col_List + ", " + term + "_total int DEFAULT 0, " + term + "_percentage decimal(4,2) DEFAULT 0 ";
                    }
                }
                //Response.Write(col_List);
                table_name = course_type + "_" + course_no + "_" + entry_type;
                string query = "If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(Personal_No varchar(10) PRIMARY KEY, Name varchar(50), Rank varchar(20)" + col_List + ")";
                SqlCommand cmd = new SqlCommand(query, con,tran);
                //Response.Write(query);
                cmd.ExecuteNonQuery();

                foreach (GridViewRow g1 in GridView1.Rows)
                {

                    cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "','" + g1.Cells[3].Text + "')", con,tran);
                    cmd.ExecuteNonQuery();
                    i++;
                }
                tran.Commit();
                con.Close();
                column_List.Clear();

                //string script = "alert(\" "+ i + " Trainees Added to " + course_type + course_no + " " +entry_type +" \");";
                //ScriptManager.RegisterStartupScript(this, GetType(),
                //                      "ServerControlScript", script, true)
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }

                    
                    reset();
                    Response.Write("<script>window.open ('AlreadyExisting.aspx?table_name=" + table_name + "','_blank');</script>");


                }
                else
                    Response.Write(ex);
            }
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
            SqlCommand com = new SqlCommand("select Course_No from Sailor_Courses where Course_Name ='"+ name + "'", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
            ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseNo.DataBind();

            name = ddlCourseType.SelectedValue.Replace(" ","_") + "_ENTRY_TYPE";
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
            String name = ddlCourseType.SelectedValue.Replace(" ","_") + "_ENTRY_TYPE";
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