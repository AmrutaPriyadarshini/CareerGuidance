using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected Dictionary<int, string> dctInterest = new Dictionary<int, string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["Pubs"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string selectSQL = "SELECT * FROM Interest ";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            SqlDataReader reader1;

            try
            {
                con.Open();
                reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    dctInterest.Add(Convert.ToInt32(reader1["InterestID"]), reader1["Interest"].ToString());
                }

                reader1.Close();
            }
            catch (Exception err)
            {
                lblerr.Text = "Error getting Data: " + err.Message;
            }
        }
    }
}