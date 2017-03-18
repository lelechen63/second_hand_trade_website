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
    public partial class CreateAccount : System.Web.UI.Page
    {

        String first_Name, last_Name, user_Name, phone_number, pass_word, email_address;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            first_Name = firstname.Text;
            last_Name = lastname.Text;
            user_Name = username.Text;
            phone_number = phone.Text;
            pass_word = password.Text;
            email_address = email.Text;
            
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveInformation(first_Name, last_Name, user_Name, phone_number, pass_word, email_address);

        }
        [System.Web.Services.WebMethod]
        public static string  SaveInformation(string fn, string ln, string un, string pn, string pw, string ea)
        {
            string msg;
            DbConnection connect = new DbConnection(); 
            SqlConnection con = connect.getConnection();
            con.Open();
            String queryStatement = " SELECT Username FROM UserInformation WHERE Username= " + un;
            SqlCommand check_emailaddress = new SqlCommand("SELECT COUNT(*) FROM Users WHERE ([Username] = @un)", con);
            check_emailaddress.Parameters.AddWithValue("@un", un);
            int UserExist = (int)check_emailaddress.ExecuteScalar();
            
            if (UserExist > 0)
            {
               
                msg = "The username is used by anthother user. Please enter a new one";
            }
            else
            {
                
                msg = "Registration Completed!";
                try
                {
                    using (SqlConnection openCon = connect.getConnection())
                    {
                        string createAccount = "INSERT into Users (emailAddress,FirstName,lastname,password,Telephone,Username) VALUES (@ea,@fn,@ln,@pw,@pn,@un)";

                        using (SqlCommand queryCreateAccount = new SqlCommand(createAccount))
                        {

                            queryCreateAccount.Connection = openCon;

                            queryCreateAccount.Parameters.Add("@ea", SqlDbType.VarChar, 50).Value = ea;
                            queryCreateAccount.Parameters.Add("@fn", SqlDbType.VarChar, 50).Value = fn;
                            queryCreateAccount.Parameters.Add("@ln", SqlDbType.VarChar, 50).Value = ln;
                            queryCreateAccount.Parameters.Add("@pw", SqlDbType.VarChar, 50).Value = pw;
                            queryCreateAccount.Parameters.Add("@pn", SqlDbType.VarChar, 50).Value = pn;
                            queryCreateAccount.Parameters.Add("@un", SqlDbType.VarChar, 50).Value = un;
                            int recordsAffected = queryCreateAccount.ExecuteNonQuery();

                        }
                    }
                  }
                catch (Exception ex)
                {

                }
                String str1 = "ss";
                //Username doesn't exist.
            }
            return msg;
        }
        
    }
}