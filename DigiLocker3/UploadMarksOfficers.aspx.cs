using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace DigiLocker3
{
    public partial class UploadMarksOfficers : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Session["User_ID"] as string))
            {


                if (Session["Access_Level"].ToString().Equals("1"))
                {
                    opnAddCourse.Visible = true;
                    opnAddTrainees.Visible = true;
                    opnCreateCourse.Visible = true;
                    opnUpdateMarks.Visible = true;
                    opnViewResult.Visible = true;
                    opnViewTrainees.Visible = true;
                    opnUploadMarks.Visible = true;
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
                Response.Redirect("Home.aspx", false);
                opnAddCourseOfficer.Visible = false;
                opnAddTraineesOfficer.Visible = false;
                opnCreateCourseOfficer.Visible = false;
                opnUpdateMarksOfficer.Visible = false;
                opnViewResultOfficer.Visible = false;
                opnViewTraineesOfficer.Visible = true;
                opnUploadMarksOfficer.Visible = false;

                opnAddCourse.Visible = false;
                opnAddTrainees.Visible = false;
                opnCreateCourse.Visible = false;
                opnUpdateMarks.Visible = false;
                opnViewResult.Visible = false;
                opnViewTrainees.Visible = true;
                opnUploadMarks.Visible = false;
            }
            if (!this.IsPostBack)
            {
                con.Open();

                SqlCommand com = new SqlCommand("select Distinct(Course_Name) from OFFICER_COURSE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["Course_Name"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["Course_Name"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();
                ddlCourseType.Items.Insert(0, new ListItem("Select", "0"));
                div1.Visible = false;
                div2.Visible = false;

                /*
                string course_name = ddlCourseType.Items[0].Value;
                com = new SqlCommand("select Course_No from Sailor_Course where Course_Name ='" + course_name + "'", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();

                string table_name = course_name.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                List<string> termLabel = new List<string>();
                //string term_Label = "";
                com = new SqlCommand("select ENROLLEDIN from " + table_name, con); // table name 
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        List<string> term_Label = dr[0].ToString().Split('_').ToList();
                        foreach (string lbl in term_Label)
                        {
                            if(!lbl.Equals(""))
                                termLabel.Add(lbl);
                        }
                    }
                }
                ddlTerm.DataSource = termLabel.Distinct().ToList();
                ddlTerm.DataBind();

                table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                com = new SqlCommand("select * from " + table_name, con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();

                string term = ddlTerm.SelectedValue;
                string query = "";
                string type_name = "";
                com = new SqlCommand("select Distinct(Entry_NAME) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty), con); // table name 
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        type_name = dr[0].ToString().Replace(" ", "_");
                        table_name = course_name.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + type_name + "_" + "SUBJECTS";
                        query = query + "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + term + "' UNION ";
                    }
                }
                query = query.Substring(0, query.LastIndexOf("UNION"));
                com = new SqlCommand(query, con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlSubject1.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                ddlSubject1.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                ddlSubject1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlSubject1.DataBind();

    */
                con.Close();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "delete from tblPersons";
            SqlCommand com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            if (FileUpload1.HasFile)
            {
                Session["FileUpload1"] = FileUpload1;
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);
                //Import_To_Grid(FilePath, Extension);
                FileInfo newFile = new FileInfo(FilePath);
                ExcelPackage pck = new ExcelPackage(newFile);
                var theWorkbook = pck.Workbook;
                var theSheet = theWorkbook.Worksheets[1];
                int num = Convert.ToInt32(theSheet.Cells[9, 2].Value.ToString());
                num = num + 12;
                for (int row = 12; row < num; row++)
                {
                    string entry_type = "app_power";
                    string name = theSheet.Cells[row, 3].Value.ToString();
                    string rank = theSheet.Cells[row, 4].Value.ToString();
                    string number = theSheet.Cells[row, 5].Value.ToString();
                    string theory = theSheet.Cells[row, 6].Value.ToString();
                    string ia = theSheet.Cells[row, 7].Value.ToString();
                    string practical = theSheet.Cells[row, 8].Value.ToString();
                    string marks = theSheet.Cells[row, 9].Value.ToString();
                    query = "insert into tblPersons(Personal_No, Name, Rank, Theory, IA, Practical, Marks) values( '" + number + "', '" + name + "', '" + rank + "', '" + theory + "', '" + ia + "', '" + practical + "', '" + marks + "')";
                    com = new SqlCommand(query, con);
                    com.ExecuteNonQuery();
                }
                //GridView1.DataSource = WorksheetToDataTable(theSheet);
                //GridView1.DataBind();
                //var data = theSheet.Cells["A1:P34"].Value;
                //var Summary = workbook1.Worksheets[1];
                query = "Select Name, Rank, Personal_No, Theory, IA, Practical, Marks from tblPersons";

                com = new SqlCommand(query, con);
                SqlDataAdapter adpt = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                adpt.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
                ConfirmButton.Visible = true;
                ConfirmButton.EnableViewState = true;
            }
            else
            {
                Response.Write("<script language='javascript'>alert('Please Select a File');</script>");
            }
            con.Close();
        }

        private DataTable WorksheetToDataTable(ExcelWorksheet oSheet)
        {
            //int totalRows = oSheet.Dimension.End.Row;
            //int totalCols = oSheet.Dimension.End.Column;
            DataTable dt = new DataTable(oSheet.Name);
            DataRow dr = null;
            for (int i = 12; i <= 48; i++)
            {
                if (i > 12) dr = dt.Rows.Add();
                for (int j = 3; j <= 9; j++)
                {
                    if (i == 12)
                        dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
                    else
                        dr[j - 3] = oSheet.Cells[i, j].Value.ToString();
                }
            }
            return dt;
        }
        private void Import_To_Grid(string FilePath, string Extension)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "Yes");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            //Response.Write("Hello World submit " + GridView1.Rows.Count);
        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            con.Open();
            int i = 0;
            string course_type = ddlCourseType.SelectedValue.Replace(" ", "_");
            string course_no = ddlCourseNo.SelectedValue.Replace(".", string.Empty);
            string entry_type = ddlEntryType.SelectedValue.Replace(" ", "_");
            string subject = ddlSubject1.SelectedItem.Text.Replace(" ", "_");
            string term = ddlTerm.SelectedValue;
            int max_marks = Convert.ToInt32(ddlSubject1.SelectedValue);
            SqlCommand cmd;
            string query;
            string marks_entered = "\n";
            string table_name;
            int markspresent = -1;
            string list = "";
            int flag = 0;
            foreach (GridViewRow g1 in GridView1.Rows)
            {
                if (ddlTerm.SelectedValue.Equals("D") & (ddlCourseType.SelectedValue.Equals("MEAT POWER") || ddlCourseType.SelectedValue.Equals("MEAT RADIO")))
                {
                    table_name = course_type + "_" + course_no + "_D_TERM";
                    query = "select Total from " + table_name + " where Personal_No = '" + g1.Cells[2].Text + "' and Subject_Name = '" + ddlSubject1.SelectedItem.Text + "'";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        markspresent = dr.GetInt32(0);
                    }
                    dr.Close();
                    query = "Select ENTRY_NAME from " + ddlCourseType.SelectedValue.Replace(" ", ".") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + " where Personal_No = '" + g1.Cells[2].Text + "'";
                    string entry_name = "";
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        entry_name = dr.GetString(0);
                    }
                    dr.Close();
                    query = "update " + table_name + " set total " + "= " + g1.Cells[6].Text + ", theory = " + g1.Cells[3].Text + ", IA = " + g1.Cells[4].Text + ", Practical = " + g1.Cells[5].Text + " where Personal_No = '" + g1.Cells[2].Text + "' and Subject_Name = '" + ddlSubject1.SelectedItem.Text + "'";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    int marks = Convert.ToInt32(g1.Cells[6].Text);
                    if (markspresent == 0)
                    {
                        if (marks < (50.0 * max_marks) / 100.0)
                        {

                            if (markspresent == 0)
                            {
                                table_name = ddlCourseType.SelectedValue.Replace(" ", ".") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + entry_name.Replace(" ", "_");
                                query = "update " + table_name + " set " + term + "_Failed = " + term + "_failed + 1" + " where Personal_No = '" + g1.Cells[2].Text + "'";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        query = "";
                        table_name = course_type + "_" + course_no + "_D_TERM";
                        query = "update " + table_name + " set total " + "= " + g1.Cells[6].Text + ", theory = " + g1.Cells[3].Text + ", IA = " + g1.Cells[4].Text + ", Practical = " + g1.Cells[5].Text + " where Personal_No = '" + g1.Cells[2].Text + "' and Subject_Name = '" + ddlSubject1.SelectedItem.Text + "'";
                        //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }

                    else
                    {
                        query = "";
                        if (markspresent < (55.0 * max_marks) / 100.0)
                        {
                            table_name = course_type + "_" + course_no + "_D_TERM";
                            query = "update " + table_name + " set total " + "= " + g1.Cells[6].Text + ", theory = " + g1.Cells[3].Text + ", IA = " + g1.Cells[4].Text + ", Practical = " + g1.Cells[5].Text + " where Personal_No = '" + g1.Cells[2].Text + "' and Subject_Name = '" + ddlSubject1.SelectedItem.Text + "'";
                            //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            if (Convert.ToInt32(g1.Cells[6].Text) > (55.0 * max_marks) / 100.0)
                            {
                                table_name = ddlCourseType.SelectedValue.Replace(" ", ".") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + entry_name.Replace(" ", "_");
                                query = "update " + table_name + " set " + term + "_Failed = CASE WHEN " + term + "_failed > 0 THEN " + term + "_failed - 1 END where Personal_No = '" + g1.Cells[2].Text + "'";
                                cmd = new SqlCommand(query, con);
                                //cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            marks_entered = marks_entered + g1.Cells[2].Text + "\n";
                            list = list + g1.Cells[2].Text + ",  ";
                            flag = 1;
                        }

                    }
                }
                else {
                    string personal_no = g1.Cells[2].Text;
                    table_name = course_type + "_" + course_no;
                    query = "select entry_name from " + table_name + " where Personal_No = '" + personal_no + "'";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    string entry = "";
                    while (dr.Read())
                    {
                        entry = dr.GetString(0);
                    }
                    dr.Close();
                    table_name = course_type + "_" + course_no + "_" + entry.Replace(" ", "_");
                    int marks = Convert.ToInt32(g1.Cells[6].Text);
                    query = "select " + subject + " from " + table_name + " where Personal_No = '" + g1.Cells[2].Text + "'";
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        markspresent = dr.GetInt32(0);
                    }
                    dr.Close();
                    if (markspresent == 0)
                    {
                        if (marks < (50.0 * max_marks) / 100.0)
                        {

                            if (markspresent == 0)
                            {
                                query = "update " + table_name + " set " + term + "_Failed = " + term + "_failed + 1" + " where Personal_No = '" + g1.Cells[2].Text + "'";
                                cmd = new SqlCommand(query, con);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        query = "";
                        query = "update " + table_name + " set " + subject + "= " + g1.Cells[6].Text + ", " + subject + "_theory = " + g1.Cells[3].Text + ", " + subject + "_IA = " + g1.Cells[4].Text + ", " + subject + "_Practical = " + g1.Cells[5].Text + " where Personal_No = '" + g1.Cells[2].Text + "'";
                        //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }

                    else
                    {
                        query = "";
                        if (markspresent < (55.0 * max_marks) / 100.0)
                        {
                            query = "update " + table_name + " set " + subject + "= " + g1.Cells[6].Text + ", " + subject + "_theory = " + g1.Cells[3].Text + ", " + subject + "_IA = " + g1.Cells[4].Text + ", " + subject + "_Practical = " + g1.Cells[5].Text + " where Personal_No = '" + g1.Cells[2].Text + "'";
                            //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                            if (Convert.ToInt32(g1.Cells[4].Text) > (55.0 * max_marks) / 100.0)
                            {
                                query = "update " + table_name + " set " + term + "_Failed = CASE WHEN " + term + "_failed > 0 THEN " + term + "_failed - 1 END where Personal_No = '" + g1.Cells[2].Text + "'";
                                cmd = new SqlCommand(query, con);
                                //cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            marks_entered = marks_entered + g1.Cells[2].Text + "\n";
                            list = list + g1.Cells[2].Text + ",  ";
                            flag = 1;
                        }

                    }
                }


                //Response.Write(Marks+"      ");
                //cmd.ExecuteNonQuery();

                i++;
            }
            con.Close();
            FileUpload1 = (FileUpload)Session["FileUpload1"];
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string path = "~/Results/" + ddlCourseType.SelectedValue.Replace(" ", "_").ToUpper() + "/" + ddlCourseType.SelectedValue.Replace(" ", "_").ToUpper() + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "/" + ddlTerm.SelectedValue.Replace(" ", "_").ToUpper();

            Directory.CreateDirectory(Server.MapPath(path));
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FilePath = Server.MapPath(FolderPath + FileName);
            FileInfo newFile = new FileInfo(FilePath);
            ExcelPackage pck = new ExcelPackage(newFile);
            var theWorkbook = pck.Workbook;
            var theSheet = theWorkbook.Worksheets[1];

            FilePath = Server.MapPath("~/Results/" + ddlCourseType.SelectedValue.Replace(" ", "_").ToUpper() + "/" + ddlCourseType.SelectedValue.Replace(" ", "_").ToUpper() + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "/" + ddlTerm.SelectedValue.Replace(" ", "_").ToUpper() + "/" + FileName);
            FileUpload1.SaveAs(FilePath);
            if (flag == 0)
                Response.Write("<script language='javascript'>alert('Marks Added Successfully.');</script>");
            else
                Response.Write("<script language='javascript'>alert('Marks Already entered for following trainees " + list + "Change marks using update marks. ');</script>");
            reset();
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            reset();
        }

        protected void reset()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }



        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {
            ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("0"));
            if (ddlCourseType.SelectedValue.Equals("0"))
            {
                div1.Visible = false;
                div2.Visible = false;
            }
            else {
                div1.Visible = true;
                div2.Visible = false;
                con.Open();


                string name = ddlCourseType.SelectedValue;
                SqlCommand com = new SqlCommand("select Course_No from Officer_Course where Course_Name ='" + name + "'", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();
                ddlCourseNo.Items.Insert(0, new ListItem("Select", "0"));

                //name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                //com = new SqlCommand("select * from " + name, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                //ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                //ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlEntryType.DataBind();

                //string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                //List<string> termLabel = new List<string>();
                ////string term_Label = "";
                //com = new SqlCommand("select ENROLLEDIN from " + table_name, con); // table name 
                //using (SqlDataReader dr = com.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        List<string> term_Label = dr[0].ToString().Split('_').ToList();
                //        foreach (string lbl in term_Label)
                //        {
                //            if (!lbl.Equals(""))
                //                termLabel.Add(lbl);
                //        }
                //    }
                //}
                //ddlTerm.DataSource = termLabel.Distinct().ToList();
                //ddlTerm.DataBind();

                //name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) ;
                //com = new SqlCommand("select DISTINCT(" + ddlTerm.SelectedValue + ")  from " + name, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset

                //ddlClass.DataTextField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString(); // text field name of table dispalyed in dropdown
                //ddlClass.DataValueField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString();             // to retrive specific  textfield name 
                //ddlClass.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlClass.DataBind();

                //string query = "";
                //string type_name = "";
                //table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                //com = new SqlCommand("select Distinct(Entry_NAME) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty), con); // table name 
                //using (SqlDataReader dr = com.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        type_name = dr[0].ToString().Replace(" ", "_");
                //        table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + type_name + "_" + "SUBJECTS";
                //        query = query + "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + ddlTerm.Items[0].Text + "' UNION ";
                //    }
                //}
                //query = query.Substring(0, query.LastIndexOf("UNION"));
                //com = new SqlCommand(query, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlSubject1.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                //ddlSubject1.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                //ddlSubject1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlSubject1.DataBind();
                con.Close();
            }


            //con.Close();
        }

        protected void ddlCourseNoIndexChanged(object sender, EventArgs e)
        {
            ddlCourseNo.Items.Remove(ddlCourseNo.Items.FindByValue("0"));
            if (ddlCourseNo.SelectedValue.Equals("0"))
            {
                div1.Visible = false;
                div2.Visible = false;
            }
            else {
                div1.Visible = true;
                div2.Visible = true;
                div6.Visible = true;
                div3.Visible = false;
                div4.Visible = false;
                div5.Visible = false;
                con.Open();
                String name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                SqlCommand com = new SqlCommand("select * from " + name, con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();

                string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                List<string> termLabel = new List<string>();
                //string term_Label = ""; 
                com = new SqlCommand("select Distinct(ENROLLEDIN) from " + table_name, con); // table name 
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        List<string> term_Label = dr[0].ToString().Split('_').ToList();
                        foreach (string lbl in term_Label)
                        {
                            if (!lbl.Equals(""))
                                termLabel.Add(lbl);
                        }
                    }
                }
                ddlTerm.DataSource = termLabel.Distinct().ToList();
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Select", "0"));

                //name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty);
                //com = new SqlCommand("select DISTINCT(" + ddlTerm.SelectedValue + ")  from " + name, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset

                //ddlClass.DataTextField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString(); // text field name of table dispalyed in dropdown
                //ddlClass.DataValueField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString();             // to retrive specific  textfield name 
                //ddlClass.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlClass.DataBind();
                con.Close();
            }

        }
        protected void ddlTermIndexChanged(object sender, EventArgs e)
        {
            ddlTerm.Items.Remove(ddlTerm.Items.FindByValue("0"));
            if (ddlTerm.SelectedValue.Equals("0"))
            {
                div1.Visible = false;
                div2.Visible = false;
            }
            else {
                div1.Visible = true;
                div2.Visible = true;
                div6.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                div5.Visible = true;
                if (ddlTerm.SelectedValue.Equals("D") & (ddlCourseType.SelectedValue.Equals("MEAT POWER") || ddlCourseType.SelectedValue.Equals("MEAT RADIO")))
                {
                    con.Open();
                    String name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty);
                    SqlCommand com = new SqlCommand("select DISTINCT(" + ddlTerm.SelectedValue + ")  from " + name, con); // table name 
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds);  // fill dataset

                    ddlClass.DataTextField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString(); // text field name of table dispalyed in dropdown
                    ddlClass.DataValueField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString();             // to retrive specific  textfield name 
                    ddlClass.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                    ddlClass.DataBind();

                    string query = "";
                    string type_name = "";
                    string table_name = "MEAT_POWER_D_TERM_SUBJECTS";


                    query = query + "Select Subject_Name, Max_Marks from " + table_name;


                    com = new SqlCommand(query, con); // table name 
                    da = new SqlDataAdapter(com);
                    ds = new DataSet();
                    da.Fill(ds);  // fill dataset
                    ddlSubject1.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                    ddlSubject1.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                    ddlSubject1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                    ddlSubject1.DataBind();
                    //ddlSubject1.Items.Insert(0, new ListItem("Select", "0"));
                    con.Close();
                }
                else {
                    con.Open();
                    String name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty);
                    SqlCommand com = new SqlCommand("select DISTINCT(" + ddlTerm.SelectedValue + ")  from " + name, con); // table name 
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    da.Fill(ds);  // fill dataset

                    ddlClass.DataTextField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString(); // text field name of table dispalyed in dropdown
                    ddlClass.DataValueField = ds.Tables[0].Columns[ddlTerm.SelectedValue].ToString();             // to retrive specific  textfield name 
                    ddlClass.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                    ddlClass.DataBind();

                    string query = "";
                    string type_name = "";
                    string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                    com = new SqlCommand("select Distinct(Entry_NAME) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty), con); // table name 
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            type_name = dr[0].ToString().Replace(" ", "_");
                            table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + type_name + "_" + "SUBJECTS";
                            query = query + "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + ddlTerm.SelectedValue + "' UNION ";
                        }
                    }
                    query = query.Substring(0, query.LastIndexOf("UNION"));
                    com = new SqlCommand(query, con); // table name 
                    da = new SqlDataAdapter(com);
                    ds = new DataSet();
                    da.Fill(ds);  // fill dataset
                    ddlSubject1.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                    ddlSubject1.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                    ddlSubject1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                    ddlSubject1.DataBind();
                    ddlSubject1.Items.Insert(0, new ListItem("Select", "0"));
                    con.Close();
                }
            }
        }

        protected void ddlSubjectIndexChanged(object sender, EventArgs e)
        {
            ddlSubject1.Items.Remove(ddlSubject1.Items.FindByValue("0"));
            if (ddlSubject1.SelectedValue.Equals("0"))
            {
                div1.Visible = false;
                div2.Visible = false;
            }
            else {
                div1.Visible = true;
                div2.Visible = true;
                div6.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                div5.Visible = true;

            }
        }


        protected void ddlEntryTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string term = ddlTerm.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            entry_type = entry_type.Replace(" ", "_");
            //string name = ddlCourseType.SelectedValue.Replace(" ","_") + "_" + entry_type + "_SUBJECTS";
            //SqlCommand com = new SqlCommand("select Subject_Name from " + name + " where Term ='" + term + "'", con); // table name 
            //SqlDataAdapter da = new SqlDataAdapter(com);
            //DataSet ds = new DataSet();
            //da.Fill(ds);  // fill dataset
            //ddlSubject1.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
            //ddlSubject1.DataValueField = ds.Tables[0].Columns["Subject_Name"].ToString();             // to retrive specific  textfield name 
            //ddlSubject1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            //ddlSubject1.DataBind();
            //string term = ddlTerm.SelectedValue;
            string query = "";
            string type_name = "";
            string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + entry_type;
            SqlCommand com = new SqlCommand("select TYPE_NAME from " + table_name, con); // table name 
            using (SqlDataReader dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    type_name = dr[0].ToString().Replace(" ", "_");
                    table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + type_name + "_" + "SUBJECTS";
                    query = query + "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + term + "' UNION ";
                }
            }
            query = query.Substring(0, query.LastIndexOf("UNION"));
            com = new SqlCommand(query, con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlSubject1.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
            ddlSubject1.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
            ddlSubject1.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlSubject1.DataBind();
            con.Close();
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx", false);
        }
    }
}