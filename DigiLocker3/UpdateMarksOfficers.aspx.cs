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
using System.Windows.Input;

namespace DigiLocker3
{
    public partial class UpdateMarksOfficers : System.Web.UI.Page
    {
        string seniority;
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
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                div5.Visible = false;
                div6.Visible = false;

                //string course_name = ddlCourseType.Items[0].Value;
                //com = new SqlCommand("select Course_No from Sailor_Course where Course_Name ='" + course_name + "'", con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                //ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                //ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlCourseNo.DataBind();

                //string table_name = course_name.Replace(" ", "_") + "_ENTRY_TYPE";
                //List<string> termLabel = new List<string>();
                ////string term_Label = "";
                //com = new SqlCommand("select TERM_LABEL from " + table_name, con); // table name 
                //using (SqlDataReader dr = com.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        List<string> term_Label = dr[0].ToString().Split('_').ToList();
                //        foreach (string lbl in term_Label)
                //        {
                //            termLabel.Add(lbl);
                //        }
                //    }
                //}
                //ddlTerm.DataSource = termLabel.Distinct().ToList();
                //ddlTerm.DataBind();

                //table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".",string.Empty) + "_ENTRY_TYPE";
                //com = new SqlCommand("select * from " + table_name, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                //ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                //ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlEntryType.DataBind();

                //table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                //string query = "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + ddlTerm.SelectedValue + "'";

                //com = new SqlCommand(query, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                //ddlSubject.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                //ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlSubject.DataBind();

                //table_name = "SAILOR_COURSE_TYPE";
                //com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con);
                //SqlDataReader dk = com.ExecuteReader();

                //while (dk.Read())
                //{
                //    seniority = dk.GetValue(0).ToString();
                //    //seniority = dr.GetValue(1).ToString();
                //}
                //dk.Close();
                //if (seniority.Equals("1"))
                //    ddlSubject.Items.Add(new ListItem("Seniority", "0"));


