using MySql.Data.MySqlClient;
using System;
using static MilToys.Utils;

namespace MilToys {
    class EditDB {
        static string _connStr = DatabaseSettings.ConnectionString;

        public static int AddToy(Toy newToy) {
            int id = 0;

            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"INSERT INTO Toys (name, stock, age, price) VALUES ('{newToy.Name}','{newToy.Stock}', '{newToy.Age}', {newToy.Price})";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "SELECT LAST_INSERT_ID();";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    id = Convert.ToInt32(rdr[0]);
                }

                rdr.Close();
            }
            catch (Exception ex) {
                ShowError("Помилка підключення до сервера");
            }

            conn.Close();

            return id;
        }

        public static void DeleteToy(int id) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"DELETE FROM Toys WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                ShowError("Помилка підключення до сервера");
            }

            conn.Close();
        }

        public static void EditToy(int id, string stock, string age, int price) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"UPDATE Toys SET stock = '{stock}', age = '{age}', price = {price} WHERE id = {id}";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                ShowError("Помилка підключення до сервера");
            }

            conn.Close();
        }

        public static void DeleteAgeLimit(string age) {
            MySqlConnection conn = new MySqlConnection(_connStr);

            try {
                conn.Open();

                string sql = $"DELETE FROM Toys WHERE age = '{age}'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) {
                ShowError("Помилка підключення до сервера");
            }

            conn.Close();
        }
    }
}
