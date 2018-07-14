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
                        data += "<tr><td><a href=\"CourseDetails.aspx?coursename=" + CourseName + "\" >" + CourseName + "</a></td></tr>";
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
                        data += "<tr><td>" + i + "</td><td><a href=\"EnrolledCourseDetails.aspx?id=" + CourseName.Replace(" ", "_") + CourseNo + "\">" + CourseName +" " + CourseNo + "</a></td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><a href=\"#\" runat=\"server\" onServerClick=\"MyFuncion_Click\" >" + DocName + "</a></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><asp:LinkButton id=\"myid\" runat=\"server\" OnClick=\"MyFunction_Click\" >" + DocName + "</asp:LinkButton></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                    }
                }
            }
            return data;
        }

    }
}