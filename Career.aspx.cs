using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Career : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["skills"] != null)
            {
                LoadCareers(Request.QueryString["skills"]);
            }
            else
            {
                lblerr.Text = "No skills selected.";
            }
        }
    }

    private void LoadCareers(string skillIDs)
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["Pubs"].ConnectionString;

        // Validate query string
        if (string.IsNullOrEmpty(skillIDs))
        {
            lblerr.Text = "Invalid Skill.";
            return;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // Build dynamic IN clause safely
            string selectSQL = $@"
            SELECT TOP 1 Career, Description, Photo 
            FROM Skills 
            WHERE SkillID IN ({skillIDs})
            ORDER BY Rank ASC";

            SqlCommand cmd = new SqlCommand(selectSQL, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblName.Text = reader["Career"].ToString();
                    lblDes.Text = reader["Description"].ToString().Replace("\n", "<br />");
                    string photoName = reader["Photo"].ToString();

                    if (!string.IsNullOrEmpty(photoName))
                        Image1.ImageUrl = "~/Images/" + photoName;
                    else
                        Image1.ImageUrl = "Images/Sample.jpg"; // fallback

                    lblerr.Text = "";
                }
                else
                {
                    lblName.Text = "Career not found.";
                }

                reader.Close();
            }
            catch (Exception err)
            {
                lblerr.Text = "Error loading Career: " + err.Message;
            }
        }
    }
}