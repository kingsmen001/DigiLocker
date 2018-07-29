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
    public partial class AddSubjectsSailors : System.Web.UI.Page
    {

        string coursename;

        string table_name = "";
        int flag;
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
            if (Request.QueryString["coursename"] != null)
            {
                heading.InnerHtml = Request.QueryString["coursename"] + " (Add Subjects)";
                //ddlCourseType.Visible = false;
                //ddlCourseType.EnableViewState = false;
                //lblType.Visible = false;
                //lblType.EnableViewState = false;
                divType.Visible = false;
                divType.EnableViewState = false;
                coursename = Request.QueryString["coursename"].Replace(" ", "_");



            }


            if (!this.IsPostBack)
            {
                divgridspl.Visible = false;
                divspl.Visible = false;
                flag = 0;
                con.Open();
                //ddlCourseType.SelectedIndex = 0;
                ddlTerm.SelectedIndex = 0;
                lbEntryType.SelectedIndex = 0;

                SqlCommand com = new SqlCommand("select * from SAILOR_COURSE_TYPE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();
                ddlCourseType.Items.Insert(0, new ListItem("Select", "0"));
                if (coursename == null)
                {
                    //coursename = ddlCourseType.Items[0].Text;
                    //courseno = ddlCourseNo.Items[0].Text.Replace(".", string.Empty);

                    div1.Visible = false;
                    div2.Visible = false;
                    div3.Visible = false;

                }
                else
                {


                    if (coursename == null)
                        coursename = ddlCourseType.Items[0].Value.Replace(" ", "_");
                    com = new SqlCommand("select * from " + coursename.Replace(" ", "_") + "_ENTRY_TYPE", con); // table name 
                    da = new SqlDataAdapter(com);
                    ds = new DataSet();
                    da.Fill(ds);  // fill dataset
                    lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                    lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                    lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                    lbEntryType.DataBind();

                    string term_Label = "______________";
                    string table_name = coursename.Replace(" ", "_") + "_ENTRY_TYPE";
                    string entry_name = "";

                    for (int i = 0; i < lbEntryType.Items.Count; i++)
                    {
                        if (lbEntryType.Items[i].Selected)
                        {
                            string new_term_Label = " ";
                            entry_name = lbEntryType.Items[i].Text;
                            com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry_name + "'", con); // table name 
                            using (SqlDataReader dr = com.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    new_term_Label = dr[0].ToString();
                                }
                            }
                            if ((new_term_Label.Split('_').Length - 1) < ((term_Label.Split('_').Length - 1)))
                            {
                                term_Label = new_term_Label;
                            }
                            //Response.Write(selectedItem + "   ");
                            //insert command
                        }

                    }

                    entry_name = lbEntryType.Items[0].Text;
                    com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry_name + "'", con); // table name 
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            term_Label = dr[0].ToString();
                        }
                    }

                    ddlTerm.DataSource = term_Label.Split('_');
                    ddlTerm.DataBind();




                    con.Close();
                    ShowData();
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            /*if (flag == 0)
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension);
                    ConfirmButton.Visible = true;
                    ConfirmButton.EnableViewState = true;
                }
                else
                {
                    Response.Write("<script language='javascript'>alert('Please Select a File');</script>");
                }
            }
            else
            {*/
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            if (!string.IsNullOrWhiteSpace(txtMarks.Text) & !string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                con.Open();
                try
                {
                    if (ddlTerm.SelectedValue.Equals("D") & (ddlCourseType.SelectedValue.Equals("MEAT POWER") || ddlCourseType.SelectedValue.Equals("MEAT RADIO")))
                    {
                        string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_D_TERM_SUBJECTS";
                        string query = "insert into " + table_name + "(Subject_Name, Max_Marks, Theory, IA, Practical, class) values ('" + txtSubject.Text + "','" + txtMarks.Text + "','" + txtTheory.Text + "','" + txtIA.Text + "','" + txtPractical.Text + "','" + txtSpc.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("Update " + table_name + " set Max_Marks = Theory + IA + Practical", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        string table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                        string query = "insert into " + table_name + "(Subject_Name, Max_Marks, Theory, IA, Practical, Term) values ('" + txtSubject.Text + "','" + txtMarks.Text + "','" + txtTheory.Text + "','" + txtIA.Text + "','" + txtPractical.Text + "','" + ddlTerm.SelectedValue + "')";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("Update " + table_name + " set Max_Marks = Theory + IA + Practical", con);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        string script = "alert(\" Subject Already Exists. \");";
                        ScriptManager.RegisterStartupScript(this, GetType(),
                                              "ServerControlScript", script, true);

                    }
                    Response.Write(ex.Message);
                }
                con.Close();
                if (ddlTerm.SelectedValue.Equals("D") & (ddlCourseType.SelectedValue.Equals("MEAT POWER") || ddlCourseType.SelectedValue.Equals("MEAT RADIO")))
                {
                    ShowData1();
                }
                else
                {
                    ShowData();
                }
            }
            else
            {
                Response.Write("<script language='javascript'>alert('Please Fill Complete Details');</script>");
            }

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
            SqlTransaction tran = con.BeginTransaction();
            //int i = 0;
            try
            {
                string course_type = ddlCourseType.SelectedValue;
                course_type = course_type.Replace(" ", "_");
                List<string> entry_list = new List<string>();


                for (int i = 0; i < lbEntryType.Items.Count; i++)
                {
                    if (lbEntryType.Items[i].Selected)
                    {
                        string entry_type = lbEntryType.Items[i].Text.Replace(" ", "_");
                        string table_name = course_type + "_" + entry_type + "_" + "SUBJECTS";
                        foreach (GridViewRow g1 in GridView1.Rows)
                        {
                            string query = "insert into " + table_name + "(Subject_Name, Max_Marks, Term) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + ddlTerm.SelectedValue + "')";
                            SqlCommand cmd = new SqlCommand(query, con, tran);
                            //Response.Write("     " + query);
                            cmd.ExecuteNonQuery();


                        }

                    }
                }
                tran.Commit();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Response.Write(exRollback.Message);
                    }

                    string script = "alert(\" Your Data contains Duplicate Records. Remove and insert again. \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);

                }
                Response.Write(ex.Message);
            }


            con.Close();
            DataTable dk = new DataTable();
            GridView1.DataSource = dk;
            GridView1.DataBind();
            ShowData();


        }

        protected void lblEntryTypeIndexChanged(object sender, EventArgs e)
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");

            con.Open();



            string term_Label = "______________";
            string table_name = coursename.Replace(" ", "_") + "_ENTRY_TYPE";

            string new_term_Label = " ";
            string entry_name = lbEntryType.SelectedValue;
            SqlCommand com = new SqlCommand("select TERM_LABEL from " + table_name + " where TYPE_NAME = '" + entry_name + "'", con); // table name 
            using (SqlDataReader dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    term_Label = dr[0].ToString();
                }
            }

            //Response.Write(selectedItem + "   ");
            //insert command



            ddlTerm.DataSource = term_Label.Split('_');
            ddlTerm.DataBind();

            table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
            string query = "Select Subject_name, Max_Marks, Theory, IA, Practical from " + table_name + " where term = '" + ddlTerm.Items[0].Text + "'";
            //Response.Write(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            con.Close();
            ShowData();

            //GridView2.DataSource = dt;
            //GridView2.DataBind();




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



        protected void ddlCourseTypeIndexChanged(object sender, EventArgs e)
        {
            ddlCourseType.Items.Remove(ddlCourseType.Items.FindByValue("0"));
            if (ddlCourseType.SelectedValue.Equals("0"))
            {
                div1.Visible = false;
                div3.Visible = false;
                div2.Visible = false;
            }
            else {
                div1.Visible = true;
                div3.Visible = true;
                div2.Visible = true;
                con.Open();




                string name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_ENTRY_TYPE";
                SqlCommand com = new SqlCommand("select * from " + name, con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                lbEntryType.DataBind();
                string term_Label = "";
                com = new SqlCommand("select TERM_LABEL from " + name + " where TYPE_NAME = '" + lbEntryType.SelectedValue.Replace(" ", "_") + "'", con); // table name 
                using (SqlDataReader dr = com.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        term_Label = dr[0].ToString();
                    }
                }

                //Response.Write(selectedItem + "   ");
                //insert command



                ddlTerm.DataSource = term_Label.Split('_');
                ddlTerm.DataBind();

                string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + lbEntryType.Items[0].Text.Replace(" ", "_") + "_SUBJECTS";
                string query = "Select Subject_name, Max_Marks, Theory, IA, Practical from " + table_name + " where term = '" + ddlTerm.Items[0].Text + "'";
                //Response.Write(query);
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                con.Close();
                ShowData();
            }

            //GridView2.DataSource = dt;
            //GridView2.DataBind();



        }

        protected void ddlTermIndexChanged(object sender, EventArgs e)
        {
            if (ddlTerm.SelectedValue.Equals("D") & (ddlCourseType.SelectedValue.Equals("MEAT POWER") || ddlCourseType.SelectedValue.Equals("MEAT RADIO")))
            {
                ShowData1();
                //meatDTerm();
                divgrid.Visible = false;
                divgridspl.Visible = true;
                divspl.Visible = true;
                div3.Visible = false;
            }
            else
            {
                if (coursename == null)
                    coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
                con.Open();

                string table_name = coursename + "_" + lbEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
                string query = "Select Subject_name, Max_Marks from " + table_name + " where term = '" + ddlTerm.SelectedValue + "'";
                //Response.Write(query);
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                con.Close();
                divgrid.Visible = true;
                divgridspl.Visible = false;
                divspl.Visible = false;
                div3.Visible = true;
                ShowData();
            }

        }

        protected void meatDTerm()
        {

        }

        protected void ShowData1()
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            con.Open();
            table_name = "MEAT_POWER_D_TERM_SUBJECTS";
            string query = "Select ID, Subject_name, Theory, IA, Practical, Max_Marks, class from " + table_name;
            //Response.Write(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);


            exlfile.Visible = false;
            single.Visible = true;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            flag = 1;
            SubmitButton.Text = "Add";
            txtMarks.Text = "";



            con.Close();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            String name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_ENTRY_TYPE";
            SqlCommand com = new SqlCommand("select * from" + name, con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            lbEntryType.DataBind();

        }

        protected void txtCourseName_TextChanged(object sender, EventArgs e)
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            if (!string.IsNullOrWhiteSpace(txtSubject.Text) & !System.Text.RegularExpressions.Regex.IsMatch(txtSubject.Text, "[^a-zA-Z0-9\x20]", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                con.Open();
                string table_name = coursename + "_" + lbEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
                //Response.Write(table_name);
                SqlCommand com = new SqlCommand("select Count(SUBJECT_NAME) from " + table_name + " where SUBJECT_NAME = '" + txtSubject.Text + "'", con); // table name 
                int count = (int)com.ExecuteScalar();
                if (count == 1)
                {
                    string script = "alert(\" Subject Name Already Exists \");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                                          "ServerControlScript", script, true);
                    txtSubject.Text = "";
                }
                con.Close();
            }
            else
            {
                string script = "alert(\" Only AlphaNumeric Characters are Allowed. Name Cannot start from Space. \");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                txtSubject.Text = "";
            }
            txtMarks.Focus();
        }

        protected void ShowData()
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            con.Open();
            table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_") + "_SUBJECTS";
            string query = "Select ID, Subject_name, Theory, IA, Practical, Max_Marks from " + table_name + " where term = '" + ddlTerm.SelectedValue + "'";
            //Response.Write(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            //if (dt.Rows.Count > 0)
            //{
            exlfile.Visible = false;
            single.Visible = true;
            GridView3.DataSource = dt;
            GridView3.DataBind();
            flag = 1;
            SubmitButton.Text = "Add";
            txtMarks.Text = "";
            txtSubject.Text = "";
            //}
            //else
            //{
            //    GridView3.DataSource = dt;
            //    GridView3.DataBind();
            //    exlfile.Visible = true;
            //    single.Visible = false;
            //    flag = 0;
            //    SubmitButton.Text = "Submit";
            //}


            con.Close();
        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView3.EditIndex = e.NewEditIndex;
            ShowData();

        }
        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            //Finding the controls from Gridview for the row which is going to update
            Label id = GridView3.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox name = GridView3.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            //TextBox marks = GridView3.Rows[e.RowIndex].FindControl("txt_City") as TextBox;
            TextBox Theory = GridView3.Rows[e.RowIndex].FindControl("txt_Theory") as TextBox;
            TextBox IA = GridView3.Rows[e.RowIndex].FindControl("txt_IA") as TextBox;
            TextBox Practical = GridView3.Rows[e.RowIndex].FindControl("txt_Pra") as TextBox;
            con.Open();
            //updating the record  
            table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_") + "_SUBJECTS";
            SqlCommand cmd = new SqlCommand("Update " + table_name + " set SUBJECT_NAME ='" + name.Text + "', Theory = '" + Theory.Text + "', IA = '" + IA.Text + "', Practical = '" + Practical.Text + "' where ID=" + Convert.ToInt32(id.Text), con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Update " + table_name + " set Max_Marks = Theory + IA + Practical", con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView3.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView3.EditIndex = -1;
            ShowData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            Label id = GridView3.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            con.Open();
            table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_") + "_SUBJECTS";
            SqlCommand cmd = new SqlCommand("delete FROM " + table_name + " where ID=" + Convert.ToInt32(id.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView3.EditIndex = e.NewEditIndex;
            ShowData();

        }
        protected void GridView2_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            //Finding the controls from Gridview for the row which is going to update
            Label id = GridView3.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            TextBox name = GridView3.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
            //TextBox marks = GridView3.Rows[e.RowIndex].FindControl("txt_City") as TextBox;
            TextBox Theory = GridView3.Rows[e.RowIndex].FindControl("txt_Theory") as TextBox;
            TextBox IA = GridView3.Rows[e.RowIndex].FindControl("txt_IA") as TextBox;
            TextBox Practical = GridView3.Rows[e.RowIndex].FindControl("txt_Pra") as TextBox;
            con.Open();
            //updating the record  
            table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_") + "_SUBJECTS";
            SqlCommand cmd = new SqlCommand("Update " + table_name + " set SUBJECT_NAME ='" + name.Text + "', Theory = '" + Theory.Text + "', IA = '" + IA.Text + "', Practical = '" + Practical.Text + "' where ID=" + Convert.ToInt32(id.Text), con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("Update " + table_name + " set Max_Marks = Theory + IA + Practical", con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView3.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        protected void GridView2_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView3.EditIndex = -1;
            ShowData();
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (coursename == null)
                coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
            Label id = GridView3.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            con.Open();
            table_name = coursename.Replace(" ", "_") + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_") + "_SUBJECTS";
            SqlCommand cmd = new SqlCommand("delete FROM " + table_name + " where ID=" + Convert.ToInt32(id.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            ShowData();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx", false);
        }
    }
}
