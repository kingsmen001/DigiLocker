using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace DigiLocker3
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] file = new byte[FileUploadControl.PostedFile.ContentLength];
                FileUploadControl.PostedFile.InputStream.Read(file, 0, FileUploadControl.PostedFile.ContentLength);

                string fileName = FileUploadControl.PostedFile.FileName;
                byte[] Key = Encoding.UTF8.GetBytes("asdf!@#$1234ASDF");
                //UploadFile();
                
                string outputFile = Path.Combine(Server.MapPath("~/Documents/"), fileName);
                //Response.Write(outputFile);
                if (File.Exists(outputFile))
                {
                    // Show Already exist Message 
                    Response.Write("Already Exists");
                }
                else
                {
                    FileStream fs = new FileStream(outputFile, FileMode.Create);
                    RijndaelManaged rmCryp = new RijndaelManaged();
                    CryptoStream cs = new CryptoStream(fs, rmCryp.CreateEncryptor(Key, Key), CryptoStreamMode.Write);
                    foreach (var dataa in file)
                    {
                        cs.WriteByte((byte)dataa);
                    }
                    cs.Close();
                    fs.Close();
                    Response.Write(outputFile);

                    //FileUploadControl.SaveAs(Server.MapPath("~/Documents/") + filename);
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                    con.Open();
                    //string insertQuery = "insert into Documents(DocName,IssuedBy,IssuedOn,IssuedTo,filename)values (@DocName,@IssuedBy,@IssuedOn,@IssuedTo,@filename)";
                    //SqlCommand cmd = new SqlCommand(insertQuery, con);
                    //cmd.Parameters.AddWithValue("@Docname", Doc_Name_TextBox.Text);
                    //cmd.Parameters.AddWithValue("@IssuedBy", Issued_By_TextBox.Text);
                    //cmd.Parameters.AddWithValue("@IssuedOn", Issued_On_TextBox.Text);
                    //cmd.Parameters.AddWithValue("@IssuedTo", "09102K");
                    //cmd.Parameters.AddWithValue("@filename", "filename");

                    //cmd.ExecuteNonQuery();

                    Response.Write("Document Uploaded Successfully!!!thank you");

                    con.Close();

                    }
                
                }

            
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
            }

            
        }

        
    }
}