                con.Close();
                //ddlCourseType.SelectedIndex = 0;
                //ddlCourseNo.SelectedIndex = 0;
                //ddlTerm.SelectedIndex = 0;
                //ddlEntryType.SelectedIndex = 0;
                //ddlSubject.SelectedIndex = 0;
                //updateGrid();
            }

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            //if (FileUpload1.HasFile)
            //{
            //    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //    string FilePath = Server.MapPath(FolderPath + FileName);
            //    FileUpload1.SaveAs(FilePath);
            //    Import_To_Grid(FilePath, Extension);
            //    ConfirmButton.Visible = true;
            //    ConfirmButton.EnableViewState = true;
            //}
            //else
            //{
            //    Response.Write("<script language='javascript'>alert('Please Select a File');</script>");
            //}
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
            string subject = ddlSubject.SelectedItem.Text.Replace(" ", "_");
            string term = ddlTerm.SelectedValue;
            int max_marks = Convert.ToInt32(ddlSubject.SelectedValue);
            SqlCommand cmd;
            string query;
            string table_name;
            int markspresent = -1;
            foreach (GridViewRow g1 in GridView1.Rows)
            {
                table_name = course_type + "_" + course_no + "_" + g1.Cells[0].Text.Replace(" ", "_");
                int marks = Convert.ToInt32(g1.Cells[4].Text);
                query = "select " + subject + " from " + table_name + " where Personal_No = '" + g1.Cells[1].Text + "'";
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    markspresent = dr.GetInt32(0);
                }
                dr.Close();
                if (markspresent == 0)
                {
                    if (marks < (55.0 * max_marks) / 100.0)
                    {

                        if (markspresent == 0)
                        {
                            query = "update " + table_name + " set " + term + "_Failed = " + term + "_failed + 1" + " where Personal_No = '" + g1.Cells[1].Text + "'";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    query = "";
                    query = "update " + table_name + " set " + subject + "= " + g1.Cells[4].Text + " where Personal_No = '" + g1.Cells[1].Text + "'";
                    //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
                else if (markspresent < (55.0 * max_marks) / 100.0)
                {
                    query = "";
                    query = "update " + table_name + " set " + subject + "= " + g1.Cells[4].Text + " where Personal_No = '" + g1.Cells[1].Text + "'";
                    //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    if (marks >= (55.0 * max_marks) / 100.0)
                    {
                        query = "update " + table_name + " set " + term + "_Failed = ( WHEN " + term + "_failed = 1 THEN " + term + "_failed - 1 END)";
                        cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    Response.Write("<script language='javascript'>alert('Marks Already Entered for" + g1.Cells[1].Text + "');</script>");
                }

                //Response.Write(Marks+"      ");
                //cmd.ExecuteNonQuery();

                i++;
            }
            con.Close();
            Response.Write("<script language='javascript'>alert('Marks Added Successfully');</script>");
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

        protected void updateGrid()
        {

            con.Open();
            string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_");
            string query;
            string term = ddlTerm.SelectedValue;
            if (ddlSubject.SelectedItem.Text.Equals("Seniority"))
            {
                div1.Visible = true;
                string table_name1 = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + ddlTerm.SelectedValue.Replace(" ", "_") + "_SENIORITY";
                query = "select MAX(seniority) from " + table_name1;
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                string maxsen = "";
                while (dr.Read())
                {
                    maxsen = dr.GetString(0);
                }
                dr.Close();
                senTextBox.Text = maxsen;
                query = "Select Personal_No, Name, Rank, " + term + "_Failed, " + term + "_Seniority_Gained, " + term + "_Seniority_Lost" + " from " + table_name;
                GridView1.DataKeyNames = new string[] { "Personal_No", "Name", "Rank", term + "_Failed", term + "_Seniority_Gained" };
            }
            else {
                div1.Visible = false;
                query = "Select Personal_No, Name, Rank, " + ddlSubject.SelectedItem.Text.Replace(" ", "_") + " from " + table_name;
                GridView1.DataKeyNames = new string[] { "Personal_No", "Name", "Rank" };
            }
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        protected void ddlSubjectIndexChanged(object sender, EventArgs e)
        {
            div6.Visible = true;
            updateGrid();
        }
        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string Personal_No = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[0]);
            string Name = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[1]);
            string Rank = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[2]);
            string course_type = ddlCourseType.SelectedValue.Replace(" ", "_");
            string course_no = ddlCourseNo.SelectedValue.Replace(".", string.Empty);
            string entry_type = ddlEntryType.SelectedValue.Replace(" ", "_");
            string subject = ddlSubject.SelectedItem.Text.Replace(" ", "_");
            string term = ddlTerm.SelectedValue;
            string table_name = course_type + "_" + course_no + "_" + entry_type;
            string query;
            SqlCommand cmd;
            con.Open();
            if (ddlSubject.SelectedItem.Text.Equals("Seniority"))
            {
                GridView1.DataKeyNames = new string[] { "Personal_No", "Name", "Rank", term + "_Failed", term + "_Seniority_Gained" };
                string seniority_gained = Convert.ToString(GridView1.DataKeys[e.RowIndex].Values[4]);
                double seniority_lost = Convert.ToDouble((row.Cells[6].Controls[0] as TextBox).Text);
                query = "update " + table_name + " set " + term + "_Seniority_Lost = " + seniority_lost + " where Personal_No = '" + Personal_No + "'";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                Response.Write("<script language=javascript>alert('Seniority Updated Successfully.');</script>");
            }
            else {
                GridView1.DataKeyNames = new string[] { "Personal_No", "Name", "Rank" };

                int marks = Convert.ToInt32((row.Cells[4].Controls[0] as TextBox).Text);
                int max_marks = Convert.ToInt32(ddlSubject.SelectedValue);


                if (marks > max_marks)
                {
                    Response.Write("<script language=javascript>alert('Marks Cannot be greater than Maximum Marks.');</script>");
                }
                else
                {
                    //Response.Write("<script language=javascript>confirm('Marks are less than the minimum passing marks.');</script>");



                    int markspresent = -1;

                    query = "select " + subject + " from " + table_name + " where Personal_No = '" + Personal_No + "'";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        markspresent = dr.GetInt32(0);
                    }
                    dr.Close();



                    query = "";
                    query = "update " + table_name + " set " + subject + "= " + marks + " where Personal_No = '" + Personal_No + "'";
                    //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    if (marks < (55.0 * max_marks) / 100.0)
                    {

                        if (markspresent == 0)
                        {
                            query = "update " + table_name + " set " + term + "_Failed = " + term + "_failed + 1" + " where Personal_No = '" + Personal_No + "'";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (markspresent < (55.0 * max_marks) / 100.0)
                    {

                        if (marks >= (55.0 * max_marks) / 100.0)
                        {
                            query = "update " + table_name + " set " + term + "_Failed = CASE WHEN " + term + "_failed > 0 THEN " + term + "_failed - 1 END where Personal_No = '" + Personal_No + "'";
                            cmd = new SqlCommand(query, con);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    Response.Write("<script language=javascript>alert('Marks Updated Successfully.');</script>");



                }
            }
            con.Close();
            GridView1.EditIndex = -1;
            this.updateGrid();
        }


        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.updateGrid();
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            this.updateGrid();
        }
        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {
            ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("0"));
            if (ddlCourseType.SelectedValue.Equals("0"))
            {
                div2.Visible = false;
                div3.Visible = false;
                div2.Visible = false;
            }
            else {
                div2.Visible = true;
                div3.Visible = false;
                div4.Visible = false;
                div5.Visible = false;
                div6.Visible = false;
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

                //List<string> termLabel = new List<string>();
                //string query = "Select DISTINCT(TERM) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
                //com = new SqlCommand(query, con);
                //SqlDataReader dr = com.ExecuteReader();
                //while (dr.Read())
                //{
                //    termLabel.Add(dr.GetString(0));
                //}
                //dr.Close();

                //ddlTerm.DataSource = termLabel.Distinct().ToList();
                //ddlTerm.DataBind();



                //string term = ddlTerm.SelectedValue;
                //string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                //query = "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + term + "'";

                //com = new SqlCommand(query, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                //ddlSubject.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                //ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlSubject.DataBind();
                //table_name = "SAILOR_COURSE_TYPE";
                //com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con);
                //SqlDataReader dk = com.ExecuteReader();

                //while (dk.Read())
                //{
                //    seniority = dk.GetValue(0).ToString();
                //    //seniority = dr.GetValue(1).ToString();
                //}
                //dk.Close();
                //if (seniority.Equals("1"))
                //    ddlSubject.Items.Add(new ListItem("Seniority", "0"));
                //con.Close();


                //updateGrid();
                con.Close();
            }
        }

        protected void ddlCourseNoIndexChanged(object sender, EventArgs e)
        {
            ddlCourseNo.Items.Remove(ddlCourseNo.Items.FindByValue("0"));
            if (ddlCourseNo.SelectedValue.Equals("0"))
            {
                div2.Visible = false;
                div3.Visible = false;
                div2.Visible = false;
            }
            else {
                div2.Visible = true;
                div3.Visible = true;
                div4.Visible = false;
                div5.Visible = false;
                div6.Visible = false;
                con.Open();
                String name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE";
                SqlCommand com = new SqlCommand("select * from " + name + " where EnrolledIn IS NOT NULL", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();
                ddlEntryType.Items.Insert(0, new ListItem("Select", "0"));



                //List<string> termLabel = new List<string>();
                //string query = "Select DISTINCT(TERM) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
                //com = new SqlCommand(query, con);
                //SqlDataReader dr = com.ExecuteReader();
                //while (dr.Read())
                //{
                //    termLabel.Add(dr.GetString(0));
                //}
                //dr.Close();

                //ddlTerm.DataSource = termLabel.Distinct().ToList();
                //ddlTerm.DataBind();



                //string term = ddlTerm.SelectedValue;
                //string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                //query = "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + term + "'";

                //com = new SqlCommand(query, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                //ddlSubject.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                //ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlSubject.DataBind();
                //table_name = "SAILOR_COURSE_TYPE";
                //com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con);
                //SqlDataReader dk = com.ExecuteReader();

                //while (dk.Read())
                //{
                //    seniority = dk.GetValue(0).ToString();
                //    //seniority = dr.GetValue(1).ToString();
                //}
                //dk.Close();
                //if (seniority.Equals("1"))
                //    ddlSubject.Items.Add(new ListItem("Seniority", "0"));
                //con.Close();


                //updateGrid();
                con.Close();
            }

        }
        protected void ddlTermIndexChanged(object sender, EventArgs e)
        {
            ddlTerm.Items.Remove(ddlTerm.Items.FindByValue("0"));
            if (ddlTerm.SelectedValue.Equals("0"))
            {
                div2.Visible = false;
                div3.Visible = false;
                div2.Visible = false;
            }
            else {
                div2.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                div5.Visible = true;
                div6.Visible = true;

                con.Open();
                string term = ddlTerm.SelectedValue;
                string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                string query = "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + term + "'";

                SqlCommand com = new SqlCommand(query, con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                ddlSubject.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlSubject.DataBind();
                table_name = "OFFICER_COURSE_TYPE";
                com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con);
                SqlDataReader dk = com.ExecuteReader();

                while (dk.Read())
                {
                    seniority = dk.GetValue(0).ToString();
                    //seniority = dr.GetValue(1).ToString();
                }
                dk.Close();
                if (seniority.Equals("1"))
                    ddlSubject.Items.Add(new ListItem("Seniority", "0"));
                con.Close();
                updateGrid();
            }

        }


        protected void ddlEntryTypeIndexChanged(object sender, EventArgs e)
        {
            ddlEntryType.Items.Remove(ddlEntryType.Items.FindByValue("0"));
            if (ddlEntryType.SelectedValue.Equals("0"))
            {
                div2.Visible = false;
                div3.Visible = false;
                div2.Visible = false;
            }
            else {
                div2.Visible = true;
                div3.Visible = true;
                div4.Visible = true;
                div5.Visible = false;
                div6.Visible = false;
                con.Open();


                List<string> termLabel = new List<string>();
                string query = "select Distinct(ENROLLEDIN) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_ENTRY_TYPE where TYPE_NAME = '" + ddlEntryType.SelectedValue + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    termLabel = dr.GetString(0).Split('_').ToList();
                }
                dr.Close();

                ddlTerm.DataSource = termLabel.Distinct().ToList();
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("Select", "0"));

                //string term = ddlTerm.SelectedValue;
                //String table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue.Replace(".", string.Empty) + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                //query = "Select Subject_Name, Max_Marks from " + table_name + " where term = '" + term + "'";

                //com = new SqlCommand(query, con); // table name 
                //SqlDataAdapter da = new SqlDataAdapter(com);
                //DataSet ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                //ddlSubject.DataValueField = ds.Tables[0].Columns["Max_Marks"].ToString();             // to retrive specific  textfield name 
                //ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlSubject.DataBind();
                //table_name = "SAILOR_COURSE_TYPE";
                //com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con);
                //SqlDataReader dk = com.ExecuteReader();

                //while (dk.Read())
                //{
                //    seniority = dk.GetValue(0).ToString();
                //    //seniority = dr.GetValue(1).ToString();
                //}
                //dk.Close();
                //if (seniority.Equals("1"))
                //    ddlSubject.Items.Add(new ListItem("Seniority", "0"));
                //con.Close();
                //updateGrid();
                con.Close();
            }
        }
        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx", false);
        }
    }
}