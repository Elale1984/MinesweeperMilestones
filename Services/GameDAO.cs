using CST_350_Minesweeper.Controllers;
using CST_350_Minesweeper.Models;
using Milestone.Models;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace CST_350_Minesweeper.Services
{
    public class GameDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CST-350-Minesweeper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        BoardModel boardModel;
        
        
        public bool SaveGame(BoardModel boardModel)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(
                    "INSERT INTO [Games] (UserID, SaveTime, GameData) VALUES (@UserId, @SaveTime, @GameData)",
                connection);
                command.Parameters.AddWithValue("@UserId", boardModel.UID);
                command.Parameters.AddWithValue("@SaveTime", boardModel.date);
                command.Parameters.AddWithValue("@GameData", boardModel.boardCells);

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Game inserted successfully
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }

        public BoardModel GetSavedGameFromDatabase(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM [Games] WHERE  Id = @id",
                connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.HasRows) 
                    {
                        while (reader.Read())
                        {
                            boardModel = new BoardModel()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UID = reader.GetInt32(reader.GetOrdinal("userid")),
                                boardCells = reader.GetString(reader.GetOrdinal("gamedata")),
                                date = reader.GetString(reader.GetOrdinal("savetime")),

                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return boardModel;
        }

        public IEnumerable<BoardModel> GetAllSavedGamesFromDatabase()
        {
            List<BoardModel> savedGames = new List<BoardModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(
                    "SELECT * FROM [Games] ORDER BY [id] DESC",
                        connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            boardModel = new BoardModel()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UID = reader.GetInt32(reader.GetOrdinal("userid")),
                                boardCells = reader.GetString(reader.GetOrdinal("gamedata")),
                                date = reader.GetString(reader.GetOrdinal("savetime")),

                            };
                            savedGames.Add(boardModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return savedGames;
        }

        public bool DeleteOneGameFromDatabase(int id)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(
                    "DELETE FROM [Games] WHERE Id = @id",
                    connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return success;
        }

    }
}