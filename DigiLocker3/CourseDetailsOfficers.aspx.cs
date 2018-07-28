using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace DigiLocker3
{
    public partial class CourseDetailsOfficers : System.Web.UI.Page
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["User_ID"] as string))
            {

                list_login.Visible = false;
                list_pass.Visible = false;
                list_userid.Visible = false;
                list_logout.Visible = true;
                enrol.Visible = true;
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
            }

            heading.InnerHtml = Request.QueryString["coursename"];
            con.Open();
            string course_name = Request.QueryString["coursename"].Replace(" ", "_");
            string table_name = course_name + "_ENTRY_TYPE";
            string query = "select TYPE_NAME as Entry, Replace(TERM_LABEL, '_', ',') as Term from " + table_name;
            //Response.Write(query);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }

        protected string MyMethodCall()
        {
            string data = "";
            con.Open();
            string course_name = Request.QueryString["coursename"];
            string table_name = course_name.Replace(" ", "_") + "_ENTRY_TYPE";
            string query = "select * from " + table_name;
            //string entry_name1 = "APP";
            //string query = "select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry_name1 + "'";
            //Response.Write(query);
            List<string> entry_name_list = new List<string>();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            //string data = "";
            string term_label = "";
            string abc = "";
            string def = "";
            string pqr = "";
            string list = "";
            string select = "";
            entry_name_list.Clear();
            while (dr.Read())
            {
                entry_name_list.Add(dr.GetString(0));
                pqr = pqr + dr.GetString(0);
            }
            dr.Close();
            foreach (string entry in entry_name_list)
            {
                table_name = course_name.Replace(" ", "_") + "_ENTRY_TYPE";
                query = "select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry + "'";
                com = new SqlCommand(query, con);
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    term_label = dr.GetString(0);

                }
                dr.Close();
                data = data + "<div><label class=\"button button3  btn btn-primary\" style=\"width:100%; background-color:#e0e0e0; color:black\">" + entry + "</label><div><table Class=\"table table-striped table-bordered table-hover columnscss\"><tr><th>Term</th><th>Subjects</th></tr>";
                foreach (string term in term_label.Split('_'))
                {
                    data = data + "<tr><td>" + term + "</td><td ><table Class=\"table table-striped table-bordered table-hover columnscss\">";
                    table_name = course_name.Replace(" ", "_") + "_" + entry.Replace(" ", "_") + "_SUBJECTS";
                    query = "select SUBJECT_NAME from " + table_name + " where TERM = '" + term + "'";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader dk = cmd.ExecuteReader();
                    while (dk.Read())
                    {
                        string Subject_name = dk.GetString(0);
                        data = data + "<tr><td>" + Subject_name + "</td></tr>";

                    }
                    dk.Close();
                    data = data + "</table></td></tr>";

                }
                data = data + "</table></div></div>";
            }


            con.Close();
            Response.Write(abc);
            //Response.Write("<script language='javascript'>alert('" + pqr + "==" +list + "==" + select + "==" + def + "==" + abc + "');</script>");

            return data;
            //return "";
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
                        Response.Redirect("CourseDetails.aspx?coursename=" + Request.QueryString["coursename"], false);
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
            Response.Redirect("CourseDetails.aspx?coursename=" + Request.QueryString["coursename"], false);
            //Response.Redirect("Home.aspx", true);
        }
    }
}