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
    public partial class ViewResult1 : System.Web.UI.Page
    {
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        string seniority = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                con.Open();
                ddlCourseType.SelectedIndex = 0;
                ddlCourseNo.SelectedIndex = 0;
                ddlEntryType.SelectedIndex = 0;

                //lbTerm.SelectedIndex = null;

                SqlCommand com = new SqlCommand("select Distinct(Course_Name) from SAILOR_COURSE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["Course_Name"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["Course_Name"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();

                string name = ddlCourseType.Items[0].Value;
                string entry_type;
                com = new SqlCommand("select Course_No from Sailor_Course where Course_Name ='" + name + "'", con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();



                name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_ENTRY_TYPE";
                com = new SqlCommand("select TYPE_NAME from " + name, con); // table name 
                da = new SqlDataAdapter(com);
                ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlEntryType.DataBind();

                name = ddlCourseType.Items[0].Value.Replace(" ", "_") + "_ENTRY_TYPE";
                List<string> termLabel = new List<string>();
                string query = "Select DISTINCT(TERM) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
                com = new SqlCommand(query, con);
                SqlDataReader dr = com.ExecuteReader();
                //string term_label = "";
                while (dr.Read())
                {
                    termLabel.Add(dr.GetString(0));
                }
                dr.Close();
                //List<string> termLabel = new List<string>();
                //com = new SqlCommand("select TERM_LABEL from " + name + " where TYPE_NAME = '" + ddlEntryType.Items[0].Value + "'", con); // table name 
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
                lbTerm.DataSource = termLabel.Distinct().ToList();
                lbTerm.DataBind();
                lbTerm.Items.Insert(0, new ListItem("Please Select", "0"));


                con.Close();
                showResult();
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            lbTerm.Items.RemoveAt(0);
            lbTerm.Items.Remove(lbTerm.Items.FindByValue("Please Select"));
            calculateResult();
            DataTable dt = new DataTable();
            string course = ddlCourseType.SelectedValue;
            string course_no = ddlCourseNo.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            string col_List = "";
            string table_name = "";
            SqlCommand cmd;
            con.Open();
            for (int i = 0; i < lbTerm.Items.Count; i++)
            {
                if (lbTerm.Items[i].Selected)
                {
                    string term = lbTerm.Items[i].Value;
                    table_name = course.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + entry_type.Replace(" ", "_") + "_SUBJECTS";

                    cmd = new SqlCommand("select Subject_Name from " + table_name + " where Term = '" + term + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    //string subject_code;
                    List<string> column_List = new List<string>();

                    string col_name;
                    while (dr.Read())
                    {
                        col_name = dr.GetValue(0).ToString().Replace(" ", "_");
                        //Response.Write(col_name + i);
                        //i++;

                        column_List.Add(col_name);


                    }

                    dr.Close();

                    foreach (string col_nam in column_List)
                    {
                        col_List = col_List + ", " + col_nam;
                    }
                    col_List = col_List + ", " + term + "_total, " + term + "_failed, " + term + "_percentage, " + term + "_Seniority_gained, " + term + "_Seniority_lost, " + term + "_Seniority_total";
                    column_List.Clear();
                }
            }
            //con.Open();
            table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type.Replace(" ", "_");
            string query = "select Personal_No, Name, Rank" + col_List + " from " + table_name;
            //Response.Write(query);
            cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void Export_Clicked(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName;
            string heading;
            if (lbTerm.SelectedItem.Text.Equals("Please Select"))
            {
                FileName = ddlCourseType.SelectedValue.Replace(" ", "_") + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "Result.xls";
                heading = ddlCourseType.SelectedValue.Replace(" ", " ") + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", " ");
            }
            else {
                FileName = ddlCourseType.SelectedValue.Replace(" ", "_") + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + lbTerm.SelectedItem.Text + "Result.xls";
                heading = ddlCourseType.SelectedValue.Replace(" ", " ") + ddlCourseNo.SelectedValue + " " + ddlEntryType.SelectedValue.Replace(" ", " ") + " " + lbTerm.SelectedItem.Text + "_Term";
            }
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //for (int i = 0; i < GridView3.Rows.Count; i++)
            //{
            //    GridViewRow row = GridView1.Rows[i];
            //    row.Cells[3].Visible = false;
            //}
            
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write("<B>");
            Response.Write(ddlCourseType.SelectedValue + " " + ddlCourseNo.SelectedValue + " " + ddlEntryType.SelectedValue);
            Response.Write("</B>");
            Response.Write(strwritter.ToString());
            Response.End();
        }

        protected void showResult()
        {
            calculateResult();
            DataTable dt = new DataTable();
            string course = ddlCourseType.SelectedValue;
            string course_no = ddlCourseNo.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;

            string table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type.Replace(" ", "_");
            SqlCommand cmd;
            con.Open();

            string query = "Select DISTINCT(TERM) from " + course.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + entry_type.Replace(" ", "_") + "_SUBJECTS";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            string term_label = "";
            while (dr.Read())
            {
                term_label = term_label + dr.GetString(0) + "_";
            }
            dr.Close();
            term_label = term_label.Remove(term_label.Length - 1);
            string col_list1 = "";

            foreach (string term in term_label.Split('_'))
            {
                col_list1 = col_list1 + term + "_total, " + term + "_percentage, " + term + "_failed, ";
                if (seniority.Equals("1"))
                {
                    col_list1 = col_list1 + term + "_seniority_gained, " + term + "_seniority_lost, " + term + "_seniority_total, ";
                }
            }
            col_list1 = col_list1 + "Total_Marks, TOTAL_Percentage, total_seniority_gained, total_seniority_lost, total_seniority";
            table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type.Replace(" ", "_");
            query = "Select Personal_No, Name, Rank, " + col_list1 + " from " + table_name;

            cmd = new SqlCommand(query, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            adpt.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            con.Close();
        }



        protected void calculateResult()
        {
            string course = ddlCourseType.SelectedValue;
            string course_no = ddlCourseNo.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            con.Open();
            string query = "Select DISTINCT(TERM) from " + course.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + entry_type.Replace(" ", "_") + "_SUBJECTS";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader dt = com.ExecuteReader();
            string term_label = "";
            while (dt.Read())
            {
                term_label = term_label + dt.GetString(0) + "_";
            }
            dt.Close();
            term_label = term_label.Remove(term_label.Length - 1);
            string table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type;
            string query2 = "update " + table_name + " set Total_marks = ";
            string query3 = "update " + table_name + " set Total_seniority_gained = ";
            string query4 = "update " + table_name + " set Total_seniority_lost = ";
            foreach (string term in term_label.Split('_'))
            {


                table_name = course.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + entry_type.Replace(" ", "_") + "_SUBJECTS";
                //string query = "Select Subject_Name from " + table_name;
                //table_name = course.Replace(" ", "_") + "_" + lbTerm.Items[i].Value + "_SENIORITY_CRITERIA";

                SqlCommand cmd = new SqlCommand("select Subject_Name from " + table_name + " where Term = '" + term + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                //string subject_code;
                List<string> column_List = new List<string>();

                string col_name;
                while (dr.Read())
                {
                    col_name = dr.GetValue(0).ToString().Replace(" ", "_");
                    //Response.Write(col_name + i);
                    //i++;

                    column_List.Add(col_name);


                }

                dr.Close();
                string col_List = "";
                string col_List2 = "";
                foreach (string col_nam in column_List)
                {
                    col_List = col_List + ", " + col_nam;
                    col_List2 = col_List2 + col_nam + " + ";

                }
                col_List2 = col_List2.Remove(col_List2.Length - 2);
                //table_name = course_type + "_" + entry_type + "_SUBJECT";
                query = "Select SUM(Max_Marks) from " + table_name + " where term = '" + term + "'";
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                string total_max_marks = "";
                while (dr.Read())
                {
                    total_max_marks = dr[0].ToString();
                }
                dr.Close();
                table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type;
                //query = "select Personal_No, Name, Rank" + col_List + " from " + table_name ;
                //query = "select Personal_No, Name, Rank" + col_List + " as Total , ((" + col_List2 + ")/" + total_max_marks + ")*100 as Percentage from " + table_name;
                query = "update " + table_name + " set " + term + "_total = " + col_List2;
                //Response.Write(query);
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                query = "update " + table_name + " set " + term + "_percentage  = " + term + "_total * 100.0/" + total_max_marks;
                //Response.Write(query);
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                string lower_lmt = "";
                string period = "";
                table_name = "SAILOR_COURSE_TYPE";
                com = new SqlCommand("select seniority from " + table_name + " where TYPE_NAME = '" + ddlCourseType.SelectedValue + "'", con);
                dr = com.ExecuteReader();

                while (dr.Read())
                {
                    seniority = dr.GetValue(0).ToString();
                    //seniority = dr.GetValue(1).ToString();
                }
                dr.Close();
                query2 = query2 + term + "_total + ";
                if (seniority == "1")
                {
                    table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_" + term + "_SENIORITY";

                    query = "Select lower_lmt, seniority from " + table_name + " order by upper_lmt desc";
                    cmd = new SqlCommand(query, con);
                    dr = cmd.ExecuteReader();
                    table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type;
                    query = "update " + table_name + " set " + term + "_Seniority_gained = CASE";
                    string subquery = "";
                    while (dr.Read())
                    {
                        lower_lmt = dr[0].ToString();
                        period = dr[1].ToString();
                        subquery = subquery + " WHEN (" + term + "_percentage" + " > " + lower_lmt + ") THEN " + period;
                    }
                    dr.Close();
                    query = query + subquery + " else " + term + "_Seniority_gained End";
                    //Response.Write(query);
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    query = "update " + table_name + " set " + term + "_Seniority_total = " + term + "_Seniority_gained - " + term + "_Seniority_lost";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    query3 = query3 + term + "_Seniority_gained + ";
                    query4 = query4 + term + "_Seniority_lost + ";
                }




                column_List.Clear();




            }
            query2 = query2.Remove(query2.Length - 2);
            query3 = query3.Remove(query3.Length - 2);
            query4 = query4.Remove(query4.Length - 2);
            com = new SqlCommand(query2, con);
            com.ExecuteNonQuery();
            if (seniority.Equals("1"))
            {
                com = new SqlCommand(query3, con);
                com.ExecuteNonQuery();
                com = new SqlCommand(query4, con);
                com.ExecuteNonQuery();
            }
            table_name = course.Replace(" ", "_") + "_" + course_no + "_" + entry_type;
            query = "update " + table_name + " set total_seniority = total_seniority_gained - total_seniority_lost";
            com = new SqlCommand(query, con);
            com.ExecuteNonQuery();
            con.Close();
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
            string course_type = ddlCourseType.SelectedValue;
            string course_no = ddlCourseNo.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            //string subject = ddlSubject.SelectedValue;
            entry_type = entry_type.Replace(" ", "_");
            string table_name = course_type + "_" + entry_type + "_SUBJECT";
            SqlCommand cmd;
            //Response.Write(table_name + subject +"confirm");
            cmd = new SqlCommand("select Subject_code from " + table_name + " where Subject_Name = '" + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            string subject_code = "";
            while (dr.Read())
            {
                subject_code = lbTerm.SelectedValue + "_" + dr[0].ToString();
            }
            dr.Close();
            //Response.Write(subject_code);

            //SqlCommand cmd = new SqlCommand("If not exists(select name from sysobjects where name = '" + table_name + "') CREATE TABLE " + table_name + "(Personal_No varchar(10), Name varchar(50), Rank varchar(20));", con);
            //cmd.ExecuteNonQuery();
            string query;

            foreach (GridViewRow g1 in GridView1.Rows)
            {
                table_name = course_type + "_" + course_no + "_" + g1.Cells[0].Text;
                query = "update " + table_name + " set " + subject_code + "= " + g1.Cells[4].Text + " where Personal_No = '" + g1.Cells[1].Text + "'";
                //cmd = new SqlCommand("insert into " + table_name + "(Personal_No, Name, Rank) values ('" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "')", con);
                cmd = new SqlCommand(query, con);
                Response.Write(query + "      ");
                //cmd.ExecuteNonQuery();
                i++;
            }
            con.Close();

            string script = "alert(\" " + i + " Trainees Added to " + course_type + course_no + " " + entry_type + " \");";
            ScriptManager.RegisterStartupScript(this, GetType(),
                                  "ServerControlScript", script, true);
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
            con.Open();


            string name = ddlCourseType.SelectedValue;
            SqlCommand com = new SqlCommand("select Course_No from Sailor_Course where Course_Name ='" + name + "'", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
            ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
            ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlCourseNo.DataBind();

            name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_ENTRY_TYPE";
            com = new SqlCommand("select * from " + name, con); // table name 
            da = new SqlDataAdapter(com);
            ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlEntryType.DataBind();

            name = ddlCourseType.Items[0].Value.Replace(" ", "_") + "_ENTRY_TYPE";
            List<string> termLabel = new List<string>();
            string query = "Select DISTINCT(TERM) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
            com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            //string term_label = "";
            while (dr.Read())
            {
                termLabel.Add(dr.GetString(0));
            }
            dr.Close();
            //List<string> termLabel = new List<string>();
            //com = new SqlCommand("select TERM_LABEL from " + name + " where TYPE_NAME = '" + ddlEntryType.Items[0].Value + "'", con); // table name 
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
            lbTerm.DataSource = termLabel.Distinct().ToList();
            lbTerm.DataBind();
            lbTerm.Items.Insert(0, new ListItem("Please Select", "0"));

            con.Close();
            showResult();
        }

        protected void ddlCourseNoIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            String name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_ENTRY_TYPE";
            SqlCommand com = new SqlCommand("select * from" + name, con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
            ddlEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
            ddlEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
            ddlEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
            ddlEntryType.DataBind();

            name = ddlCourseType.Items[0].Value.Replace(" ", "_") + "_ENTRY_TYPE";
            List<string> termLabel = new List<string>();
            string query = "Select DISTINCT(TERM) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
            com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            //string term_label = "";
            while (dr.Read())
            {
                termLabel.Add(dr.GetString(0));
            }
            dr.Close();
            //List<string> termLabel = new List<string>();
            //com = new SqlCommand("select TERM_LABEL from " + name + " where TYPE_NAME = '" + ddlEntryType.Items[0].Value + "'", con); // table name 
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
            lbTerm.DataSource = termLabel.Distinct().ToList();
            lbTerm.DataBind();
            lbTerm.Items.Insert(0, new ListItem("Please Select", "0"));

            con.Close();
            showResult();

        }
        protected void ddlTermIndexChanged(object sender, EventArgs e)
        {
        }


        protected void ddlEntryTypeIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string term = lbTerm.SelectedValue;
            string entry_type = ddlEntryType.SelectedValue;
            entry_type = entry_type.Replace(" ", "_");
            string name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + entry_type + "_SUBJECTS";
            SqlCommand com = new SqlCommand("select Subject_Name from " + name + " where Term ='" + term + "'", con); // table name 
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset
                          //ddlSubject.DataTextField = ds.Tables[0].Columns["Subject_Name"].ToString(); // text field name of table dispalyed in dropdown
                          //ddlSubject.DataValueField = ds.Tables[0].Columns["Subject_Name"].ToString();             // to retrive specific  textfield name 
                          //ddlSubject.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                          //ddlSubject.DataBind();
            name = ddlCourseType.Items[0].Value.Replace(" ", "_") + "_ENTRY_TYPE";
            List<string> termLabel = new List<string>();
            string query = "Select DISTINCT(TERM) from " + ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + ddlEntryType.SelectedValue.Replace(" ", "_") + "_SUBJECTS";
            com = new SqlCommand(query, con);
            SqlDataReader dr = com.ExecuteReader();
            //string term_label = "";
            while (dr.Read())
            {
                termLabel.Add(dr.GetString(0));
            }
            dr.Close();
            //List<string> termLabel = new List<string>();
            //com = new SqlCommand("select TERM_LABEL from " + name + " where TYPE_NAME = '" + ddlEntryType.Items[0].Value + "'", con); // table name 
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
            lbTerm.DataSource = termLabel.Distinct().ToList();
            lbTerm.DataBind();
            lbTerm.Items.Insert(0, new ListItem("Please Select", "0"));
            con.Close();
            showResult();
        }
    }
}