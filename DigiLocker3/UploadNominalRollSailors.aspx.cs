
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

        string coursename ;
        string courseno ;
        string table_name = "";
        int flag;
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["coursename"] != null)
            {
                heading.InnerHtml = Request.QueryString["coursename"] + Request.QueryString["courseno"] + "(Add Trainees)";
                ddlCourseType.Visible = false;
                ddlCourseType.EnableViewState = false;
                ddlCourseNo.Visible = false;
                ddlCourseNo.EnableViewState = false;
                labeltype.Visible = false;
                labeltype.EnableViewState = false;
                labelnumber.Visible = false;
                labelnumber.EnableViewState = false;
                coursename = Request.QueryString["coursename"];
                courseno = Request.QueryString["courseno"];
            }

            
            if (!this.IsPostBack)
            {
                flag = 0;

                con.Open();
                
                //ddlEntryType.SelectedIndex = 0;
                //ddlCourseNo.SelectedIndex = 0;

                SqlCommand com = new SqlCommand("select *from SAILOR_COURSE_TYPE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();

                string name = ddlCourseType.SelectedValue.Replace(" ", "_");
                com = new SqlCommand("select Course_No from Sailor_Course where Course_Name ='" + ddlCourseType.SelectedValue + "'", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();
                if (coursename == null & courseno == null)
                {
                    coursename = ddlCourseType.Items[0].Text;
                    courseno = ddlCourseNo.Items[0].Text;
                }

                if (coursename == null)
                    coursename = ddlCourseType.SelectedValue.Replace(" ", "_") ;
                com = new SqlCommand("select * from " + coursename.Replace(" ","_") + "_ENTRY_TYPE", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();

                
                table_name = coursename.Replace(" ", "_") + "_" + courseno.Replace(" ", "_") + "_" + ddlEntryType.SelectedValue.Replace(" ", "_");
                string query = "If exists(select name from sysobjects where name = '" + table_name + "') Select Personal_no, Name, Rank from " + table_name;
                //Response.Write(query);
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rbtnind.Checked = true;
                    rbtnmulti.Checked = false;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                    flag = 1;
                    txtName.Enabled = true;
                    txtRank.Enabled = true;
                    txtNo.Enabled = true;
                    FileUpload1.Enabled = false;
                    

                }
                else
                {
                    rbtnind.Checked = false;
                    rbtnmulti.Checked = true;
                    flag = 0;
                    FileUpload1.Enabled = true;
                    txtName.Enabled = false;
                    txtRank.Enabled = false;
                    txtNo.Enabled = false;


                }


                con.Close();
            }
            showData();
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (coursename == null & courseno == null)
            {
                coursename = ddlCourseType.SelectedValue;
                courseno = ddlCourseNo.SelectedValue;
            }
            if (rbtnind.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtNo.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtRank.Text))
                {
                    Response.Write("<script language='javascript'>alert('Please fill all Values');</script>");
                }
                else
                {
                    con.Open();
                    createTable();
                    table_name = coursename.Replace(" ", "_") + "_" + courseno.Replace(" ", "_") + "_" + ddlEntryType.SelectedValue.Replace(" ", "_");
                    try
                    {
                        int i = 0;

                        table_name = coursename.Replace(" ", "_") + "_" + courseno.Replace(" ", "_") + "_" + ddlEntryType.SelectedValue.Replace(" ", "_");


                        SqlCommand cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + txtNo.Text + "','" + txtName.Text + "','" + txtRank.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        i++;

                        con.Close();


                        //string script = "alert(\" "+ i + " Trainees Added to " + course_type + course_no + " " +entry_type +" \");";
                        //ScriptManager.RegisterStartupScript(this, GetType(),
                        //                      "ServerControlScript", script, true)
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {


                            reset();
                            Response.Write("<script language='javascript'>alert('Record Already Exists');</script>");


                        }
                        else
                            Response.Write(ex);
                    }
                    con.Close();
                    showData();
                }
            }
            else
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
                else
                {
                    Response.Write("<script language='javascript'>alert('Please Select a File');</script>");
                }
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
            if (coursename == null & courseno == null)
            {
                coursename = ddlCourseType.SelectedValue;
                courseno = ddlCourseNo.SelectedValue;
            }
            string table_name = "";
            createTable();
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            //int i = 0;
            try
            {
                int i = 0;

                table_name = coursename.Replace(" ", "_") + "_" + courseno.Replace(" ", "_") + "_" + ddlEntryType.SelectedValue.Replace(" ", "_");
                foreach (GridViewRow g1 in GridView1.Rows)
                {

                    SqlCommand cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "','" + g1.Cells[3].Text + "')", con, tran);
                    cmd.ExecuteNonQuery();
                    i++;
                }
                tran.Commit();

                con.Close();
                reset();
                Response.Write("<script language='javascript'>alert('Trainees Added');</script>");

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
                    Response.Write("<script language='javascript'>alert('Record Already Exists. Please Check');</script>");


                }
                else
                    Response.Write(ex);
            }
            showData();
        }

        protected void createTable()
        {
            if (coursename == null & courseno == null)
            {
                coursename = ddlCourseType.SelectedValue;
                courseno = ddlCourseNo.SelectedValue;
            }
            string table_name = "";
            //string course_type = ddlCourseType.SelectedValue.Replace(" ", "_");
            //course_type = course_type.Replace(" ", "_");
            //string course_no = ddlCourseNo.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue.Replace(" ", "_");
            entry_type = entry_type.Replace(" ", "_");
            table_name = coursename.Replace(" ","_") + "_" + courseno + "_" + entry_type;
            con.Open();
            string query = "If not exists(select name from sysobjects where name = '" + coursename.Replace(" ","_") + "_" + courseno + "_" + entry_type + "_SUBJECTS') Select * into " + coursename.Replace(" ","_") + "_" + courseno + "_" + entry_type + "_SUBJECTS from " +coursename.Replace(" ","_") + "_" +entry_type + "_SUBJECTS";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            
            query = "IF NOT EXISTS(SELECT TYPE_NAME FROM " + coursename.Replace(" ","_") + "_" + courseno + "_ENTRY_TYPE WHERE TYPE_NAME='" + entry_type.Replace("_", " ") + "')insert into " + coursename.Replace(" ","_") + "_" + courseno + "_ENTRY_TYPE (TYPE_NAME) values('" + entry_type.Replace("_"," ") + "')";
            cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            SqlCommand com = new SqlCommand("select Subject_Name, Term from " + coursename.Replace(" ","_") +"_" + courseno.Replace(" ","_") + "_" + entry_type + "_SUBJECTS", con);
            SqlDataReader dr = com.ExecuteReader();
            List<string> column_List = new List<string>();

            string col_name;
            while (dr.Read())
            {
                col_name = dr.GetValue(0).ToString().Replace(" ", "_");
                //Response.Write(col_name + i);


                column_List.Add(col_name);


            }

            dr.Close();
            string col_List = "";
            foreach (string col_nam in column_List)
            {
                col_List = col_List + ", " + col_nam + " int DEFAULT 0";

            }
            //table_name = course_type +  + "_SENIORITY_CRITERIA";
            table_name = coursename.Replace(" ","_") + "_ENTRY_TYPE";
            com = new SqlCommand("select Term_Label from " + table_name + " where TYPE_NAME = '" + ddlEntryType.SelectedValue + "'", con);
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
            com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + coursename + "'", con);
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
                    col_List = col_List + ", " + term + "_total int DEFAULT 0, " + term + "_percentage decimal(4,2) DEFAULT 0, " + term + "_seniority_gained decimal(4,2) DEFAULT 0, " + term + "_seniority_lost decimal(4,2) DEFAULT 0, " + term + "_seniority_total decimal(4,2) DEFAULT 0 " ;
                }
                col_List = col_List + ", total_seniority_gained decimal(4,2) DEFAULT 0, total_seniority_lost decimal(4,2) DEFAULT 0, total_seniority decimal(4,2) DEFAULT 0 ";
            }
            else
            {
                foreach (string term in term_label.Split('_'))
                {
                    col_List = col_List + ", " + term + "_total int DEFAULT 0, " + term + "_percentage decimal(4,2) DEFAULT 0 ";
                }
            }
            foreach (string term in term_label.Split('_'))
            {
                col_List = col_List + ", " + term + "_Failed int DEFAULT 0, " + term + "_Qualified varchar(15) default 'NO'";
            }
            col_List = col_List + ", Total_Marks int DEFAULT 0, TOTAL_Percentage decimal(4,2) DEFAULT 0, " + "QUALIFIED varchar(15) default 'NO'";
            //Response.Write(col_List);
            table_name = coursename.Replace(" ","_") + "_" + courseno + "_" + entry_type;
            query = "If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(Personal_No varchar(10) PRIMARY KEY, Name varchar(50), Rank varchar(20)" + col_List + ")";
            cmd = new SqlCommand(query, con);
            //Response.Write(query);
            cmd.ExecuteNonQuery();
            column_List.Clear();
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

        protected void showData()
        {
            if (coursename == null & courseno == null)
            {
                coursename = ddlCourseType.SelectedValue;
                courseno = ddlCourseNo.SelectedValue;
            }
            con.Open();
            table_name = coursename.Replace(" ", "_") + "_" + courseno + "_" + ddlEntryType.SelectedValue.Replace(" ", "_");
            //Response.Write(table_name);
            string query = "If exists(select name from sysobjects where name = '" + table_name + "') Select Personal_no, Name, Rank from " + table_name;
            //Response.Write(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                rbtnind.Checked = true;
                rbtnmulti.Checked = false;
                GridView2.DataSource = dt;
                GridView2.DataBind();
                flag = 1;

            }
            else
            {
                rbtnind.Checked = false;
                rbtnmulti.Checked = true;
                GridView2.DataSource = dt;
                GridView2.DataBind();
                flag = 0;

            }
            if (rbtnmulti.Checked)
            {
                FileUpload1.Enabled = true;
                txtName.Enabled = false;
                txtRank.Enabled = false;
                txtNo.Enabled = false;
            }

            if (rbtnind.Checked)
            {
                txtName.Enabled = true;
                txtRank.Enabled = true;
                txtNo.Enabled = true;
                FileUpload1.Enabled = false;
            }


            con.Close();
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
                selectQuery = "select Course_No from Sailor_Course";
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
            showData();
        }

        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();


            string name = ddlCourseType.SelectedValue;
            SqlCommand com = new SqlCommand("select Course_No from Sailor_Course where Course_Name ='" + name + "'", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
            ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseNo.DataBind();

            name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_ENTRY_TYPE";
            com = new SqlCommand("select * from " + name, con); // table name 
            da = new SqlDataAdapter(com);
            ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlEntryType.DataBind();
            con.Close();
            showData();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            showData();

        }
        protected void input_CheckedChanged(Object sender, EventArgs e)
        {
            if (rbtnmulti.Checked)
            {
                FileUpload1.Enabled = true;
                txtName.Enabled = false;
                txtRank.Enabled = false;
                txtNo.Enabled = false;
            }

            if (rbtnind.Checked)
            {
                txtName.Enabled = true;
                txtRank.Enabled = true;
                txtNo.Enabled = true;
                FileUpload1.Enabled = false;
            }
        }
    }
}