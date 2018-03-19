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
    public partial class Albums : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");

        protected void Page_Load(object sender, EventArgs e)
        {
            addingNewAlbum.Visible = false;
            NoAlbumsLabel.Visible = false;
            
            string id = Request.QueryString["id"];
            


            string queryIdLogat = "SELECT Id from AspNetUsers where (UserName  like   @UserName )";
            con.Open();
            SqlCommand com1 = new SqlCommand(queryIdLogat, con);
            com1.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            string idLogat = (string)com1.ExecuteScalar();
            con.Close();

            if(!idLogat.Equals(id))
            {
                addAlbum.Visible = false;
            }

            con.Open();


            string queryAfisareAlbume = "Select Id,Name from Albums where idUser like @id";

            SqlCommand comAlbums = new SqlCommand(queryAfisareAlbume, con);
            comAlbums.Parameters.AddWithValue("id", id);

            SqlDataReader reader = comAlbums.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HyperLink myHyperLink = new HyperLink();
                    myHyperLink.Text = reader.GetString(1);
                    myHyperLink.NavigateUrl = "~/Account/AlbumPhotos.aspx?id=" + reader.GetInt32(0)+"&idUser=" +id;
                    AlbumsDiv.Controls.Add(myHyperLink);
                   
                    AlbumsDiv.Controls.Add(new LiteralControl("<br />"));
                    AlbumsDiv.Controls.Add(new LiteralControl("<br />"));



                }
            }
            else
            {
                NoAlbumsLabel.Visible = true;
            }
            reader.Close();
            con.Close();
        }


        protected void addNewAlbum (object sender,EventArgs e)
        {
            addAlbum.Visible = false;
            addingNewAlbum.Visible = true;

        }
        protected void addAlbumInDB(object sender,EventArgs e)
        {
            string name = albumName.Text;
            string id = Request.QueryString["id"];
            con.Open();
            string queryInsertAlbum= " Insert into Albums(idUser,Name)  OUTPUT INSERTED.ID " +
                    " VALUES ( @idUser,@Name ) ";
            SqlCommand com = new SqlCommand(queryInsertAlbum, con);
            com.Parameters.AddWithValue("idUser", id);
            com.Parameters.AddWithValue("Name", name);

            com.ExecuteScalar();

            con.Close();
            Label lbl = new Label();
            lbl.Text = "Album " + name + " added!";
            lbl.ForeColor = System.Drawing.Color.Green;
            addingNewAlbum.Controls.Add(lbl);

            System.Threading.Thread.Sleep(5000);

            Response.Redirect("~/Account/Albums.aspx?id=" + id);





        }
    }
}