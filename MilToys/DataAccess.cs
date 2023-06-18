using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using static MilToys.Utils;

namespace MilToys {
    public class DataAccess {
        static string _connStr = DatabaseSettings.ConnectionString;

        public static List<string> LoadAgeLimits() {          
            MySqlConnection conn = new MySqlConnection(_connStr);

            List<string> res = new List<string>();

            try {
                conn.Open();

                string sql = "SELECT DISTINCT(age) FROM Toys";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res.Add(rdr[0].ToString());
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка завантаження даних 1");
            }

            conn.Close();

            return res;
        }

        public static List<Toy> LoadToysFromAgeLimit(string age) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            List<Toy> res = new List<Toy>();

            try {
                conn.Open();

                string sql = $"SELECT * FROM Toys WHERE age = '{age}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res.Add(new Toy(Convert.ToInt32(rdr[0]), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), Convert.ToInt32(rdr[4])));
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка завантаження даних 2");
            }

            conn.Close();

            return res;
        }

        public static Toy GetToyById(int id) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            Toy res = null;

            try {
                conn.Open();

                string sql = $"SELECT * FROM Toys WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    res = new Toy(Convert.ToInt32(rdr[0]), rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString(), Convert.ToInt32(rdr[4]));
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка завантаження даних 3");
            }

            conn.Close();

            return res;
        }
    }
}
