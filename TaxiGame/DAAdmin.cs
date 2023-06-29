using MySql.Data.MySqlClient;
using System.Data;

namespace TaxiGame
{
    public class DAAdmin
    {
        public string Admin_Edit_User(string pUsername, string pPassword, string pEmail, bool pIsLocked, bool pIsAdmin)
        {
            List<MySqlParameter> adminEditUserParams = new List<MySqlParameter>();

            MySqlParameter aUsername = new MySqlParameter("@pUsername", MySqlDbType.VarChar, 20);
            aUsername.Value = pUsername;
            adminEditUserParams.Add(aUsername);

            MySqlParameter aPassword = new MySqlParameter("@pPassword", MySqlDbType.VarChar, 30);
            aPassword.Value = pPassword;
            adminEditUserParams.Add(aPassword);

            MySqlParameter aEmail = new MySqlParameter("@pEmail", MySqlDbType.VarChar, 50);
            aEmail.Value = pEmail;
            adminEditUserParams.Add(aEmail);

            MySqlParameter aIsLocked = new MySqlParameter("@pIsLocked", MySqlDbType.Bit);
            aIsLocked.Value = pIsLocked;
            adminEditUserParams.Add(aIsLocked);

            MySqlParameter aIsAdmin = new MySqlParameter("@pIsAdmin", MySqlDbType.Bit);
            aIsAdmin.Value = pIsAdmin;
            adminEditUserParams.Add(aIsAdmin);

            var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Admin_Edit_User(@pUsername, @pPassword, @pEmail, @pIsLocked, @pIsAdmin)", adminEditUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }

        public string Admin_New_User(string pUsername, string pPassword, string pEmail)
        {
            using (var connection = new MySqlConnection(DataAccess.ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Admin_New_User";

                cmd.Parameters.AddWithValue("@pUsername", pUsername);
                cmd.Parameters.AddWithValue("@pPassword", pPassword);
                cmd.Parameters.AddWithValue("@pEmail", pEmail);

                var pMessage = cmd.Parameters.Add("@pMessage", MySqlDbType.VarChar, 255);
                pMessage.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                return pMessage.Value.ToString();
            }
        }

        public string Admin_Delete_User(int userID)
        {
            List<MySqlParameter> adminDeleteUserParams = new List<MySqlParameter>();

            MySqlParameter aUserID = new MySqlParameter("@pUserID", MySqlDbType.Int32);
            aUserID.Value = userID;
            adminDeleteUserParams.Add(aUserID);

            var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Admin_Delete_User(@pUserID)", adminDeleteUserParams.ToArray());

            return aDataSet.Tables[0].Rows[0].Field<string>("Message");
        }


        public PlayerInDB GetUserData(int userID)
        {
            List<MySqlParameter> getUserParams = new List<MySqlParameter>();

            MySqlParameter aUserID = new MySqlParameter("@UserID", MySqlDbType.Int32);
            aUserID.Value = userID;
            getUserParams.Add(aUserID);

            var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Get_User_Data(@UserID)", getUserParams.ToArray());

            if (aDataSet.Tables[0].Rows.Count > 0)
            {
                PlayerInDB user = new PlayerInDB();
                user.Username = aDataSet.Tables[0].Rows[0].Field<string>("Username");
                user.Password = aDataSet.Tables[0].Rows[0].Field<string>("Password");
                user.Email = aDataSet.Tables[0].Rows[0].Field<string>("Email");

                return user;
            }

            return null;
        }
        
    }
}
