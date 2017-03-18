using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace simpleTest
{
    public partial class UserReqistration : System.Web.UI.Page 
    {
        DbConnection connect = new DbConnection();
        string url = "empty url";
        protected void Page_Load(object sender, EventArgs e)
        {
            String stremailAddress = txtemailaddress.Text;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SendMail(txtemailaddress.Text);
        }
        [System.Web.Services.WebMethod]
        public  static string  SendMail(string emailaddress)//There was a bug -- sending same emails.
        {
            UserReqistration obj = new UserReqistration();
            String strMessage;
            string u;
            if (obj.SaveRegistation(emailaddress))
            {
                strMessage = "The email address had already been used! Please enter a different one!";
                return strMessage;
            }
            else
            {
                strMessage = "We have sent you an url to your email address! Please check your email and go to the url to complete registration!";
                u = obj.url;
            }
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();

            // Add the message properties.
            myMessage.From = new MailAddress("simran@example.com");

            // Add multiple addresses to the To field.
            List<String> recipients = new List<String>
            {
                emailaddress

            };
            myMessage.AddTo(recipients);

            myMessage.Subject = "Testing the SendGrid Library";

            //Add the HTML and Text bodies
            myMessage.Html = "<p>Hello There! Please go to the following url to complete your registration!</p> <br/><p>"+u+"</p>";
            myMessage.Text = "Hello World plain text!";
            var credentials = new NetworkCredential("azure_81b0bdec81e9317eb9411acf7dd01b1b@azure.com", "hell123$");
            var transportWeb = new Web(credentials);
           
            transportWeb.DeliverAsync(myMessage);
           
            return strMessage;
        }
        public bool SaveRegistation(string emailaddress)
        {
            Boolean exist = false;
            
            SqlConnection con = connect.getConnection();
            con.Open();
            String queryStatement = " select Emailaddress from UserRequestforReg where Emailaddress=" + emailaddress;
            SqlCommand check_emailaddress = new SqlCommand("SELECT COUNT(*) FROM UserRequestforReg WHERE ([Emailaddress] = @emailaddress)", con);
            check_emailaddress.Parameters.AddWithValue("@emailaddress", emailaddress);
            int UserExist = (int)check_emailaddress.ExecuteScalar();

            if (UserExist > 0)
            {
                exist = true;
            }
            else
            {
                exist = false;
                try
                {
                    using (SqlConnection openCon = connect.getConnection())
                    {
                        string saveRegistration = "INSERT into UserRequestforReg (Emailaddress,Url) VALUES (@emailaddress,@url)";

                        using (SqlCommand querysaveRegistration = new SqlCommand(saveRegistration))
                        {
                            querysaveRegistration.Connection = openCon;
                            querysaveRegistration.Parameters.Add("@emailaddress", SqlDbType.VarChar, 50).Value = emailaddress;
                            url = "http://localhost:52215/CreateAccount.aspx" + "?" + emailaddress;
                            querysaveRegistration.Parameters.Add("@url", SqlDbType.VarChar, 500).Value = url;
                            int recordsAffected = querysaveRegistration.ExecuteNonQuery();

                        }
                    }
                }
                catch (Exception ex)
                {

                }
                String str1 = "ss";
            }
            return exist;
        }
        
    }
}