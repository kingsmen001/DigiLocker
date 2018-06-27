using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DigiLocker3
{
    public partial class SaveFIle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fileName = Request.QueryString["filename"].ToString();
                //Response.Write(Request.QueryString["filename"]);
                string filePath = Path.Combine(Server.MapPath("~/Documents/"), fileName); ;
                // key for decryption
                byte[] Key = Encoding.UTF8.GetBytes("asdf!@#$1234ASDF");

                //try
                //{
                //UnicodeEncoding ue = new UnicodeEncoding();
                FileStream fsd = new FileStream(filePath, FileMode.Open);
                RijndaelManaged rmDeCryp = new RijndaelManaged();
                CryptoStream csd = new CryptoStream(fsd, rmDeCryp.CreateDecryptor(Key, Key), CryptoStreamMode.Read);

                // Decrypt & Download Here
                Response.ContentType = "application/octet-stream";
                //Response.AddHeader("Content-Disposition","attachment; filename=" + Path.GetFileName(filePath) + Path.GetExtension(filePath));
                Response.AddHeader("Content-Disposition", "attachment; filename="+fileName);
                int data;
                while ((data = csd.ReadByte()) != -1)
                {
                    Response.OutputStream.WriteByte((byte)data);
                    Response.Flush();

                }
                csd.Close();
                fsd.Close();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
            }
            //string closeWindowScript = "<script language=javascript>window.top.close();</script>";
            //if ((!ClientScript.IsStartupScriptRegistered("clientScript")))
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "clientScript", closeWindowScript);
            //}
        }
    }
}