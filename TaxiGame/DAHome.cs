﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TaxiGame
{
    public class DAHome
    {
        public string Log_Out(string pUsername)
        {
            try
            {
                List<MySqlParameter> logOutParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                logOutParams.Add(aUsername);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Log_Out(@Username)", logOutParams.ToArray());

                return aDataSet.Tables[0].Rows[0].Field<string>("Message");
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string Delete_User(string pUsername)
        {
            try
            {
                List<MySqlParameter> deleteUserParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@Username", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                deleteUserParams.Add(aUsername);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "Call delete_User(@Username)", deleteUserParams.ToArray());

                return aDataSet.Tables[0].Rows[0].Field<string>("Message");
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public int Create_Game(string pUsername)
        {
            try
            {
                List<MySqlParameter> createGameParams = new List<MySqlParameter>();

                MySqlParameter aUsername = new MySqlParameter("@pUsername", MySqlDbType.VarChar, 20);
                aUsername.Value = pUsername;
                createGameParams.Add(aUsername);

                MySqlParameter aGameID = new MySqlParameter("@pGameID", MySqlDbType.Int32);
                aGameID.Direction = ParameterDirection.Output;
                createGameParams.Add(aGameID);

                DataAccess.MySqlConnection.Open();

                MySqlHelper.ExecuteNonQuery(DataAccess.MySqlConnection, "CALL Create_Game(@pUsername, @pGameID)", createGameParams.ToArray());

                return Convert.ToInt32(aGameID.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
            finally
            {
                if (DataAccess.MySqlConnection.State == ConnectionState.Open)
                {
                    DataAccess.MySqlConnection.Close();
                }
            }
        }

        public List<GameInDB> Game_List()
        {
            List<GameInDB> games = new List<GameInDB>();

            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    connection.Open();

                    var query = "SELECT g.gameID, u.username FROM tblGame g INNER JOIN tblUser u ON g.userID = u.userID";
                    var command = new MySqlCommand(query, connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int gameID = Convert.ToInt32(reader["gameID"]);
                        string username = reader["username"].ToString();

                        GameInDB game = new GameInDB(gameID, username);
                        games.Add(game);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return games;
        }


        public List<PlayerInDB> Active_User_List()
        {
            try
            {
                List<PlayerInDB> activePlayers = new List<PlayerInDB>();
                List<MySqlParameter> activePlayersParams = new List<MySqlParameter>();

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Active_User_List()", activePlayersParams.ToArray());

                foreach (DataRow row in aDataSet.Tables[0].Rows)
                {
                    PlayerInDB player = new PlayerInDB();
                    player.Username = row["username"].ToString();

                    activePlayers.Add(player);
                }

                return activePlayers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<PlayerInDB>(); // return empty list
            }
        }

        public string Join_Game(int gameID, int userID)
        {
            try
            {
                List<MySqlParameter> joinGameParams = new List<MySqlParameter>();

                MySqlParameter aGameID = new MySqlParameter("@pGameID", MySqlDbType.Int32);
                aGameID.Value = gameID;
                joinGameParams.Add(aGameID);

                MySqlParameter aUserID = new MySqlParameter("@pUserID", MySqlDbType.Int32);
                aUserID.Value = userID;
                joinGameParams.Add(aUserID);

                var aDataSet = MySqlHelper.ExecuteDataset(DataAccess.MySqlConnection, "CALL Join_Game(@pGameID, @pUserID)", joinGameParams.ToArray());

                return aDataSet.Tables[0].Rows[0].Field<string>("Message");
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public bool KillGame(int gameID)
        {
            try
            {
                using (var connection = new MySqlConnection(DataAccess.ConnectionString))
                {
                    string procedureName = "KillGame";

                    MySqlCommand command = new MySqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pGameID", gameID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}

