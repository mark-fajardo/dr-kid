using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class DB : MonoBehaviour
{
    private string DBName = "URI=file:dr_kid.db";

    private int[,] AllLevels = {
        {1, 1, 0},
        {2, 0, 0},
        {3, 0, 0},
        {4, 0, 0},
        {5, 0, 0},

        {6, 0, 0},
        {7, 0, 0},
        {8, 0, 0},
        {9, 0, 0},
        {10, 0, 0},

        {11, 0, 0},
        {12, 0, 0},
        {13, 0, 0},
        {14, 0, 0},
        {15, 0, 0},

        {16, 0, 0},
        {17, 0, 0},
        {18, 0, 0},
        {19, 0, 0},
        {20, 0, 0}
    };

    // Start is called before the first frame update
    void Start()
    {
        SetupDB();
    }

    public void SetupDB()
    {
        CreateDB();
        SetupLevels();
    }

    public List<string> CheckLevel(string LvlNo)
    {
        List<string> ReturnLvlDetails = new List<string>{ };
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM levels WHERE level_no IN {LvlNo} AND NOT (initial = 1 OR is_done = 1);";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        ReturnLvlDetails.Add(reader["level_no"].ToString());
                        //Array.Resize(ref ReturnLvlDetails, ReturnLvlDetails.Length + 1);
                        //Debug.Log();
                        //ReturnLvlDetails[ReturnLvlDetails.GetUpperBound(0)] = reader["level_no"].ToString();

                    reader.Close();
                }
            }

            connection.Close();
        }

        return ReturnLvlDetails;
    }

    private void CreateDB()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS levels (id INT PRIMARY KEY, level_no INT, initial INT, is_done INT);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private void SetupLevels()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = GetLevels();
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
    
    private string GetLevels()
    {
        int TotalLevels = (AllLevels.Length / 3);
        string InsertLevelsQuery = "";
        for (int i = 0; i < TotalLevels; i++)
        {
            InsertLevelsQuery += $"INSERT OR IGNORE INTO levels(id, level_no, initial, is_done) VALUES({AllLevels[i, 0]}, {AllLevels[i, 0]}, {AllLevels[i, 1]}, {AllLevels[i, 2]});";
        }

        return InsertLevelsQuery;
    }
}
