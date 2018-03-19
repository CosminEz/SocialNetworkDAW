using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DAWprojectSocialNetwork.Account
{
    public partial class Photo : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");
        protected void insertComment(object sender,EventArgs e)
        {
            int idPhoto = Int32.Parse(Request.QueryString["id"]);
            
            string queryIdLogat = "SELECT Id from AspNetUsers where (UserName  like   @UserName )";
            con.Open();
            SqlCommand com1 = new SqlCommand(queryIdLogat, con);
            com1.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            string idLogat = (string)com1.ExecuteScalar();
            con.Close();

            string comment = TextBoxComment.Text;

            string queryInsertComment = "Insert into Comments  (IdUser,IdPhoto,Date,TextContained) " +
                " Values (@IdUser,@IdPhoto,@Date,@TextContained) ";
            con.Open();
            SqlCommand comInsertComment = new SqlCommand(queryInsertComment, con);
            comInsertComment.Parameters.AddWithValue("IdUser", idLogat);
            comInsertComment.Parameters.AddWithValue("IdPhoto",idPhoto);
            comInsertComment.Parameters.AddWithValue("Date", DateTime.Now);
            comInsertComment.Parameters.AddWithValue("TextContained", comment);

            comInsertComment.ExecuteScalar();
            con.Close();

            Response.Redirect("~/Account/Photo.aspx?id=" + idPhoto + "&idUser=" + Request.QueryString["idUser"]);





        }

        protected string byteArrayToPhotoUrl(byte[] bytes)
        {

            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            return "data:image/jpeg;base64," + base64String;
        }

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
            int idPhoto = Int32.Parse(Request.QueryString["id"]);
            string idUser = Request.QueryString["idUser"];
            string queryPhoto = "Select Photo from Photos where Id = @idPhoto";
            LabelUserName.Text = idToUserName(idUser);
            con.Open();

            SqlCommand com = new SqlCommand(queryPhoto, con);
            com.Parameters.AddWithValue("idPhoto", idPhoto);
            byte[] bytes = (byte[])com.ExecuteScalar();
            con.Close();

            Image img = new Image();
            img.Height = 500;
            img.Width = 500;
            img.ImageUrl = byteArrayToPhotoUrl(bytes);
            photoDiv.Controls.Add(img);

            con.Open();

            string queryBringComments = " Select IdUser,TextContained From Comments where IdPhoto=@idPhoto Order By Date ";
            SqlCommand comBringComments = new SqlCommand(queryBringComments, con);
            comBringComments.Parameters.AddWithValue("idPhoto", idPhoto);

            SqlDataReader reader = comBringComments.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string textComment = reader.GetString(1);
                    string idUserComment = reader.GetString(0);

                    Image imagine = new Image();
                    imagine.Height = Unit.Pixel(30);
                    imagine.Width = Unit.Pixel(30);
                    imagine.ImageUrl = idToPhotoUrl(idUserComment);

                    Label name = new Label();
                    name.Text = idToUserName(idUserComment);
                    
                    comments.Controls.Add(imagine);
                    comments.Controls.Add(new LiteralControl("  "));
                    comments.Controls.Add(name);
                    comments.Controls.Add(new LiteralControl("<br />"));

                    Label commentText = new Label();
                    commentText.Text = textComment;
                    commentText.BackColor = System.Drawing.Color.LightGray;

                    comments.Controls.Add(commentText);

                    comments.Controls.Add(new LiteralControl("<br />"));
                    comments.Controls.Add(new LiteralControl("<br />"));





                }
            }
            else
            {

            }

            reader.Close();
            con.Close();
        }
    }
}