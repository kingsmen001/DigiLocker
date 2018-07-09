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
    public partial class AddUser : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string username = NameTextBox.Text;
            string userNo = NumberTextBox.Text;
            string password = PasswordTextBox.Text;
            int accesslevel = Accessddl.SelectedIndex;

            string Query = "insert into users (user_name, user_no, password, access_level) values(@username, @userno, @password, @accesslevel)";
            SqlCommand cmd = new SqlCommand(Query);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@userno", userNo);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@accesslevel", accesslevel);
            cmd.ExecuteNonQuery();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
         
        }
    }
}