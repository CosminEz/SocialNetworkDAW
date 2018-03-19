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
    public partial class FriendRequests : System.Web.UI.Page
    {

        protected string idToUserName(string id)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
            con.Open();
            string queryName = "Select UserName from AspNetUsers where Id=@IdSender";
            SqlCommand comName = new SqlCommand(queryName, con);
            comName.Parameters.AddWithValue("IdSender", id);

            return (string)comName.ExecuteScalar();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["filter"];

            string query = "SELECT SenderId FROM Friends WHERE ReceiverId like '%' + @ReceiverId + '%' AND Status like 'Requested' ";
            
            

            SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("ReceiverId", id);

            
   
            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string idSender = reader.GetString(0);


                    HyperLink myHyperLink = new HyperLink();
                    myHyperLink.Text = idToUserName(idSender);
                    myHyperLink.NavigateUrl = "~/Account/Profile.aspx?id="+idSender;
 
                    div1.Controls.Add(myHyperLink);
                    Button Accept = new Button();
                    Accept.Text = "Accept";
                    Accept.Click += delegate
                    {
                        SqlCommand cmdUpdateStatus = new SqlCommand("update Friends set Status='Accepted' where SenderId=@id", con);
                        cmdUpdateStatus.Parameters.AddWithValue("@id", idSender);
                        cmdUpdateStatus.ExecuteNonQuery();
                    };
                    div1.Controls.Add(new LiteralControl(" "));
                    div1.Controls.Add(Accept);
                    Button Refuse = new Button();
                    Refuse.Text = "Refuse";
                    Refuse.Click += delegate
                     {

                         SqlCommand cmdDeleteStatus = new SqlCommand("delete from Friends  where SenderId=@id", con);
                         cmdDeleteStatus.Parameters.AddWithValue("@id", idSender);
                         cmdDeleteStatus.ExecuteNonQuery();

                     };
                    div1.Controls.Add(new LiteralControl(" "));
                    div1.Controls.Add(Refuse);
                    //Accept.Click = accept(idSender);
                    div1.Controls.Add(new LiteralControl("<br />"));




                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();


        }

       
    }
}