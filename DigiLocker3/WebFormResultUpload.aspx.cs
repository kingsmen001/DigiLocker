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
    public partial class WebFormResultUpload : System.Web.UI.Page
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
                string entry_type;
                com = new SqlCommand("select Course_No from Sailor_Courses where Course_Name ='" + name + "'", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();

                string term = ddlTerm.SelectedValue;

                name = ddlCourseType.Items[0].Value + "_COURSE_TYPE";
                com = new SqlCommand("select * from " + name, con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();

                entry_type = ddlEntryType.SelectedValue;
                entry_type = entry_type.Replace(" ", "_");
                name = ddlCourseType.SelectedValue + "_" + entry_type + "_SUBJECT";
                com = new SqlCommand("select Subject_Name from " + name + " where Term ='" + term + "'", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                ddlSubject.DataValueField = ds.Tables[0].Columns["Subject_Name"].ToString();             // to retrive specific  textfield name 
                ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlSubject.DataBind();


                con.Close();
            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {

            con.Open();
            string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

            }
            conString = string.Format(conString, excelPath, "Yes");
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", excel_con))
                {
                    oda.Fill(dtExcelData);
                }
                excel_con.Close();
                SqlCommand cmd = new SqlCommand("Delete from tblPersons", con);
                cmd.ExecuteNonQuery();
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.tblPersons";

                    //[OPTIONAL]: Map the Excel columns with that of the database table
                    sqlBulkCopy.ColumnMappings.Add("Entry", "Entry");
                    sqlBulkCopy.ColumnMappings.Add("Personal_No", "Personal_No");
                    sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                    sqlBulkCopy.ColumnMappings.Add("Rank", "Rank");

                    sqlBulkCopy.WriteToServer(dtExcelData);

                }

                cmd = new SqlCommand("select * from tblPersons", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                con.Close();
                ConfirmButton.Visible = true;
                ConfirmButton.EnableViewState = true;
                // }
            }
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            Response.Write("Confirm Clicked");
            foreach (GridViewRow g1 in GridView1.Rows)
            {
                Response.Write(g1.Cells[3].Text + "    ");
                TextBox xyz = (TextBox)g1.Cells[4].FindControl("TextBox1");
                string abs = xyz.Text;
                Response.Write(abs + "    ");

            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            // reset();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        protected void ddlCourseNoIndexChanged(object sender, EventArgs e)
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
        protected void ddlTermIndexChanged(object sender, EventArgs e)
        {
        }


        protected void ddlEntryTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string term = ddlTerm.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            entry_type = entry_type.Replace(" ", "_");
            string name = ddlCourseType.SelectedValue + "_" + entry_type + "_SUBJECT";
            SqlCommand com = new SqlCommand("select Subject_Name from " + name + " where Term ='" + term + "'", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
            ddlSubject.DataValueField = ds.Tables[0].Columns["Subject_Name"].ToString();             // to retrive specific  textfield name 
            ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlSubject.DataBind();
            con.Close();
        }


    }
}