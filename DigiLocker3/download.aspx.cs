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
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string closeWindowScript = "<script language=javascript>window.top.close();</script>";
            //if ((!ClientScript.IsStartupScriptRegistered("clientScript")))
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", closeWindowScript);
            //}
        }
        public string getStudentData()
        {
            string data = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
            con.Open();
            string sql = "SELECT * from Documents";
            SqlCommand cmd = new SqlCommand(sql, con);
            using (SqlDataReader sqlRdr = cmd.ExecuteReader())
            {
                // table = new DataTable();  
                // table.Load(reader);  
                if (sqlRdr.HasRows)
                {
                    while (sqlRdr.Read())
                    {
                        //int DocID = sqlRdr.GetInt32(0);
                        string DocName = sqlRdr.GetString(1);
                        string IssuedBy = sqlRdr.GetString(2);
                        string IssuedOn = sqlRdr.GetDateTime(3).ToString("dd mm yyyy");
                        string filename = sqlRdr.GetString(5);
                        string id = "09102k";
                        data += "<tr><td>" + "1234" + "</td><td><a href=\"SaveFile.aspx?id=" + id + "&filename=" + filename + "\" target =\"_blank\">" + DocName + "</a></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><a href=\"#\" runat=\"server\" onServerClick=\"MyFuncion_Click\" >" + DocName + "</a></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                        //data += "<tr><td>" + DocID + "</td><td><asp:LinkButton id=\"myid\" runat=\"server\" OnClick=\"MyFunction_Click\" >" + DocName + "</asp:LinkButton></td><td>" + IssuedBy + "</td><td>" + IssuedOn + "</td></tr>";
                    }
                }
            }
            return data;
        }
        public void MyFuncion_Click()
        {
            Response.Redirect("Index.aspx");
        }
    }
}