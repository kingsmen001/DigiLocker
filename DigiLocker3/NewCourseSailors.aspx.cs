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
using System.Text.RegularExpressions;

namespace DigiLocker3
{
    public partial class Terms : System.Web.UI.Page
    {
        string name;
        static int flag1;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["User_ID"] as string))
            {


                if (Session["Access_Level"].ToString().Equals("1"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = true;
                    opnUpdateMarks.Visible = true;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;
                    opnAddCourseOfficer.Visible = true;
                    opnAddTraineesOfficer.Visible = true;
                    opnCreateCourseOfficer.Visible = true;
                    opnUpdateMarksOfficer.Visible = true;
                    opnViewResultOfficer.Visible = true;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = true;
                }
                else if (Session["Access_Level"].ToString().Equals("2"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = true;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;

                    opnAddCourseOfficer.Visible = true;
                    opnAddTraineesOfficer.Visible = true;
                    opnCreateCourseOfficer.Visible = true;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = true;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = true;
                }
                else if (Session["Access_Level"].ToString().Equals("3"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = false;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = false;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = false;

                    opnAddCourseOfficer.Visible = true;
                    opnAddTraineesOfficer.Visible = true;
                    opnCreateCourseOfficer.Visible = false;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = false;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = false;
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                    opnAddCourse.Visible = false;
                    opnAddTrainees.Visible = false;
                    opnCreateCourse.Visible = false;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = false;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;

                    opnAddCourseOfficer.Visible = false;
                    opnAddTraineesOfficer.Visible = false;
                    opnCreateCourseOfficer.Visible = false;
                    opnUpdateMarksOfficer.Visible = false;
                    opnViewResultOfficer.Visible = false;
                    opnViewTraineesOfficer.Visible = true;
                    opnUploadMarksOfficer.Visible = true;
                }

            }
            else
            {
                opnAddCourseOfficer.Visible = false;
                opnAddTraineesOfficer.Visible = false;
                opnCreateCourseOfficer.Visible = false;
                opnUpdateMarksOfficer.Visible = false;
                opnViewResultOfficer.Visible = false;
                opnViewTraineesOfficer.Visible = true;
                opnUploadMarksOfficer.Visible = false;
                opnAddCourse.Visible = false;
                opnAddTrainees.Visible = false;
                opnCreateCourse.Visible = false;
                opnUpdateMarks.Visible = false;
                opnViewResult.Visible = false;
                opnViewTrainees.Visible = true;
                opnUploadMarks.Visible = false;
            }

            if (Request.QueryString["coursename"] != null)
            {
                heading.InnerHtml = Request.QueryString["coursename"];
                ddlCourseType.Visible = false;
                ddlCourseType.EnableViewState = false;
                labeltype.Visible = false;
                labeltype.EnableViewState = false;
                name = Request.QueryString["coursename"];


            }
            else
            {
                name = ddlCourseType.SelectedValue;
            }


            if (!this.IsPostBack)
            {
                flag1 = 0;
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
                ddlCourseType.Items.Insert(0, new ListItem("Select", "0"));

                if (Request.QueryString["coursename"] != null)
                {
                    heading.InnerHtml = Request.QueryString["coursename"];
                    ddlCourseType.Visible = false;
                    ddlCourseType.EnableViewState = false;
                    labeltype.Visible = false;
                    labeltype.EnableViewState = false;
                    name = Request.QueryString["coursename"];
                    div1.Visible = true;
                    string query = "select Type_Name from " + name.Replace(" ", "_") + "_ENTRY_TYPE";
                    com = new SqlCommand(query, con);
                    da = new SqlDataAdapter(com);
                    ds = new DataSet();
                    da.Fill(ds);
                    GridView1.DataSource = ds;
                    GridView1.DataBind();

                }
                else
                {
                    name = ddlCourseType.SelectedValue;
                    if (ddlCourseType.SelectedItem.Text.Equals("Select"))
                    {
                        div1.Visible = false;
                    }
                    else {
                        div1.Visible = true;
                        List<string> termList = new List<string>();
                        string query = "select Type_Name from " + name.Replace(" ", "_") + "_ENTRY_TYPE";
                        com = new SqlCommand(query, con);
                        da = new SqlDataAdapter(com);
                        ds = new DataSet();
                        da.Fill(ds);
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                    }
                }


                con.Close();


            }
        }
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (flag1 == 0)
            {
                string script = "alert(\" No entry is Selected to enroll. \");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
            }
            else {
                int flag = 0;
                foreach (GridViewRow g1 in GridView1.Rows)
                {
                    CheckBox chkBox = (CheckBox)g1.Cells[0].FindControl("ChkBox1");
                    TextBox txtBox = (TextBox)g1.Cells[2].FindControl("TextBox1");
                    if (string.IsNullOrWhiteSpace(txtBox.Text) & chkBox.Checked)
                    {

                        flag = 1;
                        break;
                    }
                }
                if (string.IsNullOrWhiteSpace(Course_Number_TextBox.Text))
                {
                    string script = "alert(\" Enter Course Number \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    Course_Number_TextBox.Text = "";
                }
                else if (flag == 1)
                {
                    string script = "alert(\" Enter Course Number for each Selected Entry \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);

                }
                else if (!System.Text.RegularExpressions.Regex.IsMatch(Course_Number_TextBox.Text, "[^a-zA-Z.0-9\x20]", System.Text.RegularExpressions.RegexOptions.IgnoreCase) & Course_Number_TextBox.Text[0] != ' ' & Course_Number_TextBox.Text[0] != '\t')
                {

                    try
                    {
                        //string filename = Path.GetFileName(FileUploadControl.FileName);
                        //FileUploadControl.SaveAs(Server.MapPath("~/Records/MEAT19/NominalRolls/") + filename);

                        con.Open();
                        //string course = this.DropDownList1.SelectedIndex; GetItemText(this.DropDownList1.SelectedItem);
                        if (name == null)
                        {
                            name = ddlCourseType.SelectedValue;
                        }
                        string insertQuery;
                        SqlCommand cmd;
                        insertQuery = "insert into Sailor_Course(Course_No, Course_Name, StartDate, EndDate)values (@Course_No, @Course_Name, @StartDate, @EndDate)";
                        cmd = new SqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@Course_No", Course_Number_TextBox.Text);
                        cmd.Parameters.AddWithValue("@Course_Name", name);
                        cmd.Parameters.AddWithValue("@StartDate", TextBoxStart.Text);
                        cmd.Parameters.AddWithValue("@EndDate", TextBoxEnd.Text);
                        cmd.ExecuteNonQuery();
                        string query = "Select Type_Name from SAILOR_COURSE_TYPE";
                        //cmd = new SqlCommand(query, con);
                        //SqlDataReader dr = cmd.ExecuteReader();
                        //List<string> entryList = new List<string>();
                        //while(dr.Read())
                        //{
                        //    entryList.Add(dr.GetString(0));
                        //}
                        //dr.Close();
                        //foreach(string entry in entryList)
                        //{
                        //    query = "Select * into " + name.Replace(" ", "_") + "_" + Course_Number_TextBox.Text + "_" + entry.Replace(" ", "_") + "_SUBJECTS from " + name.Replace(" ", "_") + "_" + entry.Replace(" ", "_") + "_SUBJECTS" ;
                        //    cmd = new SqlCommand(query, con);
                        //    cmd.ExecuteNonQuery();
                        //}
                        query = "Create table " + name.Replace(" ", "_") + "_" + Course_Number_TextBox.Text.Replace(".", string.Empty) + "_ENTRY_TYPE ( TYPE_NAME VARCHAR(50), NUMBER VARCHAR(50), ENROLLEDIN VARCHAR(50) DEFAULT NULL)";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        foreach (GridViewRow g1 in GridView1.Rows)
                        {
                            CheckBox chkBox = (CheckBox)g1.Cells[0].FindControl("ChkBox1");
                            TextBox txtBox = (TextBox)g1.Cells[2].FindControl("TextBox1");
                            if (!string.IsNullOrWhiteSpace(txtBox.Text) & chkBox.Checked)
                            {
                                query = "insert into " + name.Replace(" ", "_") + "_" + Course_Number_TextBox.Text.Replace(".", string.Empty) + "_ENTRY_TYPE(TYPE_NAME, NUMBER)  values( '" + g1.Cells[1].Text + "', '" + txtBox.Text + "')";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        query = "Create table " + name.Replace(" ", "_") + "_" + Course_Number_TextBox.Text.Replace(".", string.Empty) + " (Personal_No VARCHAR(50), ENTRY_NAME VARCHAR(50), TERM VARCHAR(50) DEFAULT ' ')";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        //Response.Write("Record Uploaded Successfully!!!thank you");
                        Response.Redirect("UploadNominalRollSailors.aspx?coursename=" + name + "&courseno=" + Course_Number_TextBox.Text);
                        string script = "alert(\" Course Enrolled Succefullly \");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);

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
                else {
                    string script = "alert(\" Only AlphaNumeric Characters are Allowed. Name Cannot start with Space \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    Course_Number_TextBox.Text = "";
                }
            }


        }

        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox Pri = (TextBox)row.FindControl("TextBox1");
            Regex regex = new Regex("^\\d\\d[.]\\d\\d\\d\\d*$");
            if (!regex.IsMatch(Pri.Text))
            {
                Response.Write("<script language=javascript>alert('Invalid Format for Course Number');</script>");
            }
        }

        protected void CheckBoxChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            CheckBox chkBox = (CheckBox)row.FindControl("ChkBox1");
            TextBox Pri = (TextBox)row.FindControl("TextBox1");
            if (chkBox.Checked == true)
            {
                Pri.Enabled = true;
                flag1 = flag1 + 1;
                //Response.Write("<script language=javascript>alert('Checked');</script>");
            }
            else
            {
                Pri.Enabled = false;
                flag1 = flag1 - 1;
                //Response.Write("<script language=javascript>alert('Unchecked');</script>");

            }
        }

        protected void Text1Changed(object sender, EventArgs e)
        {

            Regex regex1 = new Regex("^\\d\\d[.]\\d\\d\\d\\d*$");
            Regex regex2 = new Regex("^\\d\\d*$");
            if (!regex1.IsMatch(Course_Number_TextBox.Text) & !regex2.IsMatch(Course_Number_TextBox.Text))
            {
                Response.Write("<script language=javascript>alert('Invalid Format for Course Number');</script>");
            }
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("0"));
            if (ddlCourseType.SelectedItem.Text.Equals("Select"))
            {
                div1.Visible = false;
            }
            else {
                div1.Visible = true;
                con.Open();
                string query = "select Type_Name from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_ENTRY_TYPE";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();

                con.Close();
            }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx", false);
        }
    }
}