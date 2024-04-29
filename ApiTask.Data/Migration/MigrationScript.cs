using System;
using System.Data.SQLite;
using System.IO;

namespace ApiTask.Data.Migration
{
    public class MigrationScript
    {
        private string dbName = "daynor.tito.sqlite.db";
        private string connectionString;

        public MigrationScript()
        {
            connectionString = $"Data Source=../ApiTask.Data/Migration/{dbName};Version=3;";
        }
        
        public void Migrate()
        {
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            string sqlFilePath = "../resources/backupDB/Dump20240428.sql";
            string sql = File.ReadAllText(sqlFilePath);
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string[] statements = sql.Split(';');
                foreach (string statement in statements)
                {
                    if (!string.IsNullOrWhiteSpace(statement))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(statement, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                Console.WriteLine("Migration successfully completed.");
            }
        }
    }
}