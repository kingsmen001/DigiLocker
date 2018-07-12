using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;

namespace DigiLocker3
{
    public partial class Terms : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            { 
                con.Open();
                SqlCommand com = new SqlCommand("select CONCAT(COURSE_NAME, ' ', COURSE_NO) AS TYPE_NAME from SAILOR_COURSE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                DropDownList1.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                DropDownList1.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                DropDownList1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                DropDownList1.DataBind();
                con.Close();

                con.Open();

                com = new SqlCommand("select * from SAILOR_COURSE_TYPE", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();
                con.Close();
            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Course_Number_TextBox.Text))
            {
                string script = "alert(\" Enter Course Number \");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                Course_Number_TextBox.Text = "";
            }
            else
            {
                try
                {
                    //string filename = Path.GetFileName(FileUploadControl.FileName);
                    //FileUploadControl.SaveAs(Server.MapPath("~/Records/MEAT19/NominalRolls/") + filename);

                    con.Open();
                    //string course = this.DropDownList1.SelectedIndex; GetItemText(this.DropDownList1.SelectedItem);
                    string name = ddlCourseType.SelectedValue;
                    string insertQuery;
                    SqlCommand cmd;
                    insertQuery = "insert into Sailor_Course(Course_No, Course_Name)values (@Course_No, @Course_Name)";
                    cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@Course_No", Course_Number_TextBox.Text);
                    cmd.Parameters.AddWithValue("@Course_Name", name);
                    cmd.ExecuteNonQuery();
                    //Response.Write("Record Uploaded Successfully!!!thank you");
                    Response.Redirect("UploadNominalRollSailors.aspx?coursename=" + name);

                    con.Close();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        string script = "alert(\" Course Already Exists \");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);
                        Course_Number_TextBox.Text = "";
                    }
                    else Response.Write(ex);
                }
            }

            
        }

        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {

        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}