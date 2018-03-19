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
    public partial class Group : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = 'C:\Users\Filote Cosmin\Documents\Visual Studio 2015\Projects\DAWprojectSocialNetwork\DAWprojectSocialNetwork\App_Data\aspnet-DAWprojectSocialNetwork-20180105023655.mdf'; Integrated Security = True");

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

        protected void joinGroupEvent(object sender, EventArgs e)
        {
            string idLogat;
            string queryIdLogat = "SELECT Id from AspNetUsers where (UserName  like   @UserName  )";
            con.Open();
            SqlCommand comIdLogat = new SqlCommand(queryIdLogat, con);
            comIdLogat.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            idLogat = (string)comIdLogat.ExecuteScalar();
            con.Close();

            con.Open();

            int idGroup = Int32.Parse(Request.QueryString["id"]);

            string QueryInsertIntoGroup = "Insert into PersonsInGroups (IdUser,IdGroup) Values " +
                " (@idUser,@idGroup) ";
            SqlCommand comInsert = new SqlCommand(QueryInsertIntoGroup, con);
            comInsert.Parameters.AddWithValue("idUser", idLogat);
            comInsert.Parameters.AddWithValue("idGroup", idGroup);
            comInsert.ExecuteScalar();
            con.Close();

            Response.Redirect("~/Account/Group.aspx?id=" + idGroup);
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

        protected void sendMessageOnGroup(object sender,EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            joinGroup.Visible = false;
            divMembers.Visible = false;
            ChatGroup.Visible = false;
            int idGroup = Int32.Parse(Request.QueryString["id"]);
            string idLogat;
            string queryIdLogat = "SELECT Id from AspNetUsers where (UserName  like   @UserName  )";
            con.Open();
            SqlCommand comIdLogat = new SqlCommand(queryIdLogat, con);
            comIdLogat.Parameters.AddWithValue("UserName", HttpContext.Current.User.Identity.Name);
            idLogat = (string)comIdLogat.ExecuteScalar();
            con.Close();

            string queryIsInGroup = "IF EXISTS (SELECT * FROM PersonsInGroups Where IdUser like @idLogat And IdGroup = @idGroup) " +
                " Select 1 ELSE Select NULL";
            SqlCommand comIsInGroup =new SqlCommand(queryIsInGroup, con);
            comIsInGroup.Parameters.AddWithValue("idLogat", idLogat);
            comIsInGroup.Parameters.AddWithValue("idGroup", idGroup);
            con.Open();
            //Nu e in Grup
            if (comIsInGroup.ExecuteScalar().Equals(DBNull.Value))
            {
                joinGroup.Visible = true;
                divMembers.Visible = false;
                con.Close();

            }
            else // e in grup
            {
                con.Close();
                divMembers.Visible = true;
                ChatGroup.Visible = true;

                //query membri grup
                string queryMembri = "Select IdUser from PersonsInGroups where IdGroup = @idGroup";
                SqlCommand comMembri = new SqlCommand(queryMembri, con);
                con.Open();
                comMembri.Parameters.AddWithValue("idGroup", idGroup);

                SqlDataReader reader = comMembri.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string idMember = reader.GetString(0);
                        Image img = new Image();

                        img.Height = 50;
                        img.Width = 50;
                        img.ImageUrl = idToPhotoUrl(idMember);
                        divMembers.Controls.Add(img);

                        divMembers.Controls.Add(new LiteralControl("   "));


                        HyperLink myHyperLink = new HyperLink();
                        myHyperLink.Text = idToUserName(idMember);
                        myHyperLink.NavigateUrl = "~/Account/Profile.aspx?id=" + idMember;

                        divMembers.Controls.Add(myHyperLink);

                        divMembers.Controls.Add(new LiteralControl("<br />"));
                        divMembers.Controls.Add(new LiteralControl("<br />"));





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
    }
