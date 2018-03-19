using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using DAWprojectSocialNetwork.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAWprojectSocialNetwork.Account
{
    public partial class Register : Page
    {
        private byte[] bytes;
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedFile = ProfilePhotoUpload.PostedFile;
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

            }
            else
            {
                //TODO : pusa imagine daca nu bagam de profil de la inceput
            }


            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            string selectedValue = PrivateProfileRadioList.SelectedValue;
            string profileType;
            if(selectedValue.Equals("1"))
            {
                profileType = "Private";
            }
            else
            {
                profileType = "Public";
            }
            var user = new ApplicationUser() { UserName = Username.Text, Email = Email.Text , BirthDate = DateTime.Parse(BirthDate.Text), ProfilePhoto = bytes,ProfileStatus=profileType};
            IdentityResult result = manager.Create(user, Password.Text);
            
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                
                
                Response.Redirect("~/Account/Profile.aspx?id=" + user.Id);
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}