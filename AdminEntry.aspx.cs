using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class AdminEntry : System.Web.UI.Page
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
            LoadCareer();
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        txtID.Text = "";
        txtIntID.Text = "";
        txtSkill.Text = "";
        txtCareer.Text = "";
        txtDes.Text = "";
        txtRank.Text = "";
        Image1.ImageUrl = "";
        MyEnabled(true);

        ViewState["IsAdd"] = true;
        this.SetFocus(txtIntID.ClientID);
    }
    private void MyEnabled(bool plEnabled)
    {
        txtIntID.Enabled = plEnabled;
        txtSkill.Enabled = plEnabled;
        txtCareer.Enabled = plEnabled;
        txtDes.Enabled = plEnabled;
        txtRank.Enabled = plEnabled;
        fupPic.Enabled = plEnabled;
        btnSave.Enabled = plEnabled;
        btnCancel.Enabled = plEnabled;

        btnFirst.Enabled = !plEnabled;
        btnLast.Enabled = !plEnabled;
        btnPrev.Enabled = !plEnabled;
        btnNext.Enabled = !plEnabled;
        btnEdit.Enabled = !plEnabled;
        btnDel.Enabled = !plEnabled;
        btnNew.Enabled = !plEnabled;
        txtSearchID.Enabled = !plEnabled;
        btnSearch.Enabled = !plEnabled;
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((bool)ViewState["IsAdd"] == true)
        {
            if (txtIntID.Text == "" || txtSkill.Text == "" || txtCareer.Text == "" || txtRank.Text == "")
            {
                lblErr.Text = "Interest ID or Skill Name or Rank or Career Name is missing.";
                return;
            }

            if (!fupPic.HasFile)
            {
                lblErr.Text = "Please upload an image.";
                return;
            }

            string ext = Path.GetExtension(fupPic.FileName).ToLower();
            if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
            {
                lblErr.Text = "Only .png, .jpg, .jpeg files are allowed";
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Check for duplicate rank for the given interest
                string strSQLDupRank = "SELECT COUNT(*) FROM Skills WHERE Rank = @Rank AND InterestID = @InterestID";
                SqlCommand cmdDupRank = new SqlCommand(strSQLDupRank, con);
                cmdDupRank.Parameters.AddWithValue("@Rank", txtRank.Text.Trim());
                cmdDupRank.Parameters.AddWithValue("@InterestID", txtIntID.Text.Trim());

                // Generate new SkillID
                string strSQLNewID = "SELECT ISNULL((SELECT TOP 1 SkillID FROM Skills ORDER BY SkillID DESC), 0) FROM Skills";
                SqlCommand cmdNewID = new SqlCommand(strSQLNewID, con);

                // Insert record
                string strSQL = @"INSERT INTO Skills (Rank, SkillID, Skill, Career, Description, Photo, InterestID) 
                          VALUES (@Rank, @SkillID, @Skill, @Career, @Description, @Photo, @InterestID)";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@Rank", txtRank.Text.Trim());
                cmd.Parameters.AddWithValue("@Skill", txtSkill.Text.Trim());
                cmd.Parameters.AddWithValue("@Career", txtCareer.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDes.Text.Trim());
                cmd.Parameters.AddWithValue("@InterestID", txtIntID.Text.Trim());

                try
                {
                    con.Open();

                    int duplicateCount = (int)cmdDupRank.ExecuteScalar();
                    if (duplicateCount > 0)
                    {
                        lblErr.Text = "Can't Save, this Rank is already used for the selected Interest.";
                        return;
                    }

                    int NewSkillID = (int)cmdNewID.ExecuteScalar() + 1;

                    // Save image
                    string fileName = "Career_" + NewSkillID + ext;
                    string savePath = @"D:\Amruta\VS_CS\CareerGuidance\Images\" + fileName;
                    fupPic.SaveAs(savePath);

                    cmd.Parameters.AddWithValue("@SkillID", NewSkillID);
                    cmd.Parameters.AddWithValue("@Photo", fileName);

                    int added = cmd.ExecuteNonQuery();
                    lblErr.Text = added + " record saved successfully.";

                    txtID.Text = NewSkillID.ToString();
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
                string updateQuery = "UPDATE Skills SET Rank=@Rank, Skill = @Skill, Career = @Career, Description = @Description, InterestID = @InterestID";

                bool newImageUploaded = fupPic.HasFile;
                string ext = "";
                string fileName = "";

                if (newImageUploaded)
                {
                    ext = Path.GetExtension(fupPic.FileName).ToLower();
                    if (ext != ".png" && ext != ".jpg" && ext != ".jpeg")
                    {
                        lblErr.Text = "Only .png, .jpg, .jpeg files are allowed.";
                        return;
                    }

                    fileName = "Career_" + txtID.Text.Trim() + ext;  // Use SAME ID for filename
                    updateQuery += ", Photo = @Photo";
                }

                updateQuery += " WHERE SkillID = @SkillID";

                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@SkillID", txtID.Text.Trim());
                cmd.Parameters.AddWithValue("@Rank", txtRank.Text.Trim());
                cmd.Parameters.AddWithValue("@Skill", txtSkill.Text.Trim());
                cmd.Parameters.AddWithValue("@Career", txtCareer.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDes.Text.Trim());
                cmd.Parameters.AddWithValue("@InterestID", txtIntID.Text.Trim());

                if (newImageUploaded)
                {
                    cmd.Parameters.AddWithValue("@Photo", fileName);
                }

                // 🔍 Duplicate Rank check — exclude current record
                string dupCheckQuery = @"SELECT COUNT(*) FROM Skills 
                                 WHERE Rank = @Rank 
                                 AND InterestID = @InterestID 
                                 AND SkillID <> @SkillID";

                SqlCommand cmdDup = new SqlCommand(dupCheckQuery, con);
                cmdDup.Parameters.AddWithValue("@Rank", txtRank.Text.Trim());
                cmdDup.Parameters.AddWithValue("@InterestID", txtIntID.Text.Trim());
                cmdDup.Parameters.AddWithValue("@SkillID", txtID.Text.Trim());

                try
                {
                    con.Open();

                    // ✔ Check duplicates BEFORE update
                    int count = (int)cmdDup.ExecuteScalar();
                    if (count > 0)
                    {
                        lblErr.Text = "Can't update, this Rank is already used for the selected Interest.";
                        return;
                    }

                    // ✔ Perform UPDATE
                    int updated = cmd.ExecuteNonQuery();

                    // ✔ Save image only if a new image was uploaded
                    if (newImageUploaded)
                    {
                        string savePath = @"D:\Amruta\VS_CS\CareerGuidance\Images\" + fileName;
                        fupPic.SaveAs(savePath);
                    }

                    lblErr.Text = updated + " record updated successfully.";
                    ViewState["IsEdit"] = false;
                    MyEnabled(false);
                }
                catch (Exception ex)
                {
                    lblErr.Text = "Error updating record: " + ex.Message;
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

        string strSQL = "SELECT * FROM Skills WHERE SkillID = " + ViewState["SkillID"];
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
                    txtID.Text = reader["SkillID"].ToString();
                    txtRank.Text = reader["Rank"].ToString();
                    txtSkill.Text = reader["Skill"].ToString();
                    txtCareer.Text = reader["Career"].ToString();
                    txtDes.Text = reader["Description"].ToString();
                    txtIntID.Text = reader["InterestID"].ToString();

                    ViewState["SkillID"] = int.Parse(txtID.Text);
                    string photoName = reader["Photo"].ToString();

                    if (!string.IsNullOrEmpty(photoName))
                        Image1.ImageUrl = "~/Images/" + photoName;
                    else
                        Image1.ImageUrl = "Images/Sample.jpg"; // fallback

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
        string strSQL = "SELECT TOP 1 * FROM Skills ORDER BY SkillID DESC ";
        MyShowSQLData(strSQL, "");
    }

    protected void btnFirst_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Skills ORDER BY SkillID ";
        MyShowSQLData(strSQL, "");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Skills WHERE SkillID > " + ViewState["SkillID"] + " ORDER BY SkillID";
        MyShowSQLData(strSQL, "Can't Move End Of Data.");
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT TOP 1 * FROM Skills WHERE SkillID < " + ViewState["SkillID"] + " ORDER BY SkillID DESC ";
        MyShowSQLData(strSQL, "Can't Move Begin Of Data.");
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["IsEdit"] = true;
        MyEnabled(true);

        this.SetFocus(txtIntID.ClientID);
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // STEP 1: Get photo file name before deleting record
            string getPhotoQuery = "SELECT Photo FROM Skills WHERE SkillID = @SkillID";
            SqlCommand cmdGet = new SqlCommand(getPhotoQuery, con);
            cmdGet.Parameters.AddWithValue("@SkillID", txtID.Text.Trim());

            string photoName = "";

            try
            {
                con.Open();
                object result = cmdGet.ExecuteScalar();
                if (result != null)
                    photoName = result.ToString();
            }
            catch (Exception ex)
            {
                lblErr.Text = "Error while reading photo: " + ex.Message;
                return;
            }
            finally
            {
                con.Close();
            }

            // STEP 2: Delete database row
            string deleteQuery = "DELETE FROM Skills WHERE SkillID = @SkillID";
            SqlCommand cmdDelete = new SqlCommand(deleteQuery, con);
            cmdDelete.Parameters.AddWithValue("@SkillID", txtID.Text.Trim());

            try
            {
                con.Open();
                int deleted = cmdDelete.ExecuteNonQuery();

                if (deleted > 0)
                {
                    lblErr.Text = deleted + " record(s) deleted successfully.";

                    // STEP 3: Delete photo file from disk (only if exists)
                    if (!string.IsNullOrEmpty(photoName))
                    {
                        string filePath = @"D:\Amruta\VS_CS\CareerGuidance\Images\" + photoName;

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                    }
                }

                // Move to next or last record
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
    private void LoadCareer(string searchCondition = "")
    {
        if (searchCondition != "") { searchCondition = " WHERE " + searchCondition; }
        string conString = WebConfigurationManager.ConnectionStrings["Pubs"].ConnectionString;
        string sql = "SELECT TOP 10 * FROM Skills " + searchCondition;
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
                    "<td>{0}</td>", row1["SkillID"]).AppendFormat(
                    "<td>{0}</td>", row1["Skill"]).AppendFormat(
                    "<td>{0}</td>", row1["Career"]).AppendFormat(
                    "<td>{0}</td>", row1["Description"]).AppendFormat(
                    "<td>{0}</td>", row1["Photo"]).AppendFormat(
                    "<td>{0}</td>", row1["InterestID"]).AppendFormat(
                    "</tr>");
            }
            // lblErr.Text = "";
        }
        else
        {
            lblErr.Text = "No records found";
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        String strCondition = "";
        if (txtSearchID.Text.Trim().Length > 0)
        {
            strCondition += " (InterestID LIKE '%" + txtSearchID.Text.Trim() + "%') ";
            string InterestID = txtSearchID.Text.Trim();
            txtSearchID.Text = "";
            string strSQL = "SELECT TOP 1 * FROM Skills WHERE InterestID = " + InterestID;
            MyShowSQLData(strSQL, "Not Available ID " + InterestID);
        }
        LoadCareer(strCondition);
    }
}