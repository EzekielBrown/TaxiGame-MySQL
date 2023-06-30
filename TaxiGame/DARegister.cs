using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace TaxiGame
{
    public class DARegister
    {
        public string New_User(string pUsername, string pPassword, string pEmail)
        {
            try
            {
                bool usernameExists = CheckUsernameExists(pUsername);
                if (usernameExists)
                {
                    return "User Exists";
                }

                bool emailExists = CheckEmailExists(pEmail);
                if (emailExists)
                {
                    return "Email Exists";
                }

                List<MySqlParameter> newUserParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 45);
                aUsername.Value = pUsername;
                newUserParams.Add(aUsername);

                MySqlParameter aPassword = new MySqlParameter("@Password", MySqlDbType.VarChar, 45);
                aPassword.Value = pPassword;
                newUserParams.Add(aPassword);

                MySqlParameter aEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 100);
                aEmail.Value = pEmail;
                newUserParams.Add(aEmail);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL New_User(@Username, @Password, @Email)", newUserParams.ToArray());

                return aDataSet.Tables[0].Rows[0].Field<string>("Message");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "An error occurred during registration.";
            }
        }


        private bool CheckUsernameExists(string pUsername)
        {
            try
            {
                List<MySqlParameter> checkUsernameParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 45);
                aUsername.Value = pUsername;
                checkUsernameParams.Add(aUsername);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "SELECT COUNT(*) FROM tblUser WHERE username = @Username", checkUsernameParams.ToArray());
                int count = Convert.ToInt32(aDataSet.Tables[0].Rows[0][0]);

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }


        private bool CheckEmailExists(string pEmail)
        {
            try
            {
                List<MySqlParameter> checkEmailParams = new List<MySqlParameter>();

                MySqlParameter aEmail = new MySqlParameter("@Email", MySqlDbType.VarChar, 100);
                aEmail.Value = pEmail;
                checkEmailParams.Add(aEmail);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "SELECT COUNT(*) FROM tblUser WHERE email = @Email", checkEmailParams.ToArray());
                int count = Convert.ToInt32(aDataSet.Tables[0].Rows[0][0]);

                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
