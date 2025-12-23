using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Admin : System.Web.UI.Page
{
    protected StringBuilder sbTableData = new StringBuilder();
    string connectionString = WebConfigurationManager.ConnectionStrings["Pubs"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Session["isLogin"] == null) || (Session["isLogin"].ToString() != "yes"))       // Check For Valid Login
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            ViewState["IsAdd"] = false;
            ViewState["IsEdit"] = false;
            // Show Last Record
            btnLast_Click(sender, e);
        }
        LoadInterest();

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        txtInt.Text = "";
        txtIntID.Text = "";
        MyEnabled(true);

        ViewState["IsAdd"] = true;
        this.SetFocus(txtInt.ClientID);
    }
    private void MyEnabled(bool plEnabled)
    {
        txtInt.Enabled = plEnabled;
        btnSave.Enabled = plEnabled;
        btnCancel.Enabled = plEnabled;

        btnFirst.Enabled = !plEnabled;
        btnLast.Enabled = !plEnabled;
        btnPrev.Enabled = !plEnabled;
        btnNext.Enabled = !plEnabled;
        btnEdit.Enabled = !plEnabled;
        btnDel.Enabled = !plEnabled;
        btnNew.Enabled = !plEnabled;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((bool)ViewState["IsAdd"] == true)
        {
            if (txtInt.Text == "" )
            {
                lblErr.Text = "Intrest Name is missing.";
                return;

            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string strSQLNewID = "SELECT ISNULL((SELECT TOP 1 InterestID FROM Interest ORDER BY InterestID DESC), 0) AS InterestID ";
                SqlCommand cmdNewID = new SqlCommand(strSQLNewID, con);
                int NewInterestID = 1;
                string strSQL = @"INSERT INTO Interest (InterestID,Interest) 
                         VALUES (@InterestID, @Interest)";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@Interest", txtInt.Text.Trim());
                try
                {
                    con.Open();

                    NewInterestID = (int)cmdNewID.ExecuteScalar();
                    NewInterestID += 1;

                        cmd.Parameters.AddWithValue("@InterestID", NewInterestID);

                        int added = cmd.ExecuteNonQuery();
                        lblErr.Text = added + " record saved successfully.";

                        txtIntID.Text = NewInterestID.ToString();

                        ViewState["IsAdd"] = false;
                        MyEnabled(false);
                    
                }
                catch (Exception ex)
                {
                    lblErr.Text = "Error inserting record: " + ex.Message;
                }
            }
        }

        if ((bool)ViewState["IsEdit"] == true)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Interest SET Interest = @Interest WHERE InterestID = @InterestID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@InterestID", txtIntID.Text.Trim());
                cmd.Parameters.AddWithValue("@Interest", txtInt.Text.Trim());

                try
                {
                    con.Open();
                    int added = cmd.ExecuteNonQuery();
                    lblErr.Text = added + " record updated successfully.";
                    ViewState["IsEdit"] = false;
                    MyEnabled(false);

                }
                catch (Exception ex)
                {
                    lblErr.Text = "Error updateing record: " + ex.Message;
                }
            }
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["IsEdit"] = false;
        ViewState["IsAddt"] = false;
        MyEnabled(false);

        lblErr.Text = "";

        string strSQL = "SELECT * FROM Interest WHERE InterestID = " + ViewState["InterestID"];
        MyShowSQLData(strSQL, "");

    }
    private void MyShowSQLData(string pcSQL, string pcShowErrMsg)
    {
        // sql command to show Data
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(pcSQL, con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtIntID.Text = reader["InterestID"].ToString();
                    txtInt.Text = reader["Interest"].ToString();

                    ViewState["InterestID"] = int.Parse(txtIntID.Text);
                }
                else
                {
                    lblErr.Text = pcShowErrMsg;
                }
                reader.Close();
            }
            catch (Exception err)
            {
                lblErr.Text = "Error In Data: " + err.Message;// + "<br />" + pcSQL;
            }
            finally
            {
                con.Close();
            }
        }
    }

    protected void btnLast_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Interest ORDER BY InterestID DESC ";
        MyShowSQLData(strSQL, "");
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Interest ORDER BY InterestID ";
        MyShowSQLData(strSQL, "");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Interest WHERE InterestID > " + ViewState["InterestID"] + " ORDER BY InterestID";
        MyShowSQLData(strSQL, "Can't Move End Of Data.");
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Interest WHERE InterestID < " + ViewState["InterestID"] + " ORDER BY InterestID DESC ";
        MyShowSQLData(strSQL, "Can't Move Begin Of Data.");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["IsEdit"] = true;
        MyEnabled(true);

        this.SetFocus(txtInt.ClientID);
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Interest WHERE InterestID = @InterestID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@InterestID", txtIntID.Text.Trim());

            try
            {
                con.Open();
                int deleted = cmd.ExecuteNonQuery();
                lblErr.Text = deleted + " record(s) deleted successfully.";
                btnNext_Click(sender, e);
                if (lblErr.Text == "Can't Move End Of Data.")
                {
                    lblErr.Text = "";
                    btnLast_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = "Error deleting record: " + ex.Message;
            }
        }

    }
    private void LoadInterest()
    {
        string conString = WebConfigurationManager.ConnectionStrings["Pubs"].ConnectionString;
        string sql = "SELECT * FROM Interest";   // Always load all records

        SqlConnection con1 = new SqlConnection(conString);
        SqlCommand cmd = new SqlCommand(sql, con1);
        SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();

        using (con1)
        {
            try
            {
                con1.Open();
                sda1.Fill(dt1);
            }
            catch (Exception ex)
            {
                lblErr.Text = "Error In Data: " + ex.Message;
            }
        }

        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow row1 in dt1.Rows)
            {
                sbTableData.Append("<tr>").AppendFormat(
                    "<td>{0}</td>", row1["InterestID"]).AppendFormat(
                    "<td>{0}</td>", row1["Interest"]).AppendFormat(
                    "</tr>");
            }
        }
        else
        {
            lblErr.Text = "No records found";
        }
    }

}