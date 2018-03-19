using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace DAWprojectSocialNetwork.Account
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.QueryString["filter"];

            string query = "SELECT Id,UserName from AspNetUsers where (UserName like '%' + @UserName + '%')" ;
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("UserName", name);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListItem li = new ListItem(reader.GetString(1), "~/Account/Profile.aspx?id=" + reader.GetString(0));
                    UsersList.Items.Add(li);
                    
                    
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            con.Close();

            string queryGroups = "  SELECT Id, Name from Groups where (Name like  '%' + @groupName + '%') ";

            SqlCommand comGroups = new SqlCommand(queryGroups, con);
            con.Open();

            comGroups.Parameters.AddWithValue("groupName",name);


            SqlDataReader reader1 = comGroups.ExecuteReader();

            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    ListItem li = new ListItem(reader1.GetString(1), "~/Account/Group.aspx?id=" + reader1.GetInt32(0));
                    GroupsList.Items.Add(li);


                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            con.Close();









            con.Close();

        }
    }
}