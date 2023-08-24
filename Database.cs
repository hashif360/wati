using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wati
{
    public class Database
    {
        public static async void InsertNumbers(int num1, int num2, int sum)
        {
            string cs = Environment.GetEnvironmentVariable("DBConnection");

            string query = $@"INSERT INTO `WatiPersistant` (`num1`, `num2`, `sum`) VALUES ({num1},{num2},{sum})";



            using (var conn = new MySqlConnection(cs))
            {
                await conn.OpenAsync();

                // Insert some data
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }
    }
}
