using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;


namespace DAWprojectSocialNetwork.Account
{
    public partial class Profile : System.Web.UI.Page
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

        protected string idToPhotoUrl(string id)
        {
            String ImageUrl;
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");

            string query = "SELECT ProfilePhoto from AspNetUsers where (Id like '%' + @Id + '%')";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("Id", id);
            if (com.ExecuteScalar().Equals(DBNull.Value))
            {
                con.Close();
                ImageUrl = "~/Images/ProfilePhoto.jpg";
            }
            else
            {

                byte[] bytes = (byte[])com.ExecuteScalar();
                
                con.Close();


                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ImageUrl = "data:image/jpeg;base64," + base64String;

            }

            return ImageUrl;



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FriendRequest.Visible = false;
            FriendRequestsButton.Visible = false;
            divFriends.Visible = false;
            Albums.Visible = false;

            string id = Request.QueryString["id"];

            SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
            con.Open();

            string queryName = "Select UserName from AspNetUsers where Id=@Id";
            SqlCommand comName = new SqlCommand(queryName, con);
            comName.Parameters.AddWithValue("Id", id);

           
            NameProfileText.Text = (string)comName.ExecuteScalar();

            con.Close();

            

            

            string query = "SELECT ProfilePhoto from AspNetUsers where (Id like '%' + @Id + '%')";
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("Id", id);
            if (com.ExecuteScalar().Equals(DBNull.Value))
            {
                con.Close();
                ProfileImage.ImageUrl = "~/Images/ProfilePhoto.jpg";
            }
            else
            {

                byte[] bytes = (byte[])com.ExecuteScalar();
                con.Close();


                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                ProfileImage.ImageUrl = "data:image/jpeg;base64," + base64String;

            }

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                

                string query1 = "SELECT Id from AspNetUsers where (UserName  like   @UserName )";
                con.Open();
                SqlCommand com1 = new SqlCommand(query1, con);
                com1.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name); 
                string idLogat = (string)com1.ExecuteScalar();
                con.Close();

                con.Open();

                string queryStatus = " IF EXISTS (SELECT * from Friends where   (SenderId = @SenderId AND ReceiverId = @ReceiverId) OR " +
                " (SenderId = @ReceiverId AND ReceiverId = @SenderId) ) " +
                " SELECT Status from Friends where   (SenderId = @SenderId AND ReceiverId = @ReceiverId) OR " +
                " (SenderId = @ReceiverId AND ReceiverId = @SenderId) " +
                " ELSE SELECT NULL ";

                SqlCommand comStatus = new SqlCommand(queryStatus, con);
                comStatus.Parameters.AddWithValue("SenderId", id);
                comStatus.Parameters.AddWithValue("ReceiverId", idLogat);
                //string Status = (string)comStatus.ExecuteScalar();

                // onAnotherProfile
                if (comStatus.ExecuteScalar().Equals(DBNull.Value) && !(idLogat.Equals(id)))
                {
                    
                      FriendRequest.Visible = true;
                      
                }
                else
                // onAnotherFriendAcceptedProfile
                if (!(idLogat.Equals(id)) && ((string)comStatus.ExecuteScalar()).Equals("Accepted"))
                {
                    FriendRequest.Visible = false;
                    Label label = new Label();
                    label.Text = "You are already friend with " + NameProfileText.Text;
                    div1.Controls.Add(label);
                    Albums.Visible = true;



                }
                else

                if (!(idLogat.Equals(id)) && ((string)comStatus.ExecuteScalar()).Equals("Requested"))
                {
                    FriendRequest.Visible = false;
                    Label label = new Label();
                    label.Text = "You already sent a friend Request to " + NameProfileText.Text;
                    div1.Controls.Add(label);
                    

                }
                //On Your profile
                else
                {
                    // Show friends
                    divFriends.Visible = true;
                    FriendRequestsButton.Visible = true;
                    Albums.Visible = true;

                    string queryFriendsAccepted = "SELECT SenderId FROM Friends WHERE ReceiverId = @Id AND Status ='Accepted' " +
                    " UNION SELECT ReceiverId FROM Friends WHERE SenderId = @Id AND Status ='Accepted' ";
                    SqlCommand comFriends = new SqlCommand(queryFriendsAccepted, con);
                    comFriends.Parameters.AddWithValue("Id", id);


                    SqlDataReader reader = comFriends.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string idFriend = reader.GetString(0);
                            Image img = new Image();

                            img.Height = 50;
                            img.Width = 50;
                            img.ImageUrl = idToPhotoUrl(idFriend);
                            divFriends.Controls.Add(img);

                            divFriends.Controls.Add(new LiteralControl("   "));


                            HyperLink myHyperLink = new HyperLink();
                            myHyperLink.Text = idToUserName(idFriend);
                            myHyperLink.NavigateUrl = "~/Account/Message.aspx?id="+idFriend;

                            divFriends.Controls.Add(myHyperLink);

                            divFriends.Controls.Add(new LiteralControl("<br />"));
                            divFriends.Controls.Add(new LiteralControl("<br />"));





                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }
                con.Close();

                

                
                



            }


        }
        
        protected void FriendRequestClick(object sender, EventArgs e)
        {

            string ReceiverId = Request.QueryString["id"];
            string querySenderId = "SELECT Id from AspNetUsers where (UserName  like '%' + @UserName + '%')";
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
            con.Open();
            SqlCommand com1 = new SqlCommand(querySenderId, con);
            com1.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            string SenderId = (string)com1.ExecuteScalar();

            string queryFriendRequest = " IF NOT EXISTS (SELECT * FROM FRIENDS WHERE " +
                " (SenderId = @SenderId AND ReceiverId = @ReceiverId) OR " +
                " (SenderId = @ReceiverId AND ReceiverId = @SenderId) ) " +
                " Insert into Friends(SenderId,ReceiverId,Date,Status)  OUTPUT INSERTED.ID " +
                " VALUES ( @SenderId , @ReceiverId , @Date, @Status ) " +
                " ELSE SELECT NULL  ";

            SqlCommand comFriendRequest = new SqlCommand(queryFriendRequest, con);

            comFriendRequest.Parameters.AddWithValue("SenderId", SenderId);
            comFriendRequest.Parameters.AddWithValue("ReceiverId", ReceiverId);
            comFriendRequest.Parameters.AddWithValue("Date", DateTime.Now);
            comFriendRequest.Parameters.AddWithValue("Status", "Requested");



            if (comFriendRequest.ExecuteScalar().Equals(DBNull.Value))
            {
                FriendRequestLabel.Text = "Friend Request Failure.You already sent or received!";
                FriendRequestLabel.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                FriendRequestLabel.Text = "Friend Request Send";
                FriendRequestLabel.ForeColor = System.Drawing.Color.Green;
                

            }

            con.Close();


        }

        protected void FriendRequestsSearch(object sender, EventArgs e)
        {

            Response.Redirect("~/Account/FriendRequests.aspx?filter=" + Request.QueryString["id"]);

        }

        protected void goToAlbums(object sender , EventArgs e)
        {
            Response.Redirect("~/Account/Albums.aspx?id=" + Request.QueryString["id"]);
        }

    }
}