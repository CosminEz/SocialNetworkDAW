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
    public partial class Message : System.Web.UI.Page
    {
        SqlConnection con;
        string idLogat;
        string idFriend;

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


        protected void sendMessage(object sender, EventArgs e)
        {
            string message = TextChat.Text;
            if (!message.Equals(""))
            {
                con.Open();
                string queryInsertMessage = " Insert into Messages(SenderId, ReceiverId, Date, TextContained)  OUTPUT INSERTED.ID " +
                    " VALUES ( @SenderId , @ReceiverId , @Date, @TextContained ) ";

                SqlCommand comSendMessage = new SqlCommand(queryInsertMessage, con);

                comSendMessage.Parameters.AddWithValue("SenderId", idLogat);
                comSendMessage.Parameters.AddWithValue("ReceiverId", idFriend);
                comSendMessage.Parameters.AddWithValue("Date", DateTime.Now);
                comSendMessage.Parameters.AddWithValue("TextContained", message);

                comSendMessage.ExecuteScalar();
                con.Close();
                Response.Redirect("~/Account/Message.aspx?id=" + idFriend);

            }
            else
            {

                //TODO : You can't insert empty string}

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
            string queryIdLogat = "SELECT Id from AspNetUsers where (UserName  like   @UserName  )";
            con.Open();
            SqlCommand comIdLogat = new SqlCommand(queryIdLogat, con);
            comIdLogat.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            idLogat = (string)comIdLogat.ExecuteScalar();
            con.Close();

            idFriend = Request.QueryString["id"];

            string queryBringMessages = "SELECT TextContained,Date,SenderId,ReceiverId FROM (SELECT TextContained,Date,SenderId,ReceiverId FROM Messages WHERE ReceiverId = @idLogat AND SenderId=@idFriend  " +
                    " UNION SELECT TextContained,Date,SenderId,ReceiverId FROM Messages WHERE SenderId = @IdLogat AND ReceiverId=@idFriend ) AS TBL  ORDER BY Date ";

            SqlCommand comMessages = new SqlCommand(queryBringMessages, con);
            comMessages.Parameters.AddWithValue("idLogat", idLogat);
            comMessages.Parameters.AddWithValue("idFriend", idFriend);

            con.Open();

            SqlDataReader reader = comMessages.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string messageSender = reader.GetString(2);
                    string messageText = reader.GetString(0);
                    if(messageSender.Equals(idFriend))
                    {
                        Image img = new Image();

                        img.Height = 30;
                        img.Width = 30;
                        img.ImageUrl = idToPhotoUrl(idFriend);
                        Chat.Controls.Add(img);
                        Chat.Controls.Add(new LiteralControl("  "));

                        Label msg = new Label();
                        msg.Text = messageText;
                        msg.ForeColor = System.Drawing.Color.Black;
                        msg.BackColor = System.Drawing.Color.White;
                        Chat.Controls.Add(msg);
                        Chat.Controls.Add(new LiteralControl("<br />"));
                        Chat.Controls.Add(new LiteralControl("<br />"));

                    }
                    else
                    {
                        Label msg = new Label();
                        msg.Text = messageText;
                        msg.ForeColor = System.Drawing.Color.White;
                        msg.BackColor = System.Drawing.Color.Blue;
                        msg.Attributes.Add("Style", "float: right");
                        Chat.Controls.Add(msg);
                        Chat.Controls.Add(new LiteralControl("<br />"));
                        Chat.Controls.Add(new LiteralControl("<br />"));

                    }




                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            con.Close();
        }
    }
}