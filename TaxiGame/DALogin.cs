using MySql.Data.MySqlClient;
using System.Data;

namespace TaxiGame
{
    public class DALogin
    {
        public string Log_In(string pUsername, string pPassword, out bool isAdmin)
        {
            isAdmin = false;

            try
            {
                List<MySqlParameter> logInParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@pUsername", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                logInParams.Add(aUsername);

                MySqlParameter aPassword = new MySqlParameter("@pPassword", MySqlDbType.VarChar, 30);
                aPassword.Value = pPassword;
                logInParams.Add(aPassword);

                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    connection.Open();

                    var aDataSet = MySqlHelper.ExecuteDataset(connection, @"
        SELECT userID, password, numLoginAttempts, isLocked, lockedUntil, isAdmin
        FROM tblUser 
        WHERE username = @pUsername", logInParams.ToArray());

                    if (aDataSet.Tables[0].Rows.Count > 0)
                    {
                        string passwordFromDb = aDataSet.Tables[0].Rows[0].Field<string>("password");
                        int numLoginAttempts = aDataSet.Tables[0].Rows[0].Field<int>("numLoginAttempts");
                        ulong isLocked = aDataSet.Tables[0].Rows[0].Field<ulong>("isLocked");
                        DateTime? lockedUntil = aDataSet.Tables[0].Rows[0].Field<DateTime?>("lockedUntil");
                        isAdmin = aDataSet.Tables[0].Rows[0].Field<bool>("isAdmin");

                        if (isLocked == 1 && lockedUntil.HasValue && DateTime.Now > lockedUntil.Value)
                        {
                            UnlockAccount(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"));
                            isLocked = 0;
                        }

                        if (passwordFromDb == pPassword && isLocked == 0)
                        {
                            UpdateUserStatus(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"));
                            return "Login Successful";
                        }
                        else if (isLocked == 1)
                        {
                            return "Account Locked";
                        }
                        else
                        {
                            UpdateLoginAttempts(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"), numLoginAttempts + 1);

                            if (numLoginAttempts + 1 >= 5)
                            {
                                LockAccount(connection, aDataSet.Tables[0].Rows[0].Field<int>("userID"));
                                return "Account Locked";
                            }
                            else
                            {
                                return "Login Failed";
                            }
                        }
                    }
                    else
                    {
                        return "Login Failed";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "An error occurred during login.";
            }
        }

        private void UnlockAccount(MySqlConnection connection, int userID)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE tblUser SET isLocked = 0, numLoginAttempts = 0, lockedUntil = NULL WHERE userID = @UserID", connection);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void UpdateUserStatus(MySqlConnection connection, int userID)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "UPDATE tblUser SET isOnline = 1, numLoginAttempts = 0 WHERE userID = @UserID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




        private void UpdateLoginAttempts(MySqlConnection connection, int userID, int numLoginAttempts)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                MySqlCommand cmd = new MySqlCommand("UPDATE tblUser SET numLoginAttempts = @NumLoginAttempts WHERE userID = @UserID", connection);
                cmd.Parameters.AddWithValue("@NumLoginAttempts", numLoginAttempts);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private void LockAccount(MySqlConnection connection, int userID)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                DateTime lockedUntil = DateTime.Now.AddMinutes(5);

                MySqlCommand cmd = new MySqlCommand("UPDATE tblUser SET isLocked = 1, lockedUntil = @LockedUntil WHERE userID = @UserID", connection);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@LockedUntil", lockedUntil);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
