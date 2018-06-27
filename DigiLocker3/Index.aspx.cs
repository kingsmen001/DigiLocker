using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigiLocker3
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void MyFuncion_Click()
        {
            Console.Write("Hello");
            Response.Redirect("Update.aspx?Name=Pandian");
        }
    }
}