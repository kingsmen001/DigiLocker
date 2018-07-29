using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigiLocker3
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["User_ID"] as string))
            {

                list_login.Visible = false;
                list_pass.Visible = false;
                list_userid.Visible = false;
                list_logout.Visible = true;
                enrol.Visible = true;
                create.Visible = true;
                if (Session["Access_Level"].ToString().Equals("1"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = true;
                    opnUpdateMarks.Visible = true;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;
                    enrol.Visible = true;
                    Div1.Visible = true;
                    A1.Visible = true;
                    create.Visible = true;
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
                    enrol.Visible = true;
                    Div1.Visible = true;
                    A1.Visible = true;
                    create.Visible = true;
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
                    enrol.Visible = true;
                    Div1.Visible = true;
                    A1.Visible = true;
                    create.Visible = false;
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
                    opnAddCourse.Visible = false;
                    opnAddTrainees.Visible = false;
                    opnCreateCourse.Visible = false;
                    opnUpdateMarks.Visible = false;
                    opnViewResult.Visible = false;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;
                    enrol.Visible = false;
                    Div1.Visible = false;
                    A1.Visible = false;
                    create.Visible = false;
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
                list_login.Visible = true;
                list_pass.Visible = true;
                list_userid.Visible = true;
                list_logout.Visible = false;
                opnAddCourse.Visible = false;
                opnAddTrainees.Visible = false;
                opnCreateCourse.Visible = false;
                opnUpdateMarks.Visible = false;
                opnViewResult.Visible = false;
                opnViewTrainees.Visible = true;
                opnUploadMarks.Visible = false;
                enrol.Visible = false;
                create.Visible = false;
                Div1.Visible = false;
                A1.Visible = false;
            }
        }

        public string getCourseDetails()
        {
            string data = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            con.Open();
            string sql = "SELECT TYPE_NAME from SAILOR_COURSE_TYPE";
            SqlCommand cmd = new SqlCommand(sql, con);
            int i = 0;
            using (SqlDataReader sqlRdr = cmd.ExecuteReader())
            {
                // table = new DataTable();  
                // table.Load(reader);  
                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        //int DocID = sqlRdr.GetInt32(0);
                        string CourseName = sqlRdr.GetString(0);
                        i++;
                        data += "<tr><td><a href=\"CourseDetailsSailors.aspx?coursename=" + CourseName + "\" >" + CourseName + "</a></td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><a href=\"#\" runat=\"server\" onServerClick=\"MyFuncion_Click\" >" + DocName + "</a></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><asp:LinkButton id=\"myid\" runat=\"server\" OnClick=\"MyFunction_Click\" >" + DocName + "</asp:LinkButton></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                    }
                }
            }
            return data;
        }

        public string getOfficerCourseDetails()
        {
            string data = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            con.Open();
            string sql = "SELECT TYPE_NAME from OFFICER_COURSE_TYPE";
            SqlCommand cmd = new SqlCommand(sql, con);
            int i = 0;
            using (SqlDataReader sqlRdr = cmd.ExecuteReader())
            {
                // table = new DataTable();  
                // table.Load(reader);  
                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        //int DocID = sqlRdr.GetInt32(0);
                        string CourseName = sqlRdr.GetString(0);
                        i++;
                        data += "<tr><td><a href=\"CourseDetailsOfficers.aspx?coursename=" + CourseName + "\" >" + CourseName + "</a></td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><a href=\"#\" runat=\"server\" onServerClick=\"MyFuncion_Click\" >" + DocName + "</a></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><asp:LinkButton id=\"myid\" runat=\"server\" OnClick=\"MyFunction_Click\" >" + DocName + "</asp:LinkButton></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                    }
                }
            }
            return data;
        }

        public string getEnrolledCourseDetails()
        {
            string data = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            con.Open();
            string sql = "SELECT COURSE_NAME, COURSE_NO from SAILOR_COURSES";
            SqlCommand cmd = new SqlCommand(sql, con);
            int i = 0;
            using (SqlDataReader sqlRdr = cmd.ExecuteReader())
            {
                // table = new DataTable();  
                // table.Load(reader);  
                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        //int DocID = sqlRdr.GetInt32(0);
                        string CourseName = sqlRdr.GetString(0);
                        string CourseNo = sqlRdr.GetString(1);
                        i++;
                        data += "<tr><td>" + i + "</td><td><a href=\"CourseDetailsSailors.aspx?id=" + CourseName.Replace(" ", "_") + CourseNo + "\">" + CourseName + " " + CourseNo + "</a></td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><a href=\"#\" runat=\"server\" onServerClick=\"MyFuncion_Click\" >" + DocName + "</a></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><asp:LinkButton id=\"myid\" runat=\"server\" OnClick=\"MyFunction_Click\" >" + DocName + "</asp:LinkButton></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                    }
                }
            }
            return data;
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                con.Open();
                string sql = "SELECT * from Users where user_no = @personalno and password = @password";
                
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@personalno", UserId_TextBox.Text);
                cmd.Parameters.AddWithValue("password", Password_TextBox.Text);
                SqlDataReader sqlRdr = cmd.ExecuteReader();
                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        Session.Add("User_ID", sqlRdr.GetString(1));
                        Session.Add("Access_Level", sqlRdr.GetInt32(3));
                        Response.Redirect("Home.aspx",false);
                        //Response.Write("<script language='javascript'>alert('" + Session["User_ID"].ToString() + "');</script>");
                    }
                }
                else
                {
                    {

                        //Response.Redirect("#");
                        Response.Write("<script language='javascript'>alert('Wrong Username or Password');</script>");
                    }
                }
                sqlRdr.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx",false);
        }
    }
}