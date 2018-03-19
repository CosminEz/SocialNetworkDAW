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
    public partial class AlbumPhotos : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");

        protected void addPhoto(object sender, EventArgs e)
        {
            byte[] bytes;
            HttpPostedFile postedFile = UploadPhotoToAlbum.PostedFile;
            string photoName = Path.GetFileName(postedFile.FileName);
            string photoExtension = Path.GetExtension(photoName);
            int photoSize = postedFile.ContentLength;

            if (photoExtension.ToLower() == ".jpg" || photoExtension.ToLower() == ".bmp" ||
                photoExtension.ToLower() == ".gif" || photoExtension.ToLower() == ".png"
                || photoExtension.ToLower() == ".jpeg")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                bytes = binaryReader.ReadBytes((int)stream.Length);

                con.Open();
                int idAlbum = Int32.Parse(Request.QueryString["id"]);
                string queryInsertPhoto = "Insert into Photos (IdAlbum,Photo) OUTPUT INSERTED.ID " +
                    " VALUES (@IdAlbum,@Photo) ";
                SqlCommand com = new SqlCommand(queryInsertPhoto, con);
                com.Parameters.AddWithValue("IdAlbum", idAlbum);
                com.Parameters.AddWithValue("Photo", bytes);
                com.ExecuteScalar();
                con.Close();
                Response.Redirect("~/Account/AlbumPhotos.aspx?id=" + idAlbum);

            }
            else
            {
                //TODO : formatul nu e bun . Fa ceva! 
            }



        }




        protected string idAlbumToIdUser(int id)
        {
            con.Open();
            int idAlbum = id;
            string query = "Select IdUser from Albums where Id=@id";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("id", idAlbum);
            string rtn= (string)com.ExecuteScalar();
            con.Close();
            return rtn;
            

        }

        protected string byteArrayToPhotoUrl(byte[] bytes)
        {
           
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            return  "data:image/jpeg;base64," + base64String;
        }

        protected void redirectToPhoto(object sender, EventArgs e,int id,string IdUser)
        {
            
            Response.Redirect("~/Account/Photo.aspx?id=" + id+"&idUser="+IdUser);
            
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            int idAlbum = Int32.Parse(Request.QueryString["id"]);
            string id = Request.QueryString["idUser"];
            string idUser = idAlbumToIdUser(idAlbum);
            NoPhotosLabel.Visible = false;

            string queryIdLogat = "SELECT Id from AspNetUsers where (UserName  like   @UserName )";
            con.Open();
            SqlCommand com1 = new SqlCommand(queryIdLogat, con);
            com1.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            string idLogat = (string)com1.ExecuteScalar();
            con.Close();

            if (!idLogat.Equals(id))
            {
                addNewPhoto.Visible = false;
            }

            con.Open();
            string queryPhotos = "Select Photo,Id from Photos where IdAlbum = @idAlbum";
            SqlCommand com = new SqlCommand(queryPhotos, con);
            com.Parameters.AddWithValue("idAlbum", idAlbum);

            int k = 1;

            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    byte[] bytes = (byte[])reader[0];
                    int photoId = (int)reader.GetInt32(1);
                    ImageButton img = new ImageButton();

                    img.Height = 200;
                    img.Width = 200;
                    img.ImageUrl = byteArrayToPhotoUrl(bytes);
                    img.Click += new ImageClickEventHandler((a, b) => redirectToPhoto(a, b, photoId,idUser));
                    AlbumPhotosDiv.Controls.Add(img);
                    

                    if (k%3==0)
                    {
                        AlbumPhotosDiv.Controls.Add(new LiteralControl("<br />"));
                        AlbumPhotosDiv.Controls.Add(new LiteralControl("<br />"));
                        AlbumPhotosDiv.Controls.Add(new LiteralControl("<br />"));
                        
                    }
                    else
                    {
                        AlbumPhotosDiv.Controls.Add(new LiteralControl("&nbsp"));
                        AlbumPhotosDiv.Controls.Add(new LiteralControl("&nbsp"));
                        AlbumPhotosDiv.Controls.Add(new LiteralControl("&nbsp"));


                       
                    }
                    k++;

                }
            }
            else
            {
                NoPhotosLabel.Visible = true;

            }
            reader.Close();
            con.Close();

             }

        
    }
}