using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DigiLocker3.Home
{
    public partial class Home : System.Web.UI.Page
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
                string sql = "SELECT * from Individual where Pno= '" + PNo_TextBox.Text + "' and password = '" + PAssword_TextBox.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader sqlRdr = cmd.ExecuteReader();
                if (sqlRdr.HasRows)
                {
                    Response.Redirect("Index.aspx?id=" + PNo_TextBox.Text);
                }
                else
                {
                    {
                        Response.Redirect("Index.aspx?id=Wrong");
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}