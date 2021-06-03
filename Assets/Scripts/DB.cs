using UnityEngine;
using System.IO;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;

public class DB : MonoBehaviour
{
    private string DBName = "URI=file:";

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

        {16, 1, 0},
        {17, 0, 0},
        {18, 0, 0},
        {19, 0, 0},
        {20, 0, 0}
    };

    private int[,] AllLevelsScore = {
        {1, 0},
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},

        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},

        {11, 0},
        {12, 0},
        {13, 0},
        {14, 0},
        {15, 0},

        {16, 0},
        {17, 0},
        {18, 0},
        {19, 0},
        {20, 0}
    };

    // Start is called before the first frame update
    void Start()
    {
        DBName += SetupDBName();
        SetupDB();
    }

    public void SetupDB()
    {
        CreateDB();
        SetupLevels();
        SetupConfig();
        SetupConfigForGenderSelection();
        SetupLevelsScore();
    }

    public void UpdateLevelDone(int LvlNo, int NextLvl = 0)
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE levels SET is_done = 1 WHERE level_no = {LvlNo};";
                if (NextLvl != 0)
                {
                    command.CommandText += $"UPDATE levels SET initial = 1 WHERE level_no = {NextLvl};";
                }
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateLevelSCore(int LvlNo, int TimesSelected)
    {
        int LevelScore = GetScore(TimesSelected);

        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE scores SET score = {LevelScore} WHERE id = {LvlNo};";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateConfigDone(string ToUpdate)
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"UPDATE config SET {ToUpdate} WHERE id = 1;";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    public void UpdateGenderConfigSetting()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE config SET gender = 1 WHERE id = 2;";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
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

                    reader.Close();
                }
            }

            connection.Close();
        }

        return ReturnLvlDetails;
    }

    public List<int[]> CheckScore(string LvlNo)
    {
        List<int[]> ReturnLvlDetails = new List<int[]> { };
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM scores WHERE id IN {LvlNo} AND score != 0;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int[] TempScore = { Int32.Parse(reader["id"].ToString()), Int32.Parse(reader["score"].ToString()) };
                        ReturnLvlDetails.Add(TempScore);
                    }

                    reader.Close();
                }
            }

            connection.Close();
        }

        return ReturnLvlDetails;
    }

    public List<string> CheckLanguage()
    {
        List<string> ReturnConfig = new List<string> { };
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT language FROM config WHERE id = 1;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        ReturnConfig.Add("lang_" + reader["language"].ToString());

                    reader.Close();
                }
            }

            connection.Close();
        }

        return ReturnConfig;
    }

    public List<string> CheckGender()
    {
        List<string> ReturnGender = new List<string> { };
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT gender FROM config WHERE id = 1;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        ReturnGender.Add("gen_" + reader["gender"].ToString());

                    reader.Close();
                }
            }

            connection.Close();
        }

        return ReturnGender;
    }

    public List<string> CheckGenderSetting()
    {
        List<string> ReturnGender = new List<string> { };
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT gender FROM config WHERE id = 2;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        ReturnGender.Add("gen_setting_" + reader["gender"].ToString());

                    reader.Close();
                }
            }

            connection.Close();
        }

        return ReturnGender;
    }

    private void CreateDB()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS levels (id INT PRIMARY KEY, level_no INT, initial INT, is_done INT);";
                // language: 0 = English, 1 = Filipino; gender: 0 = female, 1 = male
                command.CommandText += "CREATE TABLE IF NOT EXISTS config (id INT PRIMARY KEY, language INT, gender INT);";
                command.CommandText += "CREATE TABLE IF NOT EXISTS scores (id INT PRIMARY KEY, score INT);";
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

    private void SetupLevelsScore()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = GetLevelsScore();
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private void SetupConfig()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT OR IGNORE INTO config(id, language, gender) VALUES (1, 0, 0);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private void SetupConfigForGenderSelection()
    {
        using (var connection = new SqliteConnection(DBName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //gender = 0; if gender is not yet selected
                //gender = 1; if gender was selected
                command.CommandText = "INSERT OR IGNORE INTO config(id, language, gender) VALUES (2, 0, 0);";
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    private void SetupDialogLanguage()
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

    private string GetLevelsScore()
    {
        int TotalLevelsScore = (AllLevelsScore.Length / 2);
        string InsertLevelsQuery = "";
        for (int i = 0; i < TotalLevelsScore; i++)
        {
            InsertLevelsQuery += $"INSERT OR IGNORE INTO scores(id, score) VALUES({AllLevelsScore[i, 0]}, {AllLevelsScore[i, 1]});";
        }

        return InsertLevelsQuery;
    }

    private string SetupDBName()
    {
        string ReturnDBName = "";
        //if (Application.platform != RuntimePlatform.Android)
        //{
        //    ReturnDBName = Application.dataPath + "/dr_kid.db";
        //}
        //else
        //{
        ReturnDBName = Application.persistentDataPath + "/dr_kid.db";
        if (!File.Exists(ReturnDBName))
        {
            WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "dr_kid.s3db");
            while (!load.isDone) { }
            File.WriteAllBytes(ReturnDBName, load.bytes);
        }
        //}

        return ReturnDBName;
    }

    private int GetScore(int TimesSelected)
    {
        if (TimesSelected == 0)
        {
            return 3;
        }
        else if (TimesSelected >= 1 && TimesSelected <= 2)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
