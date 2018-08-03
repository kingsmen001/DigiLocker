using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigiLocker3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                con.Open();
                string sql = "SELECT * from Users where user_no = @personalno and password = @password";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@personalno", PNo_TextBox.Text);
                cmd.Parameters.AddWithValue("password", Password_TextBox.Text);
                SqlDataReader sqlRdr = cmd.ExecuteReader();
                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        Session.Add("User_ID", sqlRdr.GetString(1));
                        Session.Add("Access_Level", sqlRdr.GetInt32(3));
                        Response.Redirect("Home.aspx", false);
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
    }
}