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
    public partial class ViewTrainees1 : System.Web.UI.Page
    {
        string coursename;

        string table_name = "";
        string personal_no;
        int flag;
        private SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {



            if (!this.IsPostBack)
            {
                if (Request.QueryString["coursename"] != null)
                {
                    heading.InnerHtml = Request.QueryString["coursename"] + " Trainees Details";
                    ddlCourseType.Visible = false;
                    ddlCourseType.EnableViewState = false;
                    coursename = Request.QueryString["coursename"];


                }
                flag = 0;
                con.Open();
                ddlCourseType.SelectedIndex = 0;
                ddlCourseNo.SelectedIndex = 0;
                lbEntryType.SelectedIndex = 0;

                SqlCommand com = new SqlCommand("select Distinct Course_Name from SAILOR_COURSE", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseType.DataTextField = ds.Tables[0].Columns["Course_NAME"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseType.DataValueField = ds.Tables[0].Columns["Course_NAME"].ToString();             // to retrive specific  textfield name 
                ddlCourseType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseType.DataBind();
                ddlCourseType.Items.Insert(0, new ListItem("Select", "0"));
                ddlCourseType.SelectedIndex = 0;
                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = false;

                //if (coursename == null)
                //    coursename = ddlCourseType.SelectedValue;
                //com = new SqlCommand("select Course_No from Sailor_Course where Course_Name = '" + coursename + "'", con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                //ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                //ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //ddlCourseNo.DataBind();




                //com = new SqlCommand("select * from " + coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + "ENTRY_TYPE", con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                //lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                //lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //lbEntryType.DataBind();
                //lbEntryType.Items.Insert(0, new ListItem("All", "0"));
                //con.Close();

                //lbEntryType.SelectedIndex = 0;
                //ShowData();
            }
            else
            {

            }

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {

        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {

        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            reset();
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
            string FileName = ddlCourseType.SelectedValue.Replace(" ", "_") + ddlCourseNo.SelectedValue + "_" + lbEntryType.SelectedValue.Replace(" ", "_") + ".xls";
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
            if (lbEntryType.SelectedItem.Text.Equals("All"))
            {
                GridView1.GridLines = GridLines.Both;
                GridView1.HeaderStyle.Font.Bold = true;
                GridView1.RenderControl(htmltextwrtter);
            }
            else {
                GridView3.Columns[4].Visible = false;
                GridView3.GridLines = GridLines.Both;
                GridView3.HeaderStyle.Font.Bold = true;
                GridView3.RenderControl(htmltextwrtter);
            }
            Response.Write("<B>");
            Response.Write(ddlCourseType.SelectedValue + " " + ddlCourseNo.SelectedValue + " " + lbEntryType.SelectedValue);
            Response.Write("</B>");
            Response.Write(strwritter.ToString());
            Response.End();
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
                div3.Visible = false;
                div2.Visible = false;
                con.Open();

                //if (coursename == null)
                coursename = ddlCourseType.SelectedValue;


                SqlCommand com = new SqlCommand("select Course_No from Sailor_Course where Course_Name = '" + coursename + "'", con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                ddlCourseNo.DataTextField = ds.Tables[0].Columns["Course_No"].ToString(); // text field name of table dispalyed in dropdown
                ddlCourseNo.DataValueField = ds.Tables[0].Columns["Course_No"].ToString();             // to retrive specific  textfield name 
                ddlCourseNo.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                ddlCourseNo.DataBind();
                ddlCourseNo.Items.Insert(0, new ListItem("Select", "0"));

                //string name = coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + "ENTRY_TYPE";
                //com = new SqlCommand("select * from " + name, con); // table name 
                //da = new SqlDataAdapter(com);
                //ds = new DataSet();
                //da.Fill(ds);  // fill dataset
                //lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                //lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                //lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                //lbEntryType.DataBind();
                //lbEntryType.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
                //ShowData();
            
        }
        //GridView2.DataSource = dt;
        //GridView2.DataBind();



    }

    protected void ddlCourseNoIndexChanged(object sender, EventArgs e)
    {
            ddlCourseNo.Items.Remove(ddlCourseNo.Items.FindByValue("0"));
            if (ddlCourseNo.SelectedValue.Equals("0"))
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

                if (coursename == null)
                    coursename = ddlCourseType.SelectedValue;



                string name = coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + "ENTRY_TYPE";
                SqlCommand com = new SqlCommand("select * from " + name, con); // table name 
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);  // fill dataset
                lbEntryType.DataTextField = ds.Tables[0].Columns["TYPE_NAME"].ToString(); // text field name of table dispalyed in dropdown
                lbEntryType.DataValueField = ds.Tables[0].Columns["TYPE_NAME"].ToString();             // to retrive specific  textfield name 
                lbEntryType.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
                lbEntryType.DataBind();
                lbEntryType.Items.Insert(0, new ListItem("All", "0"));

                con.Close();
                ShowData();
            }

        //GridView2.DataSource = dt;
        //GridView2.DataBind();



    }

    protected void lblEntryTypeIndexChanged(object sender, EventArgs e)
    {
        ShowData();
    }



    protected void txtCourseName_TextChanged(object sender, EventArgs e)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(txtSubject.Text, "[^a-zA-Z0-9\x20]", System.Text.RegularExpressions.RegexOptions.IgnoreCase) & txtSubject.Text[0] != ' ' & txtSubject.Text[0] != '\t')
        {
            con.Open();
            string table_name = ddlCourseType.SelectedValue.Replace(" ", "_") + "_" + lbEntryType.SelectedValue.Replace(" ", "_") + "_" + "SUBJECTS";
            //Response.Write(table_name);
            SqlCommand com = new SqlCommand("select Count(SUBJECT_NAME) from " + table_name + " where SUBJECT_NAME = '" + txtSubject.Text + "'", con); // table name 
            int count = (int)com.ExecuteScalar();
            if (count == 1)
            {
                string script = "alert(\" Course Name Already Exists \");";
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
    }

    protected void ShowData()
    {
        string query;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        if (coursename == null)
            coursename = ddlCourseType.SelectedValue.Replace(" ", "_");
        con.Open();

        if (lbEntryType.SelectedItem.Text.Equals("All"))
        {
            query = "select Type_name from " + coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_Entry_Type where Enrolledin IS Not NULL";
            cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            query = "";
            while (dr.Read())
            {
                query = query + "Select Personal_No as \"Personal No\", Name, Rank from " + coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + dr.GetString(0).Replace(" ", "_") + " UNION ";
            }
            dr.Close();
            query = query.Substring(0, query.LastIndexOf("UNION"));
            cmd = new SqlCommand(query, con);
            adpt = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adpt.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView3.Visible = false;
            GridView3.EnableViewState = false;
            GridView1.Visible = true;
            GridView1.EnableViewState = true;
        }
        else {
            table_name = coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_");
            query = "If exists(select name from sysobjects where name = '" + table_name + "') Select Personal_No as \"ID\", Personal_No as \"Personal No\", Name, Rank from " + table_name;
            //Response.Write(query);
            cmd = new SqlCommand(query, con);
            adpt = new SqlDataAdapter(cmd);
            dt = new DataTable();
            adpt.Fill(dt);

            //if (dt.Rows.Count > 0)
            //{
            //exlfile.Visible = false;
            //single.Visible = true;
            GridView3.DataSource = dt;
            GridView3.DataBind();
            GridView3.Visible = true;
            GridView3.EnableViewState = true;
            GridView1.Visible = false;
            GridView1.EnableViewState = false;
            flag = 1;
            SubmitButton.Text = "Add";
        }
        Button1.Visible = true;
        Button1.EnableViewState = true;


        con.Close();
    }

    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        //NewEditIndex property used to determine the index of the row being edited. 
        //Label id = GridView3.Rows[e.NewEditIndex].FindControl("lbl_No") as Label;
        //personal_no = id.Text;
        GridView3.EditIndex = e.NewEditIndex;
        ShowData();
        //TextBox id = (TextBox)GridView1.Rows[e.NewEditIndex].FindControl("txt_No");
        //personal_no = id.Text;
    }
    protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        if (coursename == null)
            coursename = ddlCourseType.Items[0].Value.Replace(" ", "_");
        string pno = personal_no;
        //Finding the controls from Gridview for the row which is going to update
        Label id1 = GridView3.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
        TextBox id = GridView3.Rows[e.RowIndex].FindControl("txt_No") as TextBox;
        TextBox name = GridView3.Rows[e.RowIndex].FindControl("txt_Name") as TextBox;
        TextBox marks = GridView3.Rows[e.RowIndex].FindControl("txt_Rank") as TextBox;
        con.Open();
        //updating the record  
        table_name = coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_");
        SqlCommand cmd = new SqlCommand("Update " + table_name + " set Personal_No ='" + id.Text + "', Name='" + name.Text + "', Rank ='" + marks.Text + "' where Personal_no = '" + id1.Text + "'", con);
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
            coursename = ddlCourseType.SelectedValue;
        Label id = GridView3.Rows[e.RowIndex].FindControl("lbl_No") as Label;
        table_name = coursename.Replace(" ", "_") + "_" + ddlCourseNo.SelectedValue + "_" + lbEntryType.SelectedItem.Text.Replace(" ", "_");
        con.Open();

        SqlCommand cmd = new SqlCommand("delete FROM " + table_name + " where Personal_No = '" + id.Text + "'", con);
        cmd.ExecuteNonQuery();
        con.Close();
        ShowData();
    }


}
}
