using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Skill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSkills();
        }
    }

    private void BindSkills()
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["Pubs"].ConnectionString;

        // Check if InterestID exists in query string
        if (Request.QueryString["InterestID"] == null)
        {
            lblerr.Text = "Invalid Interest.";
            return;
        }

        int interestID = Convert.ToInt32(Request.QueryString["InterestID"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string selectSQL = "SELECT SkillID, Skill FROM Skills WHERE InterestID = @InterestID ";

            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Parameters.AddWithValue("@InterestID", interestID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                // Bind Skills to CheckBoxList
                CheListSkill.DataSource = reader;
                CheListSkill.DataTextField = "Skill";     // Display skill name
                CheListSkill.DataValueField = "SkillID";  // Store SkillID
                CheListSkill.DataBind();

                reader.Close();
            }
            catch (Exception err)
            {
                lblerr.Text = "Error loading skills: " + err.Message;
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        List<string> selectedSkills = new List<string>();

        // Loop through all checked items
        foreach (ListItem item in CheListSkill.Items)
        {
            if (item.Selected)
            {
                selectedSkills.Add(item.Value); // SkillID
            }
        }

        if (selectedSkills.Count == 0)
        {
            lblerr.Text = "Please select at least one skill.";
            return;
        }

        // Convert list to comma-separated string
        string skillIDs = string.Join(",", selectedSkills);

        // Redirect with skill ids
        Response.Redirect("Career.aspx?skills=" + skillIDs);
    
    }
}
